using Jitter.Collision;
using Jitter;
using OpenTK.Mathematics;
using Jitter.Dynamics;
using Newtonsoft.Json.Linq;

namespace FLKEngine.Components
{
    [System.Serializable]
    public class Scene
    {
        public List<GameObject> ObjectsInScene = new List<GameObject>();
        CollisionSystem collision = new CollisionSystemSAP();
        World world;
        public List<Camera> GameCameras = new List<Camera>();

        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -12.0f),
            new Vector3(0.0f, 0.0f, -3.0f)
        };

        public Camera _camera;


        public void InitializeScene()
        {
            world = new World(collision);
            EngineWindows.instance.ConsoleData.Log ("STARTING SCENE");
        }

        public void AddBody (RigidBody body)
        {
            if (world != null) {
                body.IsStatic = false;
                world.AddBody(body);
            }
        }

        public void StartComponents()
        {
#if WINDOWS
            for (int i = 0; i < ObjectsInScene.Count; i++)
            {
                ObjectsInScene[i].StartScript();
            }
#endif
        }

        public void UpdateScene()
        {
#if WINDOWS                       
            world.Step(1.0f / 100.0f, true);

            for (int i = 0; i < ObjectsInScene.Count; i++)
            {
                ObjectsInScene[i].UpdateScript();
            }
#endif
        }
    }
}