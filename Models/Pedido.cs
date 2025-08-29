using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.Models
{
    public class Pedido
    {
        [Key]
        public Guid Id { get; set; }

        //Chave estrangeira
        public Guid CompradorId { get; set; }
        public Pessoa Comprador { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        public string Status { get; set; } // Ex: "Aguardando Pagamento", "Pago", "Enviado", "Entregue", "Cancelado"
        public DateTime DataDoPedido { get; set; }

        // Chave estrangeira para o endereço de entrega escolhido
        public Guid EnderecoDeEntregaId { get; set; }
        public Endereco EnderecoDeEntrega { get; set; }
        
        // Um pedido é composto por vários itens
        public ICollection<ItemDoPedido> ItensDoPedido { get; set; }

        public Pedido()
        {
            Id = Guid.NewGuid();
            DataDoPedido = DateTime.UtcNow;
            Status = "Aguardando Pagamento";
            ItensDoPedido = new List<ItemDoPedido>();
        }
    }
}