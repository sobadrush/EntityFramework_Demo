using CodeFirst_Demo.Models;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
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

app.Run();