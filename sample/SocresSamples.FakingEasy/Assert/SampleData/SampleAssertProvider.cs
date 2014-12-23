namespace SocresSamples.FakingEasy.Assert.SampleData
{
    using System;
    using System.Threading.Tasks;

    public class SampleAssertProvider
    {
        public async Task<bool> ExecuteWithSingleException()
        {
            throw new InvalidOperationException();

            // ReSharper disable CSharpWarnings::CS0162
            // ReSharper disable HeuristicUnreachableCode
            await Task.Delay(1000);
            return true;
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore CSharpWarnings::CS0162
        }

        public async Task<bool> ExecuteWithMultipleExceptions()
        {
            throw new AggregateException(
                new ArgumentException(),
                new InvalidOperationException());

            // ReSharper disable CSharpWarnings::CS0162
            // ReSharper disable HeuristicUnreachableCode
            await Task.Delay(1000);
            return true;
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore CSharpWarnings::CS0162
        }
    }
}
