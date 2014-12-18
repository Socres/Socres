namespace SocresSamples.FakingEasy.Tests
{
    using Socres.FakingEasy.AutoFakeItEasy;
    using SocresSamples.FakingEasy.SampleData;
    using FakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class SampleProviderTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_Execute_Succeeds(
            ISampleObject sampleObject)
        {
            A.CallTo(() => sampleObject.CanExecute)
                .Returns(true);

            var sampleProvider = new SampleProvider(sampleObject);
            sampleProvider.ExecuteSample();

            A.CallTo(() => sampleObject.Execute())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_ExecuteWithParam_Succeeds(
            ISampleObject sampleObject,
            string value,
            string returnValue)
        {
            A.CallTo(() => sampleObject.CanExecute)
                .Returns(true);
            A.CallTo(() => sampleObject.ExecuteWithParam(value))
                .Returns(returnValue);

            var sampleProvider = new SampleProvider(sampleObject);
            var actual = sampleProvider.ExecuteSampleWithParam(value);

            A.CallTo(() => sampleObject.ExecuteWithParam(A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true, "ReturnValueFirst")]
        [InlineAutoFakeItEasyData(false, "")]
        public void SampleProvider_ExecuteWithParams_CanExecute_Succeeds(
            bool canExecute,
            string returnValue,
            ISampleObject sampleObject,
            string value)
        {
            A.CallTo(() => sampleObject.CanExecute)
                .Returns(canExecute);
            A.CallTo(() => sampleObject.ExecuteWithParam(value))
                .Returns(returnValue);

            var sampleProvider = new SampleProvider(sampleObject);
            var actual = sampleProvider.ExecuteSampleWithParam(value);

            A.CallTo(() => sampleObject.ExecuteWithParam(A<string>.Ignored))
                .MustHaveHappened(
                    canExecute
                    ? Repeated.Exactly.Once
                    : Repeated.Never);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_ExecuteWithDto_Succeeds(
            ISampleObject sampleObject,
            SampleDto dto,
            string returnValue)
        {
            A.CallTo(() => sampleObject.CanExecute)
                .Returns(true);
            A.CallTo(() => sampleObject.ExecuteWithDto(dto))
                .Returns(returnValue);

            var sampleProvider = new SampleProvider(sampleObject);
            var actual = sampleProvider.ExecuteSampleWithDto(dto);

            A.CallTo(() => sampleObject.ExecuteWithDto(A<SampleDto>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true, "ReturnValueFirst")]
        [InlineAutoFakeItEasyData(false, "")]
        public void SampleProvider_ExecuteWithDto_CanExecute_Succeeds(
            bool canExecute,
            string returnValue,
            ISampleObject sampleObject,
            SampleDto dto)
        {
            A.CallTo(() => sampleObject.CanExecute)
                .Returns(canExecute);
            A.CallTo(() => sampleObject.ExecuteWithDto(dto))
                .Returns(returnValue);

            var sampleProvider = new SampleProvider(sampleObject);
            var actual = sampleProvider.ExecuteSampleWithDto(dto);

            A.CallTo(() => sampleObject.ExecuteWithDto(A<SampleDto>.Ignored))
                .MustHaveHappened(
                    canExecute
                    ? Repeated.Exactly.Once
                    : Repeated.Never);
            Assert.Equal(returnValue, actual);
        }
    }
}
