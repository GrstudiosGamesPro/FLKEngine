using Assimp;
using FLKEngine;
using FLKEngine.Librarys;
using OpenTK.Mathematics;

public class Model
{
    public List<VertexArrayBuffer> Buffers = new List<VertexArrayBuffer>();

    public void Draw(OpenTK.Graphics.OpenGL4.PrimitiveType type = OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles)
    {
        foreach (VertexArrayBuffer buffer in Buffers)
            buffer.Draw(type);
    }

    public static Model FromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            EngineWindows.instance.ConsoleData.Error ("The model not exist");
            return null;
        }
        else
        {
            using (AssimpContext importer = new AssimpContext())
            {
                Model model = new Model();
                Assimp.Scene scene = importer.ImportFile(filePath, PostProcessSteps.Triangulate);
                List<VertexArrayBuffer> sceneMeshes = new List<VertexArrayBuffer>();

                if (scene.HasMeshes)
                {
                    EngineWindows.instance.ConsoleData.Log("Mesh Count: " + scene.MeshCount);
                    // Console.WriteLine("Mesh Count: {0}", scene.MeshCount);
                    for (int j = 0; j < scene.Meshes.Count; j++)
                    {
                        Assimp.Mesh assimpMesh = scene.Meshes[j];
                        EngineWindows.instance.ConsoleData.Log("Mesh: " + assimpMesh.Name);

                        //Console.WriteLine("Mesh: {0}", assimpMesh.Name);

                        // copy the assimp mesh data to our mesh class format...
                        FLKEngine.Components.Mesh mesh = new FLKEngine.Components.Mesh();
                        mesh.Vertices = new Vertex[assimpMesh.VertexCount];
                        for (int i = 0; i < assimpMesh.VertexCount; i++)
                        {
                            mesh.Vertices[i] = new Vertex(new Vector3(assimpMesh.Vertices[i].X, assimpMesh.Vertices[i].Y, assimpMesh.Vertices[i].Z), Vector2.Zero, new Vector3(assimpMesh.Normals[i].X, assimpMesh.Normals[i].Y, assimpMesh.Normals[i].Z));
                            if (assimpMesh.HasTextureCoords(0))
                            {
                                mesh.Vertices[i].UV = new Vector2(assimpMesh.TextureCoordinateChannels[0][i].X, assimpMesh.TextureCoordinateChannels[0][i].Y);
                            }
                        }


                        mesh.Indices = assimpMesh.GetUnsignedIndices();

                        sceneMeshes.Add(new VertexArrayBuffer(mesh.Vertices, mesh.Indices));
                    }
                }

                return new Model { Buffers = sceneMeshes };
            }
        }
    }
}