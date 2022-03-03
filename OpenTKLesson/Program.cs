using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Common;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
namespace MyApp;

static public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("hello world");
        //Console.WriteLine(GL.GetString(StringName.Version));
        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;
        //nws.APIVersion = new(4, 6);
        gws.IsMultiThreaded = false;
        gws.RenderFrequency = 60;
        gws.UpdateFrequency = 60;

        nws.APIVersion = new(4, 1);
        nws.Size = new(1280, 720);
        
        //nws.IsEventDriven = true;


        nws.Title = "OpenTKLesson";
        //byte[] image = new byte[64 * 64 * 4];
        //for(int i = 0; i < image.Length; i++)
        //{
        //    if(i % 4 == 3)
        //    {
        //        image[i] = 255;
        //    }
        //    else if(i % 4 == 0)
        //    {
        //        image[i] = 255;
        //    }
        //    else
        //    {
        //        image[i] = (byte)i;
        //    }
        //}
        //nws.Icon = new WindowIcon(new Image(64, 64, image));

        GameWindow window = new GameWindow(gws, nws);


        Console.WriteLine(GL.GetString(StringName.Version));
        
        //int frameCount = 0;
        //window.UpdateFrame += (FrameEventArgs frameEventArgs) => {
        //    Console.WriteLine(frameCount);
        //    frameCount++;
        //};
        ShaderProgram shaderProgram = new ShaderProgram() {id=0};


        float[] verts = { -0.5f, -0.5f, 0, 0.5f, -0.5f, 0, 0, 0.5f, 0 };

        float[] verts2 = {
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f,  // Верхний правый угол
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,  // Нижний правый угол
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f,  // Нижний левый угол
            -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 0.0f   // Верхний левый угол
        };
        float[] verts3 = {
            // Позиции         // Цвета
             0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // Нижний правый угол
            -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // Нижний левый угол
             0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // Верхний угол
        };
        uint[] indices = {  // Помните, что мы начинаем с 0!
            0, 1, 3,   // Первый треугольник
            1, 2, 3    // Второй треугольник
        };
        uint[] indices3 = {
            0, 1, 2
        };

        int vao = 0;
        int vertices = 0;
        int ebo = 0;
        Random random = new Random();
        window.Load += () => {
            shaderProgram = LoadShaderProgram("../../../vertex_shader3.glsl", "../../../fragment_shader3.glsl");



            //debug triangle
            vao = GL.GenVertexArray();
            vertices = GL.GenBuffer();
            ebo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, 4*3*2, indices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * 3 * 1, indices3, BufferUsageHint.StaticDraw);
            //verts3[4] = (float)random.NextDouble();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
            //GL.BufferData(BufferTarget.ArrayBuffer, 36, verts, BufferUsageHint.StaticDraw);
            //GL.BufferData(BufferTarget.ArrayBuffer, 4*3*4, verts2, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * 6 * 3, verts3, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * 4, 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * 4, 3 * 4);
            GL.EnableVertexAttribArray(1);


            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            //GL.DeleteVertexArray(vao);
            //GL.DeleteBuffer(vertices);//??
            //GL.DeleteBuffer(ebo);//??
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>("C:/Users/oley/Source/Repos/OpenTKLesson/OpenTKLesson/Textures/container.jpg");

            var pixels = new List<byte>(4 * image.Width * image.Height);

            //for (int y = 0; y < image.Height; y++)
            //{
            //    var row = image.GetPixelRowSpan(y);

            //    for (int x = 0; x < image.Width; x++)
            //    {
            //        pixels.Add(row[x].R);
            //        pixels.Add(row[x].G);
            //        pixels.Add(row[x].B);
            //        pixels.Add(row[x].A);
            //    }
            //}

        };
        int frame = 0;

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            GL.UseProgram(shaderProgram.id);
            GL.ClearColor(0.3f, 0.3f, 0.3f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //debug triangle
            //float[] verts = { -0.5f, -0.5f, 0, 0.5f, -0.5f, 0, 0, 0.5f, 0 };

            //int vao = GL.GenVertexArray();
            //int vertices = GL.GenBuffer();

            //verts3[0] = (float)random.NextDouble();
            GL.BindVertexArray(vao);
            //verts3[0] = (float)random.NextDouble();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
            //GL.BufferData(BufferTarget.ArrayBuffer, 36, verts, BufferUsageHint.StaticDraw);
            //GL.EnableVertexAttribArray(0);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


            float val =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            int vertexColorLocation = GL.GetUniformLocation(shaderProgram.id, "outerColor");
            GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);

            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            //GL.DeleteVertexArray(vao);
            //GL.DeleteBuffer(vertices);
            //debug triangle


            Console.WriteLine($"{frame++}, {val}");
            window.SwapBuffers();
        };

        //window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        //{
        //    GL.UseProgram(shaderProgram.id);
        //    GL.ClearColor(0.3f, 0.3f, 0.3f, 0);
        //    GL.Clear(ClearBufferMask.ColorBufferBit);

        //    //debug triangle
        //    //float[] verts = { -0.5f, -0.5f, 0, 0.5f, -0.5f, 0, 0, 0.5f, 0 };

        //    //int vao = GL.GenVertexArray();
        //    //int vertices = GL.GenBuffer();




        //    //debug triangle
        //    vao = GL.GenVertexArray();
        //    vertices = GL.GenBuffer();
        //    ebo = GL.GenBuffer();
        //    GL.BindVertexArray(vao);
        //    GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
        //    //GL.BufferData(BufferTarget.ElementArrayBuffer, 4*3*2, indices, BufferUsageHint.StaticDraw);
        //    GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * 3 * 1, indices3, BufferUsageHint.StaticDraw);
        //    //verts3[4] = (float)random.NextDouble();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
        //    //GL.BufferData(BufferTarget.ArrayBuffer, 36, verts, BufferUsageHint.StaticDraw);
        //    //GL.BufferData(BufferTarget.ArrayBuffer, 4*3*4, verts2, BufferUsageHint.StaticDraw);
        //    GL.BufferData(BufferTarget.ArrayBuffer, 4 * 6 * 3, verts3, BufferUsageHint.StaticDraw);
        //    verts3[4] = (float)random.NextDouble();

        //    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * 4, 0);
        //    GL.EnableVertexAttribArray(0);

        //    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * 4, 3 * 4);
        //    GL.EnableVertexAttribArray(1);

        //    GL.DrawElements(BeginMode.Triangles, 9, DrawElementsType.UnsignedInt, 0);
        //    GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //    GL.BindVertexArray(0);
        //    GL.DeleteVertexArray(vao);
        //    GL.DeleteBuffer(ebo);
        //    GL.DeleteBuffer(vertices);






        //    //GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
        //    //GL.BufferData(BufferTarget.ArrayBuffer, 36, verts, BufferUsageHint.StaticDraw);
        //    //GL.EnableVertexAttribArray(0);
        //    //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

        //    //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


        //    float val =/*(float)Math.Sin(frame/60.0)/2f+0.5f;*/ (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
        //    float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
        //    int vertexColorLocation = GL.GetUniformLocation(shaderProgram.id, "outerColor");
        //    GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);



        //    //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //    //GL.BindVertexArray(0);
        //    //GL.DeleteVertexArray(vao);
        //    //GL.DeleteBuffer(vertices);
        //    //debug triangle


        //    Console.WriteLine($"{frame++}, {val}");
        //    window.SwapBuffers();
        //};


        window.Run();

    }

    private static void LoadImage(string Path)
    {

    }
    public static Shader LoadShader(string shaderLocation, ShaderType shaderType)
    {
        int shaderId = GL.CreateShader(shaderType);
        GL.ShaderSource(shaderId, File.ReadAllText(shaderLocation));
        GL.CompileShader(shaderId);
        string infoLog = GL.GetShaderInfoLog(shaderId);
        if(!string.IsNullOrEmpty(infoLog))
        {
            throw new Exception(infoLog);
        }
        return new Shader() { id = shaderId };
    }

    public static ShaderProgram LoadShaderProgram(string vertexShaderLocation, string fragmentShaderLocation)
    {
        int shaderProgramId = GL.CreateProgram();
        Shader vertexShader = LoadShader(vertexShaderLocation, ShaderType.VertexShader);
        Shader fragmentShader = LoadShader(fragmentShaderLocation, ShaderType.FragmentShader);
        GL.AttachShader(shaderProgramId, vertexShader.id);
        GL.AttachShader(shaderProgramId, fragmentShader.id);
        GL.LinkProgram(shaderProgramId);
        GL.DetachShader(shaderProgramId, vertexShader.id);
        GL.DetachShader(shaderProgramId, fragmentShader.id);
        GL.DeleteShader(vertexShader.id);
        GL.DeleteShader(fragmentShader.id);

        string infoLog = GL.GetProgramInfoLog(shaderProgramId);

        

        if (!string.IsNullOrEmpty(infoLog))
        {
            throw new Exception(infoLog);
        }
        return new ShaderProgram() { id = shaderProgramId };
    }
    
    public struct Shader
    {
        public int id;
    }

    public struct ShaderProgram
    {
        public int id;
    }
}
//public sealed class MainWindow : GameWindow
//{
//    public MainWindow() : base()
//    {
//        Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
//    }
//}