namespace SocresSamples.FakingEasy.AutoFakeItEasy.SampleData
{
    public interface ISampleService
    {
        bool CanExecute { get; set; }

        void Execute();

        string ExecuteWithParam(string value);
        
        string ExecuteWithDto(SampleDto value);
    }
}
