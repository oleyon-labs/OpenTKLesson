using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTKExtension;
using OpenTK.Mathematics;

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
        _ShaderProgram shaderProgram = null;


        float[] verts = { -0.5f, -0.5f, 0, 0.5f, -0.5f, 0, 0, 0.5f, 0 };

        float[] verts2 = {
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f,  // Верхний правый угол
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,  // Нижний правый угол
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f,  // Нижний левый угол
            -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 0.0f   // Верхний левый угол
        };
        float[] verts3 = {
            // Позиции         // Цвета           // координаты текстуры
             0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  1.0f, 0.0f,   // Нижний правый угол
            -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  0.0f, 0.0f,  // Нижний левый угол
             0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  0.5f, 1.0f    // Верхний угол
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

        int[] textures = { 0, 0 };
        
        Random random = new Random();

        Directory.SetCurrentDirectory("../../../../");
        Console.WriteLine(Directory.GetCurrentDirectory());

        window.Load += () => {
            shaderProgram = _ShaderProgram.LoadShaderProgram("vertex_shader2.glsl", "fragment_shader2.glsl");

            
            vao = GL.GenVertexArray();
            vertices = GL.GenBuffer();
            ebo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            
            GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * 3 * 1, indices3, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);

            GL.BufferData(BufferTarget.ArrayBuffer, 4 * (3+3+2) * 3, verts3, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (3+3+2) * 4, 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, 3 * 4);
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, (3 + 3) * 4);
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);

            #region textureGeneration


            textures[0] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            _Texture texture1 = new _Texture("Textures/container.jpg");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture1.Width, texture1.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture1.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);





            textures[1] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[1]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            _Texture texture2 = new _Texture("Textures/awesomeface.png");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture2.Width, texture2.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture2.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            #endregion

        };
        int frame = 0;

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            GL.UseProgram(shaderProgram.Id);
            GL.ClearColor(0.3f, 0.3f, 0.3f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);


            GL.BindVertexArray(vao);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram.Id, "ourTexture1"), 0);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, textures[1]);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram.Id, "ourTexture2"), 1);

            #region трансформации
            Matrix4 model;// = Matrix4.Identity;
            Vector3 vector3 = new Vector3(0.3f, -0.3f, 0.0f);
            Matrix4 translationMatrix;
            Matrix4.CreateTranslation(in vector3, out translationMatrix);

            float val3 = (float)Math.Sin(GetTimeInFloat()) / 2f + 0.5f;
            float val4 = (float)GetTimeInFloat();
            Matrix4 rotationMatrixZ, rotationMatrixY;
            Matrix4.CreateRotationZ(val4, out rotationMatrixZ);
            Matrix4.CreateRotationY(-val4, out rotationMatrixY);

            model = rotationMatrixZ * rotationMatrixY * translationMatrix;

            int matrixLocation = GL.GetUniformLocation(shaderProgram.Id, "model");
            GL.UniformMatrix4(matrixLocation, false, ref model);
            #endregion


            float val = (float)(Math.Sin(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            float val2 = (float)(Math.Cos(DateTime.Now.Ticks / 10000000.0) / 2 + 0.5);
            int vertexColorLocation = GL.GetUniformLocation(shaderProgram.Id, "outerColor");
            GL.Uniform4(vertexColorLocation, 0.0f, val2, val, 0.0f);

            GL.DrawElements(BeginMode.Triangles, 3, DrawElementsType.UnsignedInt, 0);


            GL.BindVertexArray(0);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            Console.WriteLine($"{frame++}, {val}");
            window.SwapBuffers();
        };

        window.Run();

    }

    private static double GetTimeInFloat()
    {
        var time = DateTime.Now;
        return (double)((time.Minute * 60 + time.Second) * 1000 + time.Millisecond) / 1000.0;
    }
}