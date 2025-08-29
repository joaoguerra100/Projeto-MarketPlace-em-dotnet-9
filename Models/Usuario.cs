using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome e obrigatorio!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 50 e 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Login e obrigatorio!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Login deve ter entre 20 e 3 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo Password e obrigatorio!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Password deve ter entre 100 e 3 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Funcao e obrigatorio!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo Funcao deve ter entre 20 e 3 caracteres")]
        public string Funcao { get; set; }
        
        // Chave estrangeira para a Pessoa
        public Guid PessoaId { get; set; }
        // Propriedade de navegação para os dados da Pessoa
        public Pessoa Pessoa { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
        }
    }
}

