using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CodeFirst_Demo.Models;

public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    [Required]
    public int Age { get; set; }

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