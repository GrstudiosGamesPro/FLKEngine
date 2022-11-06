using FLKEngine.Components;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace FLKEngine.Lua.Librarys
{
    [System.Serializable]
    public class LuaCL
    {
        public GameObject Owner;

        public class ConsoleLua
        {
            public void Log(string message)
            {
                Console.WriteLine(message);
            }
        }

        public class Object
        {
            public void Instance(string TypeToInstance)
            {
                switch (TypeToInstance)
                {
                    case "Object":
                        {
                            GameObject gm = new GameObject();
                            break;
                        }
                }
            }
        }


        public string Name
        {
            get
            {
                return Owner.Name;
            }

            set
            {
                Owner.Name = value;
            }
        }

        public void NewPos(float x, float y, float z)
        {
            Owner.Position = new Vector3(x, y, z);
        }

        public Vector3 GetPos
        {
            get
            {
                return Owner.Position;
            }
        }

        public string str (Vector3 val)
        {
            return val.ToString();
        }
    }
}
