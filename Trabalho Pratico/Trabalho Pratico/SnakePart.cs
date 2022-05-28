using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    class SnakePart : Sprite
    {
        public SnakePart(Texture2D texture, Vector2 pos, Direction direction, Rectangle screen) : base(texture, pos, direction, screen)
        {
        }
        
        public void Update(GameTime gameTime, int velocity)
        {
            switch (direction)
            {
                case Direction.Up:
                    pos.Y -= velocity;
                    break;
                case Direction.Down:
                    pos.Y += velocity;
                    break;
                case Direction.Left:
                    pos.X -= velocity;
                    break;
                case Direction.Right:
                    pos.X += velocity;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }

            ScreenBorders();
        }

        public void InputKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                if (direction != Direction.Down)
                    direction = Direction.Up;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                if (direction != Direction.Up)
                    direction = Direction.Down;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                if (direction != Direction.Right)
                    direction = Direction.Left;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                if (direction != Direction.Left)
                    direction = Direction.Right;
        }

        private bool ScreenBorders()
        {
            if(pos.X < 41) //margem de cima
            {
                return true;
                //pos.X = screen.Width - 42;
            }
            if(pos.X > screen.Width - 41) //margem de baixo
            {
                //pos.X = 41;
                return true;
            }
            if (pos.Y < 41) //margem esquerda
            {
                //pos.Y = screen.Height - 42;
                return true;
            }
            if (pos.Y > screen.Height - 41) //margem direita
            {
                return true;
                //pos.Y = 41;
            }
            return false;
        }
    }
}
