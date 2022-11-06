using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace FLKEngine.Librarys
{
    public struct Vertex
    {
        public static readonly int Size = Unsafe.SizeOf<Vertex>();

        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 UV;

        public Vertex(Vector3 position, Vector2 uv, Vector3 normal)
        {
            Position = position;
            UV = uv;
            Normal = normal;
        }

        public Vertex(Vector3 position, Vector2 uv)
        {
            Position = position;
            UV = uv;
            Normal = Vector3.Zero;
        }
    }
}
