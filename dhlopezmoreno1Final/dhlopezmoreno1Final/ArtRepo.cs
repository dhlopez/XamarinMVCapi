using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dhlopezmoreno1Final.Models;
using dhlopezmoreno1Final.Helpers;

namespace dhlopezmoreno1Final
{
    class ArtRepo
    {
        HttpClient client = new HttpClient();

        public ArtRepo()
        {
            client.BaseAddress = Utility.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Painting>> GetPaintings()
        {
            var response = await client.GetAsync("api/paintings");
            if (response.IsSuccessStatusCode)
            {
                List<Painting> paintings = await response.Content.ReadAsAsync<List<Painting>>();
                return paintings;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Painting> GetPainting(int ID)
        {
            var response = await client.GetAsync($"api/paintings/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Painting paintings = await response.Content.ReadAsAsync<Painting>();
                return paintings;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }
        public async Task<List<Painting>> GetPaintingsByMovement(int MovID)
        {
            var response = await client.GetAsync($"api/paintings?MovementID={MovID}");
            if (response.IsSuccessStatusCode)
            {
                List<Painting> paintings = await response.Content.ReadAsAsync<List<Painting>>();
                return paintings;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddPainting(Painting paintToAdd)
        {
            var response = await client.PostAsJsonAsync("api/paintings", paintToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdatePainting(Painting paintToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/paintings/{paintToUpdate.ID}", paintToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeletePainting(Painting paintToDelete)
        {
            var response = await client.DeleteAsync($"api/paintings/{paintToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }

        }
    }
}
