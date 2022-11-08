using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using FLKEngine.Components;
using OpenTK.ImGui;
using ImGuiNET;
using FLKEngine.Shaders;
using FLKEngine.Commons;
using MouseState = OpenTK.Input;
using System.Text.Json.Nodes;
using FLKEngine.EngineData;
using FLKEngine.Librarys.LuaImplementation;
using ImGuiSharp;

namespace FLKEngine
{
    class EngineWindows : GameWindow
    {
        public Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -12.0f),
            new Vector3(0.0f, 0.0f, -3.0f)
        };

        public bool _firstMove = true;

        public Vector2 _lastPos;

        public string CurrentProyectUrl;

        public static EngineWindows instance;

        public Scene CurrentOpenScene;


        public ConsoleDebug ConsoleData = new ConsoleDebug();
        public GameObject CurrentObjectSelect;


        public int withd, height;

        public float cameraSpeed = 1.5f;
        public float sensitivity = 0.2f;

        public RenderData data;
        public GameObject _camera;
        bool ReadyForStartScene;

        public EngineWindows window;

        public string EngineData
        {
#if DEV
            get
            {
                return "/EngineLibrarys/";
            }
#else
            get
            {
               return "/FLKData/";
            }
#endif
        }


        public string EngineExtensionsFrag
        {
#if DEV
            get
            {
                return "frag";
            }
#else
            get
            {
                return "fragflkshader";
            }
#endif
        }

        public string EngineExtensionsVert
        {
#if DEV
            get
            {
                return "vert";
            }
#else
            get
            {
                return "vertflkshader";
            }
#endif
        }

        public Shader _lightingShader;
        public Shader _lampShader;

        public ImGuiController _ImGUI;

        public float Delta;

        public EngineWindows(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            data = new RenderData();
            CurrentOpenScene = new Scene();

            if (instance == null)
            {
                instance = this;
            }

            if (instance != null)
            {
                Console.WriteLine("Singleton Engine Created");
            }
            else
            {
                Console.WriteLine("Singleton Engine No Created");
            }

            if (data._camera == null)
            {
                Console.WriteLine("No se genero una camara para la escena de juego");
            }


            if (CurrentOpenScene != null)
            {
                CurrentOpenScene.InitializeScene();
            }


            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            string GetCurrentShaderFolder = CurrentProyectUrl + EngineData + "ShaderData/";
            string GetCurrentTextureFolder = CurrentProyectUrl + EngineData + "TextureData/";

            _lightingShader = new Shader(GetCurrentShaderFolder + "shader." + EngineExtensionsVert, GetCurrentShaderFolder + "lighting." + EngineExtensionsFrag);
            _lampShader = new Shader(GetCurrentShaderFolder + "shader." + EngineExtensionsVert, GetCurrentShaderFolder + "shader." + EngineExtensionsFrag);

            //Console.WriteLine();
            data.OnLoad();

#if DEV
            LoadSceneData();
#else
            LoadGameData();
#endif            
        }


        public void LoadSceneData()
        {
            if (Directory.Exists (CurrentProyectUrl + "/Proyects/Test/JsonData/")) {
                string[] AllArchives = Directory.GetFiles(CurrentProyectUrl + "/Proyects/Test/JsonData/");

                for (int i = 0; i < AllArchives.Length; i++)
                {
                    string JsonLoaded = AllArchives[i];

                    string JsonRead = File.ReadAllText(JsonLoaded);

                    var dataJson = JsonArray.Parse(JsonRead);

                    GameObject gm = new GameObject();
                    gm.Name = dataJson["ObjectName"][0].ToString();
                    gm.ObjectID = dataJson["ObjectID"][0].ToString();

#if WINDOWS
                    gm.ChangePositionOnLoadGame (new Vector3((float)dataJson["Position"][0], (float)dataJson["Position"][1], (float)dataJson["Position"][2]));
#else
                    gm.Position = new Vector3((float)dataJson["Position"][0], (float)dataJson["Position"][1], (float)dataJson["Position"][2]);
#endif
                    gm.Rotation = new Vector3((float)dataJson["Rotation"][0], (float)dataJson["Rotation"][1], (float)dataJson["Rotation"][2]);
                    gm.Scale = new Vector3((float)dataJson["Scale"][0], (float)dataJson["Scale"][1], (float)dataJson["Scale"][2]);

                    gm.LoadModel(dataJson["UrlModelPath"][0].ToString());
                    gm._diffuseMap = Texture.LoadFromFile(dataJson["DiffuseURLPath"][0].ToString());
                    gm._specularMap = Texture.LoadFromFile(dataJson["SpecularURLPath"][0].ToString());

                    gm._specularMapURL = dataJson["SpecularURLPath"][0].ToString();
                    gm._diffuseMapURL = dataJson["DiffuseURLPath"][0].ToString();

                    int scriptsExistentes = dataJson["Scripts"].AsArray().Count;

                    for (int a = 0; a < scriptsExistentes; a++)
                    {
                        LuaCompiller sl = new LuaCompiller();
                        sl.ScriptName = (string)dataJson["ScriptsName"][a];
                        sl.ObjectID = (string)dataJson["Scripts"][a];
                        gm.lua.Add(sl);
                    }

                    gm.UsePhysics = (bool)dataJson["UsePhysics"][0];
                }
            }
        }


        public void LoadGameData()
        {
            if (Directory.Exists(CurrentProyectUrl + "/Proyects/Test/JsonData/"))
            {

                string[] AllArchives = Directory.GetFiles(CurrentProyectUrl + "/FLKData/ObjectsJson/");

                for (int i = 0; i < AllArchives.Length; i++)
                {
                    string JsonLoaded = AllArchives[i];

                    string JsonRead = File.ReadAllText(JsonLoaded);

                    Console.WriteLine("Loading file: " + JsonRead);

                    var dataJson = JsonArray.Parse(JsonRead);

                    GameObject gm = new GameObject();
                    gm.Name = dataJson["NameObject"][0].ToString();
                    gm.ObjectID = dataJson["ObjectID"][0].ToString();

                    string newExtensionModel = Path.ChangeExtension(dataJson["UrlModelPath"][0].ToString(), ".FLKModelPackageData");

                    gm.LoadModel(CurrentProyectUrl + "/FLKData/ModelsData/" + newExtensionModel);

                    string diffuseMaptextureNexExtension = Path.ChangeExtension(dataJson["DiffuseURLPath"][0].ToString(), ".FLKTexturePackageData");
                    string specularMaptextureNexExtension = Path.ChangeExtension(dataJson["SpecularURLPath"][0].ToString(), ".FLKTexturePackageData");

                    gm._diffuseMap = Texture.LoadFromFile(CurrentProyectUrl + "/FLKData/TextureData/" + diffuseMaptextureNexExtension);
                    gm._specularMap = Texture.LoadFromFile(CurrentProyectUrl + "/FLKData/TextureData/" + specularMaptextureNexExtension);


#if WINDOWS
                gm.ChangePositionOnLoadGame(new Vector3((float)dataJson["Position"][0], (float)dataJson["Position"][1], (float)dataJson["Position"][2]));
#else
                    gm.Position = new Vector3((float)dataJson["Position"][0], (float)dataJson["Position"][1], (float)dataJson["Position"][2]);
#endif
                    gm.Rotation = new Vector3((float)dataJson["Rotation"][0], (float)dataJson["Rotation"][1], (float)dataJson["Rotation"][2]);
                    gm.Scale = new Vector3((float)dataJson["Scale"][0], (float)dataJson["Scale"][1], (float)dataJson["Scale"][2]);

                    int scriptsExistentes = dataJson["Scripts"].AsArray().Count;

                    for (int a = 0; a < scriptsExistentes; a++)
                    {
                        LuaCompiller sl = new LuaCompiller();
                        sl.ScriptName = (string)dataJson["ScriptsName"][a];
                        sl.ObjectID = (string)dataJson["Scripts"][a];
                        gm.lua.Add(sl);
                    }

                    gm.UsePhysics = (bool)dataJson["UsePhysics"][0];
#if !DEV
                gm.StartScript();
#endif
                }
            }
        }


 
        public void LoadEngineFunctions()
        { 
            _ImGUI = new ImGuiController(100, 100);
            _camera = new GameObject();
            _camera.AddComponent(new Camera());
            _camera.GetComponent<Camera>().owner = _camera;
            CurrentOpenScene.ObjectsInScene.Remove(_camera);
            _camera.owner = _camera;
            ReadyForStartScene = true;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            RenderData data = new RenderData();
            data.Render (args);

            SwapBuffers();
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            data.DevEdition (e);
            CurrentOpenScene.UpdateScene();
            Delta = (float)e.Time;
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

#if DEV
            data._camera.GetComponent<Camera>().Fov -= e.OffsetY;
#endif
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

#if DEV
            data._ImGUI.WindowResized (e.Width, e.Height);
#endif

            GL.Viewport(0, 0, Size.X, Size.Y);

            withd = e.Width;
            height = e.Height;
        }
    }
}
