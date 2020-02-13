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
    public class Level1
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
        Wall wall9;
        Wall wall10;
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
        Skeleton skeleton3;
        Bat bat1;
        Bat bat2;

        public Level1(TempleQuest game)
        {
            this.game = game;
            wall1 = new Wall(game, 168, 168);
            wall2 = new Wall(game, 168, 336);
            wall3 = new Wall(game, 168, 504);
            wall4 = new Wall(game, 0, 336);
            wall5 = new Wall(game, 336, 336);
            wall6 = new Wall(game, 1134 , 168);
            wall7 = new Wall(game, 966, 168);
            wall8 = new Wall(game, 798, 168);
            wall9 = new Wall(game, 630, 792);
            wall10 = new Wall(game, 630, 624);
            walls = new Wall[] { wall1, wall2, wall3, wall4, wall5, wall6, wall7, wall8, wall9, wall10 };
            player = new Player(game);
            ruby1 = new Ruby(game, 67, 252);
            ruby2 = new Ruby(game, 1210, 65);
            ruby3 = new Ruby(game, 1100, 800);
            life1 = new Heart(game, 10, 914);
            life2 = new Heart(game, 70, 914);
            life3 = new Heart(game, 130, 914);
            skeleton1 = new Skeleton(game, 100, 40, false, 800, 100);
            skeleton2 = new Skeleton(game, 850, 630, true, 850, 470);
            skeleton3 = new Skeleton(game, 850, 380, false, 1100, 500);
            bat1 = new Bat(game, game.graphics, random.NextDouble(), random.NextDouble(), 1200, 0);
            bat2 = new Bat(game, game.graphics, random.NextDouble(), random.NextDouble(), 1200, 800);
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
            wall9.LoadContent(content);
            wall10.LoadContent(content);
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
            skeleton3.LoadContent(content);
            bat1.LoadContent(content);
            bat2.LoadContent(content);
        }

        public void Update(GameTime timeSpan)
        {
            player.Update(timeSpan);
            skeleton1.Update(timeSpan);
            skeleton2.Update(timeSpan);
            skeleton3.Update(timeSpan);
            bat1.Update(timeSpan);
            bat2.Update(timeSpan);
            Collisions.CheckWallCollision(player, walls);

            if (Collisions.CollidesWith(player.bounds, skeleton1.bounds)
                || Collisions.CollidesWith(player.bounds, skeleton2.bounds)
                || Collisions.CollidesWith(player.bounds, skeleton3.bounds)
                || Collisions.CollidesWith(player.bounds, bat1.bounds)
                || Collisions.CollidesWith(player.bounds, bat2.bounds))
            {
                hurt.Play();
                player.bounds.X = 55;
                player.bounds.Y = 550;
                bat1.bounds.X = 1200;
                bat1.bounds.Y = 0;
                bat2.bounds.X = 1200;
                bat2.bounds.Y = 800;
                lives--;
            }

                if (Collisions.CollidesWith(player.bounds, ruby1.bounds))
            {
                if(!ruby1collected)
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
                game.level = 2;             
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
            wall9.Draw(spriteBatch);
            wall10.Draw(spriteBatch);
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
            skeleton3.Draw(spriteBatch);
            bat1.Draw(spriteBatch);
            bat2.Draw(spriteBatch);

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
