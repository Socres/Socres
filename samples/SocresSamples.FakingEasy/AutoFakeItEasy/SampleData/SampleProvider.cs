namespace SocresSamples.FakingEasy.AutoFakeItEasy.SampleData
{
    public class SampleProvider
    {
        private readonly ISampleService _sampleService;

        public SampleProvider(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public void ExecuteSample()
        {
            if (_sampleService.CanExecute)
            {
                _sampleService.Execute();
            }
        }

        public string ExecuteSampleWithParam(string value)
        {
            if (_sampleService.CanExecute)
            {
                return _sampleService.ExecuteWithParam(value);
            }

            return string.Empty;
        }

        public string ExecuteSampleWithDto(SampleDto value)
        {
            if (_sampleService.CanExecute)
            {
                return _sampleService.ExecuteWithDto(value);
            }

            return string.Empty;
        }
    }
}
