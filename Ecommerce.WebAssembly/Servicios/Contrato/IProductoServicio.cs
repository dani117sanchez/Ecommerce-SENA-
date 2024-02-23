using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Servicios.Contrato
{
    public interface IProductoServicio
    {
        Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar);
        Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string catalogo,string buscar);
        Task<ResponseDTO<ProductoDTO>> Obtener(int Id);
        Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo);
        Task<ResponseDTO<bool>> Editar(ProductoDTO modelo);
        Task<ResponseDTO<bool>> Eliminar(int Id);
    }
}
