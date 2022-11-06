using MoonSharp.Interpreter;
using FLKEngine.Lua.Librarys;
using FLKEngine.Components;
using FLKEngine.Commons;

namespace FLKEngine.Librarys.LuaImplementation
{
    [System.Serializable]
    public class LuaCompiller
    {
        public string ScriptName;
        public string ObjectID;
        public GameObject owner;

        public string LuaCodeCompiller;

        private static void Log (string hello)
        {
            Console.WriteLine ("Funcion de lua invokada con exito");
        }


        public void StartScript()
        {     

#if DEV
            string ByteRead = File.ReadAllText(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + ObjectID + ".lua");
#else
            string ByteRead = File.ReadAllText(EngineWindows.instance.CurrentProyectUrl + "/FLKData/ScriptsData/" + ObjectID + ".FLKScripts");
#endif

            BinaryFormatterEasy eas = new BinaryFormatterEasy();

#if !DEV
            string Scripting = eas.DeserializeObject (ByteRead).ToString();
#else
            string Scripting = ByteRead;
#endif

            LuaCL lcl = new LuaCL();
            Script script = new Script();

            UserData.RegisterType<LuaCL.ConsoleLua>();
            script.Globals["Console"] = new LuaCL.ConsoleLua();

            UserData.RegisterType<LuaCL.Object>();
            script.Globals["New"] = new LuaCL.Object();

            UserData.RegisterType<PlayerData>();
            script.Globals["Data"] = new PlayerData();


            UserData.RegisterType<LuaCL>();
            lcl.Owner = owner;
            script.Globals["Self"] = lcl;
            script.DoString(Scripting);

            DynValue Startfunction = script.Globals.Get("OnStart");

            if (Startfunction != null)
            {
                script.Call(Startfunction);
            }   
        }

        public void UpdateScript()
        {
#if DEV
            string ByteRead = File.ReadAllText(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + ObjectID + ".lua");
#else
            string ByteRead = File.ReadAllText(EngineWindows.instance.CurrentProyectUrl + "/FLKData/ScriptsData/" + ObjectID + ".FLKScripts");
#endif

            BinaryFormatterEasy eas = new BinaryFormatterEasy();


            string Scripting = eas.DeserializeObject(ByteRead).ToString();

            LuaCL lcl = new LuaCL();
            Script script = new Script();

            UserData.RegisterType<LuaCL.ConsoleLua>();
            script.Globals["Console"] = new LuaCL.ConsoleLua();

            UserData.RegisterType<LuaCL.Object>();
            script.Globals["New"] = new LuaCL.Object();

            UserData.RegisterType<PlayerData>();
            script.Globals["Data"] = new PlayerData();

            UserData.RegisterType<LuaCL>();
            lcl.Owner = owner;
            script.Globals["Self"] = lcl;
            script.DoString(Scripting);


            DynValue OnUpdateFunction = script.Globals.Get("OnUpdate");

            if (OnUpdateFunction != null)
            {
                DynValue res = script.Call(OnUpdateFunction);
            }
        }
    }
}