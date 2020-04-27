using ARPG.Game_States;
using ARPG.Meta.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARPG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<StateBase> states;

        public static Random Random;
        public static Camera NativeCamera;

        public static int WorldWidth = 320;
        public static int WorldHeight = 180;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Random = new Random();
            NativeCamera = new Camera(GraphicsDevice.Viewport);

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            states = new List<StateBase>()
            {
                new StatePlaying(this, Content)
            };
            getCurrentState().LoadContent();
        }

        protected override void UnloadContent()
        {
            getCurrentState().UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			var currentState = getCurrentState();

			currentState.Update(dt);
			currentState.PostUpdate(dt);

			NativeCamera.Update(dt);

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: NativeCamera.Transform);

            var currentState = getCurrentState();

            //============================[DRAW GAME]============================//

            currentState.Draw((float)gameTime.ElapsedGameTime.TotalSeconds, spriteBatch);

            //===================================================================//

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            //============================[DRAW GUI]=============================// 

            currentState.DrawGUI((float)gameTime.ElapsedGameTime.TotalSeconds, spriteBatch);

            //===================================================================//

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangeState(StateBase state)
        {
            getCurrentState().UnloadContent();
            states.Add(state);
            getCurrentState().LoadContent();

            NativeCamera.Reset();
        }

        private StateBase getCurrentState()
        {
            return states.LastOrDefault();
        }
    }
}
