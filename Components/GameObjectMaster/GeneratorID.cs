using FLKEngine.Librarys.LuaImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components.GameObjectMaster
{
    [System.Serializable]
    public class GeneratorID
    {
#if DEV
        public string GenerateRandomID(GameObject obj)
        {
            string GenerateIDScript = "";

            List<string> ABC = new List<string>()
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
                "W", "X", "Y", "Z"
            };

            List<int> NMS = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9
            };

            Random RandomLetter = new Random();
            Random RandomNMB = new Random();

            for (int a = 0; a < 20; a++)
            {
                string GenerateLetter = ABC[RandomLetter.Next(0, ABC.Count - 1)];
                string GenerateINT = NMS[RandomNMB.Next(0, NMS.Count - 1)].ToString();

                GenerateIDScript = GenerateIDScript + GenerateLetter + GenerateINT;
            }

            foreach (GameObject d in EngineWindows.instance.CurrentOpenScene.ObjectsInScene)
            {
                if (d.ObjectID != GenerateIDScript)
                {
                    obj.ObjectID = GenerateIDScript;
                }
                else
                {
                    GenerateRandomID (obj);
                }
            }
return GenerateIDScript;


        }
#endif
    }

    [System.Serializable]
    public class GeneratorIDScript
    {
#if DEV
        public string GenerateRandomID ()
        {
            string GenerateIDScript = "";

            List<string> ABC = new List<string>()
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
                "W", "X", "Y", "Z"
            };

            List<int> NMS = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9
            };

            Random RandomLetter = new Random();
            Random RandomNMB = new Random();

            for (int a = 0; a < 20; a++)
            {
                string GenerateLetter =  ABC[RandomLetter.Next(0, ABC.Count - 1)];
                string GenerateINT    =  NMS[RandomNMB.Next(0, NMS.Count - 1)].ToString();

                GenerateIDScript = GenerateIDScript + GenerateLetter + GenerateINT;
            }


            if (!File.Exists(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + GenerateIDScript + ".lua"))
            {
                return GenerateIDScript;
            }
            else
            {
                GenerateRandomID();
            }
            

            return "NullValue";
        }
#endif
    }
}
