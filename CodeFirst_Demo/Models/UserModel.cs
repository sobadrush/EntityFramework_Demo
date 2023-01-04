using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CodeFirst_Demo.Models;

public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("▲ID▲")]
    public int Id { get; set; }
    
    [Required]
    [DisplayName("▲Name▲")]
    public string Name { get; set; }
    
    [Required]
    [Display(Name = "⌘UserName⌘")]
    public string UserName { get; set; }
    
    [DisplayName("▲Email▲")]
    public string Email { get; set; }
    
    [Required]
    [DisplayName("▲Age▲")]
    public int Age { get; set; }

    [DisplayName("▲Nickname▲")]
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