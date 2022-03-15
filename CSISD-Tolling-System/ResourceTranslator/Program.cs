using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceTranslator
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public TextResult SourceText { get; set; }
        public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }
        public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
    }

    class Program
    {
        static string baseDirectory;
        static string template;

        static string subscriptionEndpoint = "https://api.cognitive.microsofttranslator.com/";
        static string subscriptionKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        static async Task<Dictionary<string, Dictionary<string, string>>> TranslateAll(HashSet<string> keys)
        {
            List<object> json = new List<object>();

            foreach (string key in keys)
            {
                json.Add(new { Text = key });
            }

            Dictionary<string, Dictionary<string, string>> translationMap = new Dictionary<string, Dictionary<string, string>>();

            if (json.Count > 0)
            {
                string route = "/translate?api-version=3.0&from=en&to=fr&to=ar&to=nb&to=sv&to=da&to=fi";
                var requestBody = JsonConvert.SerializeObject(json);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(subscriptionEndpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", "centralus");

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    string result = await response.Content.ReadAsStringAsync();

                    TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);

                    for (int i = 0; i < deserializedOutput.Length; ++i)
                    {
                        TranslationResult r = deserializedOutput[i];
                        string input = (json[i] as dynamic).Text;

                        if (!translationMap.ContainsKey(input))
                        {
                            translationMap[input] = new Dictionary<string, string>();
                        }

                        foreach (Translation trans in r.Translations)
                        {
                            translationMap[input].Add(trans.To, trans.Text);
                        }
                    }
                }
            }

            return translationMap;
        }

        static void CreateResourceFile(Dictionary<string, Dictionary<string, string>> translationMap, HashSet<string> keys, string lang, string viewRelativePath)
        {
            string resourceFile = baseDirectory + "\\Resources" + viewRelativePath;
            resourceFile = resourceFile.Replace("cshtml", lang + ".resx");

            if (!File.Exists(resourceFile))
            {
                FileInfo fileInfo = new FileInfo(resourceFile);
                fileInfo.Directory.Create();

                File.WriteAllText(resourceFile, template);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(resourceFile);

            XmlElement element = doc.DocumentElement;

            XmlNodeList nodes = element.SelectNodes("data");

            foreach (XmlNode node in nodes)
                element.RemoveChild(node);

            foreach (string key in keys)
            {
                XmlAttribute nameAttribute = doc.CreateAttribute("name");
                nameAttribute.Value = key;

                XmlAttribute space = doc.CreateAttribute("xml:space");
                space.Value = "preserve";

                XmlElement node = doc.CreateElement("data");
                node.Attributes.Append(nameAttribute);
                node.Attributes.Append(space);

                XmlElement value = doc.CreateElement("value");

                value.InnerText = translationMap[key][lang];

                node.AppendChild(value);
                element.AppendChild(node);

            }

            doc.Save(resourceFile);
        }

        static void Main(string[] args)
        {
            baseDirectory = "C:\\Projects\\CSISD-Toll-System\\CSISD-Tolling-System\\CSISD_Toll_Operator_Assignment";// args[0];
            string viewsDirectory = baseDirectory + "\\Views";
            template = File.ReadAllText(baseDirectory + "\\Resources\\TemplateResource.resx");

            string[] files = Directory.GetFiles(viewsDirectory, "*.cshtml", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                HashSet<string> keys = new HashSet<string>();

                string contents = File.ReadAllText(file);
                MatchCollection matches = Regex.Matches(contents, "@Localizer\\[\"(.*)\".*\\]");

                foreach (Match match in matches)
                {
                    keys.Add(match.Groups[1].Value);
                }

                string fileRelativePath = file.Replace(baseDirectory, "");

                var translationMap = TranslateAll(keys);

                CreateResourceFile(translationMap.Result, keys, "fr", fileRelativePath);
                CreateResourceFile(translationMap.Result, keys, "ar", fileRelativePath);
                CreateResourceFile(translationMap.Result, keys, "nb", fileRelativePath);
                CreateResourceFile(translationMap.Result, keys, "sv", fileRelativePath);
                CreateResourceFile(translationMap.Result, keys, "da", fileRelativePath);
                CreateResourceFile(translationMap.Result, keys, "fi", fileRelativePath);
            }
        }
    }
}
