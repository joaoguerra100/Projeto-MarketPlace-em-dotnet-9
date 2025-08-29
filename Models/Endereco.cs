using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Endereco
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Rua e obrigatorio")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 30 caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O campo Numero e obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 10 caracteres")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo Bairro e obrigatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Bairro deve ter entre 3 e 20 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo Cidade e obrigatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Cidade deve ter entre 3 e 20 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo Estado e obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O campo Estado deve ter entre 3 e 10 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Cep e obrigatorio")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo Cep deve ter entre 3 e 30 caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo Pais e obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O campo Pais deve ter entre 3 e 10 caracteres")]
        public string Pais { get; set; }

        public Guid PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        
        public Endereco()
        {
            Id = Guid.NewGuid();
        }
    }
}