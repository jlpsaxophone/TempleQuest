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
    public class Level0
    {
        SpriteFont text; 

        public Level0(ContentManager content)
        {
            text = content.Load<SpriteFont>("level0");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                text,
                "Welcome to Temple Quest! Your task is to collect precious rubies in a dangerous dungeon full of monsters.",
                new Vector2(200, 200),
                Color.Black
                );
            spriteBatch.DrawString(
                text,
                "Collect rubies and avoid contact with bats, skeletons, wizards, and the powerful dragon!",
                new Vector2(200, 300),
                Color.Black
                );
            spriteBatch.DrawString(
                text,
                "Wizards and the dragon will shoot fireballs onto the level, so always be on the lookout for fast moving projectiles.",
                new Vector2(200, 400),
                Color.Black
                );
            spriteBatch.DrawString(
                text,
                "Collect three gems on each of the three levels to win. Good luck to you, brave and noble knight!",
                new Vector2(200, 500),
                Color.Black
                );
            spriteBatch.DrawString(
                text,
                "Press enter to continue!",
                new Vector2(200, 500),
                Color.Black
                );
        }
    }
}
