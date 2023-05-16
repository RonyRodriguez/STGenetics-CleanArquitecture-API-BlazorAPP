using STG.Domain.Domain;
using System.Text;
using System.Text.Json;

namespace STG.BlazorApp.Service
{
    public class AnimalService
    {
        private readonly HttpClient httpClient;
        private readonly string? apiUrl;
        private readonly string? apiUrlS;
        private readonly string? apiUrlB;

        public AnimalService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            apiUrl = configuration.GetSection("AppSettings:ApiUrl").Value;
            apiUrlS = configuration.GetSection("AppSettings:ApiUrlS").Value;
            apiUrlB = configuration.GetSection("AppSettings:ApiUrlB").Value;
        }

        public async Task<List<AnimalDomain>> GetAnimalsRemote()
        {
            var dataArray = await httpClient.GetFromJsonAsync<AnimalDomain[]>(apiUrl);
            if (dataArray != null)
            {
                return new List<AnimalDomain>(dataArray);
            }
            else
            {
                return new List<AnimalDomain>();
            }

        }

        public async Task<List<String>> GetAnimalSexes()
        {
            var dataArray = await httpClient.GetFromJsonAsync<String[]>(apiUrlS);
            if (dataArray != null)
            {
                return new List<String>(dataArray);
            }
            else
            {
                return new List<String>();
            }
        }

        public async Task<List<String>> GetAnimalBreeds()
        {
            var dataArray = await httpClient.GetFromJsonAsync<String[]>(apiUrlB);
            if (dataArray != null)
            {
                return new List<String>(dataArray);
            }
            else
            {
                return new List<String>();
            }
        }

        public async Task UpdateAnimalsRemote(IEnumerable<AnimalDomain> animalDomains)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(animalDomains), Encoding.UTF8, "application/json");
            await httpClient.PostAsync(apiUrl + "/animals", jsonContent);
        }

    }
}
