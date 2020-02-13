using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TempleQuest
{
    enum State
    {
        Right = 1,
        Left = 2,
        Idle = 3,
    }
    public class Player
    {
        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;

        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 160;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 32;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 44;

        TempleQuest game;
        public BoundingRectangle bounds = new BoundingRectangle();
        Texture2D texture;
        State state;
        TimeSpan timer;
        int frame;
        int previousDirection;
        //arbitrary value to pass to Draw overload
        Vector2 temp;
        public Vector2 lastPosition;

        public Player(TempleQuest game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            bounds.X = 55;
            bounds.Y = 550;
            lastPosition.X = 55;
            lastPosition.Y = 550;
            state = State.Right;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player_running");
            bounds.Width = 64;
            bounds.Height = 88;
        }


        public void Update(GameTime timeSpan)
        {
            var newKeyboardState = Keyboard.GetState();
            float delta = (float)timeSpan.ElapsedGameTime.TotalSeconds;

            // Update the player state based on input
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                previousDirection = 1;
                state = State.Left;
                lastPosition.X = bounds.X;
                bounds.X -= delta * PLAYER_SPEED;
            }
            if (newKeyboardState.IsKeyDown(Keys.Up)) {
                if (previousDirection == 1) state = State.Left;
                else state = State.Right;
                lastPosition.Y = bounds.Y;
                bounds.Y -= delta * PLAYER_SPEED;
            }
            if (newKeyboardState.IsKeyDown(Keys.Down)) {
                if (previousDirection == 1) state = State.Left;
                else state = State.Right;
                lastPosition.Y = bounds.Y;
                bounds.Y += delta * PLAYER_SPEED;
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                previousDirection = 2;
                state = State.Right;
                lastPosition.X = bounds.X;
                bounds.X += delta * PLAYER_SPEED;
            }
            if(newKeyboardState.GetPressedKeyCount() == 0)
            {
                state = State.Idle;
            }
            

            // Update the player animation timer when the player is moving
            if (state != State.Idle) timer += timeSpan.ElapsedGameTime;

            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 6;

            if (bounds.Y < 0) bounds.Y = 0;
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height) bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            if (bounds.X < 0) bounds.X = 0;
            if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width) bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state == State.Idle)
            {
                if(previousDirection == 1)
                {
                    var destination1 = new Rectangle((int)bounds.X, (int)bounds.Y, 64, 88);
                    var source1 = new Rectangle(0, 0, 32, 44);
                    spriteBatch.Draw(texture, destination1, source1, Color.White, 0, temp, SpriteEffects.FlipHorizontally, 1);
                }
                else
                {
                    var destination1 = new Rectangle((int)bounds.X, (int)bounds.Y, 64, 88);
                    var source1 = new Rectangle(0, 0, 32, 44);
                    spriteBatch.Draw(texture, destination1, source1, Color.White);
                }
                
            }
            // determine the source rectagle of the sprite's current frame
            else if(state == State.Left)
            {
                var destination2 = new Rectangle((int)bounds.X, (int)bounds.Y, 64, 88);
                var source2 = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                0, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );
                // render the sprite               
                spriteBatch.Draw(texture, destination2, source2, Color.White, 0, temp, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                var destination2 = new Rectangle((int)bounds.X, (int)bounds.Y, 64, 88);
                var source2 = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                0, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );
                // render the sprite
                spriteBatch.Draw(texture, destination2, source2, Color.White);
            }            
        }
    }
}
