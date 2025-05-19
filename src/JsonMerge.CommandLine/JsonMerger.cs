using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonMerge.CommandLine
{
public static class JsonMerger
    {
        /// <summary>
        /// Merges multiple JSONs into one.
        /// </summary>
        /// <param name="sources">JSON texts to merge.</param>
        /// <returns>Merged JSON text.</returns>
        public static string Merge(IEnumerable<string> sources)
        {
            var targetObject = new JObject();

            var mergeSettings = new JsonMergeSettings
            {
                MergeNullValueHandling = MergeNullValueHandling.Merge,
                MergeArrayHandling = MergeArrayHandling.Replace
            };

            foreach (var source in sources)
            {
                var sourceObject = JObject.Parse(source);
                targetObject.Merge(sourceObject, mergeSettings);
            }

            return targetObject.ToString(Formatting.Indented);
        }
    }
}
