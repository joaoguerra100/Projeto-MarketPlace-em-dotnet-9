using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome e obrigatorio!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Descricao e obrigatorio!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Descricao deve ter entre 3 e 200 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Preco e obrigatorio!")]
        public Decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo QtdEstoque e obrigatorio!")]
        public int QtdEstoque { get; set; }

        [Required(ErrorMessage = "O campo Status e obrigatorio!")]
        public string Status { get; set; } // Ativo, Pausado, Vendido

        public DateTime DataDeCriacao { get; set; }
        public DateTime DataDeAtualizacao { get; set; }

        public Guid VendedorId { get; set; }
        public Pessoa Vendedor { get; set; }

        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Produto()
        {
            Id = Guid.NewGuid();
            DataDeCriacao = DateTime.UtcNow;
        }

    }
}