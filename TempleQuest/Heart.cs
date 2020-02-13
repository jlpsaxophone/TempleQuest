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
    public class Heart
    {
        TempleQuest game;

        public BoundingRectangle bounds = new BoundingRectangle();
        Texture2D texture;

        public Heart(TempleQuest game, int bound1, int bound2)
        {
            this.game = game;
            bounds.X = bound1;
            bounds.Y = bound2;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("heart");
            bounds.Width = 52;
            bounds.Height = 36;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
