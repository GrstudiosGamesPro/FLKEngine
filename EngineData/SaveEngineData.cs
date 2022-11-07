using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using FLKEngine.Components;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using MoonSharp.Interpreter;
using System.Security.Cryptography.X509Certificates;

namespace FLKEngine.EngineData
{
    public class SaveEngineData
    {
        
        public void Save(bool SaveAndExit)
        {
            if (!SaveAndExit)
            {
                for (int i = 0; i < EngineWindows.instance.CurrentOpenScene.ObjectsInScene.Count; i++)
                {
                    JArray ObjectID = new JArray();
                    ObjectID.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID);

                    Console.WriteLine("ID saved: " + EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID);

                    JArray NameArray = new JArray();
                    NameArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Name);

                    JArray PositionArray = new JArray();
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.X);
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.Y);
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.Z);

                    JArray RotationArray = new JArray();
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.X);
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.Y);
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.Z);

                    JArray ScaleArray = new JArray();
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.X);
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.Y);
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.Z);

                    JArray PathModelURL = new JArray();
                    PathModelURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].CurrentModelPath);

                    JArray PathDiffuseTextureURL = new JArray();
                    PathDiffuseTextureURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i]._diffuseMapURL);

                    JArray PathTextureSpecularMapURL = new JArray();
                    PathTextureSpecularMapURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i]._specularMapURL);

                    JArray PathScripts = new JArray();
                    JArray PathScriptsName = new JArray();

                    JArray UsePhysics = new JArray();
                    UsePhysics.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].UsePhysics);

                    for (int c = 0; c < EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua.Count; c++)
                    {
                        PathScripts.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua[c].ObjectID);
                    }

                    for (int c = 0; c < EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua.Count; c++)
                    {
                        PathScriptsName.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua[c].ScriptName);
                    }

                    JObject obj = new JObject();
                    obj["ObjectName"] = NameArray;
                    obj["Scale"] = ScaleArray;
                    obj["ObjectID"] = ObjectID;
                    obj["Position"] = PositionArray;
                    obj["Rotation"] = RotationArray;
                    obj["UrlModelPath"] = PathModelURL;
                    obj["DiffuseURLPath"] = PathDiffuseTextureURL;
                    obj["SpecularURLPath"] = PathTextureSpecularMapURL;
                    obj["Scripts"] = PathScripts;
                    obj["ScriptsName"] = PathScriptsName;
                    obj["UsePhysics"] = UsePhysics;

                    string json = obj.ToString();

                    File.WriteAllText(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/JsonData/" + EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID + ".FLKData", json);
                }
                EngineWindows.instance.ConsoleData.Log("Data saved successfully");
            }
            else
            {
                for (int i = 0; i < EngineWindows.instance.CurrentOpenScene.ObjectsInScene.Count; i++)
                {
                    JArray ObjectID = new JArray();
                    ObjectID.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID);

                    Console.WriteLine("ID saved: " + EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID);

                    JArray NameArray = new JArray();
                    NameArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Name);

                    JArray ScaleArray = new JArray();
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.X);
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.Y);
                    ScaleArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Scale.Z);

                    JArray PositionArray = new JArray();
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.X);
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.Y);
                    PositionArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Position.Z);

                    JArray RotationArray = new JArray();
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.X);
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.Y);
                    RotationArray.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].Rotation.Z);

                    JArray PathModelURL = new JArray();
                    PathModelURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].CurrentModelPath);

                    JArray PathDiffuseTextureURL = new JArray();
                    PathDiffuseTextureURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i]._diffuseMapURL);

                    JArray PathTextureSpecularMapURL = new JArray();
                    PathTextureSpecularMapURL.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i]._specularMapURL);

                    JArray PathScripts = new JArray();
                    JArray PathScriptsName = new JArray();

                    JArray UsePhysics = new JArray();
                    UsePhysics.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].UsePhysics);

                    for (int c = 0; c < EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua.Count; c++)
                    {
                        PathScripts.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua[c].ObjectID);
                    }

                    for (int c = 0; c < EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua.Count; c++)
                    {
                        PathScriptsName.Add(EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].lua[c].ScriptName);
                    }

                    JObject obj = new JObject();
                    obj["ObjectName"] = NameArray;
                    obj["Scale"] = ScaleArray;
                    obj["ObjectID"] = ObjectID;
                    obj["Position"] = PositionArray;
                    obj["Rotation"] = RotationArray;
                    obj["UrlModelPath"] = PathModelURL;
                    obj["DiffuseURLPath"] = PathDiffuseTextureURL;
                    obj["SpecularURLPath"] = PathTextureSpecularMapURL;
                    obj["Scripts"] = PathScripts;
                    obj["ScriptsName"] = PathScriptsName;
                    obj["UsePhysics"] = UsePhysics;

                    string json = obj.ToString();

                    File.WriteAllText(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/JsonData/" + EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i].ObjectID + ".FLKData", json);
                }

                EngineWindows.instance.Close();
            }
        }

    }
}
