using Microsoft.EntityFrameworkCore;

namespace CodeFirst_Demo.Models;

public class MyDbContext_CodeFirst : DbContext
{
    // 建立連線
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(@"Data Source=E:\dotnet_workspace\EntityFramework_Demo\EmpDept.sqlite");

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