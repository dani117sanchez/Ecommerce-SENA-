using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {

        private ILocalStorageService _localStorageService;
        private ISyncLocalStorageService _syncLocalStorageService;
        private IToastService _toastService;

        public CarritoServicio(
            ILocalStorageService localStorageService,
            ISyncLocalStorageService syncLocalStorageService,
            IToastService toastService
            )
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action MostrarItems;

        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {

                // crear el carrito en local
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

                // valida si existe in carrito 
                if ( carrito == null )
                    carrito = new List<CarritoDTO>();

                // encontrar productos existentes en el carrito
                var encontrado = carrito.FirstOrDefault(c => c.Producto.IdProducto == modelo.Producto.IdProducto);

                if( encontrado != null )
                    carrito.Remove( encontrado );
                carrito.Add( modelo );

                //Guardar nuevamente el objeto carrito es como actualizar el carrito
                await _localStorageService.SetItemAsync("carrito", carrito);

                if (encontrado == null)
                    _toastService.ShowSuccess("Producto Actualizado en carrito");
                else
                    _toastService.ShowSuccess("Producto Agregado al carrito");
                
                
                // Actualiza la vista
                MostrarItems.Invoke();
            }
            catch 
            {
                _toastService.ShowError("El producto no fue agregado al carrito");
            }

           
        }

        public int cantidadProductos()
        {
            var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");
            return carrito == null?0 : carrito.Count();
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
            if( carrito == null )
                carrito = new List<CarritoDTO>();
            return carrito;

        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if( carrito != null )
                {
                    var elemento = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                    if( elemento != null)
                    {
                        carrito.Remove(elemento);
                        await _localStorageService.SetItemAsync("carrito", carrito);
                        MostrarItems.Invoke();
                    }
                }

                
            }
            catch
            {

            }
        }

        public async Task LimpiarCarrito()
        {
            await _localStorageService.RemoveItemAsync("carrito");
            MostrarItems.Invoke();
        }
    }
}
