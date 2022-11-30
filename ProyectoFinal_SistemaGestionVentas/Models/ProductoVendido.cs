namespace ProyectoFinal_SistemaGestionVentas.Models
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public int Stock { get; set; }

        public ProductoVendido() 
        {
            Id = 0;
            IdProducto = 0;
            IdVenta = 0;
            Stock = 0;
        }
    }
}