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
    public IActionResult Editor(string password)
    {
        if (password == AdminPassword)
        {
            // Password is correct, redirect to admin panel
            return RedirectToAction("AdminPanel");
        }

        // Default case - show login
        return View(new AdminViewModel { IsAuthenticated = false });
    }

    [HttpGet]
    public IActionResult AdminPanel()
    {
        // Check authentication - you might want to use proper session management
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

    // Add Component Views
    [HttpGet]
    public IActionResult AddComponent(string type)
    {
        ViewBag.ComponentType = type;
        ViewBag.Title = $"Add {type.ToUpper()}";

        switch (type.ToLower())
        {
            case "cpu":
                return View("AddCpu", new Cpu());
            case "motherboard":
                return View("AddMotherboard", new Motherboard());
            case "gpu":
                return View("AddGpu", new Gpu());
            case "ram":
                return View("AddRam", new Ram());
            case "ssd":
                return View("AddSsd", new Ssd());
            case "psu":
                return View("AddPsu", new Psu());
            case "case":
                return View("AddCase", new Case());
            case "cooler":
                return View("AddCooler", new Cooler());
            default:
                return NotFound();
        }
    }

    // Edit Component Views
    [HttpGet]
    public IActionResult EditComponent(string type, int id)
    {
        ViewBag.ComponentType = type;
        ViewBag.Title = $"Edit {type.ToUpper()}";

        switch (type.ToLower())
        {
            case "cpu":
                var cpu = context.Cpus.Find(id);
                if (cpu == null) return NotFound();
                return View("EditCpu", cpu);
            case "motherboard":
                var motherboard = context.Motherboards.Find(id);
                if (motherboard == null) return NotFound();
                return View("EditMotherboard", motherboard);
            case "gpu":
                var gpu = context.Gpus.Find(id);
                if (gpu == null) return NotFound();
                return View("EditGpu", gpu);
            case "ram":
                var ram = context.Rams.Find(id);
                if (ram == null) return NotFound();
                return View("EditRam", ram);
            case "ssd":
                var ssd = context.Ssds.Find(id);
                if (ssd == null) return NotFound();
                return View("EditSsd", ssd);
            case "psu":
                var psu = context.Psus.Find(id);
                if (psu == null) return NotFound();
                return View("EditPsu", psu);
            case "case":
                var caseItem = context.Cases.Find(id);
                if (caseItem == null) return NotFound();
                return View("EditCase", caseItem);
            case "cooler":
                var cooler = context.Coolers.Find(id);
                if (cooler == null) return NotFound();
                return View("EditCooler", cooler);
            default:
                return NotFound();
        }
    }

    // Add Component Actions
    [HttpPost]
    public IActionResult AddCpu(Cpu cpu)
    {
        if (ModelState.IsValid)
        {
            context.Cpus.Add(cpu);
            context.SaveChanges();
            TempData["Success"] = "CPU added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(cpu);
    }

    [HttpPost]
    public IActionResult AddMotherboard(Motherboard motherboard)
    {
        if (ModelState.IsValid)
        {
            context.Motherboards.Add(motherboard);
            context.SaveChanges();
            TempData["Success"] = "Motherboard added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(motherboard);
    }

    [HttpPost]
    public IActionResult AddGpu(Gpu gpu)
    {
        if (ModelState.IsValid)
        {
            context.Gpus.Add(gpu);
            context.SaveChanges();
            TempData["Success"] = "GPU added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(gpu);
    }

    [HttpPost]
    public IActionResult AddRam(Ram ram)
    {
        if (ModelState.IsValid)
        {
            context.Rams.Add(ram);
            context.SaveChanges();
            TempData["Success"] = "RAM added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(ram);
    }

    [HttpPost]
    public IActionResult AddSsd(Ssd ssd)
    {
        if (ModelState.IsValid)
        {
            context.Ssds.Add(ssd);
            context.SaveChanges();
            TempData["Success"] = "SSD added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(ssd);
    }

    [HttpPost]
    public IActionResult AddPsu(Psu psu)
    {
        if (ModelState.IsValid)
        {
            context.Psus.Add(psu);
            context.SaveChanges();
            TempData["Success"] = "PSU added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(psu);
    }

    [HttpPost]
    public IActionResult AddCase(Case caseItem)
    {
        if (ModelState.IsValid)
        {
            context.Cases.Add(caseItem);
            context.SaveChanges();
            TempData["Success"] = "Case added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(caseItem);
    }

    [HttpPost]
    public IActionResult AddCooler(Cooler cooler)
    {
        if (ModelState.IsValid)
        {
            context.Coolers.Add(cooler);
            context.SaveChanges();
            TempData["Success"] = "Cooler added successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(cooler);
    }

    // Edit Component Actions
    [HttpPost]
    public IActionResult EditCpu(Cpu cpu)
    {
        if (ModelState.IsValid)
        {
            context.Cpus.Update(cpu);
            context.SaveChanges();
            TempData["Success"] = "CPU updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(cpu);
    }

    [HttpPost]
    public IActionResult EditMotherboard(Motherboard motherboard)
    {
        if (ModelState.IsValid)
        {
            context.Motherboards.Update(motherboard);
            context.SaveChanges();
            TempData["Success"] = "Motherboard updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(motherboard);
    }

    [HttpPost]
    public IActionResult EditGpu(Gpu gpu)
    {
        if (ModelState.IsValid)
        {
            context.Gpus.Update(gpu);
            context.SaveChanges();
            TempData["Success"] = "GPU updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(gpu);
    }

    [HttpPost]
    public IActionResult EditRam(Ram ram)
    {
        if (ModelState.IsValid)
        {
            context.Rams.Update(ram);
            context.SaveChanges();
            TempData["Success"] = "RAM updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(ram);
    }

    [HttpPost]
    public IActionResult EditSsd(Ssd ssd)
    {
        if (ModelState.IsValid)
        {
            context.Ssds.Update(ssd);
            context.SaveChanges();
            TempData["Success"] = "SSD updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(ssd);
    }

    [HttpPost]
    public IActionResult EditPsu(Psu psu)
    {
        if (ModelState.IsValid)
        {
            context.Psus.Update(psu);
            context.SaveChanges();
            TempData["Success"] = "PSU updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(psu);
    }

    [HttpPost]
    public IActionResult EditCase(Case caseItem)
    {
        if (ModelState.IsValid)
        {
            context.Cases.Update(caseItem);
            context.SaveChanges();
            TempData["Success"] = "Case updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(caseItem);
    }

    [HttpPost]
    public IActionResult EditCooler(Cooler cooler)
    {
        if (ModelState.IsValid)
        {
            context.Coolers.Update(cooler);
            context.SaveChanges();
            TempData["Success"] = "Cooler updated successfully!";
            return RedirectToAction("AdminPanel");
        }
        return View(cooler);
    }

    // Delete Component Actions
    [HttpPost]
    public IActionResult DeleteComponent(string type, int id)
    {
        try
        {
            switch (type.ToLower())
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
            TempData["Success"] = $"{type} deleted successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error deleting {type}: {ex.Message}";
        }

        return RedirectToAction("AdminPanel");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}