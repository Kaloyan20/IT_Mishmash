using Services.Interfaces;
using LLama.Common;
using LLama;
using Data;
using System.Text.Json;
using Data.Data;


namespace Services
{
    public class PCService : IPCService
    {
        private readonly PcPartsPickerContext _context;
        public static readonly string PATH_TO_LLAMA = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Models\\Llama-3.2-1B-Instruct-f16.gguf");

        public PCService()
        {
            _context = new PcPartsPickerContext();
        }

        public async Task InitializeModel(string modelPath, uint contextSize, int gpuLayerCount, int maxTokens, List<string> antiPrompts)
        {
            var parameters = new ModelParams(modelPath)
            {
                ContextSize = contextSize, // The longest length of chat as memory.
                GpuLayerCount = gpuLayerCount // How many layers to offload to GPU. Please adjust it according to your GPU memory.
            };
            using var model = LLamaWeights.LoadFromFile(parameters);
            using var context = model.CreateContext(parameters);
            var executor = new InteractiveExecutor(context);

            var chatHistory = new ChatHistory();

            var cases = JsonSerializer.Serialize(_context.Cases.Select(x => x.Model).ToList());
            var ram = JsonSerializer.Serialize(_context.Rams.Select(x => x.Model).ToList());
            var cpus = JsonSerializer.Serialize(_context.Cpus.Select(x => x.Model).ToList());
            var gpus = JsonSerializer.Serialize(_context.Gpus.Select(x => x.Model).ToList());
            var ssds = JsonSerializer.Serialize(_context.Ssds.Select(x => x.Model).ToList());
            var psus = JsonSerializer.Serialize(_context.Psus.Select(x => x.Model).ToList());
            var motherboards = JsonSerializer.Serialize(_context.Motherboards.Select(x => x.Model).ToList());
            var coolers = JsonSerializer.Serialize(_context.Coolers.Select(x => x.Model).ToList());

            var modules = new Dictionary<string, string>
            {
                { "CPU", $"Available CPUs:\n{cpus}\nRecommend a CPU based on the user's performance needs and budget. Ensure socket compatibility with the selected motherboard. Avoid bottlenecks with the GPU." },
                { "GPU", $"Available GPUs:\n{gpus}\nSuggest a GPU that fits the user's use case, resolution, and budget. Ensure compatibility with the PSU, case, and CPU. Balance performance and value." },
                { "Motherboard", $"Available Motherboards:\n{motherboards}\nSelect a motherboard that matches the CPU socket and desired form factor. Ensure support for RAM type, SSD interface, and necessary features." },
                { "CPU Cooler", $"Available CPU Coolers:\n{coolers}\nChoose a cooler that fits the selected CPU and case. Factor in thermal output, noise preferences, and whether air or liquid cooling is better suited." },
                { "PSU", $"Available PSUs:\n{psus}\nPick a PSU that provides enough wattage for all selected components. Ensure it fits in the case and has the right connectors. Favor efficient models." },
                { "SSD", $"Available SSDs:\n{ssds}\nSelect storage that meets the user's speed and capacity needs. Confirm compatibility with the motherboard and allow room for future upgrades." },
                { "Case", $"Available Cases:\n{cases}\nChoose a case that fits the selected motherboard, GPU, cooler, and PSU. Consider airflow, clearance, aesthetics, and cable management." },
                { "RAM", $"Available RAM Kits:\n{ram}\nSelect RAM based on workload, capacity needs, and speed. Ensure compatibility with the CPU and motherboard. Recommend reliable and balanced kits." }
            };

            // Combine prompts into a single system message
            var systemPrompt = string.Join("\n\n", modules.Select(module => $"{module.Key}: {module.Value}"));
            chatHistory.AddMessage(AuthorRole.System, systemPrompt);

            ChatSession session = new(executor, chatHistory);

            InferenceParams inferenceParams = new InferenceParams()
            {
                MaxTokens = maxTokens, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
                AntiPrompts = antiPrompts, // Stop generation once antiprompts appear.
            };
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
    }
}
