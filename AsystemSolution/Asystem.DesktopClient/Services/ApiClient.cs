using System.Net.Http.Json;
using Asystem.Core.Entities;

namespace Asystem.DesktopClient.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        public ApiClient(string baseAddress)
        {
            var handler = new HttpClientHandler
            {
                UseProxy = false, // отключаем системный прокси, который может давать 502 для localhost
                AllowAutoRedirect = true,
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(15)
            };
            _http.DefaultRequestHeaders.ConnectionClose = false;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _http.GetFromJsonAsync<List<Order>>("api/orders") ?? new List<Order>();
        }

        public async Task<Order?> GetOrderAsync(int id)
        {
            return await _http.GetFromJsonAsync<Order>($"api/orders/{id}");
        }

        public async Task<bool> CalculateAsync(int id)
        {
            var resp = await _http.PostAsync($"api/orders/{id}/stage", null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeStageAsync(int id, object stage)
        {
            var resp = await _http.PostAsJsonAsync($"api/orders/{id}/stage", stage);
            return resp.IsSuccessStatusCode;
        }

        public async Task<List<Asystem.Core.Entities.Material>> GetMaterialsAsync()
        {
            return await _http.GetFromJsonAsync<List<Asystem.Core.Entities.Material>>("api/materials") ?? new List<Asystem.Core.Entities.Material>();
        }

        public async Task<bool> CompleteTaskAsync(int orderId, int taskId)
        {
            var resp = await _http.PostAsync($"api/orders/{orderId}/task/{taskId}/complete", null);
            return resp.IsSuccessStatusCode;
        }

        // Методы для работы с формулами
        public async Task<List<Formula>> GetFormulasAsync()
        {
            return await _http.GetFromJsonAsync<List<Formula>>("api/formulas") ?? new List<Formula>();
        }

        public async Task<Formula?> GetFormulaAsync(int id)
        {
            return await _http.GetFromJsonAsync<Formula>($"api/formulas/{id}");
        }

        public async Task<Formula?> GetFormulaByProductTypeAsync(string productType)
        {
            return await _http.GetFromJsonAsync<Formula>($"api/formulas/by-product/{productType}");
        }

        public async Task<Formula?> CreateFormulaAsync(Formula formula)
        {
            var resp = await _http.PostAsJsonAsync("api/formulas", formula);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadFromJsonAsync<Formula>();
            }
            return null;
        }

        public async Task<bool> UpdateFormulaAsync(int id, Formula formula)
        {
            var resp = await _http.PutAsJsonAsync($"api/formulas/{id}", formula);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFormulaAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/formulas/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> ActivateFormulaAsync(int id)
        {
            var resp = await _http.PostAsync($"api/formulas/{id}/activate", null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeactivateFormulaAsync(int id)
        {
            var resp = await _http.PostAsync($"api/formulas/{id}/deactivate", null);
            return resp.IsSuccessStatusCode;
        }

        // Products
        public async Task<List<Asystem.Core.Entities.Product>> GetProductsAsync()
        {
            return await _http.GetFromJsonAsync<List<Asystem.Core.Entities.Product>>("api/products") ?? new List<Asystem.Core.Entities.Product>();
        }

        public async Task<Asystem.Core.Entities.Product?> GetProductAsync(int id)
        {
            return await _http.GetFromJsonAsync<Asystem.Core.Entities.Product>($"api/products/{id}");
        }

        public async Task<Asystem.Core.Entities.Product?> CreateProductAsync(Asystem.Core.Entities.Product product)
        {
            var resp = await _http.PostAsJsonAsync("api/products", product);
            if (resp.IsSuccessStatusCode) return await resp.Content.ReadFromJsonAsync<Asystem.Core.Entities.Product>();
            return null;
        }

        public async Task<bool> UpdateProductAsync(int id, Asystem.Core.Entities.Product product)
        {
            var resp = await _http.PutAsJsonAsync($"api/products/{id}", product);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/products/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
