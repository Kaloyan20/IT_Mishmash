namespace Services.Interfaces
{
    public interface IPCService
    {
        Task InitializeModel(string modelPath, uint contextSize, int gpuLayerCount, int maxTokens, List<string> antiPrompts);
    }
}
