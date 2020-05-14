using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    public class Boll : Sprite
    {
        private float Timer = 0f; // Ökar farten med tiden
        private Vector2? startPos = null;
        private float? startSpeed;
        private bool Spelar;

        public Poäng Poäng;
        public int fartÖkning = 10; // Hur ofta farten ökar

        public Boll(Texture2D Texture)
            : base(Texture)
        {
            speed = 3f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if(startPos == null)
            {
                startPos = Pos;
                startSpeed = speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Spelar = true;

            if (!Spelar)
                return;

            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Timer > fartÖkning)
            {
                speed++;
                Timer = 0;
            }

            foreach(var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Fart.X > 0 && this.KollisionVänster(sprite))
                    this.Fart.X = -this.Fart.X;
                if (this.Fart.X < 0 && this.KollisionHöger(sprite))
                    this.Fart.X = -this.Fart.X;
                if (this.Fart.Y > 0 && this.KollisionTopp(sprite))
                    this.Fart.Y = -this.Fart.Y;
                if (this.Fart.Y < 0 && this.KollisionNere(sprite))
                    this.Fart.Y = -this.Fart.Y;
            }

            if (Pos.Y <= 0 || Pos.Y + Texture.Height >= Game1.screenHöjd)
                Fart.Y = -Fart.Y;

            if(Pos.X + Texture.Width <= 0)
            {
                Restart();
            }

            if (Pos.X + Texture.Width >= Game1.screenBredd || Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Restart();
            }

            Pos += Fart * speed;
        }

        // Restart
        public void Restart()
        {
            var riktning = Game1.rand.Next(0, 4); // Den random riktning bollen börjar med

            switch (riktning)
            {
                case 0:
                    Fart = new Vector2(1, 1);
                    break;
                case 1:
                    Fart = new Vector2(1, -1);
                    break;
                case 2:
                    Fart = new Vector2(-1, -1);
                    break;
                case 3:
                    Fart = new Vector2(-1, 1);
                    break;
            }

            // Allt resettas
            Pos = (Vector2)startPos;
            speed = (float)startSpeed;
            Timer = 0;
            Spelar = false;
        }
    }
}
