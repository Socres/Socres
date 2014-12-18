namespace SocresSamples.FakingEasy.SampleData
{
    public class SampleProvider
    {
        private readonly ISampleObject _sampleObject;

        public SampleProvider(ISampleObject sampleObject)
        {
            _sampleObject = sampleObject;
        }

        public void ExecuteSample()
        {
            if (_sampleObject.CanExecute)
            {
                _sampleObject.Execute();
            }
        }

        public string ExecuteSampleWithParam(string value)
        {
            if (_sampleObject.CanExecute)
            {
                return _sampleObject.ExecuteWithParam(value);
            }

            return string.Empty;
        }

        public string ExecuteSampleWithDto(SampleDto value)
        {
            if (_sampleObject.CanExecute)
            {
                return _sampleObject.ExecuteWithDto(value);
            }

            return string.Empty;
        }
    }
}
