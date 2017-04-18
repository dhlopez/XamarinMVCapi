using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dhlopezmoreno1Final.Models;
using dhlopezmoreno1Final.Helpers;

namespace dhlopezmoreno1Final
{
    public class ArtistsRepo 
    {
        HttpClient client = new HttpClient();
        public ArtistsRepo()
        {
            client.BaseAddress = Utility.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Artists>> GetArtists()
        {
            var response = await client.GetAsync("api/Artists");
            if (response.IsSuccessStatusCode)
            {
                List<Artists> Artists = await response.Content.ReadAsAsync<List<Artists>>();
                return Artists;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }
        
        public async Task<Artists> GetArtist(int ID)
        {
            var response = await client.GetAsync($"api/Artists/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Artists artists = await response.Content.ReadAsAsync<Artists>();
                return artists;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddArtist(Artists artistToAdd)
        {
            var response = await client.PostAsJsonAsync("api/Artists", artistToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtist(Artists artistToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/Artists/{artistToUpdate.ID}", artistToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtist(Artists artistToDelete)
        {
            var response = await client.DeleteAsync($"api/Artists/{artistToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }
    }
}
