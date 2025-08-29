using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "O campo de Login e obrigatorio!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo de Login deve ter entre 3 a 20 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo de Password e obrigatorio!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo de Password deve ter entre 3 a 20 caracteres.")]
        public string Password { get; set; }
    }
}