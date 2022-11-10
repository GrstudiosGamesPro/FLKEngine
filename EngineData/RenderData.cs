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
using FLKEngine.Components.Managers;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Security.AccessControl;
using System.Windows.Input;
using System;
using FLKEngine.Commons.Compiller;
using FLKEngine.GUI;
using FLKEngine.EngineData;

namespace FLKEngine.EngineData
{
    [Serializable]
    public class RenderData
    {
        public Shader _lightingShader
        {
            get
            {
                return EngineWindows.instance._lightingShader;
            }
        }
        public Shader _lampShader
        {
            get
            {
                return EngineWindows.instance._lampShader;
            }
        }


        public int _vaoModel;

        public GameObject CurrentObjectSelect
        {
            get
            {
                return EngineWindows.instance.CurrentObjectSelect;
            }
        }

        public Scene CurrentOpenScene
        {
            get
            {
                return EngineWindows.instance.CurrentOpenScene;
            }
        }

        public int _vaoLamp;

        public Vector3[] _pointLightPositions
        {
            get
            {
                return EngineWindows.instance._pointLightPositions;
            }
        }

        public string CurrentProyectUrl
        {
            get
            {
                return EngineWindows.instance.CurrentProyectUrl;
            }
        }

        public string EngineExtensionsVert
        {
            get
            {
                return EngineWindows.instance.EngineExtensionsVert;
            }
        }

        public string EngineExtensionsFrag
        {
            get
            {
                return EngineWindows.instance.EngineExtensionsFrag;
            }
        }

        public string EngineData
        {
            get
            {
                return EngineWindows.instance.EngineData;
            }
        }

        public int _vertexBufferObject;

        private readonly float[] _vertices =
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

        public GameObject _camera
        {
            get
            {
                return EngineWindows.instance._camera;
            }
        }


        public void OnLoad ()
        {
            Console.WriteLine("Current Folder Proyect: " + CurrentProyectUrl);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            {
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            }

            {
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                var positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            }

            EngineWindows.instance.LoadEngineFunctions();
        }


        public void DevEdition (FrameEventArgs e)
        {
            if (!EngineWindows.instance.IsFocused)
            {
                return;
            }

            var mouse = EngineWindows.instance.MouseState;

            Single deltaX = mouse.X - EngineWindows.instance._lastPos.X;
            Single deltaY = mouse.Y - EngineWindows.instance._lastPos.Y;


            EngineWindows.instance._lastPos = new Vector2(mouse.X, mouse.Y);

            var kd = EngineWindows.instance.KeyboardState;

#if DEV
            if (CurrentObjectSelect != null && kd.IsKeyPressed(Keys.Delete))
            {
                CurrentOpenScene.ObjectsInScene.Remove(CurrentObjectSelect);
                EngineWindows.instance.CurrentObjectSelect = null;
            }
#endif

            if (EngineWindows.instance.MouseState.IsButtonDown(MouseButton.Right))
            {
                EngineWindows.instance.CursorState = CursorState.Grabbed;

                var input = EngineWindows.instance.KeyboardState;

                if (input.IsKeyDown(Keys.W))
                {
                    _camera.owner.Position += _camera.GetComponent<Camera>().Front * EngineWindows.instance.cameraSpeed * (float)e.Time; // Forward
                }

                if (input.IsKeyDown(Keys.S))
                {
                    _camera.owner.Position -= _camera.GetComponent<Camera>().Front * EngineWindows.instance.cameraSpeed * (float)e.Time; // Backwards
                }
                if (input.IsKeyDown(Keys.A))
                {
                    _camera.owner.Position -= _camera.GetComponent<Camera>().Right * EngineWindows.instance.cameraSpeed * (float)e.Time; // Left
                }
                if (input.IsKeyDown(Keys.D))
                {
                    _camera.owner.Position += _camera.GetComponent<Camera>().Right * EngineWindows.instance.cameraSpeed * (float)e.Time; // Right
                }
                if (input.IsKeyDown(Keys.E))
                {
                    _camera.owner.Position += _camera.GetComponent<Camera>().Up * EngineWindows.instance.cameraSpeed * (float)e.Time; // Up
                }
                if (input.IsKeyDown(Keys.Q))
                {
                    _camera.owner.Position -= _camera.GetComponent<Camera>().Up * EngineWindows.instance.cameraSpeed * (float)e.Time; // Down
                }

                _camera.GetComponent<Camera>().Yaw += deltaX * EngineWindows.instance.sensitivity;
                _camera.GetComponent<Camera>().Pitch -= deltaY * EngineWindows.instance.sensitivity;
            }
            else
            {
                EngineWindows.instance.CursorState = CursorState.Normal;
            }
        }


        public void Render (FrameEventArgs args)
        {
            GL.CullFace(CullFaceMode.Front);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel);

            _lightingShader.Use();

            _lightingShader.SetMatrix4("view", _camera.GetComponent<Camera>().GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetComponent<Camera>().GetProjectionMatrix());

            _lightingShader.SetVector3("viewPos", _camera.Position);

            _lightingShader.SetInt("material.diffuse", 0);
            _lightingShader.SetInt("material.specular", 1);
            _lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _lightingShader.SetFloat("material.shininess", 32.0f);

     

            // Directional light
            _lightingShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _lightingShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _lightingShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _lightingShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));


#if DEV
            if (CurrentObjectSelect != null)
            {
                GameObject obj = CurrentObjectSelect;

                if (obj.body != null)
                {
                    if (obj.UsePhysics)
                    {
                        obj.Position = new Vector3(obj.body.Position.X, obj.body.Position.Y, obj.body.Position.Z);
                        obj.body.IsStatic = false;
                    }
                    else
                    {
                        obj.Position = new Vector3(obj.body.Position.X, obj.body.Position.Y, obj.body.Position.Z);
                        obj.body.IsStatic = true;
                    }
                }
                else
                {
                    if (!obj.UsePhysics)
                    {
                        EngineWindows.instance.ConsoleData.Error("Not exist rigidbody in " + obj.Name);
                    }
                }
            }
#else

            for (int i = 0; i < EngineWindows.instance.CurrentOpenScene.ObjectsInScene.Count; i++)
            {
                GameObject obj = EngineWindows.instance.CurrentOpenScene.ObjectsInScene[i];

                if (obj.body != null)
                {
                    if (obj.UsePhysics)
                    {
                        obj.Position = new Vector3(obj.body.Position.X, obj.body.Position.Y, obj.body.Position.Z);
                        obj.body.IsStatic = false;
                    }
                    else
                    {
                        obj.Position = new Vector3(obj.body.Position.X, obj.body.Position.Y, obj.body.Position.Z);
                        obj.body.IsStatic = true;
                    }
                }
                else
                {
                    if (!obj.UsePhysics)
                    {
                        EngineWindows.instance.ConsoleData.Error("Not exist rigidbody in " + obj.Name);
                    }
                }
            }
            
#endif


            for (int i = 0; i < CurrentOpenScene.ObjectsInScene.Count; i++)
            {
                Matrix4 model = Matrix4.CreateScale  (CurrentOpenScene.ObjectsInScene[i].Scale);
                model *= Matrix4.CreateRotationX     (CurrentOpenScene.ObjectsInScene[i].Rotation.X);
                model *= Matrix4.CreateRotationY     (CurrentOpenScene.ObjectsInScene[i].Rotation.Y);
                model *= Matrix4.CreateRotationZ     (CurrentOpenScene.ObjectsInScene[i].Rotation.Z);
                model *= Matrix4.CreateTranslation   (CurrentOpenScene.ObjectsInScene[i].Position);
                
                _lightingShader.SetMatrix4("model", model);

                if (CurrentOpenScene.ObjectsInScene[i].modelo != null && CurrentOpenScene.ObjectsInScene[i] != null)
                {
                    CurrentOpenScene.ObjectsInScene[i]._diffuseMap.Use(TextureUnit.Texture0);
                    CurrentOpenScene.ObjectsInScene[i]._specularMap.Use(TextureUnit.Texture1);

                    CurrentOpenScene.ObjectsInScene[i].modelo.Draw();
                }
            }

            GL.BindVertexArray(_vaoLamp);


            _lampShader.Use();

            _lampShader.SetMatrix4("view", _camera.GetComponent<Camera>().GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetComponent<Camera>().GetProjectionMatrix());

            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
                lampMatrix = lampMatrix * Matrix4.CreateTranslation(_pointLightPositions[i]);

                _lampShader.SetMatrix4("model", lampMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }
        }
    }
}
