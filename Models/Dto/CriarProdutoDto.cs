using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models.Dto
{
    public class CriarProdutoDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(200, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Descricao é obrigatório!")]
        [StringLength(1000, MinimumLength = 3)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Preco é obrigatório!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo QtdEstoque é obrigatório!")]
        public int QtdEstoque { get; set; }

        [Required]
        public Guid VendedorId { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }
    }
}
