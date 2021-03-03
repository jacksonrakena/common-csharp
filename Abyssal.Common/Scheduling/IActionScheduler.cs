using System;
using System.Threading.Tasks;

namespace Abyssal.Common
{
    /// <summary>
    ///     A utility service that can schedule callbacks for execution at a later date.
    /// </summary>
    public interface IActionScheduler
    {
        /// <summary>
        ///     Schedules a <see cref="Func{Task}"/> for execution.
        ///     This method will return <c>null</c> if the requested schedule time is in the past.
        /// </summary>
        /// <param name="time">
        ///     The <see cref="DateTimeOffset"/> at which this action will execute.
        /// </param>
        /// <param name="action">
        ///     The callback to be executed when this action is executed.
        /// </param>
        /// <returns>
        ///     If <see cref="time"/> is in the past, <c>null</c>.
        /// </returns>
        public ScheduledAction? Schedule(DateTimeOffset time, Func<Task> action);

        /// <summary>
        ///     Removes a <see cref="ScheduledAction"/> from the scheduler, cancelling its delay.
        /// </summary>
        /// <param name="action">
        ///     The <see cref="ScheduledAction"/> to unschedule.
        /// </param>
        /// <returns>
        ///     A <see cref="Boolean"/> representing the removal operation from the internal actions list.
        /// </returns>
        public bool Unschedule(ScheduledAction action);
    }
}