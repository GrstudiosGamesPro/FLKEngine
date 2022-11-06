using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Librarys
{
    [System.Serializable]
    public class VertexArrayBuffer
    {
        /// <summary>
        /// The total index count (indices) for this instance.
        /// </summary>
        public int IndexCount { get; private set; }

        /// <summary>
        /// The Vertex Buffer Object Handle
        /// </summary>
        public int BufferHandle { get; private set; }

        /// <summary>
        /// The Element Array Object Handle
        /// </summary>
        public int IndexBufferHandle { get; private set; }

        /// <summary>
        /// The Vertex Array Object Handle
        /// </summary>
        public int ArrayHandle { get; private set; }

        public bool IsDisposed { get; private set; }

        public VertexArrayBuffer(Vertex[] vertices, uint[] indices)
        {
            BufferHandle = GL.GenBuffer();
            IndexBufferHandle = GL.GenBuffer();
            ArrayHandle = GL.GenVertexArray();

            SetData(vertices, indices);
        }

        ~VertexArrayBuffer()
        {
            Dispose(false);
        }

        public void SetData(Vertex[] vertices, uint[] indices)
        {
            IndexCount = indices != null ? indices.Length : vertices.Length;

            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.Size, vertices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IndexCount * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            SetShaderAttribs();
            Unbind();
        }

        public void Draw(PrimitiveType type = PrimitiveType.Triangles)
        {
            GL.BindVertexArray(ArrayHandle);
            GL.DrawElements(type, IndexCount, DrawElementsType.UnsignedInt, 0);
        }

        private void SetShaderAttribs()
        {
            //int posLocation = Shader.GetAttributeLocation("in_position");

            // position
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Size, 0);

            //int normLocation = Shader.GetAttributeLocation("in_normals");

            // normal
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Size, 12);

            //int texLocation = Shader.GetAttributeLocation("in_texCoord");

            // uv
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, Vertex.Size, 24);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (IndexBufferHandle != 0)
                {
                    GL.DeleteBuffer(IndexBufferHandle);
                    GL.DeleteBuffer(BufferHandle);
                    GL.DeleteVertexArray(ArrayHandle);
                }

                IsDisposed = true;
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(ArrayHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferHandle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}