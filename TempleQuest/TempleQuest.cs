using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace TempleQuest
{
    public class TempleQuest : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;
        Random random = new Random();
        public int level = 0;

        Level0 level0;
        Level1 level1;
        Level2 level2;
        Level3 level3;

        public TempleQuest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            level1 = new Level1(this);
            level2 = new Level2(this);
            level3 = new Level3(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1302;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level0 = new Level0(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Escape)) Exit();

            if(level == 0)
            {
                if(newKeyboardState.IsKeyDown(Keys.Enter))
                {
                    level1.player.bounds.X = 55;
                    level1.player.bounds.Y = 550;
                    level1.lives = 3;
                    level2.lives = 3;
                    level3.lives = 3;
                    level = 1;
                    level1.LoadContent(Content);
                    level2.LoadContent(Content);
                    level3.LoadContent(Content);
                }
            }
            if(level == 1)
            {
                level1.Update(gameTime);
                if(level1.lives <= 0 )
                {
                    level1.ruby1collected = false;
                    level1.ruby2collected = false;
                    level1.ruby3collected = false;
                    level2.ruby1collected = false;
                    level2.ruby2collected = false;
                    level2.ruby3collected = false;
                    level3.ruby1collected = false;
                    level3.ruby2collected = false;
                    level3.ruby3collected = false;
                    level = 0;
                }
            }
            if (level == 2)
            {
                level2.Update(gameTime);
                if (level2.lives <= 0)
                {
                    level1.ruby1collected = false;
                    level1.ruby2collected = false;
                    level1.ruby3collected = false;
                    level2.ruby1collected = false;
                    level2.ruby2collected = false;
                    level2.ruby3collected = false;
                    level3.ruby1collected = false;
                    level3.ruby2collected = false;
                    level3.ruby3collected = false;
                    level = 0;
                }
            }
            if (level == 3)
            {
                level3.Update(gameTime);
                if (level3.lives <= 0)
                {
                    level1.ruby1collected = false;
                    level1.ruby2collected = false;
                    level1.ruby3collected = false;
                    level2.ruby1collected = false;
                    level2.ruby2collected = false;
                    level2.ruby3collected = false;
                    level3.ruby1collected = false;
                    level3.ruby2collected = false;
                    level3.ruby3collected = false;
                    level = 0;                   
                }
                if (level3.winlock == false)
                {
                    if (newKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        level1.ruby1collected = false;
                        level1.ruby2collected = false;
                        level1.ruby3collected = false;
                        level2.ruby1collected = false;
                        level2.ruby2collected = false;
                        level2.ruby3collected = false;
                        level3.ruby1collected = false;
                        level3.ruby2collected = false;
                        level3.ruby3collected = false;
                        level = 0;
                        level3.winlock = true;
                    }
                }
            }        

            base.Update(gameTime);

            oldKeyboardState = newKeyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            spriteBatch.Begin();
            if(level == 0)
            {
                level0.Draw(spriteBatch);
            }
            if(level == 1)
            {
                level1.Draw(spriteBatch);
            }
            if (level == 2)
            {
                level2.Draw(spriteBatch);
            }
            if (level == 3)
            {
                level3.Draw(spriteBatch);
            }            
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
