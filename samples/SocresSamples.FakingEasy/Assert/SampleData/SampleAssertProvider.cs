namespace SocresSamples.FakingEasy.Assert.SampleData
{
    using System;
    using System.Threading.Tasks;

    public class SampleAssertProvider
    {
        public async Task<bool> ExecuteWithSingleException()
        {
            await Task.Delay(100);
            
            throw new InvalidOperationException();
        }

        public async Task<bool> ExecuteWithMultipleExceptions()
        {
            await Task.Delay(100);

            throw new AggregateException(
                new ArgumentException(),
                new InvalidOperationException());
        }
    }
}
