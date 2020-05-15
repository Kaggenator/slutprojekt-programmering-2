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
        public bool Spelar;

        public Poäng Poäng;
        public int fartÖkning = 10; // Hur ofta farten ökar

        public Boll(Texture2D Texture)
            : base(Texture)
        {
            speed = 3f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Restartar spelet om du trycker Escape
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Restart();

            if (startPos == null)
            {
                startPos = Pos;
                startSpeed = speed;

                Reset();
            }

            // Starta bollen genom att trycka Space
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Spelar = true;

            if (!Spelar)
                return;

            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Om timern är högre än fartökningsvariabeln ökar farten med ett f
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

            if (Pos.Y <= 0 || Pos.Y + Texture.Height >= Game1.screenHöjd) // Studsar bollen om den träffar kanterna i y-led
                Fart.Y = -Fart.Y;

            if(Pos.X + Texture.Width <= 0) // Resettar bollen om player 2 gör mål
            {
                Poäng.Poäng2++;
                Reset();
            }

            if (Pos.X + Texture.Width >= Game1.screenBredd) // Resettar bollen om player 1 gör mål
            {
                Poäng.Poäng1++;
                Reset();
            }

            Pos += Fart * speed;
        }

        // Reset
        public void Reset()
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

            // Allt resettas förutom score
            Pos = (Vector2)startPos;
            speed = (float)startSpeed;
            Timer = 0;
            Spelar = false;
        }

        // Restartar spelet
        public void Restart()
        {
            Reset();
            Poäng.Poäng1 = 0;
            Poäng.Poäng2 = 0;   
        }
    }
}
