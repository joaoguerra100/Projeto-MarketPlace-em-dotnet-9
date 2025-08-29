using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.Models
{
    public class ItemDoPedido
    {
        [Key]
        public Guid Id { get; set; }

        // A qual pedido este item pertence?
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // Qual produto foi comprado?
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        // É CRUCIAL guardar o preço do produto no momento da compra,
        // pois o preço do produto original pode mudar no futuro.
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }

        public ItemDoPedido()
        {
            Id = Guid.NewGuid();
        }
    }
}