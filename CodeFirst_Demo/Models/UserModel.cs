using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeFirst_Demo.Models;

public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("▲ID▲")]
    public int Id { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    [DisplayName("▲Name▲")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "UserName 為必填欄位！")]
    [DataType(DataType.Text)]
    [Display(Name = "⌘UserName⌘")]
    public string UserName { get; set; }
    
    [EmailAddress(ErrorMessage = "Email 欄位為必填欄位！")]
    [DisplayName("▲Email▲")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Age 欄位為必填欄位！")]
    [DataType(DataType.Text)]
    [DisplayName("▲Age▲")]
    public string Age { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    [Display(Name = "▲▲Nickname▲▲")]
    public string Nickname { get; set; }

    public UserModel()
    {
    }

    public UserModel(int id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}