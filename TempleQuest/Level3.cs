using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace TempleQuest
{
    public class Level3
    {
        Random random = new Random();
        public int lives = 3;
        SpriteFont text;

        TempleQuest game;
        public Player player;
        Wall wall1;
        Wall wall2;
        Wall wall3;
        Wall wall4;
        Wall wall5;
        Wall wall6;
        Wall[] walls;
        Ruby ruby1;
        Ruby ruby2;
        Ruby ruby3;
        Heart life1;
        Heart life2;
        Heart life3;
        public bool ruby1collected = false;
        public bool ruby2collected = false;
        public bool ruby3collected = false;
        SoundEffect hurt;
        SoundEffect ruby_collect;
        Skeleton skeleton1;
        Skeleton skeleton2;
        Dragon dragon;
        public bool winlock = true;

        public Level3(TempleQuest game)
        {
            this.game = game;
            wall1 = new Wall(game, 0, 0);
            wall2 = new Wall(game, 0, 792);
            wall3 = new Wall(game, 1134, 0);
            wall4 = new Wall(game, 1134, 792);
            wall5 = new Wall(game, 950, 320);
            wall6 = new Wall(game, 950, 488);
            walls = new Wall[] { wall1, wall2, wall3, wall4, wall5, wall6 };
            player = new Player(game);
            ruby1 = new Ruby(game, 630, 20);
            ruby2 = new Ruby(game, 630, 890);
            ruby3 = new Ruby(game, 1200, 465);
            life1 = new Heart(game, 10, 914);
            life2 = new Heart(game, 70, 914);
            life3 = new Heart(game, 130, 914);
            skeleton1 = new Skeleton(game, 100, 100, false, 1015, 168);
            skeleton2 = new Skeleton(game, 900, 740, false, 1015, 168);
            dragon = new Dragon(game, game.graphics, random.NextDouble(), random.NextDouble(), 1110, 0, random);           
        }

        public void LoadContent(ContentManager content)
        {
            wall1.LoadContent(content);
            wall2.LoadContent(content);
            wall3.LoadContent(content);
            wall4.LoadContent(content);
            wall5.LoadContent(content);
            wall6.LoadContent(content);
            player.LoadContent(content);
            ruby1.LoadContent(content);
            ruby2.LoadContent(content);
            ruby3.LoadContent(content);
            life1.LoadContent(content);
            life2.LoadContent(content);
            life3.LoadContent(content);
            hurt = content.Load<SoundEffect>("hurt");
            ruby_collect = content.Load<SoundEffect>("ruby_pickup");
            skeleton1.LoadContent(content);
            skeleton2.LoadContent(content);
            dragon.LoadContent(content);
            text = content.Load<SpriteFont>("level0");
        }

        public void Update(GameTime timeSpan)
        {
            player.Update(timeSpan);
            skeleton1.Update(timeSpan);
            skeleton2.Update(timeSpan);
            dragon.Update(timeSpan);
            Collisions.CheckWallCollision(player, walls);

            if (Collisions.CollidesWith(player.bounds, skeleton1.bounds)
                || Collisions.CollidesWith(player.bounds, skeleton2.bounds)
                || Collisions.CollidesWith(player.bounds, dragon.bounds))
            {
                hurt.Play();
                player.bounds.X = 55;
                player.bounds.Y = 550;
                lives--;
            }
            if(dragon.fireballloaded)
            {
                if(Collisions.CollidesWith(player.bounds, dragon.fireball.bounds))
                {
                    hurt.Play();
                    player.bounds.X = 55;
                    player.bounds.Y = 550;
                    lives--;
                }
            }
            if (dragon.fireball2loaded)
            {
                if (Collisions.CollidesWith(player.bounds, dragon.fireball2.bounds))
                {
                    hurt.Play();
                    player.bounds.X = 55;
                    player.bounds.Y = 550;
                    lives--;
                }
            }

            if (Collisions.CollidesWith(player.bounds, ruby1.bounds))
            {
                if (!ruby1collected)
                {
                    ruby_collect.Play();
                    dragon.level++;
                }
                ruby1collected = true;
            }
            if (Collisions.CollidesWith(player.bounds, ruby2.bounds))
            {
                if (!ruby2collected)
                {
                    ruby_collect.Play();
                    dragon.level++;
                }
                ruby2collected = true;
            }
            if (Collisions.CollidesWith(player.bounds, ruby3.bounds))
            {
                if (!ruby3collected)
                {
                    ruby_collect.Play();
                    dragon.level++;
                }
                ruby3collected = true;                
            }
            if (ruby1collected && ruby2collected && ruby3collected)
            {
                game.level = 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            wall1.Draw(spriteBatch);
            wall2.Draw(spriteBatch);
            wall3.Draw(spriteBatch);
            wall4.Draw(spriteBatch);
            wall5.Draw(spriteBatch);
            wall6.Draw(spriteBatch);
            player.Draw(spriteBatch);
            if (!ruby1collected)
            {
                ruby1.Draw(spriteBatch);
            }
            if (!ruby2collected)
            {
                ruby2.Draw(spriteBatch);
            }
            if (!ruby3collected)
            {
                ruby3.Draw(spriteBatch);
            }
            if (ruby1collected && ruby2collected && ruby3collected)
            {
                player.bounds.Height = 0;
                player.bounds.Width = 0;
                winlock = false;
            }

            skeleton1.Draw(spriteBatch);
            skeleton2.Draw(spriteBatch);
            dragon.Draw(spriteBatch);
            if (lives == 3)
            {
                life3.Draw(spriteBatch);
            }
            if (lives >= 2)
            {
                life2.Draw(spriteBatch);
            }
            if (lives >= 1)
            {
                life1.Draw(spriteBatch);
            }
            if(winlock == false)
            {
                spriteBatch.DrawString(text, "Congratulations player! You have bested the dragon! Press Enter to play again or Escape to quit.", new Vector2(200, 200),
                Color.Black);
            }
        }
    }
}
