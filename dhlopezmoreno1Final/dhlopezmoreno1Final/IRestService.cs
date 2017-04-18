using dhlopezmoreno1Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dhlopezmoreno1Final
{
    interface IRestService
    {
        Task<List<Painting>> RefreshDataAsync();

        Task SaveTodoItemAsync(Painting painting, bool isNewItem);

        Task DeleteTodoItemAsync(string id);
    }
}
