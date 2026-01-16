using LicenciaSistemasAdmin.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LicenciaSistemasAdmin.Services
{
    public class LicenciaApiService
    {
        private readonly HttpClient _http;

        private const string BASE_URL =
            "https://localhost:7299/api/licencias";

        public LicenciaApiService()
        {
            _http = new HttpClient();
        }

        public async Task<List<Licencia>> ObtenerTodasAsync()
            => await _http.GetFromJsonAsync<List<Licencia>>(BASE_URL);

        public async Task CrearAsync(Licencia lic)
            => await _http.PostAsJsonAsync(BASE_URL, lic);

        public async Task ActualizarAsync(int id, Licencia lic)
            => await _http.PutAsJsonAsync($"{BASE_URL}/{id}", lic);

        public async Task EliminarAsync(int id)
            => await _http.DeleteAsync($"{BASE_URL}/{id}");
    }
}
