using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(20, ErrorMessage = "Este campo pode ter no máximo 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres")]
    public string Username { get; set; }

    [MaxLength(20, ErrorMessage = "Este campo pode ter no máximo 20 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres")]
    public string Password { get; set; }
    public string Role { get; set; }
  }
}