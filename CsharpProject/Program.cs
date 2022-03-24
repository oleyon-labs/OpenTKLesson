using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Common;
using static MyApp.Program;
using OpenTK.Mathematics;
using OpenTKExtension;
using System.Globalization;

namespace Praktika;


class Program
{
    static void Main(string[] args)
    {
        Directory.SetCurrentDirectory("../../../");
        Console.WriteLine(Directory.GetCurrentDirectory());



        Simple3DObject simple3DObject = new Simple3DObject("2D figure 4.obj");



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


        _ShaderProgram[] shaderPrograms =
        {
            null, null, null
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




        #region координаты куба

        float[] g_vertex_buffer_data = {
            -0.5f,-0.5f,-0.5f, // Треугольник 1 : начало
			-0.5f,-0.5f, 0.5f,
            -0.5f, 0.5f, 0.5f, // Треугольник 1 : конец
			 0.5f, 0.5f,-0.5f, // Треугольник 2 : начало
			-0.5f,-0.5f,-0.5f,
            -0.5f, 0.5f,-0.5f, // Треугольник 2 : конец
			 0.5f,-0.5f, 0.5f,
            -0.5f,-0.5f,-0.5f,
             0.5f,-0.5f,-0.5f,
             0.5f, 0.5f,-0.5f,
             0.5f,-0.5f,-0.5f,
            -0.5f,-0.5f,-0.5f,
            -0.5f,-0.5f,-0.5f,
            -0.5f, 0.5f, 0.5f,
            -0.5f, 0.5f,-0.5f,
             0.5f,-0.5f, 0.5f,
            -0.5f,-0.5f, 0.5f,
            -0.5f,-0.5f,-0.5f,
            -0.5f, 0.5f, 0.5f,
            -0.5f,-0.5f, 0.5f,
             0.5f,-0.5f, 0.5f,
             0.5f, 0.5f, 0.5f,
             0.5f,-0.5f,-0.5f,
             0.5f, 0.5f,-0.5f,
             0.5f,-0.5f,-0.5f,
             0.5f, 0.5f, 0.5f,
             0.5f,-0.5f, 0.5f,
             0.5f, 0.5f, 0.5f,
             0.5f, 0.5f,-0.5f,
            -0.5f, 0.5f,-0.5f,
             0.5f, 0.5f, 0.5f,
            -0.5f, 0.5f,-0.5f,
            -0.5f, 0.5f, 0.5f,
             0.5f, 0.5f, 0.5f,
            -0.5f, 0.5f, 0.5f,
             0.5f,-0.5f, 0.5f
        };
        float[] g_color_buffer_data = {
            0.583f,  0.771f,  0.014f,
            0.609f,  0.115f,  0.436f,
            0.327f,  0.483f,  0.844f,
            0.822f,  0.569f,  0.201f,
            0.435f,  0.602f,  0.223f,
            0.310f,  0.747f,  0.185f,
            0.597f,  0.770f,  0.761f,
            0.559f,  0.436f,  0.730f,
            0.359f,  0.583f,  0.152f,
            0.483f,  0.596f,  0.789f,
            0.559f,  0.861f,  0.639f,
            0.195f,  0.548f,  0.859f,
            0.014f,  0.184f,  0.576f,
            0.771f,  0.328f,  0.970f,
            0.406f,  0.615f,  0.116f,
            0.676f,  0.977f,  0.133f,
            0.971f,  0.572f,  0.833f,
            0.140f,  0.616f,  0.489f,
            0.997f,  0.513f,  0.064f,
            0.945f,  0.719f,  0.592f,
            0.543f,  0.021f,  0.978f,
            0.279f,  0.317f,  0.505f,
            0.167f,  0.620f,  0.077f,
            0.347f,  0.857f,  0.137f,
            0.055f,  0.953f,  0.042f,
            0.714f,  0.505f,  0.345f,
            0.783f,  0.290f,  0.734f,
            0.722f,  0.645f,  0.174f,
            0.302f,  0.455f,  0.848f,
            0.225f,  0.587f,  0.040f,
            0.517f,  0.713f,  0.338f,
            0.053f,  0.959f,  0.120f,
            0.393f,  0.621f,  0.362f,
            0.673f,  0.211f,  0.457f,
            0.820f,  0.883f,  0.371f,
            0.982f,  0.099f,  0.879f
        };

        #endregion





        #region для куба
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);

        int matrixID = 0;

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 4.0f / 3.0f, 0.1f, 100f);
        //projection = Matrix4.Crea
        Matrix4 view = Matrix4.LookAt(new Vector3(4, 3, -3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix4 Model = Matrix4.Identity;
        Matrix4 MVP = projection * view * Model;
        MVP = Model * view * projection;
        #endregion





        window.Load += () =>
        {
            shaderPrograms[0] = _ShaderProgram.LoadShaderProgram("vertex_shader3.glsl", "fragment_shader3.glsl");
            shaderPrograms[1] = _ShaderProgram.LoadShaderProgram("VS.glsl", "FS2.glsl");
            shaderPrograms[2] = _ShaderProgram.LoadShaderProgram("VSCube.glsl", "FSCube.glsl");
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

            _Texture texture1 = new _Texture("Textures/texture.png");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture1.Width, texture1.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture1.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);





            textures[1] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[1]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            _Texture texture2 = new _Texture("Textures/texture1.jpg");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture2.Width, texture2.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture2.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            #endregion



            matrixID = GL.GetUniformLocation(shaderPrograms[2].Id, "MVP");

            
            VAOs[6] = GL.GenVertexArray();

            //EBOs[6] = GL.GenBuffer();
            GL.BindVertexArray(VAOs[6]);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[5]);

            //GL.BufferData(BufferTarget.ElementArrayBuffer, 4 * 3 * 2, indices, BufferUsageHint.StaticDraw);
            VBOs[6] = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[6]);
            GL.BufferData(BufferTarget.ArrayBuffer, g_vertex_buffer_data.Length*4, g_vertex_buffer_data, BufferUsageHint.StaticDraw);

            VBOs[7] = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[7]);
            GL.BufferData(BufferTarget.ArrayBuffer, g_color_buffer_data.Length * 4, g_color_buffer_data, BufferUsageHint.StaticDraw);



            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[6]);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[7]);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
            //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, 3 * 4);
            //GL.EnableVertexAttribArray(1);

            //GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, (3 + 3 + 2) * 4, (3 + 3) * 4);
            //GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);



        };

        window.RenderFrame += (FrameEventArgs frameEventArgs) =>
        {
            GL.ClearColor(0.3f, 0.2f, 0.5f, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaderPrograms[2].Id);
            GL.BindVertexArray(VAOs[6]);

            matrixID = GL.GetUniformLocation(shaderPrograms[2].Id, "MVP");
            GL.UniformMatrix4(matrixID, false, ref MVP);
            /*
            #region трансформации
            Matrix4 identity = Matrix4.Identity;
            Matrix4 model = Matrix4.Identity;
            Vector3 vector3 = new Vector3(0.3f, -0.3f, 0.0f);
            Matrix4 translation = Matrix4.CreateTranslation(vector3);
            

            //float val3 =(float)Math.Sin(GetTimeInFloat())/2f+0.5f;
            float val3 = (float)GetTimeInFloat() / 2f + 0.5f;
            Matrix4 rotation = Matrix4.CreateRotationZ(val3);
            model = model * rotation * translation;


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
            */
            //GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 12 * 3);

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