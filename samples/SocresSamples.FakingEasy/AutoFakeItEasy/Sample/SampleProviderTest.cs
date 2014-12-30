namespace SocresSamples.FakingEasy.AutoFakeItEasy.Sample
{
    using Socres.FakingEasy.AutoFakeItEasy;
    using SocresSamples.FakingEasy.AutoFakeItEasy.SampleData;
    using FakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class SampleProviderTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_Execute_Succeeds(
            ISampleService sampleService)
        {
            // Arrange
            A.CallTo(() => sampleService.CanExecute)
                .Returns(true);
            var sampleProvider = new SampleProvider(sampleService);

            // Act
            sampleProvider.ExecuteSample();

            // Assert
            A.CallTo(() => sampleService.Execute())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_ExecuteWithParam_Succeeds(
            ISampleService sampleService,
            string value,
            string returnValue)
        {
            // Arrange
            A.CallTo(() => sampleService.CanExecute)
                .Returns(true);
            A.CallTo(() => sampleService.ExecuteWithParam(value))
                .Returns(returnValue);
            var sampleProvider = new SampleProvider(sampleService);

            // Act
            var actual = sampleProvider.ExecuteSampleWithParam(value);

            // Assert
            A.CallTo(() => sampleService.ExecuteWithParam(A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true, "ReturnValueFirst")]
        [InlineAutoFakeItEasyData(false, "")]
        public void SampleProvider_ExecuteWithParams_CanExecute_Succeeds(
            bool canExecute,
            string returnValue,
            ISampleService sampleService,
            string value)
        {
            // Arrange
            A.CallTo(() => sampleService.CanExecute)
                .Returns(canExecute);
            A.CallTo(() => sampleService.ExecuteWithParam(value))
                .Returns(returnValue);
            var sampleProvider = new SampleProvider(sampleService);

            // Act
            var actual = sampleProvider.ExecuteSampleWithParam(value);

            // Assert
            A.CallTo(() => sampleService.ExecuteWithParam(A<string>.Ignored))
                .MustHaveHappened(
                    canExecute
                    ? Repeated.Exactly.Once
                    : Repeated.Never);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void SampleProvider_ExecuteWithDto_Succeeds(
            ISampleService sampleService,
            SampleDto dto,
            string returnValue)
        {
            // Arrange
            A.CallTo(() => sampleService.CanExecute)
                .Returns(true);
            A.CallTo(() => sampleService.ExecuteWithDto(dto))
                .Returns(returnValue);
            var sampleProvider = new SampleProvider(sampleService);

            // Act
            var actual = sampleProvider.ExecuteSampleWithDto(dto);

            // Assert
            A.CallTo(() => sampleService.ExecuteWithDto(A<SampleDto>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(returnValue, actual);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true, "ReturnValueFirst")]
        [InlineAutoFakeItEasyData(false, "")]
        public void SampleProvider_ExecuteWithDto_CanExecute_Succeeds(
            bool canExecute,
            string returnValue,
            ISampleService sampleService,
            SampleDto dto)
        {
            // Arrange
            A.CallTo(() => sampleService.CanExecute)
                .Returns(canExecute);
            A.CallTo(() => sampleService.ExecuteWithDto(dto))
                .Returns(returnValue);
            var sampleProvider = new SampleProvider(sampleService);

            // Act
            var actual = sampleProvider.ExecuteSampleWithDto(dto);

            // Assert
            A.CallTo(() => sampleService.ExecuteWithDto(A<SampleDto>.Ignored))
                .MustHaveHappened(
                    canExecute
                    ? Repeated.Exactly.Once
                    : Repeated.Never);
            Assert.Equal(returnValue, actual);
        }
    }
}
