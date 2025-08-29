using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }

        //Caso queira ter Sub Categorias podendo ser nuso pois pode ser uma categoria princial sem ter um pai
        public Guid? CategoriaPaiId { get; set; }

        //Propriedade de navegação para a categoria Pai
        public Categoria CategoriaPai { get; set; }

        //Propriedade para navegação para a lista de subcategoria filhas
        public ICollection<Categoria> SubCategorias { get; set; }

        // Permite acessar acessar todos os produtos que se referenciam a esta categoria
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
            Id = Guid.NewGuid();
            Produtos = new List<Produto>();
            SubCategorias = new List<Categoria>();
        }
    }
}