using Services.Interfaces;
using LLama.Common;
using LLama;


namespace Services
{
    public class PCService : IPCService
    {
        public PCService()
        {
            // Initialize the PC service here if needed
        }
        // Example method to create a PC instance
        // This is just a placeholder and should be replaced with actual implementation
        public void CreatePC()
        {
            string modelPath =  Path.Combine(Environment.SpecialFolder.LocalApplicationData.ToString(), "Models/Llama-3.2-1B-Instruct-F16.gguf");
            var parameters = new ModelParams(modelPath)
            {
                ContextSize = 8192, // The longest length of chat as memory.
                GpuLayerCount = 32 // How many layers to offload to GPU. Please adjust it according to your GPU memory.
            };
            using var model = LLamaWeights.LoadFromFile(parameters);
            using var context = model.CreateContext(parameters);
            var executor = new InteractiveExecutor(context);

            // Add chat histories as prompt to tell AI how to act.
            var chatHistory = new ChatHistory();
            chatHistory.AddMessage(AuthorRole.System, "Transcript of a dialog, where the User interacts with an Assistant named Bob. Bob is helpful, kind, honest, good at writing, and never fails to answer the User's requests immediately and with precision.");
            chatHistory.AddMessage(AuthorRole.User, "Hello, Bob.");
            chatHistory.AddMessage(AuthorRole.Assistant, "Hello. How may I help you today?");

            ChatSession session = new(executor, chatHistory);

            InferenceParams inferenceParams = new InferenceParams()
            {
                MaxTokens = 256, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
                AntiPrompts = new List<string> { "User:" } // Stop generation once antiprompts appear.
            };
        }
    }
}
