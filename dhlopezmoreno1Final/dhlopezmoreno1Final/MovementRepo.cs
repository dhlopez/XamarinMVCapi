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
    public class MovementRepo 
    {
        HttpClient client = new HttpClient();
        public MovementRepo()
        {
            client.BaseAddress = Utility.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Movement>> GetMovements()
        {
            var response = await client.GetAsync("api/Movements");
            if (response.IsSuccessStatusCode)
            {
                List<Movement> Movements = await response.Content.ReadAsAsync<List<Movement>>();
                return Movements;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }
        
        public async Task<Movement> GetMovement(int ID)
        {
            var response = await client.GetAsync($"api/Movements/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Movement movement = await response.Content.ReadAsAsync<Movement>();
                return movement;
            }
            else
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddMovement(Movement movToAdd)
        {
            var response = await client.PostAsJsonAsync("api/Movements", movToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateMovement(Movement movToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/Movements/{movToUpdate.ID}", movToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteMovement(Movement movToDelete)
        {
            var response = await client.DeleteAsync($"api/Movements/{movToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Utility.CreateApiException(response);
                throw ex;
            }
        }
    }
}
