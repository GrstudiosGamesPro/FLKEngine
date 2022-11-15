using FLKEngine.Components;
using OpenTK.ImGui;
using ImGuiNET;
using OpenTK.Mathematics;
using FLKEngine.Commons.Compiller;
using FLKEngine.Commons;
using FLKEngine.Components.Managers;
using FLKEngine.EngineData;
using FLKEngine.Librarys.LuaImplementation;
using FLKEngine.Components.GameObjectMaster;
using System.Diagnostics;
using NUnit.Framework.Internal;
using OpenTK.Graphics.OpenGL4;
using static System.Formats.Asn1.AsnWriter;

namespace FLKEngine.GUI
{
    public class EngineHUD
    {
        public Scene CurrentOpenScene
        {
            get
            {
                return EngineWindows.instance.CurrentOpenScene;
            }
        }

        public GameObject _camera
        {
            get
            {
                return EngineWindows.instance.data._camera;
            }
        }

        public string currentProyectURL
        {
            get
            {
                return EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Models/";
            }
        }

        bool CreatingSkyBox;
        string SkyToSelect;



#if DEV
        public void DrawEngineGUI()
        {
            var style = ImGui.GetStyle();
            style.WindowBorderSize = 1f;
            style.FrameRounding = 2f;
            style.PopupRounding = 2f;
            style.WindowBorderSize = 2f;

            style.ScrollbarSize = 25;
            ImGui.GetStyle().WindowPadding = new System.Numerics.Vector2(15, 15);

            style.Colors[(int)ImGuiCol.TextDisabled]  = new System.Numerics.Vector4(110f, 110f, 110f, 1.00f);
            style.Colors[(int)ImGuiCol.Text] = new System.Numerics.Vector4(1f, 1f, 1f, 1);
            style.Colors[(int)ImGuiCol.FrameBg] = new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 52);
            style.Colors[(int)ImGuiCol.TitleBg] = new System.Numerics.Vector4(0.1f, 0.1f, 0.1f, 1);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new System.Numerics.Vector4(0.3f, 0.3f, 0.3f, 1);

            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("New Scene", ""))
                    {
                        Scene data = new Scene();
                        EngineWindows.instance.ConsoleData.Log ("New level generated");
                    }

                    ImGui.Separator();

                    if (ImGui.MenuItem("Open", ""))
                    {

                    }

                    ImGui.Separator();
                    if (ImGui.MenuItem("Save", "ctrl + s"))
                    {
                        SaveEngineData data = new SaveEngineData();
                        data.Save(false);
                    }


                    if (ImGui.MenuItem("Exit"))
                    {
                        SaveEngineData data = new SaveEngineData();
                        data.Save(true);
                    }
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Assets"))
                {
                    if (ImGui.BeginMenu("Create Component"))
                    {
                        if (ImGui.MenuItem("Create Camera"))
                        {
                            GameObject obj = new GameObject();
                            obj.LoadModel("E:\\Proyectos VS\\FLKEEngine\\Commons\\GizmosModels\\video_camera.fbx");
                            obj.Scale = new Vector3(0.01f, 0.01f, 0.01f);
                        }
                        ImGui.End();
                    } 
                    ImGui.End();
                }

                if (ImGui.BeginMenu("Settings"))
                {
                    float fv = _camera.GetComponent<Camera>().Fov;
                    ImGui.SliderFloat("Fov Camera", ref fv, 90f, 0f);
                    _camera.GetComponent<Camera>().Fov = fv;

                    ImGui.Separator();

                    float sb = EngineWindows.instance.sensitivity;
                    ImGui.SliderFloat("Sensitivity Camera", ref sb, 0.01f, 5f);
                    //ImGui.DragFloat("B", ref sb, 0.01f);
                    EngineWindows.instance.sensitivity = sb;

                    float sba = EngineWindows.instance.cameraSpeed;
                    ImGui.SliderFloat("Move Speed Camera", ref sba, 0.01f, 25f);
                    EngineWindows.instance.cameraSpeed = sba;

                    ImGui.Separator();

                    float farGet = _camera.GetComponent<Camera>().ViewDistance;

                    ImGui.DragFloat("Far", ref farGet, 1f, 100f, 1000000f);

                    _camera.GetComponent<Camera>().ViewDistance = farGet;

                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Export"))
                {
                    if (ImGui.Button("Windows"))
                    {
                        CompillerSystem systemCompiller = new CompillerSystem();
                        systemCompiller.CompileToWindows();
                    }
                    ImGui.EndMenu();
                }
                ImGui.Separator();
                ImGui.Text("OpenGL: " + $"{GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)}");
                ImGui.Text ($"FPS:{1f / EngineWindows.instance.Delta:0000.0}");

                ImGui.EndMainMenuBar();
            }

            if (CreatingSkyBox)
            {
                if (ImGui.Begin ("New SkyBox Setting"))
                {
                    string[] skg = Directory.GetFiles(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/SkyBoxTextures/");


                    if (ImGui.TreeNode("Select Sky"))
                    {
                        foreach (string d in skg)
                        {
                            if (ImGui.Button("Select SkyBox: " + Path.GetFileName(d)))
                            {
                                SkyToSelect = d;
                            }
                        }
                        ImGui.TreePop();
                    }




                    if (ImGui.Button("OK"))
                    {
                        GameObject SkyBox = new GameObject();
                        SkyBox.Name = "SkyBox_1";
                        SkyBox._diffuseMap = Texture.LoadFromFile (SkyToSelect);
                        SkyBox._specularMap = Texture.LoadFromFile (SkyToSelect);
                        SkyBox.LoadModel (EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/DefaultModels/Sphere.fbx");

                        SkyBox.Scale = new Vector3 (5, 5, 5);

                        SkyToSelect = "";
                        CreatingSkyBox = false;
                    }

                    if (ImGui.Button("CANCEL"))
                    {
                        CreatingSkyBox = false;
                    }
                    ImGui.End();
                }
            }

            if (ImGui.Begin("Hierarchy"))
            {
                if (ImGui.Button ("Create SkyBox"))
                {
                    CreatingSkyBox = true;
                }


                ImGui.Text("Objects: " + CurrentOpenScene.ObjectsInScene.Count.ToString());

                if (ImGui.TreeNode("Objects In Scene"))
                {

                    for (int i = 0; i < CurrentOpenScene.ObjectsInScene.Count; i++)
                    {
                        if (EngineWindows.instance.CurrentObjectSelect == CurrentOpenScene.ObjectsInScene[i])
                        {
                            if (ImGui.Button(CurrentOpenScene.ObjectsInScene[i].Name + " <--"))
                            {
                                EngineWindows.instance.CurrentObjectSelect = null;
                            }
                        }
                        else
                        {
                            if (ImGui.Button(CurrentOpenScene.ObjectsInScene[i].Name))
                            {
                                EngineWindows.instance.CurrentObjectSelect = CurrentOpenScene.ObjectsInScene[i];
                            }
                        }
                    }

                    ImGui.TreePop();
                }
                ImGui.End();
            }

            string nameReplace(string name)
            {
                return name.Replace("FLKEngine.Components.", "");
            }


            if (EngineWindows.instance.CurrentObjectSelect != null && ImGui.Begin("Inspector"))
            {
                if (ImGui.TreeNode("Components To Add  (Total Components)-> " + EngineWindows.instance.CurrentObjectSelect.Components.Count()))
                {
                    ComponentsManager manager = new ComponentsManager();

                    for (int i = 0; i < manager.behaviours.Count; i++)
                    {
                        string nexName = nameReplace(manager.behaviours[i].ToString());
                        Console.WriteLine(nexName);

                        if (ImGui.Button("Component: " + nexName))
                        {
                            EngineWindows.instance.CurrentObjectSelect.AddComponent(manager.behaviours[i]);
                        }
                    }

                    ImGui.TreePop();
                }

                Vector3 tmp = EngineWindows.instance.CurrentObjectSelect.Position;
                System.Numerics.Vector3 v = new System.Numerics.Vector3(tmp.X, tmp.Y, tmp.Z);

                ImGui.DragFloat3("Position", ref v, 0.01f);

                EngineWindows.instance.CurrentObjectSelect.body.Position = new Jitter.LinearMath.JVector(v.X, v.Y, v.Z);

                Vector3 rotOBJ = EngineWindows.instance.CurrentObjectSelect.Rotation;
                System.Numerics.Vector3 a = new System.Numerics.Vector3(rotOBJ.X, rotOBJ.Y, rotOBJ.Z);

                ImGui.DragFloat3("Rotation", ref a, 0.01f);
                EngineWindows.instance.CurrentObjectSelect.Rotation = new Vector3(a.X, a.Y, a.Z);

                Vector3 scaleOBJ = EngineWindows.instance.CurrentObjectSelect.Scale;
                System.Numerics.Vector3 b = new System.Numerics.Vector3(scaleOBJ.X, scaleOBJ.Y, scaleOBJ.Z);

                ImGui.DragFloat3("Scale", ref b, 0.01f);
                EngineWindows.instance.CurrentObjectSelect.Scale = new Vector3(b.X, b.Y, b.Z);

                /*float scale = EngineWindows.instance.CurrentObjectSelect.Scale;
                ImGui.DragFloat("Scale", ref scale, 0.001f);
                EngineWindows.instance.CurrentObjectSelect.Scale = scale;*/

                ImGui.Separator();

                if (EngineWindows.instance.CurrentObjectSelect != null)
                {
                    if (!EngineWindows.instance.CurrentObjectSelect.UsePhysics)
                    {
                        ImGui.Text("Physics: Disabled");

                        if (ImGui.Button("Use Physics"))
                        {
                            EngineWindows.instance.CurrentObjectSelect.UsePhysics = true;
                        }
                    }
                    else
                    {
                        ImGui.Text("Physics: In Use");

                        if (ImGui.Button("Off Physics"))
                        {
                            EngineWindows.instance.CurrentObjectSelect.UsePhysics = false;
                        }
                    }
                }

                ImGui.Separator();
                ImGui.Separator();
                ImGui.Separator();

                ImGui.Text("Texture: ");
                if (ImGui.TreeNode("Textures"))
                {
                    string GetCurrentTextureFolder = EngineWindows.instance.CurrentProyectUrl + EngineWindows.instance.EngineData + "TextureData/";
                    string[] fileArray = Directory.GetFiles(GetCurrentTextureFolder);


                    for (int e = 0; e < fileArray.Length; e++)
                    {
                        string GetExtension = Path.GetExtension(fileArray[e]);
                        string GetName = Path.GetFileName(fileArray[e]);

                        if (GetExtension == ".jpg" || GetExtension == ".png")
                        {
                            if (ImGui.Button("Texture: " + GetName))
                            {
                                EngineWindows.instance.CurrentObjectSelect._specularMap = Texture.LoadFromFile(fileArray[e]);
                                EngineWindows.instance.CurrentObjectSelect._diffuseMap = Texture.LoadFromFile(fileArray[e]);

                                EngineWindows.instance.CurrentObjectSelect._specularMapURL = fileArray[e];
                                EngineWindows.instance.CurrentObjectSelect._diffuseMapURL = fileArray[e];
                            }
                        }
                    }
                    ImGui.TreePop();
                }

                //ImGui.Text("Shader: " + CurrentObjectSelect._lampShader.ToString());


                ImGui.End();
            }

            if (EngineWindows.instance.CurrentObjectSelect != null)
            {
                if (ImGui.Begin ("Scripts"))
                {
                    if (ImGui.Button ("New Script"))
                    {
                        LuaCompiller ls = new LuaCompiller();
                        string GenName = "Script_" + new Random().Next(1000, 9999);
                        ls.ScriptName = GenName;
                        GeneratorIDScript ns = new GeneratorIDScript();
                        string GenerateName = ns.GenerateRandomID();
                        ls.ObjectID = GenerateName;

                        string Info = "\n--Do not delete the OnUpdate and OnStart functions because without them the script will not work";

                        string VarsInfo = "\n\n---------------------------\n\n\n\n--Your variables in here\n\n\n\n---------------------------";

                        string StartFuntion = "\n\n\nfunction OnStart()\n--write your code here \n--The code written here will be initialized when starting the game\nend";
                        string UpdateFunctions = "\n\n\n\nfunction OnUpdate()\n--write your code here\n--The code written here will stay running for the entire game.\nend";

                        File.WriteAllText (EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + GenerateName + ".lua", "--Script: " + GenName + Info + VarsInfo + StartFuntion + UpdateFunctions);
                        EngineWindows.instance.CurrentObjectSelect.lua.Add (ls);
                    }

                    if (ImGui.TreeNode ("Scripts"))

                    {

                        for (int a = 0; a < EngineWindows.instance.CurrentObjectSelect.lua.Count; a++)
                        {
                            if (ImGui.Button(EngineWindows.instance.CurrentObjectSelect.lua[a].ScriptName))
                            {
                                string filename = EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + EngineWindows.instance.CurrentObjectSelect.lua[a].ObjectID + ".lua";
                                OpenVsCode (filename);
                            }
                            if (ImGui.Button("Delete Script"))
                            {
                                File.Delete(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Scripts/" + EngineWindows.instance.CurrentObjectSelect.lua[a].ObjectID + ".lua");
                                EngineWindows.instance.CurrentObjectSelect.lua.Remove(EngineWindows.instance.CurrentObjectSelect.lua[a]);
                            }

                            ImGui.Separator();
                            ImGui.Separator();
                            ImGui.Separator();
                        }

                        ImGui.TreePop();
                    }

                    ImGui.End();
                }
            }

            if (ImGui.Begin("Console"))
            {
                if (ImGui.Button("Clear"))
                {
                    EngineWindows.instance.ConsoleData.ClearConsole();
                }

                if (ImGui.TreeNode("Logs"))
                {
                    foreach (string d in EngineWindows.instance.ConsoleData.Logs)
                    {
                        ImGui.Text(d);
                        ImGui.Separator();
                        ImGui.Separator();
                    }
                }
                ImGui.End();
            }
            if (ImGui.Begin("Assets"))
            {
                if (ImGui.TreeNode("Models"))
                {
                    string[] fileArray = Directory.GetFiles(EngineWindows.instance.CurrentProyectUrl + "/Proyects/Test/Models");


                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        string GetExtension = Path.GetExtension(fileArray[i]);
                        string GetName = Path.GetFileName(fileArray[i]);
                        if (GetExtension == ".fbx" || GetExtension == ".obj")
                        {
                            if (ImGui.Button(GetName))
                            {
                                if (fileArray[i] != null)
                                {

                                    Random rnd = new Random();
                                    GameObject gm = new GameObject();
                                    gm.LoadModel(fileArray[i]);
                                    gm.body.IsStatic = true;
                                    gm.Position = new Vector3(0, 0, 0);
                                    gm.Name = "GameObject" + rnd.Next(1000, 9999);
                                }
                            }
                        }
                    }
                    ImGui.TreePop();
                }


                if (ImGui.TreeNode("Textures"))
                {

                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Scripts"))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (ImGui.Button("Test button: " + i.ToString()))
                        {

                        }
                    }
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Audio"))
                {

                    ImGui.TreePop();
                }
                ImGui.Separator();
                ImGui.End();
            }

        }
#endif
        public static void Open(string app, string args)
        {
            using (Process myProcess = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(app, args);
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = startInfo.Arguments;
                myProcess.Start();
            }
        }

        public static void OpenVsCode(string filePath)
        {
            Open("code", filePath);
        }
    }
}