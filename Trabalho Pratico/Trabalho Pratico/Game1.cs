using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Trabalho_Pratico
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont font;
        Random rnd;
        Texture2D background, border;
        Fruit fruit;
        SnakePart head;
        Rectangle screen;
        List<SnakePart> snakeParts;

        private const int screenWidth = 600, screenHeight = 600, textureSize = 30;
        int velocity, currentScore = 0;

        bool isGameOver = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(80); // 20 milliseconds, or 50 FPS.
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;

            border = new Texture2D(GraphicsDevice, 600, 600);

            screen = new Rectangle(0, 0, screenWidth, screenHeight);
            rnd = new Random();
            snakeParts = new List<SnakePart>();
            velocity = textureSize;
            CreateBorder(border, 40, Color.Green);
            base.Initialize();
        }

        public static void CreateBorder(Texture2D texture, int borderWidth, Color borderColor)
        {
            Color[] colors = new Color[texture.Width * texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++)
                    {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i)
                        {
                            colors[x + y * texture.Width] = borderColor;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * texture.Width] = Color.Transparent;
                }
            }

            texture.SetData(colors);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            font = Content.Load<SpriteFont>("font");

            fruit = new Fruit(Content.Load<Texture2D>("apple"), new Vector2(rnd.Next(40 + textureSize, (screenWidth - 40) - textureSize), rnd.Next(40 + textureSize, (screenHeight - 40) - textureSize)), Direction.None, screen);
           
            Vector2 posCabeça = new Vector2(rnd.Next(40 + textureSize, (screenWidth - 40) - textureSize), rnd.Next(40 + textureSize, (screenHeight - 40) - textureSize));
            head = new SnakePart(Content.Load<Texture2D>("head_right"), posCabeça, Direction.Right, screen);
            snakeParts.Clear();
            snakeParts.Add(head);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!isGameOver) {
                foreach (SnakePart s in snakeParts)
                {
                    s.Update(gameTime, velocity);
                }

                for (int i = snakeParts.Count - 1; i > 0; i--)
                {
                    snakeParts[i].Direction = snakeParts[i - 1].Direction;
                }

                if (snakeParts[0].SpriteBox.Intersects(fruit.SpriteBox))
                {
                    fruit.Pos = new Vector2(rnd.Next(40 + textureSize, (screenWidth - 40) - textureSize), rnd.Next(40 + textureSize, (screenHeight - 40) - textureSize));
                    
                    currentScore += 10;
                    
                    SnakePart tail = new SnakePart(Content.Load<Texture2D>("body_horizontal"), new Vector2(snakeParts[snakeParts.Count - 1].Pos.X, snakeParts[snakeParts.Count - 1].Pos.Y), snakeParts[snakeParts.Count - 1].Direction, screen);
                
                    switch (snakeParts[snakeParts.Count - 1].Direction)
                    {
                        case Direction.Up:
                            tail.Pos = new Vector2(tail.Pos.X, tail.Pos.Y + textureSize);
                            break;
                        case Direction.Down:
                            tail.Pos = new Vector2(tail.Pos.X, tail.Pos.Y - textureSize);
                            break;
                        case Direction.Left:
                            tail.Pos = new Vector2(tail.Pos.X + textureSize, tail.Pos.Y);
                            break;
                        case Direction.Right:
                            tail.Pos = new Vector2(tail.Pos.X - textureSize, tail.Pos.Y);
                            break;
                        case Direction.None:
                            break;
                        default:
                            break;
                    }
                    snakeParts.Add(tail);
                }

                for (int i = 1; i < snakeParts.Count - 1; i++)
                {
                    if (snakeParts[0].SpriteBox.Intersects(snakeParts[i].SpriteBox))
                    {
                        isGameOver = true;
                    }
                }
            }
            else
            {
                Exit();
            }
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(-10, 0), Color.White);
            fruit.Draw(_spriteBatch);
            for (int i = 0; i < snakeParts.Count; i++)
            {
                if(i == 0) //cabeça
                {
                    switch (snakeParts[i].Direction)
                    {
                        case Direction.Up:
                            snakeParts[i].texture = Content.Load<Texture2D>("head_up");
                            break;
                        case Direction.Down:
                            snakeParts[i].texture = Content.Load<Texture2D>("head_down");
                            break;
                        case Direction.Left:
                            snakeParts[i].texture = Content.Load<Texture2D>("head_left");
                            break;
                        case Direction.Right:
                            snakeParts[i].texture = Content.Load<Texture2D>("head_right");
                            break;
                        default:
                            snakeParts[i].texture = Content.Load<Texture2D>("head_right");
                            break;
                    }
                }
                else //resto do corpo
                {
                    switch (snakeParts[i].Direction)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            snakeParts[i].texture = Content.Load<Texture2D>("body_vertical");
                            break;
                        case Direction.Left:
                        case Direction.Right:
                            snakeParts[i].texture = Content.Load<Texture2D>("body_horizontal");
                            break;
                        default:
                            snakeParts[i].texture = Content.Load<Texture2D>("body_horizontal");
                            break;
                    }
                }

                snakeParts[i].Draw(_spriteBatch);
            }
            _spriteBatch.Draw(border, new Vector2(0.0f, 0.0f), Color.White);

            _spriteBatch.DrawString(font, "Score: " + currentScore, new Vector2 (5, 5), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
