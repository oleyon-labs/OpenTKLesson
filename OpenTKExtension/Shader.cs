using OpenTK.Graphics.OpenGL4;

namespace OpenTKExtension
{
    public class Shader
    {
        public int Id { get; private set; }
        //private int id;
        public string Name { get; }

        public bool IsLoaded { get; private set; }

        Shader(int id, string name)
        {
            Id = id;
            Name = name;
            IsLoaded = true;
        }
        public static Shader LoadShader(string shaderLocation, ShaderType shaderType, string shaderName = "")
        {
            int shaderId = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderId, File.ReadAllText(shaderLocation));
            GL.CompileShader(shaderId);
            string infoLog = GL.GetShaderInfoLog(shaderId);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
            return new Shader(shaderId, shaderName);
        }

        public static void DeleteShader(Shader shader)
        {
            GL.DeleteShader(shader.Id);
            shader.IsLoaded = false;
        }
    }
}