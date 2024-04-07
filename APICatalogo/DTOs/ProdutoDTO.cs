using System.ComponentModel.DataAnnotations;
using ApiController.Validations;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
     public int Id { get; set; }
    [Required, StringLength(80)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required, StringLength(300)]
    public string? Descricao { get; set; }
    [Required]
    public decimal Preco { get; set; }
    [Required, StringLength(300)]
    public string? ImagemUrl { get; set; }
    public int CategoriaId { get; set; }   
}