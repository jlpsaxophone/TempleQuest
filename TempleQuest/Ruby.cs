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
    public class Ruby
    {
        TempleQuest game;

        public BoundingRectangle bounds = new BoundingRectangle();
        Texture2D texture;

        public Ruby(TempleQuest game, int bound1, int bound2)
        {
            this.game = game;
            this.bounds.X = bound1;
            this.bounds.Y = bound2;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ruby");
            bounds.Width = 32;
            bounds.Height = 50;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
