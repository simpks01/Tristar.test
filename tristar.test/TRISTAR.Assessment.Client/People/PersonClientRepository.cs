using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;


namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// This is the client-side repository for people. 
    /// It makes http requests to the person API endpoints.
    /// </summary>
    public class PersonClientRepository : IPersonRepository
    {
        private readonly HttpClient _httpClient;

        // create global variable for localhost
        private const string PeopleUrlBase = "http://localhost:3000/api/people";

        public PersonClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Invokes the http api to create a person on the server.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Person> CreatePerson(EditPersonParameters parameters)
        {
            
             var message = await _httpClient
                .PostAsync(PeopleUrlBase, new StringContent(JsonSerializer.Serialize(parameters), Encoding.UTF8, "application/json"));
            return await JsonSerializer.DeserializeAsync<Person>(await message.Content.ReadAsStreamAsync());
        }

        /// <summary>
        /// Invokes the http api to delete a person from the server.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeletePerson(Guid id)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to modify a person on the server.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<Person> EditPerson(Guid id, EditPersonParameters parameters)
        {
            // Implementation goes here!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes the http api to return one or more people that match the query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetPeople(QueryPersonParameters parameters)
        {
            var stream = _httpClient.GetStreamAsync(ParseQueryPersonParameters(parameters));
            return await JsonSerializer.DeserializeAsync<List<Person>>(await stream);

        }

        /// <summary>
        /// Invokes the http api to return one person who matches the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetPerson(Guid id)
        {
            var stream = await _httpClient.GetStreamAsync($"{PeopleUrlBase}/{id}");
            return await JsonSerializer.DeserializeAsync<Person>(stream);
        }

        // created a method for GetPeople to make cleaner
        private string ParseQueryPersonParameters(QueryPersonParameters parameters)
        {
            string url = PeopleUrlBase;
            if (parameters != null)
            {
                parameters.FirstName?.ForEach(firstName =>
                {
                    url = QueryHelpers.AddQueryString(url, "firstName", firstName);
                });

                parameters.LastName?.ForEach(lastName =>
                {
                    url = QueryHelpers.AddQueryString(url, "lastName", lastName);
                });

                parameters.Id?.ForEach(id =>
                {
                    url = QueryHelpers.AddQueryString(url, "id", id.ToString());
                });
            }
            return url;
        }
    }
}