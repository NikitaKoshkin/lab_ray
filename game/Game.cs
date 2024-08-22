using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Pacman;
using Pacman.Graphics;
using Pacman.Objects;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using OpenTK.Compute.OpenCL;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;


namespace openTK_game
{

    internal class Game : GameWindow
    {

        // create a obj
        M obj;


        ShaderProgram program;
        
        // camera
        Camera camera;



        // width and height of screen
        int width, height;

        // Constructor that sets the width, height, and calls the base constructor (GameWindow's Constructor) with default args
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;
            //center the window on monitor
            CenterWindow(new Vector2i(width, height));
        }

        // called whenever window is resized
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        // called once when game is started
        protected override void OnLoad()
        {
            base.OnLoad();

            obj = new M(new Vector3(0f, 0f, 0));
            program = new ShaderProgram("Default.vert", "Default.frag");



            GL.Enable(EnableCap.DepthTest);
            camera = new Camera(width, height, new Vector3(0.048f * 14, 0.048f * 15.5f, 2.5f));
            CursorState = CursorState.Grabbed;
        }

        // called once when game is closed
        protected override void OnUnload()
        {
            base.OnUnload();
            obj.Delete();
            program.Delete();

            // Delete, VAO, VBO, Shader Program
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Set the color to fill the screen with
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            // Fill the screen with the color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);
            //program.Bind();
            obj.Render(program);

            // swap the buffers
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        // called every frame. All updating happens here
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);
        }
      
    }
}