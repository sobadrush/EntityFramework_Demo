using CodeFirst_Demo.Models;

namespace CodeFirst_Demo.Services;

public interface IUserService
{
    // 取得所有 UserModel
    public HashSet<UserModel> GetAllUsers();

    // 根據 userId 查詢 UserModel
    public UserModel? FindByPrimaryKey(int userId);

    // 查詢 userModel by model-condition
    public UserModel? FindByCondition(UserModel model);
}