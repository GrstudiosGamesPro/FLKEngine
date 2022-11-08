using Assimp;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FLKEngine
{
    public class FLKEngineRun
    {
        public static FLKEngineRun instance;

        public FLKEngineRun()
        {
            instance = this;
        }

        public static string CurrentDirector
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }

        public static void Main()
        {

#if DEV
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Extras/Modelos/rm.fbx");
            //string[] fileArray = Directory.GetFiles(path);



            if (Directory.Exists(CurrentDirector + "/Proyects/Test/"))
            {
                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Models"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Models");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Textures"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Textures");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/JsonData"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/JsonData");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Scripts"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Scripts");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Audio"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Audio");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/ShaderData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/ShaderData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/ModelsData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/ModelsData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/TextureData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/TextureData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/JsonData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/JsonData/");
                }
            }
            else
            {
                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Models"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Models");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Textures"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Textures");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/JsonData"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/JsonData");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Scripts"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Scripts");
                }

                if (!Directory.Exists(CurrentDirector + "/Proyects/Test/Audio"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/Proyects/Test/Audio");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/ShaderData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/ShaderData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/ModelsData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/ModelsData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/TextureData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/TextureData/");
                }

                if (!Directory.Exists(CurrentDirector + "/EngineLibrarys/JsonData/"))
                {
                    Directory.CreateDirectory(CurrentDirector + "/EngineLibrarys/JsonData/");
                }
            }
#endif

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "FLKEngine -> OpenGL4",

                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new EngineWindows(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.CurrentProyectUrl = CurrentDirector;
                window.window = window;
                window.Run();
            }
        }
    }
}