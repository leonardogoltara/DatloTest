using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace DatloTest.Infrastructure.MongoDBService
{
    public static class MongoBsonDocumentConverter
    {
        /// <summary>
        /// deserializes this bson doc to a .net dynamic object
        /// </summary>
        /// <param name="bson">bson doc to convert to dynamic</param>
        public static dynamic ToDynamic(this BsonDocument bson, bool removeId)
        {
            /// Trata o json
            var json = bson.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = JsonOutputMode.Strict });

            var o = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            /// Remove a prop Id do banco
            if (removeId)
            {
                o.Property("_id").Remove();
            }

            dynamic e = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(o.ToString());

            /// Trata para o Id ser um string simples.
            if (!removeId)
            {
                BsonValue id;
                if (bson.TryGetValue("_id", out id))
                {
                    e._id = id.ToString();
                }
            }

            return e;
        }
    }
}
