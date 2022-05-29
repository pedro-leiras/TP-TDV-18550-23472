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
            base.Update(gameTime);
        }

        public void InputKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                if (direction != Direction.Down)
                    direction = Direction.Up;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                if (direction != Direction.Up)
                    direction = Direction.Down;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
                if (direction != Direction.Right)
                    direction = Direction.Left;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                if (direction != Direction.Left)
                    direction = Direction.Right;
        }

        public bool ScreenBorders(int borderWidth, int textureSize)
        {
            if(pos.X < borderWidth || pos.X > screen.Width - (borderWidth + textureSize) || pos.Y < borderWidth || pos.Y > screen.Height - (borderWidth + textureSize))
            {
                return true;
            }
            return false;
        }

        public void GrowSnake(List<SnakePart> snakeParts, Texture2D snakePartsTexture, int textureSize)
        {
            SnakePart body = new SnakePart(snakePartsTexture, new Vector2(snakeParts[snakeParts.Count - 1].Pos.X, snakeParts[snakeParts.Count - 1].Pos.Y), snakeParts[snakeParts.Count - 1].Direction, screen);

            switch (snakeParts[snakeParts.Count - 1].Direction)
            {
                case Direction.Up:
                    body.Pos = new Vector2(body.Pos.X, body.Pos.Y + textureSize);
                    break;
                case Direction.Down:
                    body.Pos = new Vector2(body.Pos.X, body.Pos.Y - textureSize);
                    break;
                case Direction.Left:
                    body.Pos = new Vector2(body.Pos.X + textureSize, body.Pos.Y);
                    break;
                case Direction.Right:
                    body.Pos = new Vector2(body.Pos.X - textureSize, body.Pos.Y);
                    break;
                default:
                    break;
            }
            snakeParts.Add(body);
        }

        public void ChangeDirection(List<SnakePart> snakeParts)
        {
            for (int i = snakeParts.Count - 1; i > 0; i--)
            {
                snakeParts[i].Direction = snakeParts[i - 1].Direction;
            }
        }
    }
}
