using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TempleQuest
{
    public class Fireball
    {
        TempleQuest game;
        GraphicsDeviceManager graphics;

        TimeSpan timer;
        int frame;

        public BoundingRectangle bounds = new BoundingRectangle();

        Texture2D texture;

        public Vector2 velocity;

        const int ANIMATION_FRAME_RATE = 124;

        public Fireball(TempleQuest game, GraphicsDeviceManager graphics, double vector1, double vector2, float x, float y)
        {
            this.game = game;
            this.graphics = graphics;
            velocity = new Vector2(
                           (float)(vector1),
                           (float)(vector2)
                       );
            //shrinks it to unit vector
            velocity.Normalize();
            bounds.X = x;
            bounds.Y = y;
            timer = new TimeSpan(0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("fireball");
            bounds.Width = 16;
            bounds.Height = 16;
        }

        public void Update(GameTime timeSpan)
        {
            float speed = 500 * (float)timeSpan.ElapsedGameTime.TotalSeconds;
            Vector2 temp = new Vector2(bounds.X, bounds.Y);
            temp += speed * (velocity);
            bounds.X = temp.X;
            bounds.Y = temp.Y;

            if (bounds.Y < 0)
            {
                velocity.Y *= -1;
                float delta = 0 - bounds.Y;
                bounds.Y += 2 * delta;
            }

            if (bounds.Y > graphics.PreferredBackBufferHeight - 16)
            {
                velocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 16 - bounds.Y;
                bounds.Y += 2 * delta;
            }

            if (bounds.X < 0)
            {
                velocity.X *= -1;
                float delta = 0 - bounds.X;
                bounds.X += 2 * delta;
            }

            if (bounds.X > graphics.PreferredBackBufferWidth - 16)
            {
                velocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 16 - bounds.X;
                bounds.X += 2 * delta;
            }
            timer += timeSpan.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var destination = new Rectangle((int)bounds.X, (int)bounds.Y, 32, 32);
            var source = new Rectangle(
            frame * 16, // X value 
            0, // Y value
            16, // Width 
            16 // Height
            );
            // render the sprite
            spriteBatch.Draw(texture, destination, source, Color.White);
        }
    }
}
