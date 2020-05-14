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
    class Player : Sprite
    {
        public Player(Texture2D Texture)
           : base(Texture)
        {
            speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Input == null)
                throw new Exception("ge värde till 'input'");

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Fart.Y = -speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Fart.Y = speed;

            Pos += Fart;

            Pos.Y = MathHelper.Clamp(Pos.Y, 0, Game1.screenHöjd - Texture.Height);

            Fart = Vector2.Zero;
        }
    }
}
