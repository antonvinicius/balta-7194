using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(60, ErrorMessage = "Este campo pode ter no máximo 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres")]
    public string Title { get; set; }

    [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O preço não pode ser menor que $1")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
  }
}