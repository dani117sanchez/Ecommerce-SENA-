﻿using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly HttpClient _httpClient;
        public ProductoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string catalogo, string buscar)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Catalogo/{catalogo}/{buscar}");
        }

        public async Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo)
        {
            var response = await _httpClient.PostAsJsonAsync("Producto/Crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(ProductoDTO modelo)
        {
            var response = await _httpClient.PutAsJsonAsync("Producto/Editar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int Id)
        {
            return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Producto/Eliminar/{Id}");
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Producto/Lista/{buscar}");
        }

        public async Task<ResponseDTO<ProductoDTO>> Obtener(int Id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<ProductoDTO>>($"Producto/Obtener/{Id}");
        }
    }
}
