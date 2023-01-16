using CodeFirst_Demo.Models;

namespace CodeFirst_Demo.Services;

public class UserService : IUserService
{
    // DI
    private readonly MyDbContext_CodeFirst _dbContext;

    // DI
    private readonly ILogger<UserService> _logger;

    [ActivatorUtilitiesConstructor] // (Optional) 告訴DI容器使用此建構式(ref. https://blog.darkthread.net/blog/aspnet-core-di-multi-constructors/)
    public UserService(ILogger<UserService> logger, MyDbContext_CodeFirst dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    // 查詢所有 User
    public HashSet<UserModel> GetAllUsers()
    {
        _logger.LogInformation("進入 {Name} ", this.GetType().Name);
        return _dbContext.Users.ToHashSet();
    }

    // 查詢 userModel by userId
    public UserModel? FindByPrimaryKey(int userId)
    {
        _logger.LogInformation("進入 {Name} - {MethodName}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        
        // LINQ 寫法
        var targetUserModel =
            (from uu in _dbContext.Users
                where uu.Id == userId
                select uu).FirstOrDefault();
        return targetUserModel != null
            ? targetUserModel
            : new UserModel
            {
                Id = 0, Age = "0", Email = "N/A", Name = "N/A", Nickname = "N/A", UserName = "N/A"
            };

        // LINQ 編程式寫法
        // return _dbContext.Users.FirstOrDefault(xx => xx.Id.Equals(userId));
    } 
    
    // 查詢 userModel by model-condition
    public UserModel? FindByCondition(UserModel model)
    {
        _logger.LogInformation("進入 {Name} - {MethodName}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        // LINQ 編程式寫法
        return _dbContext.Users.FirstOrDefault(xx => xx.Id.Equals(model.Id) && xx.UserName.Equals(model.UserName.Trim()));
    }
}