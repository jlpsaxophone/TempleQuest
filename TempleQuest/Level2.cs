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
    public class Level2
    {
        Random random = new Random();
        public int lives = 3;

        TempleQuest game;
        public Player player;
        Wall wall1;
        Wall wall2;
        Wall wall3;
        Wall wall4;
        Wall wall5;
        Wall wall6;
        Wall wall7;
        Wall wall8;
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
        Bat bat1;
        Wizard wizard1;
        Wizard wizard2;

        public Level2(TempleQuest game)
        {
            this.game = game;
            wall1 = new Wall(game, 0, 650);
            wall2 = new Wall(game, 168, 650);
            wall3 = new Wall(game, 168, 482);
            wall4 = new Wall(game, 524, 200);
            wall5 = new Wall(game, 0, 150);
            wall6 = new Wall(game, 524, 600);
            wall7 = new Wall(game, 950, 320);
            wall8 = new Wall(game, 950, 488);

            walls = new Wall[] { wall1, wall2, wall3, wall4, wall5, wall6, wall7, wall8 };
            player = new Player(game);
            ruby1 = new Ruby(game, 67, 55);
            ruby2 = new Ruby(game, 243, 860);
            ruby3 = new Ruby(game, 1200, 465);
            life1 = new Heart(game, 10, 914);
            life2 = new Heart(game, 70, 914);
            life3 = new Heart(game, 130, 914);
            skeleton1 = new Skeleton(game, 390, 500, true, 855, 70);
            skeleton2 = new Skeleton(game, 800, 500, false, 850, 400);
            bat1 = new Bat(game, game.graphics, random.NextDouble(), random.NextDouble(), 1200, 0);
            wizard1 = new Wizard(game, player, 780, 10, random);
            wizard2 = new Wizard(game, player, 780, 848, random);
        }

        public void LoadContent(ContentManager content)
        {
            wall1.LoadContent(content);
            wall2.LoadContent(content);
            wall3.LoadContent(content);
            wall4.LoadContent(content);
            wall5.LoadContent(content);
            wall6.LoadContent(content);
            wall7.LoadContent(content);
            wall8.LoadContent(content);

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
            bat1.LoadContent(content);
            wizard1.LoadContent(content);
            wizard2.LoadContent(content);
        }

        public void Update(GameTime timeSpan)
        {
            player.Update(timeSpan);
            skeleton1.Update(timeSpan);
            skeleton2.Update(timeSpan);
            bat1.Update(timeSpan);
            wizard1.Update(timeSpan);
            wizard2.Update(timeSpan);
            Collisions.CheckWallCollision(player, walls);

            if (Collisions.CollidesWith(player.bounds, skeleton1.bounds)
                || Collisions.CollidesWith(player.bounds, skeleton2.bounds)
                || Collisions.CollidesWith(player.bounds, bat1.bounds)
                || Collisions.CollidesWith(player.bounds, wizard1.bounds)
                || Collisions.CollidesWith(player.bounds, wizard2.bounds)
                || Collisions.CollidesWith(player.bounds, wizard1.fireball.bounds)
                || Collisions.CollidesWith(player.bounds, wizard2.fireball.bounds))
            {
                hurt.Play();
                player.bounds.X = 55;
                player.bounds.Y = 550;
                bat1.bounds.X = 1200;
                bat1.bounds.Y = 0;
                wizard1.fireball.bounds.X = wizard1.bounds.X;
                wizard1.fireball.bounds.Y = wizard1.bounds.Y;
                wizard2.fireball.bounds.X = wizard2.bounds.X;
                wizard2.fireball.bounds.Y = wizard2.bounds.Y;
                lives--;
            }

            if (Collisions.CollidesWith(player.bounds, ruby1.bounds))
            {
                if (!ruby1collected)
                {
                    ruby_collect.Play();
                }
                ruby1collected = true;
            }
            if (Collisions.CollidesWith(player.bounds, ruby2.bounds))
            {
                if (!ruby2collected)
                {
                    ruby_collect.Play();
                }
                ruby2collected = true;
            }
            if (Collisions.CollidesWith(player.bounds, ruby3.bounds))
            {
                if (!ruby3collected)
                {
                    ruby_collect.Play();
                }
                ruby3collected = true;
            }
            if (ruby1collected && ruby2collected && ruby3collected)
            {
                player.bounds.Height = 0;
                player.bounds.Width = 0;
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
            wall7.Draw(spriteBatch);
            wall8.Draw(spriteBatch);

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

            skeleton1.Draw(spriteBatch);
            skeleton2.Draw(spriteBatch);
            bat1.Draw(spriteBatch);
            wizard1.Draw(spriteBatch);
            wizard2.Draw(spriteBatch);

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
        }
    }
}
