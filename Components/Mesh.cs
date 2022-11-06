using FLKEngine.Librarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    public class Mesh
    {
        public int IndexCount => Indices?.Length ?? 0;
        public uint[] Indices;
        public Vertex[] Vertices;
    }
}