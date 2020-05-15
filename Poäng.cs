using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Poäng
    {
        public int Poäng1;
        public int Poäng2;

        private SpriteFont Font;
        private Texture2D texture2D;

        public Poäng(SpriteFont newFont)
        {
            Font = newFont;
        }

        // Poängen på skärmen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Poäng1.ToString(), new Vector2(320, 70), Color.White);
            spriteBatch.DrawString(Font, Poäng2.ToString(), new Vector2(430, 70), Color.White);
        }
    }
}
