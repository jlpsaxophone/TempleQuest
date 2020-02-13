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
    public class Wall
    {
        TempleQuest game;
        public BoundingRectangle bounds = new BoundingRectangle();
        Texture2D texture;

        public Wall(TempleQuest game, int bound1, int bound2)
        {
            this.game = game;
            this.bounds.X = bound1;
            this.bounds.Y = bound2;
        }

        public void LoadContent(ContentManager content)
        {         
            texture = content.Load<Texture2D>("wall");
            bounds.Width = 168;
            bounds.Height = 168;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
