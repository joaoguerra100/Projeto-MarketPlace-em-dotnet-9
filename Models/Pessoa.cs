using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Pessoa
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome e obrigatorio!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "O campo Telefone deve ter ate 20 caracteres")]
        public string Telefone { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataDeNascimento { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataDeCriacao { get; set; }

        [Required(ErrorMessage = "O campo Documento e obrigatorio!")]
        public string Documento { get; set; }

        [StringLength(20, ErrorMessage = "O campo Genero deve ter ate 10 caracteres")]
        public string Genero { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public Usuario Usuario { get; set; }

        public Pessoa()
        {
            Id = Guid.NewGuid();
            Enderecos = new List<Endereco>();
            DataDeCriacao = DateTime.UtcNow;
        }
    }
}