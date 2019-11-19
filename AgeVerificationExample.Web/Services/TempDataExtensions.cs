using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace AgeVerificationExample.Web.Services
{
    public static class TempDataExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            Contract.Requires(tempData != null);
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            Contract.Requires(tempData != null);
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
