namespace SocresSamples.FakingEasy.SampleData
{
    public interface ISampleObject
    {
        bool CanExecute { get; set; }

        void Execute();

        string ExecuteWithParam(string value);
    }
}
