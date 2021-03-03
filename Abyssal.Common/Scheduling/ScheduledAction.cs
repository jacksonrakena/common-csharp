using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abyssal.Common
{
    /// <summary>
    ///     Represents an action that has been scheduled for execution by an <see cref="ActionScheduler"/>.
    /// </summary>
    public class ScheduledAction
    {
        /// <summary>
        ///     The <see cref="DateTimeOffset"/> at which this action will execute.
        /// </summary>
        public DateTimeOffset Time { get; internal set; }
        
        /// <summary>
        ///     The callback to be executed when this action is executed.
        /// </summary>
        public Func<Task> Action { get; internal set; }

        /// <summary>
        ///     The <see cref="Task"/> representing the delay until execution.
        /// </summary>
        public Task Delay { get; internal set; }

        /// <summary>
        ///     The <see cref="CancellationTokenSource"/> controlling the cancellation of this <see cref="ScheduledAction"/>.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; internal set; }
        
        /// <summary>
        ///     The <see cref="ActionScheduler"/> that scheduled this <see cref="ScheduledAction"/>.
        /// </summary>
        public ActionScheduler Scheduler { get; internal set; }

        internal ScheduledAction()
        {
        }
    }
}