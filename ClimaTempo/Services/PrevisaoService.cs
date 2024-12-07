using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class PrevisaoService
    {
        
        private HttpClient client;
        //Cria variável para ser retornada
        private Previsao previsao;
        private Previsao previsaoProxDias;
        private JsonSerializerOptions options;
        Uri uri = new Uri("https://brasilapi.com.br/api/cptec/v1/clima/previsao/");
        
        public PrevisaoService()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<Previsao> GetPrevisaoById(int cityCode)
        {
            cityCode = 244;
            Uri requestUri = new Uri($"{uri}/{cityCode}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsao = JsonSerializer.Deserialize<Previsao>(content, options);
                }
                else
                {
                    Previsao previsao = new Previsao();
                    Clima clima = new Clima();
                    clima.Max = 32;
                    clima.Min = 32;
                    previsao.Estado = "SP";
                    previsao.Clima.Add(clima);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsao;
        }

        public async Task<Previsao> GetPrevisaoForXDaysById(int cityCode, int days)
        {
            cityCode = 244;
            days = 3;
            Uri requestUri = new Uri($"{uri}/{cityCode}/{days}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsaoProxDias = JsonSerializer.Deserialize<Previsao>(content, options);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsaoProxDias;
        }











    }
}
