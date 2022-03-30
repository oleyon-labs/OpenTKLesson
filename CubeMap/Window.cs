using OpenTKExtension;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using LearnOpenTK.Common;

namespace LearnOpenTK
{
    // In this tutorial we set up some basic lighting and look at how the phong model works
    // For more insight into how it all works look at the web version. If you are just here for the source,
    // most of the changes are in the shaders, specifically most of the changes are in the fragment shader as this is
    // where the lighting calculations happens.
    public class Window : GameWindow
    {
        // Here we now have added the normals of the vertices
        // Remember to define the layouts to the VAO's
        private readonly float[] _vertices =
        {
             // Position          Normal
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
        };

        private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);

        private int[] textures = new int[1];

        private int _vertexBufferObject;

        private int[] _vertexBufferObjects = new int[3]; // vertices, normals, texture coordinates

        private int _elementBufferObject;

        private int _vaoModel;

        private int _vaoLamp;

        private Shader _lampShader;

        private Shader _lightingShader;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        Simple3DObject susanne;
        float[] susanneVertices;
        float[] susanneNormals;
        float[] susanneTextures;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Directory.SetCurrentDirectory("../../../");
            susanne = new Simple3DObject("objects/untitled.obj");
            (susanneVertices, susanneTextures, susanneNormals) = susanne.ConvertWithoutIndices();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");



            //lamp data
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


            {
                /*
                #region susanne object
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);



                _vertexBufferObjects[0] = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[0]);
                GL.BufferData(BufferTarget.ArrayBuffer, susanne.Vertices.Length * sizeof(float), susanne.Vertices, BufferUsageHint.StaticDraw);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                // Remember to change the stride as we now have 6 floats per vertex
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 0, 0);


                _vertexBufferObjects[1] = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[1]);
                GL.BufferData(BufferTarget.ArrayBuffer, susanne.Normals.Length * sizeof(float), susanne.Normals, BufferUsageHint.StaticDraw);

                // We now need to define the layout of the normal so the shader can use it
                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 0, 0);



                _vertexBufferObjects[2] = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[2]);
                GL.BufferData(BufferTarget.ArrayBuffer, susanne.Textures.Length * sizeof(float), susanne.Textures, BufferUsageHint.StaticDraw);

                var textureLocation = _lightingShader.GetAttribLocation("aTexture");
                GL.EnableVertexAttribArray(textureLocation);
                GL.VertexAttribPointer(textureLocation, 3, VertexAttribPointerType.Float, false, 0, 0);


                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, susanne.VerticesIndices.Length * sizeof(float), susanne.VerticesIndices, BufferUsageHint.StaticDraw);


                GL.BindVertexArray(0);

                #endregion
                */
            }

            #region susanne 2
            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);



            _vertexBufferObjects[0] = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, susanneVertices.Length * sizeof(float), susanneVertices, BufferUsageHint.StaticDraw);

            var positionLocation = _lightingShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            // Remember to change the stride as we now have 6 floats per vertex
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 0, 0);


            _vertexBufferObjects[1] = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, susanneNormals.Length * sizeof(float), susanneNormals, BufferUsageHint.StaticDraw);

            // We now need to define the layout of the normal so the shader can use it
            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 0, 0);



            _vertexBufferObjects[2] = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjects[2]);
            GL.BufferData(BufferTarget.ArrayBuffer, susanneTextures.Length * sizeof(float), susanneTextures, BufferUsageHint.StaticDraw);

            var textureLocation = _lightingShader.GetAttribLocation("aTexture");
            GL.EnableVertexAttribArray(textureLocation);
            GL.VertexAttribPointer(textureLocation, 2, VertexAttribPointerType.Float, false, 0, 0);


            GL.BindVertexArray(0);

            #endregion




            #region texures
            textures[0] = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            _Texture texture1 = new _Texture("textures/Untitled.002.png");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture1.Width, texture1.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture1.PixelData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            #endregion

            //lamp data
            {
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                var positionLampLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLampLocation);
                // Also change the stride here as we now have 6 floats per vertex. Now we don't define the normal for the lamp VAO
                // this is because it isn't used, it might seem like a waste to use the same VBO if they dont have the same data
                // The two cubes still use the same position, and since the position is already in the graphics memory it is actually
                // better to do it this way. Look through the web version for a much better understanding of this.
                GL.VertexAttribPointer(positionLampLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            }

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorGrabbed = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel);

            _lightingShader.Use();

            _lightingShader.SetMatrix4("model", Matrix4.Identity);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textures[0]);
            //GL.Uniform1(GL.GetUniformLocation(shaderPrograms[0].Id, "ourTexture1"), 0);


            GL.DrawArrays(PrimitiveType.Triangles, 0, susanneVertices.Length);
            //GL.DrawElements(BeginMode.Triangles, susanne.VerticesIndices.Length, DrawElementsType.UnsignedInt, 0);



            GL.BindVertexArray(_vaoLamp);

            _lampShader.Use();

            Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

            _lampShader.SetMatrix4("model", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }
}