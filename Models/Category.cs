using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(60, ErrorMessage = "Este item pode ter no máximo 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este item tem que ter no mínimo 3 caracteres")]
    public string Title { get; set; }
  }
}