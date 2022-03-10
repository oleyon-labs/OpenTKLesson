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
        Directory.SetCurrentDirectory("../../../");
        Console.WriteLine(Directory.GetCurrentDirectory());


        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;
        //nws.APIVersion = new(4, 6);
        gws.IsMultiThreaded = false;
        gws.RenderFrequency = 60;
        gws.UpdateFrequency = 60;

        nws.APIVersion = new(4, 1);
        nws.Size = new(1024, 720);
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

        int[] VBOs = new int[10];
        int[] VAOs = new int[10];


        ShaderProgram[] shaderPrograms =
        {
            null, null
        };

        //ShaderProgram shaderProgram = new ShaderProgram();


        int[] EBOs = new int[10];

        int[] textures = { 0, 0 };





        float[] rectangleTextured = {
             // Позиции          // Цвета           // координаты текстуры
             0.5f,  0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  1.0f, 1.0f,  // Верхний правый угол
             0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f,  // Нижний правый угол
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  // Нижний левый угол
            -0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f   // Верхний левый угол
        };

        uint[] indices = {  // Помните, что мы начинаем с 0!
            0, 1, 3,   // Первый треугольник
            1, 2, 3    // Второй треугольник
        };

        window.Load += () =>
        {
            shaderPrograms[0] = ShaderProgram.LoadShaderProgram("vertex_shader3.glsl", "fragment_shader3.glsl");
            shaderPrograms[1] = ShaderProgram.LoadShaderProgram("VS.glsl", "FS2.glsl");

            #region старые треугольники
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
            #endregion

            #region прямоугольник с текстурой

            VAOs[5] = GL.GenVertexArray();
            VBOs[5] = GL.GenBuffer();
            EBOs[5] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[5]);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[5]);

            GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * 3 * 2, indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[5]);

            GL.BufferData(BufferTarget.ArrayBuffer, 4 * (3 + 3 + 2) * 4, rectangleTextured, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, 3 * 4);
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, (3 + 3) * 4);
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);
            #endregion

            #region создание текстур


            textures[0] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Texture texture1 = new Texture("Textures/texture.png");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture1.Width, texture1.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture1.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);





            textures[1] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[1]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Texture texture2 = new Texture("Textures/texture1.jpg");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture2.Width, texture2.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture2.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            #endregion
        };

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            GL.ClearColor(0.3f, 0.2f, 0.5f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderPrograms[0].Id);
            GL.BindVertexArray(VAOs[5]);

            #region трансформации
            Matrix4 model = Matrix4.Identity;
            Vector3 vector3 = new Vector3(0.3f, -0.3f, 0.0f);
            Matrix4.CreateTranslation(in vector3, out model);

            float val3 =(float)Math.Sin(GetTimeInFloat())/2f+0.5f;
            
            Matrix4.CreateRotationZ(val3, out model);

            int matrixLocation = GL.GetUniformLocation(shaderPrograms[0].Id, "model");
            GL.UniformMatrix4(matrixLocation, false, ref model);
            #endregion

            #region текстуры
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);
            GL.Uniform1(GL.GetUniformLocation(shaderPrograms[0].Id, "ourTexture1"), 0);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, textures[1]);
            GL.Uniform1(GL.GetUniformLocation(shaderPrograms[0].Id, "ourTexture2"), 1);

            #endregion

            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.BindVertexArray(0);

            window.SwapBuffers();
        };


        window.Run();
        
    }
    public static double GetTimeInFloat()
    {
        var time = DateTime.Now;
        return (double)((time.Minute*60 + time.Second) * 1000 + time.Millisecond) / 1000.0;
    }
}