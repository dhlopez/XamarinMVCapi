using dhlopezmoreno1Final.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dhlopezmoreno1Final
{
    class Utility
    {
        //public static Uri DBUri = new Uri("http://p1698webapi2.azurewebsites.net//");
        public static Uri DBUri = new Uri("http://localhost:50132//");


        //Added as per:
        //http://www.codeproject.com/Articles/825274/ASP-NET-Web-Api-Unwrapping-HTTP-Error-Results-and#Unwrapping-and-Handling-Errors-and-Exceptions-in-Web-Api

        public static ApiException CreateApiException(HttpResponseMessage response)
        {
            var httpErrorObject = response.Content.ReadAsStringAsync().Result;

            // Create an anonymous object to use as the template for deserialization:
            var anonymousErrorObject =
                new { message = "", ModelState = new Dictionary<string, string[]>() };

            // Deserialize:
            var deserializedErrorObject =
                JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            // Now wrap into an exception which best fullfills the needs of your application:
            var ex = new ApiException(response);

            // Sometimes, there may be Model Errors:
            if (deserializedErrorObject.ModelState != null)
            {
                var errors =
                    deserializedErrorObject.ModelState
                                            .Select(kvp => string.Join(". ", kvp.Value));
                for (int i = 0; i < errors.Count(); i++)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(i, errors.ElementAt(i));
                }
            }
            // Othertimes, there may not be Model Errors:
            else
            {
                var error =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(httpErrorObject);
                foreach (var kvp in error)
                {
                    // Wrap the errors up into the base Exception.Data Dictionary:
                    ex.Data.Add(kvp.Key, kvp.Value);
                }
            }
            return ex;
        }
    }
}
