using System.Diagnostics;
using System.Net;
using CodeFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    [ValidateAntiForgeryToken] // 預防跨網站攻擊 CSRF，搭配前端
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
    [HttpPost]
    [Route("AddUser")]
    public IActionResult AddUser(UserModel userModel)
    {
        Console.WriteLine(" === 進入 UserController.AddUser === ");
        // 參考: https://www.completecsharptutorial.com/asp-net-mvc5/4-ways-to-create-form-in-asp-net-mvc.php

        // -------------------------------------------------------
        Console.WriteLine(">>> Request 送來準備新增的資料：");
        Console.WriteLine($" >>> userModel = {userModel}");
        Console.WriteLine($" >>> ModelState.IsValid: {ModelState.IsValid}");

        int intAge = 0; // 先定義一個 number 去接收 TryParse 成功轉換後的值
        if (!int.TryParse(userModel.Age, out intAge)) // 若 true，透過 out 將轉換結果存入 intAge 變數
        {
            ModelState.AddModelError("Age", "Age 只能為數字！");
        }
        
        //  >>> 使用 Model-Validation 驗證 <<< 
        // 驗證通過
        if (ModelState.IsValid)
        {
            // 呼叫 _DBContext 進行 新增
            _dbContext.Users.Add(userModel);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // Model-Validation 驗證失敗
        return View("~/Views/User/AddUserPage.cshtml");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
    
}