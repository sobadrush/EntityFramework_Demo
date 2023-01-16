using Microsoft.AspNetCore.Mvc;
using CodeFirst_Demo.Models;

namespace CodeFirst_Demo.Controllers;

[Controller]
[Route("[controller]")]
public class HomeController : Controller
{
    // DI
    private readonly ILogger<HomeController> _logger;
    
    // DI
    private readonly MyDbContext_CodeFirst _dbContext;

    public HomeController(ILogger<HomeController> logger, MyDbContext_CodeFirst dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"呼叫 {this.GetType().Name} - {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
        Console.ResetColor();

        ViewData["testC"] = "我是 ViewData 傳遞的 testC";
        ViewBag.testB = "我是 ViewBag 傳遞的 testB";
        
        return View();
    }

    // [controller] → 表示 HomeController 之 "Controller" 以前的字串名 
    [HttpGet]
    [Route("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Error")]
    public IActionResult Error()
    {
        Console.WriteLine(" ...我是 HomeController.Error... ");
        return Redirect("~/500.html"); // 或 http 開頭的絕對 URL
    }
    
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}