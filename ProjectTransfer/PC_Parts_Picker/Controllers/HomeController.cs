using System.Diagnostics;
using Data;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using PC_Parts_Picker.Models;

namespace PC_Parts_Picker.Controllers;

public class HomeController : Controller
{
    private PcPartsPickerContext context;
    private const string AdminPassword = "1357642";

    public HomeController(PcPartsPickerContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Components()
    {
        return View();
    }

    public IActionResult SystemBuilder()
    {
        return View();
    }

    public IActionResult BuildResult()
    {
        var model = new SystemBuilderViewModel()
        {
            ShowResults = true,
            BuildName = "My PC",
            TotalPrice = 1000,
            Cpu = context.Cpus.FirstOrDefault(),
            Motherboard = context.Motherboards.FirstOrDefault(),
            Gpu = context.Gpus.FirstOrDefault(),
            Ram = context.Rams.FirstOrDefault(),
            Ssd = context.Ssds.FirstOrDefault(),
            Psu = context.Psus.FirstOrDefault(),
            Case = context.Cases.FirstOrDefault(),
            Cooler = context.Coolers.FirstOrDefault()
        };
        return View(model);
    }

    public IActionResult Cpus()
    {
        var cpus = context.Cpus.ToList();
        return View(cpus);
    }

    public IActionResult Motherboards()
    {
        var motherboards = context.Motherboards.ToList();
        return View(motherboards);
    }

    public IActionResult Gpus()
    {
        var gpus = context.Gpus.ToList();
        return View(gpus);
    }

    public IActionResult Memory()
    {
        var Memory = context.Rams.ToList();
        return View(Memory);
    }

    public IActionResult Storage()
    {
        var Storage = context.Ssds.ToList();
        return View(Storage);
    }

    public IActionResult PowerSupplies()
    {
        var psus = context.Psus.ToList();
        return View(psus);
    }

    public IActionResult Cases()
    {
        var cases = context.Cases.ToList();
        return View(cases);
    }

    public IActionResult Coolers()
    {
        var coolers = context.Coolers.ToList();
        return View(coolers);
    }

    // Admin functionality
    [HttpGet]
    public IActionResult Editor()
    {
        var model = new AdminViewModel
        {
            IsAuthenticated = false
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Editor(string password, string action)
    {
        if (action == "login" && password == AdminPassword)
        {
            // Password is correct, show admin panel
            var model = new AdminViewModel
            {
                IsAuthenticated = true,
                Cpus = context.Cpus.ToList(),
                Motherboards = context.Motherboards.ToList(),
                Gpus = context.Gpus.ToList(),
                Rams = context.Rams.ToList(),
                Ssds = context.Ssds.ToList(),
                Psus = context.Psus.ToList(),
                Cases = context.Cases.ToList(),
                Coolers = context.Coolers.ToList()
            };
            return View(model);
        }

        // Admin panel operations
        if (password == AdminPassword || TempData["IsAdminAuthenticated"] as bool? == true)
        {
            TempData["IsAdminAuthenticated"] = true;

            switch (action)
            {
                case "add":
                    return HandleAdd();
                case "edit":
                    return HandleEdit();
                case "delete":
                    return HandleDelete();
            }
        }

        // Default case - show login
        return View(new AdminViewModel { IsAuthenticated = false });
    }

    private IActionResult HandleAdd()
    {
        var type = Request.Form["type"];

        try
        {
            switch (type)
            {
                case "cpu":
                    var cpu = new Cpu
                    {
                        Brand = Request.Form["brand"],
                        Model = Request.Form["model"],
                        SocketType = Request.Form["socketType"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Cpus.Add(cpu);
                    break;

                case "motherboard":
                    var motherboard = new Motherboard
                    {
                        Brand = Request.Form["brand"],
                        Model = Request.Form["model"],
                        Chipset = Request.Form["chipset"],
                        SocketType = Request.Form["socketType"],
                        FormFactor = Request.Form["formFactor"],
                        MemorySupport = Request.Form["memorySupport"],
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Motherboards.Add(motherboard);
                    break;

                case "gpu":
                    var gpu = new Gpu
                    {
                        Brand = Request.Form["brand"],
                        Model = Request.Form["model"],
                        Memory = ParseInt(Request.Form["memory"]),
                        PowerConsumption = ParseInt(Request.Form["powerConsumption"]),
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Gpus.Add(gpu);
                    break;

                case "ram":
                    var ram = new Ram
                    {
                        Model = Request.Form["model"],
                        MemorySize = ParseInt(Request.Form["memorySize"]),
                        MemoryType = Request.Form["memoryType"],
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Rams.Add(ram);
                    break;

                case "ssd":
                    var ssd = new Ssd
                    {
                        Model = Request.Form["model"],
                        Storage = ParseInt(Request.Form["storage"]),
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Ssds.Add(ssd);
                    break;

                case "psu":
                    var psu = new Psu
                    {
                        Model = Request.Form["model"],
                        Wattage = ParseInt(Request.Form["wattage"]),
                        Rating = Request.Form["rating"],
                        Size = Request.Form["size"],
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Psus.Add(psu);
                    break;

                case "case":
                    var caseItem = new Case
                    {
                        Model = Request.Form["model"],
                        Aio = ParseBool(Request.Form["aio"]),
                        Size = Request.Form["size"],
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Cases.Add(caseItem);
                    break;

                case "cooler":
                    var cooler = new Cooler
                    {
                        Model = Request.Form["model"],
                        Aio = ParseBool(Request.Form["aio"]),
                        MiniItxCompatibility = ParseBool(Request.Form["miniItxCompatibility"]),
                        Color = Request.Form["color"],
                        Price = ParseDouble(Request.Form["price"])
                    };
                    context.Coolers.Add(cooler);
                    break;
            }

            context.SaveChanges();
            TempData["Success"] = "Component added successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error adding component: {ex.Message}";
        }

        return RedirectToAction("Admin");
    }

    private IActionResult HandleEdit()
    {
        var type = Request.Form["type"];
        var id = ParseInt(Request.Form["id"]);

        try
        {
            switch (type)
            {
                case "cpu":
                    var cpu = context.Cpus.Find(id);
                    if (cpu != null)
                    {
                        cpu.Brand = Request.Form["brand"];
                        cpu.Model = Request.Form["model"];
                        cpu.SocketType = Request.Form["socketType"];
                        cpu.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "motherboard":
                    var motherboard = context.Motherboards.Find(id);
                    if (motherboard != null)
                    {
                        motherboard.Brand = Request.Form["brand"];
                        motherboard.Model = Request.Form["model"];
                        motherboard.Chipset = Request.Form["chipset"];
                        motherboard.SocketType = Request.Form["socketType"];
                        motherboard.FormFactor = Request.Form["formFactor"];
                        motherboard.MemorySupport = Request.Form["memorySupport"];
                        motherboard.Color = Request.Form["color"];
                        motherboard.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "gpu":
                    var gpu = context.Gpus.Find(id);
                    if (gpu != null)
                    {
                        gpu.Brand = Request.Form["brand"];
                        gpu.Model = Request.Form["model"];
                        gpu.Memory = ParseInt(Request.Form["memory"]);
                        gpu.PowerConsumption = ParseInt(Request.Form["powerConsumption"]);
                        gpu.Color = Request.Form["color"];
                        gpu.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "ram":
                    var ram = context.Rams.Find(id);
                    if (ram != null)
                    {
                        ram.Model = Request.Form["model"];
                        ram.MemorySize = ParseInt(Request.Form["memorySize"]);
                        ram.MemoryType = Request.Form["memoryType"];
                        ram.Color = Request.Form["color"];
                        ram.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "ssd":
                    var ssd = context.Ssds.Find(id);
                    if (ssd != null)
                    {
                        ssd.Model = Request.Form["model"];
                        ssd.Storage = ParseInt(Request.Form["storage"]);
                        ssd.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "psu":
                    var psu = context.Psus.Find(id);
                    if (psu != null)
                    {
                        psu.Model = Request.Form["model"];
                        psu.Wattage = ParseInt(Request.Form["wattage"]);
                        psu.Rating = Request.Form["rating"];
                        psu.Size = Request.Form["size"];
                        psu.Color = Request.Form["color"];
                        psu.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "case":
                    var caseItem = context.Cases.Find(id);
                    if (caseItem != null)
                    {
                        caseItem.Model = Request.Form["model"];
                        caseItem.Aio = ParseBool(Request.Form["aio"]);
                        caseItem.Size = Request.Form["size"];
                        caseItem.Color = Request.Form["color"];
                        caseItem.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;

                case "cooler":
                    var cooler = context.Coolers.Find(id);
                    if (cooler != null)
                    {
                        cooler.Model = Request.Form["model"];
                        cooler.Aio = ParseBool(Request.Form["aio"]);
                        cooler.MiniItxCompatibility = ParseBool(Request.Form["miniItxCompatibility"]);
                        cooler.Color = Request.Form["color"];
                        cooler.Price = ParseDouble(Request.Form["price"]);
                    }
                    break;
            }

            context.SaveChanges();
            TempData["Success"] = "Component updated successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error updating component: {ex.Message}";
        }

        return RedirectToAction("Admin");
    }

    private IActionResult HandleDelete()
    {
        var type = Request.Form["type"];
        var id = ParseInt(Request.Form["id"]);

        try
        {
            switch (type)
            {
                case "cpu":
                    var cpu = context.Cpus.Find(id);
                    if (cpu != null) context.Cpus.Remove(cpu);
                    break;

                case "motherboard":
                    var motherboard = context.Motherboards.Find(id);
                    if (motherboard != null) context.Motherboards.Remove(motherboard);
                    break;

                case "gpu":
                    var gpu = context.Gpus.Find(id);
                    if (gpu != null) context.Gpus.Remove(gpu);
                    break;

                case "ram":
                    var ram = context.Rams.Find(id);
                    if (ram != null) context.Rams.Remove(ram);
                    break;

                case "ssd":
                    var ssd = context.Ssds.Find(id);
                    if (ssd != null) context.Ssds.Remove(ssd);
                    break;

                case "psu":
                    var psu = context.Psus.Find(id);
                    if (psu != null) context.Psus.Remove(psu);
                    break;

                case "case":
                    var caseItem = context.Cases.Find(id);
                    if (caseItem != null) context.Cases.Remove(caseItem);
                    break;

                case "cooler":
                    var cooler = context.Coolers.Find(id);
                    if (cooler != null) context.Coolers.Remove(cooler);
                    break;
            }

            context.SaveChanges();
            TempData["Success"] = "Component deleted successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error deleting component: {ex.Message}";
        }

        return RedirectToAction("Admin");
    }

    // Helper methods
    private double? ParseDouble(string value)
    {
        return double.TryParse(value, out double result) ? result : null;
    }

    private int? ParseInt(string value)
    {
        return int.TryParse(value, out int result) ? result : null;
    }

    private bool? ParseBool(string value)
    {
        return value?.ToLower() == "true" || value == "1" ? true :
               value?.ToLower() == "false" || value == "0" ? false : null;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}