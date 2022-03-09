using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKExtension
{
    public class ShaderProgram
    {
        public int Id { get; private set; }
        public string Name { get; }

        public bool IsLoaded { get; private set; }

        ShaderProgram(int id, string name)
        {
            Id = id;
            Name = name;
            IsLoaded = true;
        }
        public static ShaderProgram LoadShaderProgram(string vertexShaderLocation, string fragmentShaderLocation, string shaderProgramName = "")
        {
            int shaderProgramId = GL.CreateProgram();
            Shader vertexShader = Shader.LoadShader(vertexShaderLocation, ShaderType.VertexShader);
            Shader fragmentShader = Shader.LoadShader(fragmentShaderLocation, ShaderType.FragmentShader);
            GL.AttachShader(shaderProgramId, vertexShader.Id);
            GL.AttachShader(shaderProgramId, fragmentShader.Id);
            GL.LinkProgram(shaderProgramId);
            GL.DetachShader(shaderProgramId, vertexShader.Id);
            GL.DetachShader(shaderProgramId, fragmentShader.Id);
            Shader.DeleteShader(vertexShader);
            Shader.DeleteShader(fragmentShader);

            string infoLog = GL.GetProgramInfoLog(shaderProgramId);



            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
            return new ShaderProgram(shaderProgramId, shaderProgramName) ;
        }
    }
}
