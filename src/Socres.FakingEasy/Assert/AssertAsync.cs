namespace Socres.FakingEasy.Assert
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit.Sdk;

    /// <summary>
    /// Asynchronous assertions
    /// </summary>
    public static class AssertAsync
    {
        /// <summary>
        /// Assert wheter a certain <see cref="Exception"/> gets thrown
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static TException Throws<TException>(Func<Task> func) where TException : Exception
        {
            var exceptionType = typeof(TException);
            try
            {
                func().Wait();
            }
            catch (Exception ex)
            {
                var actual = ex;
                if (actual is AggregateException)
                {
                    var aggregateException = actual as AggregateException;
                    actual = aggregateException.InnerExceptions.Count() == 1
                        ? aggregateException.InnerExceptions.Single()
                        : aggregateException;
                }

                if (!(exceptionType == actual.GetType()))
                {
                    throw new ThrowsException(exceptionType, actual);
                }
                return actual as TException;
            }

            throw new ThrowsException(exceptionType);
        }
    }
}
