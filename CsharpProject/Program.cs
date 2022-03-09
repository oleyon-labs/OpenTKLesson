using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Common;
using static MyApp.Program;
using OpenTK.Mathematics;
using OpenTKExtension;

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

        float[] verticesWithColors = {
        -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f,
        0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,
        0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f
        };

        float[] vertices = {
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f, 0.5f, 0.0f,
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

        int[] VBOs = new int[4];
        int[] VAOs = new int[4];


        ShaderProgram[] shaderPrograms =
        {
            null, null
        };

        //ShaderProgram shaderProgram = new ShaderProgram();
        

        window.Load += () =>
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());
            shaderPrograms[0] = ShaderProgram.LoadShaderProgram("../../../VS.glsl", "../../../FS.glsl");
            shaderPrograms[1] = ShaderProgram.LoadShaderProgram("../../../VS.glsl", "../../../FS2.glsl");



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
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);






            VAOs[2] = GL.GenVertexArray();
            VBOs[2] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[2]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[2]);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);



            VAOs[3] = GL.GenVertexArray();
            VBOs[3] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[3]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[3]);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * verticesWithColors.Length, verticesWithColors, BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6*4, 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6*4, 3*4);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

        };

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            //GL.UseProgram(shaderProgram.id);
            GL.ClearColor(0.3f, 0.2f, 0.5f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //float val =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            //float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            //int vertexColorLocation = GL.GetUniformLocation(shaderProgram.id, "ourColor");
            //GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);

            Matrix4 model = Matrix4.Identity;
            Vector3 vector3 = new Vector3(0.3f, -0.3f, 0.0f);
            Matrix4.CreateTranslation(in vector3, out model);

            float val4;
            val4 = (float)(DateTime.Now.Ticks % 100000);
            float val3 =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            Console.WriteLine(val4);
            Matrix4.CreateRotationZ(val3, out model);


            GL.UseProgram(shaderPrograms[0].Id);
            GL.BindVertexArray(VAOs[3]);


            int matrixLocation = GL.GetUniformLocation(shaderPrograms[0].Id, "model");
            GL.UniformMatrix4(matrixLocation, false, ref model);

            //for (int i = 0; i < VAOs.Length; i++)
            //{
            //    GL.UseProgram(shaderPrograms[i].id);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //    float val =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            //    float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            //    int vertexColorLocation = GL.GetUniformLocation(shaderPrograms[i].id, "ourColor");
            //    GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);




            //    GL.BindVertexArray(VAOs[i]);

            //    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //}



            GL.BindVertexArray(0);

            window.SwapBuffers();
        };


        window.Run();
        
    }
}