using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Abyssal.Common
{
    /// <summary>
    ///     The default implementation of <see cref="IActionScheduler"/>, using a memory-backed database.
    /// </summary>
    public class ActionScheduler : IActionScheduler
    {
        private readonly List<ScheduledAction> _actions = new List<ScheduledAction>();
        
        /// <inheritdoc cref="IActionScheduler.Schedule"/>
        public ScheduledAction? Schedule(DateTimeOffset time, Func<Task> action)
        {
            if ((time - DateTimeOffset.Now).TotalSeconds <= 0)
            {
                return null;
            }

            var task = new ScheduledAction
            {
                Time = time,
                Action = action,
                CancellationTokenSource = new CancellationTokenSource()
            };
            task.Delay = CreateExtendedDelay(time.ToUnixTimeMilliseconds() - DateTimeOffset.Now.ToUnixTimeMilliseconds()).ContinueWith(task => action(), task.CancellationTokenSource.Token).ContinueWith(t =>
            {
                _actions.Remove(task);
            }, task.CancellationTokenSource.Token);
            _actions.Add(task);
            return task;
        }

        /// <inheritdoc cref="IActionScheduler.Unschedule"/>
        public bool Unschedule(ScheduledAction action)
        {
            action.CancellationTokenSource.Cancel();
            return _actions.Remove(action);
        }
        
        /// <summary>
        ///     Creates a <see cref="Task"/> that will wait until <see cref="delayTimeMilliseconds"/> milliseconds has elapsed.
        ///     This method uses a <see cref="ulong"/> to store time, so fixes the <see cref="int"/>-based limitations of <see cref="M:Task.Delay"/>.
        /// </summary>
        /// <param name="delayTimeMilliseconds">
        ///     The number of milliseconds to wait.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the delay.
        /// </returns>
        public static async Task CreateExtendedDelay(long delayTimeMilliseconds)
        {
            while (delayTimeMilliseconds > 0)
            {
                var currentDelay = delayTimeMilliseconds > int.MaxValue ? int.MaxValue : (int) delayTimeMilliseconds;
                await Task.Delay(currentDelay);
                delayTimeMilliseconds -= currentDelay;
            }
        }
    }
}