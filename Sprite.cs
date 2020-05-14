using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Modeller;

namespace Pong.Sprites
{
    public class Sprite
    {
        protected Texture2D Texture;

        public Vector2 Pos;
        public Vector2 Fart;
        public float speed;
        public Input Input;

        public Rectangle Rec
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, Texture.Width, Texture.Height);
            }
        }

        public Sprite(Texture2D newTexture)
        {
            Texture = newTexture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Pos, Color.White);
        }


        // Kollision
        protected bool KollisionVänster(Sprite sprite)
        {
            return this.Rec.Right + this.Fart.X > sprite.Rec.Left &&
                   this.Rec.Left < sprite.Rec.Left &&
                   this.Rec.Bottom > sprite.Rec.Top &&
                   this.Rec.Top < sprite.Rec.Bottom;
        }

        protected bool KollisionHöger(Sprite sprite)
        {
            return this.Rec.Left + this.Fart.X < sprite.Rec.Right &&
                   this.Rec.Right > sprite.Rec.Right &&
                   this.Rec.Bottom > sprite.Rec.Top &&
                   this.Rec.Top < sprite.Rec.Bottom;
        }

        protected bool KollisionTopp(Sprite sprite)
        {
            return this.Rec.Bottom + this.Fart.Y > sprite.Rec.Top &&
                   this.Rec.Top < sprite.Rec.Top &&
                   this.Rec.Right > sprite.Rec.Left &&
                   this.Rec.Left < sprite.Rec.Right;
        }

        protected bool KollisionNere(Sprite sprite)
        {
            return this.Rec.Top + this.Fart.Y < sprite.Rec.Bottom &&
                   this.Rec.Bottom > sprite.Rec.Bottom &&
                   this.Rec.Right > sprite.Rec.Left &&
                   this.Rec.Left < sprite.Rec.Right;
        }
    }
}
