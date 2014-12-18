namespace SocresSamples.FakingEasy.SampleData
{
    public interface ISampleService
    {
        bool CanExecute { get; set; }

        void Execute();

        string ExecuteWithParam(string value);
        
        string ExecuteWithDto(SampleDto value);
    }
}
