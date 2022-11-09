using FLKEngine.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Texture = FLKEngine.Commons.Texture;
using FLKEngine.Components;
using FLKEngine.Librarys.LuaImplementation;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;
using FLKEngine.Components.GameObjectMaster;
using MoonSharp.Interpreter;
using FLKEngine.Components.Data;

namespace FLKEngine.Components
{
    [System.Serializable]
    public class GameObject : FLKBehaviour
    {
        public List<LuaCompiller> lua = new List<LuaCompiller>();

        public string Name;

        public bool UsePhysics;

        private float[] _vertices =
                {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };


        public Vector3 Position = new Vector3(0, 0, 0);
        public Vector3 Rotation = new Vector3(0, 0, 0);
        public Vector3 Scale = new Vector3 (0.01f, 0.01f, 0.01f);

        public RigidBody body;

        public List<Camera> Cameras = new List<Camera>();
        public Model modelo;

        private ComponentManager componentManager;
        public Texture _diffuseMap;
        public Texture _specularMap;
        public string ObjectID;

        public List<FLKBehaviour> Components = new List<FLKBehaviour>();

        public string CurrentModelPath;

        public string _diffuseMapURL;
        public string _specularMapURL;

        public string EngineTextureExtension
        {
#if !DEV
            get
            {
                return ".FLKTexturePackageData";
            }
#else
            get
            {
                return ".jpg";
            }
#endif
        }

        public GameObject()
        {
            string modelPath = "E:\\Proyectos VS\\FLKEEngine\\Extras\\Modelos\\car.FLKPackageData";
            modelo = Model.FromFile(modelPath);

            CurrentModelPath = modelPath;

            Shape shape = new BoxShape(1f, 1f, 1f);
            body = new RigidBody(shape);
            componentManager = new ComponentManager();

#if DEV
            GeneratorID id = new GeneratorID();
            ObjectID = id.GenerateRandomID (this);
            EngineWindows.instance.ConsoleData.Log("GENERATOR ID: " + ObjectID);
#endif

            string GetTextureCurrentData = EngineWindows.instance.CurrentProyectUrl + EngineWindows.instance.EngineData + "TextureData/";
            _diffuseMap = Texture.LoadFromFile(GetTextureCurrentData + "HAND_C" + EngineTextureExtension);
            _specularMap = Texture.LoadFromFile(GetTextureCurrentData + "HAND_C" + EngineTextureExtension);

            _diffuseMapURL = GetTextureCurrentData + "HAND_C" + EngineTextureExtension;
            _specularMapURL = GetTextureCurrentData + "HAND_C" + EngineTextureExtension;


            EngineWindows.instance.CurrentOpenScene.ObjectsInScene.Add(this);
        }

        public void StartScript ()
        {
            for (int i = 0; i < lua.Count; i++)
            {
                lua[i].owner = this;
                lua[i].StartScript();
            }
        }

        public void UpdateScript()
        {
            for (int i = 0; i < lua.Count; i++)
            {
                lua[i].owner = this;
                lua[i].UpdateScript();
            }
        }

#if !DEV
        public void ChangePositionOnLoadGame (Vector3 Pos)
        {
            Position = Pos;
            body.Position = new JVector (Pos.X, Pos.Y, Pos.Z);    
        }
#endif

        public void CharacterMove (float x, float y, float z, float Speed)
        {
            JVector vel = new JVector(x * Speed * EngineWindows.instance.Delta, y * Speed * EngineWindows.instance.Delta, z * Speed * EngineWindows.instance.Delta);

            body.AddForce (vel);
        }

        public void Move (string dir, float Speed)
        {
            Position = new Vector3 (Position.X, Position.Y, Position.Z + 1 * Speed * EngineWindows.instance.Delta);
        }

        public void LoadModel (string LoadModelPath)
        {
            Shape shape = new BoxShape(1f, 1f, 1f);
            body = new RigidBody(shape);
            modelo = Model.FromFile(LoadModelPath);
            CurrentModelPath = LoadModelPath;
            Console.WriteLine("Component GameObject Initialize");

            EngineWindows.instance.CurrentOpenScene.AddBody(body);
        }

        public void RenderObjectOnStart()
        {
            Console.WriteLine("Component GameObject Initialize");
            EngineWindows.instance.CurrentOpenScene.AddBody(body);
        }


        public void AddComponent (FLKBehaviour cpp)
        {
            cpp.owner = this;
            componentManager.addComponent (cpp);
        }

        public T GetComponent<T>() where T : FLKBehaviour, new()
        {
            return componentManager.getComponent<T>();
        }
    }
}
