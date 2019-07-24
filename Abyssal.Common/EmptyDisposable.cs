using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssal.Common
{
    /// <summary>
    ///     An implementation of <see cref="IDisposable"/> that does nothing.
    /// </summary>
    public sealed class EmptyDisposable : IDisposable
    {
        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}
