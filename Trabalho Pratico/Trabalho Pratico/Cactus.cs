using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    public class Cactus : Sprite
    {
        public Cactus(Texture2D texture, Vector2 pos, Direction direction, Rectangle screen) : base(texture, pos, direction, screen)
        {
        }

        public Vector2 GenerateCactusLocation(List<Cactus> cactus, List<SnakePart> snakeParts, Fruit fruit, int textureSize, int screenWidth, int screenHeight)
        {
            bool freePosition = false;
            int x = 0, y = 0;
            Vector2 aux = new Vector2(x, y);
            Random rnd = new Random();

            do
            {
                x = rnd.Next(40 + textureSize, (screenWidth - 40) - textureSize);
                y = rnd.Next(40 + textureSize, (screenHeight - 40) - textureSize);

                if (!cactus.Exists(part => (part.Pos.X == x && part.Pos.Y == y))  && !snakeParts.Exists(part => (part.Pos.X == x && part.Pos.Y == y)) && !(fruit.Pos.X == x && fruit.Pos.Y == y))
                {
                    aux = new Vector2(x, y);
                    freePosition = true;
                }
            } while (!freePosition);

            return aux;
        }
    }
}
