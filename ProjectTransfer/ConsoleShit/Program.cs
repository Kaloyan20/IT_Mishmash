using System.Text.Json;
using Data;
using OllamaSharp;

var uri = new Uri("http://localhost:11434");

var ollama = new OllamaApiClient(uri);

var systemPrompt = "You are a highly specialized AI assistant focused exclusively on PC-related topics, including PC building, hardware components, compatibility, performance optimization, overclocking, cooling solutions, and troubleshooting. You do not provide assistance or information on subjects outside of PC hardware and building. If prompted with unrelated topics—such as software development, mobile devices, general tech support, entertainment, or personal advice—you must politely decline and guide the user back to PC building or hardware discussions. Maintain a helpful, professional tone at all times, and base your responses on current knowledge and best practices as of 2025.";


ollama.SelectedModel = "gemma3:4b";

var chat = new Chat(ollama, systemPrompt);

var _context = new PcPartsPickerContext();

var components = new Dictionary<string, string>()
{
    {"CPU", JsonSerializer.Serialize(_context.Cpus)},
    {"GPU", JsonSerializer.Serialize(_context.Gpus)},
    {"RAM", JsonSerializer.Serialize(_context.Rams)},
    {"SSD", JsonSerializer.Serialize(_context.Ssds)},
    {"PSU", JsonSerializer.Serialize(_context.Psus)},
    {"Motherboard", JsonSerializer.Serialize(_context.Motherboards)},
    {"Cooler", JsonSerializer.Serialize(_context.Coolers)},
    {"Case", JsonSerializer.Serialize(_context.Cases)}
};


var prompt = Console.ReadLine();

var response = string.Empty;

await foreach (var answerToken in chat.SendAsync($"Check whether the following: \"{prompt}\" has anything to do with PC building. Your output SHOULD be a single letter - y for yes or n for no."))
        response += answerToken;

response = response.Trim().ToLower();

if (response == "n")
{
    await foreach (var answerToken in chat.SendAsync($"Please provide a detailed response to the following prompt: \"{prompt}\""))
        Console.Write(answerToken);

    Console.WriteLine();
}
else if (response == "y")
{
    var purpose = "Gaming";
    var budget = "Budget (Under 2,000 lv.)";
    var formFactor = "Mid Tower";
    var preferredCPUBrand = "AMD";
    var preferredGPUBrand = "NVIDIA";
    var cooling = "Air Cooling";
    var color = "Black";

    var pcPrompt = $"The user is looking for  a pc with the following purpose: {purpose}, for the following budget: {budget}, with the following form factor: {formFactor}, with the following CPU brand if possible: {preferredCPUBrand}, the following GPU brand if possible: {preferredGPUBrand}, the following colling tech if possible: {cooling} and in the following color: {color}. Make sure that this prompt produces an adequate PC build.";

    chat = new Chat(ollama, pcPrompt);

    foreach (var component in components)
    {
        await foreach (var answerToken in chat.SendAsync($""))
            Console.Write(answerToken);
    }
}
else
{
    Console.WriteLine("Invalid response.");
}

