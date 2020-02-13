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
    public class Dragon
    {
        TempleQuest game;
        GraphicsDeviceManager graphics;
        Random random;
        ContentManager content;

        TimeSpan timer;
        int frame;

        public BoundingRectangle bounds = new BoundingRectangle();

        Texture2D texture;

        public Vector2 velocity;

        const int ANIMATION_FRAME_RATE = 124;
        bool direction = true;
        public int level = 1;
        public Fireball fireball;
        public Fireball fireball2;
        public bool fireballloaded = false;
        public bool fireball2loaded = false;

        public Dragon(TempleQuest game, GraphicsDeviceManager graphics, double random1, double random2, float x, float y, Random random)
        {
            this.game = game;
            this.graphics = graphics;
            this.random = random;
            velocity = new Vector2(
                           (float)(random1),
                           (float)(random2)
                       );
            //shrinks it to unit vector (same speed, random direction)
            velocity.Normalize();
            bounds.X = x;
            bounds.Y = y;
            timer = new TimeSpan(0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("dragon_flying");
            bounds.Width = 192;
            bounds.Height = 180;
            this.content = content;         
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

            if (bounds.Y > graphics.PreferredBackBufferHeight - 180)
            {
                velocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 180 - bounds.Y;
                bounds.Y += 2 * delta;
            }

            if (bounds.X < 0)
            {
                velocity.X *= -1;
                float delta = 0 - bounds.X;
                bounds.X += 2 * delta;
                direction = false;
            }

            if (bounds.X > graphics.PreferredBackBufferWidth - 192)
            {
                velocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 192 - bounds.X;
                bounds.X += 2 * delta;
                direction = true;
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
            frame %= 4;

            if (level == 2)
            {
                if(!fireballloaded)
                {
                    fireball = new Fireball(game, game.graphics, random.NextDouble(), random.NextDouble(), bounds.X, bounds.Y);
                    fireball.LoadContent(content);
                    fireballloaded = true;
                }
                fireball.Update(timeSpan);
            }
            if (level == 3)
            {
                if (!fireball2loaded)
                {
                    fireball2 = new Fireball(game, game.graphics, random.NextDouble(), random.NextDouble(), bounds.X, bounds.Y);
                    fireball2.LoadContent(content);
                    fireball2loaded = true;
                }
                fireball.Update(timeSpan);
                fireball2.Update(timeSpan);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(direction)
            {
                Vector2 temp = new Vector2();
                var destination = new Rectangle((int)bounds.X, (int)bounds.Y, 192, 180);
                var source = new Rectangle(
                frame * 64, // X value 
                0, // Y value
                64, // Width 
                60 // Height
                );
                // render the sprite
                spriteBatch.Draw(texture, destination, source, Color.White, 0, temp, SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                var destination = new Rectangle((int)bounds.X, (int)bounds.Y, 192, 180);
                var source = new Rectangle(
                frame * 64, // X value 
                0, // Y value
                64, // Width 
                60 // Height
                );
                // render the sprite
                spriteBatch.Draw(texture, destination, source, Color.White);
            }
            if (level == 2 && fireballloaded)
            {
                fireball.Draw(spriteBatch);
            }
            if (level == 3 && fireball2loaded)
            {
                fireball.Draw(spriteBatch);
                fireball2.Draw(spriteBatch);
            }
        }
    }
}
