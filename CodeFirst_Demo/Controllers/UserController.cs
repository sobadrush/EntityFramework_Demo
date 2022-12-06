using System.Diagnostics;
using CodeFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst_Demo.Controllers;

[Controller]
[Route("[controller]")]
public class UserController : Controller
{
    // DI
    private readonly ILogger<UserController> _logger;
    
    // DI
    private readonly MyDbContext_CodeFirst _dbContext;

    public UserController(ILogger<UserController> logger, MyDbContext_CodeFirst dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    // GET
    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        _logger.LogInformation("...進入 UserController Index...");
        _logger.LogInformation("...查詢所有 Users...");
        var userModels = _dbContext.Users.ToList();
        return View(userModels);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}