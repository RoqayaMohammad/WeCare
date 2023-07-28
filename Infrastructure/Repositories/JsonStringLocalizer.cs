using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    public class JsonStringLocalizer : IStringLocalizer
    {

        private readonly JsonSerializer _serializer = new JsonSerializer();
        public LocalizedString this[string name]
        {
            get
            {
                var value = getString(name);
                return new LocalizedString(name ,value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                    : actualValue;
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new StreamReader(stream);
            using JsonTextReader reader = new JsonTextReader(streamReader);
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                {
                    continue;

                }
                var key = reader.Value as string;
                reader.Read();
                var value = _serializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);
            }

        }
        private string getString(string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath) ;
            if (File.Exists(fullFilePath))
            {
                var result = getValueFromJson(key,fullFilePath);
                return result; 
            }
            return string.Empty;
        }

        private string getValueFromJson(string propName , string filePath)
        {
            if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            using FileStream stream = new FileStream(filePath,FileMode.Open,FileAccess.Read,FileShare.Read);
            using StreamReader streamReader = new StreamReader(stream);
            using JsonTextReader reader = new JsonTextReader(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }
            return string.Empty;
        }
    }
}
