using System.Diagnostics;
using System.Net;
using CodeFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

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

    [HttpPost]
    [Route("DeleteUser")]
    public IActionResult DeleteUser(UserModel userModel) // 也可宣告參數為: int Id
    {
        _logger.LogInformation("...進入 UserController deleteUser: id={UserModelId}...", userModel.Id);

        var targetUserModel = _dbContext.Users.Find(userModel.Id);
        if (targetUserModel == null)
        {
            // return NotFound(); // 到自訂的 404.html
            throw new BadHttpRequestException($"Data Not Found: {userModel.Id}");
        }

        _dbContext.Users.Remove(targetUserModel);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("ToAddUserPage")]
    public IActionResult ToAddUserPage()
    {
        if (TempData.Keys.Count != 0)
        {
            var deserializeObject = JsonConvert.DeserializeObject<List<String>>((string) TempData["errMsgs"]);
            ViewData["errMsgs"] = deserializeObject;
            TempData.Clear();
        }
        ViewData["RefererUrl"] = Request.Headers["Referer"].ToString();
        return View("~/Views/User/AddUserPage.cshtml");
    }

    [HttpPost]
    [Route("AddUser")]
    public IActionResult AddUser(UserModel userModel)
    {
        Console.WriteLine(" === 進入 UserController.AddUser === ");
        // 參考: https://www.completecsharptutorial.com/asp-net-mvc5/4-ways-to-create-form-in-asp-net-mvc.php

        // -------------------------------------------------------
        Console.WriteLine("Request 送來準備新增的資料：");
        Console.WriteLine($">>> userModel = {userModel}");
        Console.WriteLine($">>> ModelState.IsValid: {ModelState.IsValid}");

        // 使用 Model-Validation 驗證
        if (ModelState.IsValid) // 驗證通過 → 呼叫 DBContext 進行 新增
        {
            return RedirectToAction("Index");
        }
            
        // 「驗證不通過」→ 繼續在新增頁
        var errMsgs = ModelState.Values.SelectMany(vv =>
        {
            return vv.Errors.Select(xx => xx.ErrorMessage);
        });
        
        // 使用 TempData 在不同 Action 中傳遞資料
        // TempData 需使用 Json 傳遞
        TempData["errMsgs"] = JsonConvert.SerializeObject(errMsgs);
        return RedirectToAction("ToAddUserPage", "User");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}