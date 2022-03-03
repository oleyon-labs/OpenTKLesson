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
        0.5f, 0.5f, 0.0f,

        -0.5f, -0.5f, 0.0f,
        0.5f, 0.5f, 0.0f,
        -0.5f, 0.5f, 0.0f
        };
        float[] vertices1 = {
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.5f, 0.5f, 0.0f
        };
        float[] vertices2 = {
        -0.5f, -0.5f, 0.0f,
        0.5f, 0.5f, 0.0f,
        -0.5f, 0.5f, 0.0f
        };

        int[] VBOs = new int[2];
        int[] VAOs = new int[2];


        ShaderProgram shaderProgram = new ShaderProgram();
        

        window.Load += () =>
        {
            shaderProgram = MyApp.Program.LoadShaderProgram("C:/Users/oley/source/repos/OpenTKLesson/CsharpProject/VS.glsl", "C:/Users/oley/source/repos/OpenTKLesson/CsharpProject/FS.glsl");
            VAOs[0] = GL.GenVertexArray();
            VBOs[0] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*vertices1.Length, vertices1, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.BindVertexArray(0);




            VAOs[1] = GL.GenVertexArray();
            VBOs[1] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[1]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices2.Length, vertices2, BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);






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

            for (int i = 0; i < VAOs.Length; i++)
            {
                GL.BindVertexArray(VAOs[i]);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }



            GL.BindVertexArray(0);

            window.SwapBuffers();
        };


        window.Run();
        
    }
}