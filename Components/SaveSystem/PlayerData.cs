using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    [System.Serializable]
    public class PlayerData
    {

        public void SaveString (string NameVar, string Value = "")
        {

            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (!Directory.Exists(CurrentPathProyect))
            {
                Directory.CreateDirectory (CurrentPathProyect);

                JObject JsonValue = new JObject();
                JsonValue[NameVar] = Value;

                string json = JsonValue.ToString();

                File.WriteAllText (CurrentPathProyect + NameVar + ".bv", json);
            }
            else
            {
                JObject JsonValue = new JObject();
                JsonValue[NameVar] = Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
        }

        public string GetSaveString(string NameVar)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (File.Exists (CurrentPathProyect + NameVar + ".bv"))
            {
                var JsonRead = File.ReadAllText(CurrentPathProyect + NameVar + ".bv");

                var dataJson = JsonObject.Parse(JsonRead);

                if (!string.IsNullOrEmpty ((string)dataJson[NameVar]) && !string.IsNullOrWhiteSpace((string)dataJson[NameVar]))
                {
                    return (string)dataJson[NameVar];
                }
                else
                {
                    return "The Save Value Is Null";
                }
            }
            
            return "The Save Value Is Null";
        }

        public void SaveInt(string NameVar, int Value = 0)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (!Directory.Exists(CurrentPathProyect))
            {
                Directory.CreateDirectory(CurrentPathProyect);

                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (int)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
            else
            {
                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (int)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
        }

        public int GetSaveInt(string NameVar)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (File.Exists(CurrentPathProyect + NameVar + ".bv"))
            {
                var JsonRead = File.ReadAllText(CurrentPathProyect + NameVar + ".bv");

                var dataJson = JsonObject.Parse(JsonRead);

                if (!string.IsNullOrEmpty((string)dataJson[NameVar]) && !string.IsNullOrWhiteSpace((string)dataJson[NameVar]))
                {
                    return (int)dataJson[NameVar];
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        public void SaveFloat(string NameVar, float Value = 0)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (!Directory.Exists(CurrentPathProyect))
            {
                Directory.CreateDirectory(CurrentPathProyect);

                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (float)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
            else
            {
                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (float)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
        }

        public float GetSaveFloat(string NameVar)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (File.Exists(CurrentPathProyect + NameVar + ".bv"))
            {
                var JsonRead = File.ReadAllText(CurrentPathProyect + NameVar + ".bv");

                var dataJson = JsonObject.Parse(JsonRead);

                if (!string.IsNullOrEmpty((string)dataJson[NameVar]) && !string.IsNullOrWhiteSpace((string)dataJson[NameVar]))
                {
                    return (float)dataJson[NameVar];
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        public void SaveBool(string NameVar, bool Value = false)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (!Directory.Exists(CurrentPathProyect))
            {
                Directory.CreateDirectory(CurrentPathProyect);

                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (bool)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
            else
            {
                JObject JsonValue = new JObject();
                JsonValue[NameVar] = (bool)Value;

                string json = JsonValue.ToString();

                File.WriteAllText(CurrentPathProyect + NameVar + ".bv", json);
            }
        }

        public bool GetSaveBool(string NameVar)
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (File.Exists(CurrentPathProyect + NameVar + ".bv"))
            {
                var JsonRead = File.ReadAllText(CurrentPathProyect + NameVar + ".bv");

                var dataJson = JsonObject.Parse(JsonRead);

                if (!string.IsNullOrEmpty((string)dataJson[NameVar]) && !string.IsNullOrWhiteSpace((string)dataJson[NameVar]))
                {
                    return (bool)dataJson[NameVar];
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void CleanAllDataSave()
        {
            string CurrentPathProyect = Directory.GetCurrentDirectory() + "/PrefsData/";

            if (Directory.Exists (CurrentPathProyect)) {
                string[] DR = Directory.GetFiles(CurrentPathProyect);

                if (DR.Length > 0)
                {
                    foreach (var d in DR)
                    {
                        File.Delete(d);
                    }
                }
            }
        }
    }
}
