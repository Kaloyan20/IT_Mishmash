using LLama;
using LLama.Common;

namespace Services.Interfaces
{
    public interface IPCService
    {
        public (InferenceParams, InteractiveExecutor) InitializeModel(string pathToModel, uint contextSize, int gpuLayerCount, int maxTokens, List<string> antiPrompts);

        Task CreatePC(InferenceParams inferenceParams, InteractiveExecutor executor);
    }
}
