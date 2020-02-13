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
    public class Wizard
    {
        TempleQuest game;
        Random random;
        public BoundingRectangle bounds = new BoundingRectangle();
        Texture2D texture;
        public Fireball fireball;

        public Wizard(TempleQuest game, Player player, int bound1, int bound2, Random random)
        {
            this.game = game;
            this.bounds.X = bound1;
            this.bounds.Y = bound2;
            this.random = random;
            fireball = new Fireball(game, game.graphics, random.NextDouble(), random.NextDouble(), bounds.X, bounds.Y);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("fire_wizard");
            bounds.Width = 72;
            bounds.Height = 108;
            fireball.LoadContent(content);
        }

        public void Update(GameTime timespan)
        {
            fireball.Update(timespan);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            fireball.Draw(spriteBatch);
        }
    }
}
