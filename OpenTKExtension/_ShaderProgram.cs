using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKExtension
{
    public class _ShaderProgram
    {
        public int Id { get; private set; }
        public string Name { get; }

        public bool IsLoaded { get; private set; }

        _ShaderProgram(int id, string name)
        {
            Id = id;
            Name = name;
            IsLoaded = true;
        }
        public static _ShaderProgram LoadShaderProgram(string vertexShaderLocation, string fragmentShaderLocation, string shaderProgramName = "")
        {
            int shaderProgramId = GL.CreateProgram();
            _Shader vertexShader = _Shader.LoadShader(vertexShaderLocation, ShaderType.VertexShader);
            _Shader fragmentShader = _Shader.LoadShader(fragmentShaderLocation, ShaderType.FragmentShader);
            GL.AttachShader(shaderProgramId, vertexShader.Id);
            GL.AttachShader(shaderProgramId, fragmentShader.Id);
            GL.LinkProgram(shaderProgramId);
            GL.DetachShader(shaderProgramId, vertexShader.Id);
            GL.DetachShader(shaderProgramId, fragmentShader.Id);
            _Shader.DeleteShader(vertexShader);
            _Shader.DeleteShader(fragmentShader);

            string infoLog = GL.GetProgramInfoLog(shaderProgramId);



            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
            return new _ShaderProgram(shaderProgramId, shaderProgramName) ;
        }
    }
}
