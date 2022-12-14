using CodeFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 註冊 DBContext，從 appsettings.json 中取得連線字串
builder.Services.AddDbContext<MyDbContext_CodeFirst>(optionsBuilder =>
{
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("RogerSqliteConnString"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// 先註冊 AddHealthChecks 方法
builder.Services.AddHealthChecks();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // 未捕捉的Exception程式錯誤，要執行的Controller與Action(必須 / 開頭，否則會發生An error occurred while starting the application)
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // 當 env.EnvironmentName 為 Development，程式出現詳細的錯誤訊息
    // app.UseExceptionHandler("/Home/Error");
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 直接呼叫專案下的/healthz ,就會回傳專案的健康狀況 ref. https://ithelp.ithome.com.tw/articles/10242210
app.MapHealthChecks("/healthz");

// 以下設定會被定義在 class 中的 Annotation(Attribute-Based) 覆蓋
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//當用戶輸入的網址找不到時↓
app.UseStatusCodePagesWithRedirects("~/404.html"); //或直接給http開頭的絕對URL

app.Run();