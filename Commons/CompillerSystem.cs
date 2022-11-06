using FLKEngine.Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System;
using OpenTK.Graphics.OpenGL4;
using FLKEngine.Commons;

namespace FLKEngine.Commons.Compiller
{
    public class CompillerSystem
    {
        public string CurrentProyectUrl
        {
            get
            {
                return EngineWindows.instance.CurrentProyectUrl;
            }
        }

        public ConsoleDebug ConsoleData
        {
            get
            {
                return EngineWindows.instance.ConsoleData;
            }
        }

        public Scene CurrentOpenScene
        {
            get
            {
                return EngineWindows.instance.CurrentOpenScene;
            }
        }

        public void CompileToWindows()
        {
            #region GenerateFiles
            string GetCurrentShaderFolder = CurrentProyectUrl + "/Proyects/Test/Shaders/";
            string GetCurrentTextureFolder = CurrentProyectUrl + "/Proyects/Test/Textures/";
            string GetCurrentModelsFolder = CurrentProyectUrl + "/Proyects/Test/Models/";
            string GetCurrentJsonFolder = CurrentProyectUrl + "/Proyects/Test/JsonData/";
            string GetCurrentScriptsFolder = CurrentProyectUrl + "/Proyects/Test/Scripts/";

            string[] filesShaders = Directory.GetFiles(GetCurrentShaderFolder);
            string[] filesTextures = Directory.GetFiles(GetCurrentTextureFolder);
            string[] filesModels = Directory.GetFiles(GetCurrentModelsFolder);
            string[] filesScripts = Directory.GetFiles(GetCurrentScriptsFolder);

            if (!Directory.Exists(CurrentProyectUrl + "/Exports/"))
            {
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData");


                for (int i = 0; i < filesScripts.Length; i++)
                {
                    string fragExtensionNew = ".FLKScripts";

                    string newName = filesScripts[i];

                    if (Path.GetExtension(newName) == ".lua")
                    {
                        newName = Path.ChangeExtension(newName, fragExtensionNew);
                    }

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembling Scripts: " + Path.GetFileName(newName));

                        string getScriptsByte = File.ReadAllText(filesScripts[i]);
                        BinaryFormatterEasy fs = new BinaryFormatterEasy();                
                        
                        File.WriteAllText(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName), fs.SerializeObject (getScriptsByte));
                    }
                    else
                    {
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName));
                        string getScriptsByte = File.ReadAllText(filesScripts[i]);
                        BinaryFormatterEasy fs = new BinaryFormatterEasy();

                        File.WriteAllText(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName), fs.SerializeObject(getScriptsByte));
                    }
                }

                for (int i = 0; i < filesShaders.Length; i++)
                {
                    string fragExtensionNew = ".fragflkshader";
                    string vertExtensionNew = ".vertflkshader";

                    string newName = filesShaders[i];

                    if (Path.GetExtension(newName) == ".vert")
                    {
                        newName = Path.ChangeExtension(newName, vertExtensionNew);
                    }
                    else if (Path.GetExtension(newName) == ".frag")
                    {
                        newName = Path.ChangeExtension(newName, fragExtensionNew);
                    }

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembling Shaders: " + Path.GetFileName(newName));
                        File.Copy(filesShaders[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                    }
                    else
                    {


                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                        File.Copy(filesShaders[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                    }
                }

                for (int i = 0; i < filesTextures.Length; i++)
                {
                    string newName = Path.ChangeExtension(filesTextures[i], ".FLKTexturePackageData");

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembly Textures: " + Path.GetFileName(newName));
                        File.Copy(filesTextures[i], CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                    }
                    else
                    {
                        ConsoleData.Log("Replacing Textures: " + Path.GetFileName(newName));
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                        File.Copy(filesTextures[i], CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                    }
                }

                for (int i = 0; i < filesModels.Length; i++)
                {
                    string newName = Path.ChangeExtension(filesModels[i], ".FLKModelPackageData");

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembly Models: " + Path.GetFileName(newName));
                        File.Copy(filesModels[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                    }
                    else
                    {
                        ConsoleData.Log("Replacing Models: " + Path.GetFileName(newName));
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                        File.Copy(filesModels[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData");
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData");


                for (int i = 0; i < filesScripts.Length; i++)
                {
                    string fragExtensionNew = ".FLKScripts";

                    string newName = filesScripts[i];

                    if (Path.GetExtension(newName) == ".lua")
                    {
                        newName = Path.ChangeExtension(newName, fragExtensionNew);
                    }

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembling Scripts: " + Path.GetFileName(newName));

                        string getScriptsByte = File.ReadAllText(filesScripts[i]);
                        BinaryFormatterEasy fs = new BinaryFormatterEasy();

                        File.WriteAllText(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName), fs.SerializeObject(getScriptsByte));
                    }
                    else
                    {
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName));
                        string getScriptsByte = File.ReadAllText(filesScripts[i]);
                        BinaryFormatterEasy fs = new BinaryFormatterEasy();

                        File.WriteAllText(CurrentProyectUrl + "/Exports/Windows/FLKData/ScriptsData/" + Path.GetFileName(newName), fs.SerializeObject(getScriptsByte));
                    }
                }

                for (int i = 0; i < filesShaders.Length; i++)
                {
                    string fragExtensionNew = ".fragflkshader";
                    string vertExtensionNew = ".vertflkshader";

                    string newName = filesShaders[i];

                    if (Path.GetExtension(newName) == ".vert")
                    {
                        newName = Path.ChangeExtension(newName, vertExtensionNew);
                    }
                    else if (Path.GetExtension(newName) == ".frag")
                    {
                        newName = Path.ChangeExtension(newName, fragExtensionNew);
                    }

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembling Shaders: " + Path.GetFileName(newName));
                        File.Copy(filesShaders[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                    }
                    else
                    {


                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                        File.Copy(filesShaders[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ShaderData/" + Path.GetFileName(newName));
                    }
                }

                for (int i = 0; i < filesTextures.Length; i++)
                {
                    string newName = Path.ChangeExtension(filesTextures[i], ".FLKTexturePackageData");

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembly Textures: " + Path.GetFileName(newName));
                        File.Copy(filesTextures[i], CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                    }
                    else
                    {
                        ConsoleData.Log("Replacing Textures: " + Path.GetFileName(newName));
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                        File.Copy(filesTextures[i], CurrentProyectUrl + "/Exports/Windows/FLKData/TextureData/" + Path.GetFileName(newName));
                    }
                }

                for (int i = 0; i < filesModels.Length; i++)
                {
                    string newName = Path.ChangeExtension(filesModels[i], ".FLKModelPackageData");

                    if (!File.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName)))
                    {
                        ConsoleData.Log("Assembly Models: " + Path.GetFileName(newName));
                        File.Copy(filesModels[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                    }
                    else
                    {
                        ConsoleData.Log("Replacing Models: " + Path.GetFileName(newName));
                        File.Delete(CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                        File.Copy(filesModels[i], CurrentProyectUrl + "/Exports/Windows/FLKData/ModelsData/" + Path.GetFileName(newName));
                    }
                }
            }
            #endregion

            #region GenerateJson

            //WRITE OBJECT
            if (!Directory.Exists(CurrentProyectUrl + "/Exports/Windows/FLKData/ObjectsJson/"))
            {
                Directory.CreateDirectory(CurrentProyectUrl + "/Exports/Windows/FLKData/ObjectsJson/");
                WriteObjects();
            }
            else
            {
                WriteObjects();
            }

            void WriteObjects()
            {
                for (int J = 0; J < CurrentOpenScene.ObjectsInScene.Count; J++)
                {
                    GameObject GetOBJ = CurrentOpenScene.ObjectsInScene[J];

                    JArray ObjectsToWriteObjectID = new JArray();
                    JArray ObjectsToWritePosition = new JArray();
                    JArray ObjectsToWriteRotation = new JArray();
                    JArray ObjectsToWriteScale = new JArray();
                    JArray ObjectsToWriteName = new JArray();
                    JArray PathTextureSpecularMapURL = new JArray();
                    JArray PathDiffuseTextureURL = new JArray();
                    JArray PathModelURL = new JArray();
                    JArray PathUsePhysics = new JArray();
                    JArray PathUseScriptsID = new JArray();
                    JArray PathUseScriptsName = new JArray();

                    ObjectsToWriteName.Add(GetOBJ.Name);

                    ObjectsToWriteObjectID.Add(GetOBJ.ObjectID);

                    ObjectsToWritePosition.Add(GetOBJ.Position.X);
                    ObjectsToWritePosition.Add(GetOBJ.Position.Y);
                    ObjectsToWritePosition.Add(GetOBJ.Position.Z);

                    ObjectsToWriteRotation.Add(GetOBJ.Rotation.X);
                    ObjectsToWriteRotation.Add(GetOBJ.Rotation.Y);
                    ObjectsToWriteRotation.Add(GetOBJ.Rotation.Z);
                    PathUsePhysics.Add(GetOBJ.UsePhysics);

                    ObjectsToWriteScale.Add(GetOBJ.Scale);
                    PathModelURL.Add(Path.GetFileName(CurrentOpenScene.ObjectsInScene[J].CurrentModelPath));

                    PathDiffuseTextureURL.Add(Path.GetFileName(CurrentOpenScene.ObjectsInScene[J]._diffuseMapURL));

                    PathTextureSpecularMapURL.Add(Path.GetFileName(CurrentOpenScene.ObjectsInScene[J]._specularMapURL));

                    for (int c = 0; c < GetOBJ.lua.Count; c++)
                    {
                        PathUseScriptsID.Add(GetOBJ.lua[c].ObjectID);
                    }

                    for (int c = 0; c < GetOBJ.lua.Count; c++)
                    {
                        PathUseScriptsName.Add(GetOBJ.lua[c].ScriptName);
                    }


                    JObject GameExportData = new JObject();
                    GameExportData["GameExportData"] = "V1.0";
                    GameExportData["ObjectID"] = ObjectsToWriteObjectID;
                    GameExportData["Position"] = ObjectsToWritePosition;
                    GameExportData["Rotation"] = ObjectsToWriteRotation;
                    GameExportData["Scale"] = ObjectsToWriteScale;
                    GameExportData["NameObject"] = ObjectsToWriteName;
                    GameExportData["UrlModelPath"] = PathModelURL;
                    GameExportData["DiffuseURLPath"] = PathDiffuseTextureURL;
                    GameExportData["SpecularURLPath"] = PathTextureSpecularMapURL;
                    GameExportData["UsePhysics"] = PathUsePhysics;
                    GameExportData["Scripts"] = PathUseScriptsID;
                    GameExportData["ScriptsName"] = PathUseScriptsName;

                    string JsonGenerated = GameExportData.ToString();
                    string ProyectPathJsonSave = CurrentProyectUrl + "/Exports/Windows/FLKData/ObjectsJson/" + GetOBJ.ObjectID + ".Json";

                    File.WriteAllText(ProyectPathJsonSave, JsonGenerated);
                }
            }

            #endregion


            #region UnZip ExecutorGame
            string GetFileZipExecutor = CurrentProyectUrl + "/EngineLibrarys/Executors/ExecutorGame.zip";
            string DestinationFolder = CurrentProyectUrl + "/Exports/Windows/";

            string[] FilesToDelete = Directory.GetFiles(DestinationFolder);

            if (Directory.Exists (CurrentProyectUrl + "/Exports/Windows/runtimes/"))
            {
                Directory.Delete(CurrentProyectUrl + "/Exports/Windows/runtimes/", true);
            }


            for (int i = 0; i < FilesToDelete.Length; i++)
            {
                File.Delete(FilesToDelete[i]);
            }

            string ZipExecutorName = "ExecutorGame.zip";

            ZipFile.ExtractToDirectory (GetFileZipExecutor, DestinationFolder);
            #endregion

            ConsoleData.Log("The compile task for windows has finished");
        }
    }
}