using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemElementJsonParser
    {
        public PubchemElementJsonParser(string json)
        {
            ArgumentNullException.ThrowIfNull(json, nameof(json));

            JObject jObject = JObject.Parse(json);
            _jObject = jObject;
        }

        private readonly JObject _jObject;

        public PubchemElementInfo GetElement()
        {
            PubchemReference[] pubchemReferences = GetReferences();
            PubchemReference? nistReference =
                pubchemReferences.FirstOrDefault(i => i.SourceName == "NIST Physical Measurement Laboratory") ??
                pubchemReferences.FirstOrDefault(i => i.SourceName == "IUPAC Commission on Isotopic Abundances and Atomic Weights (CIAAW)") ?? 
                pubchemReferences.FirstOrDefault();

            PubchemIsotope[] isotopes = GetIsotopes(nistReference is null ? 0 : nistReference.ReferenceNumber);
            Array.Sort(isotopes);

            return new PubchemElementInfo()
            {
                AtomicNumber = GetRecordNumber(),
                Name = GetRecordTitle(),
                Isotopes = isotopes.AsReadOnly()
            };
        }

        public JObject? GetRecordJObject()
        {
            return _jObject.Value<JObject>("Record");
        }

        public int GetRecordNumber()
        {
            JObject? jObject = GetRecordJObject();
            return jObject?.Value<int>("RecordNumber") ?? 0;
        }

        public string GetRecordTitle()
        {
            JObject? jObject = GetRecordJObject();
            return jObject?.Value<string>("RecordTitle") ?? string.Empty;
        }

        public PubchemReference[] GetReferences()
        {
            JObject? jObject = GetRecordJObject();
            return jObject?.Value<JArray>("Reference")?.ToObject<PubchemReference[]>() ?? [];
        }

        public Dictionary<string, JObject> GetChapters()
        {
            JObject? jObject = GetRecordJObject();
            if (jObject is null)
                return [];

            return GetChapters(jObject);
        }

        public Dictionary<string, JObject> GetSubchapters(string chapterName)
        {
            ArgumentException.ThrowIfNullOrEmpty(chapterName, nameof(chapterName));

            Dictionary<string, JObject> chapters = GetChapters();
            if (!chapters.TryGetValue(chapterName, out var jObject))
                return [];

            return GetChapters(jObject);
        }

        public JObject? GetSubchapters(params string[] chapterNames)
        {
            ArgumentNullException.ThrowIfNull(chapterNames, nameof(chapterNames));

            JObject? jObject = GetRecordJObject();
            Dictionary<string, JObject> chapters;
            foreach (var chapterName in chapterNames)
            {
                if (jObject is null)
                    continue;

                chapters = GetChapters(jObject);
                if (!chapters.TryGetValue(chapterName, out jObject))
                    return null;
            }

            return jObject;
        }

        public PubchemIsotope[] GetIsotopes(int referenceNumber)
        {
            JObject? isotopeJObject = GetSubchapters("Isotopes", "Isotope Mass and Abundance");
            if (isotopeJObject is null)
                return [];

            JArray? informationJArray = isotopeJObject.Value<JArray>("Information");
            if (informationJArray is null)
                return [];

            List<string> isotopeColumn = [];
            List<string> atomicMassColumn = [];
            List<string> abundanceColumn = [];
            foreach (JToken jToken in informationJArray)
            {
                if (jToken is not JObject jItem)
                    continue;

                if (jItem.Value<JValue>("ReferenceNumber")?.Value is not long i || i != referenceNumber)
                    continue;

                JArray? stringJArray = jItem.Value<JObject>("Value")?.Value<JArray>("StringWithMarkup");
                if (stringJArray is null)
                    continue;

                List<string> list;
                string? name = jItem.Value<string>("Name");
                switch (name)
                {
                    case "Isotope":
                        list = isotopeColumn;
                        break;
                    case "Atomic Mass (uncertainty) [u]":
                        list = atomicMassColumn;
                        break;
                    case "Abundance (uncertainty)":
                        list = abundanceColumn;
                        break;
                    default:
                        continue;
                }

                foreach (JToken jToken2 in stringJArray)
                {
                    string text = jToken2.Value<string>("String") ?? string.Empty;
                    list.Add(text);
                }
            }

            if (isotopeColumn.Count == 0 || isotopeColumn.Count != abundanceColumn.Count || isotopeColumn.Count != abundanceColumn.Count)
                return [];

            List<PubchemIsotope> result = [];
            for (int i = 0; i < isotopeColumn.Count; i++)
            {
                string isotopeString = isotopeColumn[i];
                string atomicMassString = atomicMassColumn[i];
                string abundanceString = abundanceColumn[i];
                int index;

                index = GetNonDigitIndex(isotopeString);
                string symbol = isotopeString[index..];
                int number = int.Parse(isotopeString[..index]);

                index = atomicMassString.IndexOf('(');
                atomicMassString = index == -1 ? atomicMassString : atomicMassString[..index];

                index = abundanceString.IndexOf('(');
                abundanceString = index == -1 ? abundanceString : abundanceString[..index];

                double atomicMass = string.IsNullOrEmpty(atomicMassString) ? 0 : double.Parse(atomicMassString);
                double abundance = string.IsNullOrEmpty(abundanceString) ? 0 : double.Parse(abundanceString);

                PubchemIsotope pubchemIsotope = new()
                {
                    Symbol = symbol,
                    Number = number,
                    AtomicMass = atomicMass,
                    Abundance = abundance
                };
                result.Add(pubchemIsotope);
            }

            return result.ToArray();
        }

        public static Dictionary<string, JObject> GetChapters(JObject jObject)
        {
            ArgumentNullException.ThrowIfNull(jObject, nameof(jObject));

            Dictionary<string, JObject> result = [];

            JArray? jArray = jObject.Value<JArray>("Section");
            if (jArray is null)
                return result;

            foreach (JToken jToken in jArray)
            {
                if (jToken is not JObject jItem)
                    continue;

                string? heading = jItem.Value<string>("TOCHeading");
                if (string.IsNullOrEmpty(heading))
                    continue;

                result.Add(heading, jItem);
            }

            return result;
        }

        private static int GetNonDigitIndex(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]))
                    return i;
            }

            return -1;
        }
    }
}
