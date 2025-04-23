using LLama;
using LLama.Common;
using Services;

PCService pcService = new PCService();

(InferenceParams inferenceParams, InteractiveExecutor executor) = pcService.InitializeModel(
    pathToModel: PCService.PATH_TO_LLAMA,
    contextSize: 2048,
    gpuLayerCount: 20,
    maxTokens: 1000,
    antiPrompts: new List<string> { "exit" }
);

await pcService.CreatePC(inferenceParams, executor);