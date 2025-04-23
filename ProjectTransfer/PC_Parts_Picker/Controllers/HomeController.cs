using System.Diagnostics;
using Data;
using Microsoft.AspNetCore.Mvc;
using PC_Parts_Picker.Models;
using Services;

namespace PC_Parts_Picker.Controllers;

public class HomeController : Controller
{
    private PcPartsPickerContext context;

    public HomeController(PcPartsPickerContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        Services.PCService pcService = new Services.PCService();
        pcService.CreatePC();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}