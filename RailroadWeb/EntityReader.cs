using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RailroadWeb
{
    public class EntityReader
    {
        public static async Task<T> GetEntity<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new ArgumentException($"File for specified {path} is not exist or unavailiable");
                }

                using (var stream = new StreamReader(path, Encoding.Default))
                {
                    var text = await stream.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(text);
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
