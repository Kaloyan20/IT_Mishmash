using Services;

var service = new PCService();

await service.InitializeModel(
    modelPath: PCService.PATH_TO_LLAMA,
    contextSize: 1024,
    gpuLayerCount: 5,
    maxTokens: 256,
    antiPrompts: new List<string> { "User:", "User:\n" }
);