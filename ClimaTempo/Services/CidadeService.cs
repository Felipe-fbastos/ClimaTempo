using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    class CidadeService
    {
        private HttpClient client;
        private List<Cidade> cidades;
        private List<Previsao> previsao;
        private JsonSerializerOptions options;
        Uri uri = new Uri("https://brasilapi.com.br/api/cptec/v1/cidade/");
        Uri uriPrevisao = new Uri("https://brasilapi.com.br/api/cptec/v1/clima/previsao/");

        public CidadeService()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Cidade>> GetCidadesByName(string name)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{uri}/{name}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    cidades = JsonSerializer.Deserialize<List<Cidade>>(content, options);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return cidades;
        }

        public async Task<List<Previsao>> GetPrevisaoCidade(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{uriPrevisao}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsao = JsonSerializer.Deserialize<List<Previsao>>(content, options);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsao;
        }
    }
}
