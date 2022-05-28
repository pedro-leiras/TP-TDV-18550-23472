using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Trabalho_Pratico
{
    class Fruit : Sprite
    {
        public Fruit(Texture2D texture, Vector2 pos, Direction direction, Rectangle screen): base(texture, pos, direction, screen)
        {
        }
    }
}
