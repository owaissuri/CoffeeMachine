using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CoffeeMachine.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoffeeMachine.Repository
{
    public class FileRepo
    {
        public dynamic Read(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                try
                {
                    string json = file.ReadToEnd();

                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(json, serializerSettings);
                    return jsonObject;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem reading file", ex.Message);

                    return null;
                }
            }
        }
    }
}
