﻿using Microsoft.EntityFrameworkCore;

namespace CodeFirst_Demo.Models;

public class MyDbContext_CodeFirst : DbContext
{
    protected MyDbContext_CodeFirst()
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"MyDbContext_CodeFirst 預設建構式");
        Console.ResetColor(); 
    }

    public MyDbContext_CodeFirst(DbContextOptions options) : base(options)
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"MyDbContext_CodeFirst 1參數建構式");
        Console.ResetColor(); 
    }

    // 建立連線(寫死連線字串)
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlite(@"Data Source=E:\dotnet_workspace\EntityFramework_Demo\EmpDept.sqlite");
    
    // 建立連線(連線字串放到 appsettings.json)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        var connStr = configuration.GetConnectionString("RogerSqliteConnString");
        optionsBuilder.UseSqlite(connStr);
    }
    
    // 建立 Users 表格
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<UserModel>().HasData(
        //     new UserModel
        //     {
        //         Id = 1,
        //         Name = "Roger Lo",
        //         UserName = "友寄隆輝",
        //         Email = "test123@gmail.com.tw",
        //         Age = 23
        //     }
        // );
        List<UserModel> userModelList = new List<UserModel>
        {
            new UserModel { Id = 1, Name = "Roger Lo", UserName = "友寄隆輝", Email = "test111@gmail.com.tw", Age = 23 },
            new UserModel { Id = 2, Name = "Doraemon", UserName = "多啦A夢", Email = "test222@gmail.com.tw", Age = 24 },
            new UserModel { Id = 3, Name = "Pikachu", UserName = "皮卡丘", Email = "test333@gmail.com.tw", Age = 25 },
        };

        modelBuilder.Entity<UserModel>().HasData(userModelList);
    }
}