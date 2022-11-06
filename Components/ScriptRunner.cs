using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using FLKEngine.Librarys.LuaImplementation;
using MoonSharp.Interpreter;

namespace FLKEngine.Components
{

    [System.Serializable]
    public class ScriptRunner : FLKBehaviour
    {

        public string Scripting = File.ReadAllText (EngineWindows.instance.CurrentProyectUrl+ "/Proyects/Test/Scripts/ScriptTest.lua");


        public void Start()
        {
            Script srp = new Script();
            srp.DoString(Scripting);
            DynValue res = srp.Call(srp.Globals["start"]);
        }

        public void Update()
        {
            Script srp = new Script();
            srp.DoString(Scripting);
            DynValue res = srp.Call(srp.Globals["update"]);
        }
    }
}