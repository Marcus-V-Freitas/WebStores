namespace DLL.BLL.Models
{
    public class ItemVenda
    {
        public int ID { get; set; }

        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal Preço { get; set; }

        public int VendaId { get; set; }

        public Venda Venda { get; set; }
    }
}
