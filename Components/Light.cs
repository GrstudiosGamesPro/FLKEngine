using FLKEngine.Commons;
using FLKEngine.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    public class Light : FLKBehaviour
    {
        private Shader _lampShader;

        private Shader _lightingShader;

        private Texture _diffuseMap;

        private Texture _specularMap;
        private Camera _camera
        {
            get
            {
                return EngineWindows.instance.data._camera.GetComponent<Camera>();
            }
        }
        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -12.0f),
            new Vector3(0.0f, 0.0f, -3.0f)
        };

        public override void Start()
        {
            _lightingShader = new Shader("E:\\Proyectos VS\\FLKEngine\\Shaders\\shader.vert", "E:\\Proyectos VS\\FLKEngine\\Shaders\\lighting.frag");
            _lampShader = new Shader("E:\\Proyectos VS\\FLKEngine\\Shaders\\shader.vert", "E:\\Proyectos VS\\FLKEngine\\Shaders\\shader.frag");


            _diffuseMap = Texture.LoadFromFile("E:\\Proyectos VS\\FLKEEngine\\Modelos\\HAND_C.jpg");
            _specularMap = Texture.LoadFromFile("E:\\Proyectos VS\\FLKEEngine\\Modelos\\HAND_C.jpg");
        }

        public override void Update()
        {

        }
    }
}