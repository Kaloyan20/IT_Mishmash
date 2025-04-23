using Services.Interfaces;
using LLama.Common;
using LLama;
using Data;
using System.Text.Json;


namespace Services
{
    public class PCService : IPCService
    {
        private readonly PcPartsPickerContext _context;
        public static readonly string PATH_TO_LLAMA = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Models/Llama-3.2-1B-Instruct-F16.gguf");

        public PCService()
        {
            _context = new PcPartsPickerContext();
        }

        public async Task CreatePC(InferenceParams inferenceParams, InteractiveExecutor executor)
        {
            var chatHistory = new ChatHistory();

            var cases = JsonSerializer.Serialize(_context.Cases.ToList());
            var ram = JsonSerializer.Serialize(_context.Rams.ToList());
            var cpus = JsonSerializer.Serialize(_context.Cpus.ToList());
            var gpus = JsonSerializer.Serialize(_context.Gpus.ToList());
            var ssds = JsonSerializer.Serialize(_context.Ssds.ToList());
            var psus = JsonSerializer.Serialize(_context.Psus.ToList());
            var motherboards = JsonSerializer.Serialize(_context.Motherboards.ToList());
            var coolers = JsonSerializer.Serialize(_context.Coolers.ToList());

            chatHistory.AddMessage(AuthorRole.Assistant, $"I am a PC Build Advisor AI. I assist users in selecting compatible PC components—cases, RAM, CPUs, GPUs, SSDs, PSUs, motherboards, and CPU coolers—based on their preferences: use case (e.g., gaming, video editing), budget, preferred brands, aesthetic choices (e.g., RGB lighting), and form factor constraints. Available components: Cases: {cases}; RAM: {ram}; CPUs: {cpus}; GPUs: {gpus}; SSDs: {ssds}; PSUs: {psus}; Motherboards: {motherboards}; CPU Coolers: {coolers}. I ensure component compatibility (e.g., CPU socket and motherboard), optimize performance within budget, consider future upgrade paths, manage thermal requirements, calculate appropriate PSU wattage, and align with aesthetic preferences. I present recommendations in a structured format: CPU: [Model] - [Justification]; GPU: [Model] - [Justification]; Motherboard: [Model] - [Justification]; RAM: [Model] - [Justification]; Storage: [Model] - [Justification]; PSU: [Model] - [Justification]; Case: [Model] - [Justification]; CPU Cooler: [Model] - [Justification]. I highlight potential bottlenecks, suggest alternatives if needed, and provide purchase links if applicable.");

            ChatSession session = new(executor, chatHistory);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("The chat session has started.\nUser: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string userInput = Console.ReadLine() ?? "";

            while (userInput != "exit")
            {
                await foreach ( // Generate the response streamingly.
                    var text
                    in session.ChatAsync(
                        new ChatHistory.Message(AuthorRole.User, userInput),
                        inferenceParams))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                userInput = Console.ReadLine() ?? "";
            }
        }

        public (InferenceParams, InteractiveExecutor) InitializeModel(string pathToModel, uint contextSize, int gpuLayerCount, int maxTokens, List<string> antiPrompts)
        {
            string modelPath = pathToModel;
            var parameters = new ModelParams(modelPath)
            {
                ContextSize = contextSize, // The longest length of chat as memory.
                GpuLayerCount = gpuLayerCount // How many layers to offload to GPU. Please adjust it according to your GPU memory.
            };
            using var model = LLamaWeights.LoadFromFile(parameters);
            using var context = model.CreateContext(parameters);
            var executor = new InteractiveExecutor(context);

            InferenceParams inferenceParams = new InferenceParams()
            {
                MaxTokens = maxTokens, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
                AntiPrompts = antiPrompts // Stop generation once antiprompts appear.
            };

            return (inferenceParams, executor);
        }

        
    }
}
