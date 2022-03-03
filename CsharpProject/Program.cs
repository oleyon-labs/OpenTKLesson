using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Common;
using static MyApp.Program;

namespace Praktika;


class Program
{
    static void Main(string[] args)
    {
        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;
        //nws.APIVersion = new(4, 6);
        gws.IsMultiThreaded = false;
        gws.RenderFrequency = 60;
        gws.UpdateFrequency = 60;

        nws.APIVersion = new(4, 1);
        nws.Size = new(640, 480);
        nws.Title = "Hello triangle";
        GameWindow window = new GameWindow(gws, nws);



        float[] vertices = {
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f, 0.5f, 0.0f
        };

        int VBO = 0, VAO = 0;


        ShaderProgram shaderProgram = new ShaderProgram();
        

        window.Load += () =>
        {
            shaderProgram = MyApp.Program.LoadShaderProgram("C:/Users/oley/source/repos/OpenTKLesson/CsharpProject/VS.glsl", "C:/Users/oley/source/repos/OpenTKLesson/CsharpProject/FS.glsl");
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * 9, vertices, BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        };

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            GL.UseProgram(shaderProgram.id);
            GL.ClearColor(0.3f, 0.2f, 0.5f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            float val =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            int vertexColorLocation = GL.GetUniformLocation(shaderProgram.id, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);


            GL.BindVertexArray(VAO);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.BindVertexArray(0);

            window.SwapBuffers();
        };


        window.Run();
    }
}