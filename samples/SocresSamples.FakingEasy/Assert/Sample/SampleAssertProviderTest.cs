namespace SocresSamples.FakingEasy.Assert.Sample
{
    using System;
    using Socres.FakingEasy.Assert;
    using SocresSamples.FakingEasy.Assert.SampleData;
    using Xunit;

    public class SampleAssertProviderTest
    {
        [Fact]
        public void SampleAssertProvider_ExecuteWithSingleException_Succeeds()
        {
            var sampleAssertProvider = new SampleAssertProvider();

            AssertAsync.Throws<InvalidOperationException>(
                sampleAssertProvider.ExecuteWithSingleException
            );
        }

        [Fact]
        public void SampleAssertProvider_ExecuteWithMultipleExceptions_Succeeds()
        {
            var sampleAssertProvider = new SampleAssertProvider();

            AssertAsync.Throws<AggregateException>(
                sampleAssertProvider.ExecuteWithMultipleExceptions
            );
        }
    }
}
