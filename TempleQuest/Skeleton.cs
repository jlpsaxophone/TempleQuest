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
    public class Skeleton
    {
        TempleQuest game;
        public BoundingRectangle bounds = new BoundingRectangle();
        const int ANIMATION_FRAME_RATE = 124;
        Texture2D texture;
        bool up;
        bool direction = true;
        int right_left=0;
        float furthest;
        float closest;

        TimeSpan timer;
        int frame;
        //arbitrary value to pass to Draw overload
        Vector2 temp;

        public Skeleton(TempleQuest game, float x, float y, bool up, float furthest, float closest)
        {
            this.game = game;
            bounds.X = x;
            bounds.Y = y;
            this.up = up;
            this.furthest = furthest;
            this.closest = closest;
            timer = new TimeSpan(0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("skeleton_running");
            bounds.Width = 84;
            bounds.Height = 92;
        }

        public void Update(GameTime timeSpan)
        {
            float delta = (float)timeSpan.ElapsedGameTime.TotalSeconds;
            if (up)
            {
                if (direction)
                {
                    if (bounds.Y > furthest)
                    {
                        direction = !direction;
                    }
                    else
                    {
                        bounds.Y += delta * 160;
                    }
                }
                else
                {
                    if (bounds.Y < closest)
                    {
                        direction = !direction;
                    }
                    else
                    {
                        bounds.Y -= delta * 160;
                    }
                }
            }
            else
            {
                if (direction)
                {
                    if (bounds.X > furthest)
                    {
                        direction = !direction;
                        if(right_left == 0)
                        {
                            right_left = 1;
                        }
                        else
                        {
                            right_left = 0;
                        }
                    }
                    else
                    {
                        bounds.X += delta * 160;
                    }
                }
                else
                {
                    if (bounds.X < closest)
                    {
                        direction = !direction;
                        if (right_left == 0)
                        {
                            right_left = 1;
                        }
                        else
                        {
                            right_left = 0;
                        }
                    }
                    else
                    {
                        bounds.X -= delta * 160;
                    }
                }
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
            frame %= 6;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(right_left == 0)
            {
                var destination2 = new Rectangle((int)bounds.X, (int)bounds.Y, 84, 92);
                var source2 = new Rectangle(
                frame * 42, // X value 
                0, // Y value
                42, // Width 
                46 // Height
                );
                // render the sprite
                spriteBatch.Draw(texture, destination2, source2, Color.White);
            }
            else
            {
                var destination2 = new Rectangle((int)bounds.X, (int)bounds.Y, 84, 92);
                var source2 = new Rectangle(
                frame * 42, // X value 
                0, // Y value
                42, // Width 
                46 // Height
                );
                // render the sprite               
                spriteBatch.Draw(texture, destination2, source2, Color.White, 0, temp, SpriteEffects.FlipHorizontally, 1);
            }
        }
    }
}

