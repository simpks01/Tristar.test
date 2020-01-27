using System;
using System.Text.Json.Serialization;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// A person instance.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique identifier for this person.
        /// </summary>
        
        //change property name to camel case (JSON) from Pascal (C#)
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The given name for this person.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The family name for this person.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }
}