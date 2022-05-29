using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Trabalho_Pratico
{
    public class Level1 : State
    {
        private Game1 game;
        private SpriteFont font;
        private Texture2D background, border, snakePartsTexture;
        private Rectangle screen;
        private Random rnd;
        private Fruit fruit;
        private SnakePart head;
        private List<SnakePart> snakeParts;
        private Sounds backgroundSound;
        private List<SoundEffect> soundEffects;

        private const int textureSize = 30, borderWidth = 40;
        private int velocity, currentScore = 180;

        private bool isGameOver = false, isMuted = false;

        public Level1(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Sounds backgroundSound, bool isMuted, int score, List<SnakePart> snakeParts, Fruit fruit) : base(game, graphicsDevice, content)
        {
            this.game = game;
            this.isMuted = isMuted;
            this.backgroundSound = backgroundSound;

            screen = new Rectangle(0, 0, game.screenWidth, game.screenHeight);
            border = new Texture2D(graphicsDevice, game.screenWidth, game.screenHeight);
            CreateBorder(border, borderWidth, Color.Green);

            soundEffects = new List<SoundEffect>();
            soundEffects.Add(_content.Load<SoundEffect>("eat"));

            rnd = new Random();
            velocity = textureSize;

            background = _content.Load<Texture2D>("level1_background");
            font = _content.Load<SpriteFont>("font");
            this.snakeParts = new List<SnakePart>();
            this.snakeParts.Clear();

            if (score >= 0 && snakeParts != null && fruit != null)
            {
                this.currentScore = score;
                this.snakeParts = snakeParts;
                this.fruit = fruit;
            }
            else
            {
                this.fruit = new Fruit(_content.Load<Texture2D>("apple"), new Vector2(0, 0), Direction.None, screen);
                this.fruit.Pos = this.fruit.GenerateFruitLocation(this.snakeParts, textureSize, game.screenWidth, game.screenHeight);

                head = new SnakePart(_content.Load<Texture2D>("head_right"), new Vector2(80, 80), Direction.Right, screen);
                snakePartsTexture = _content.Load<Texture2D>("body_horizontal");
                this.snakeParts.Add(head);

                for (int i = 0; i < 3; i++)
                {
                    this.snakeParts[0].GrowSnake(this.snakeParts, snakePartsTexture, textureSize);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(-10, 0), Color.White);
            fruit.Draw(spriteBatch);

            for (int i = 0; i < snakeParts.Count; i++)
            {
                if (i == 0) //cabeça
                {
                    switch (snakeParts[i].Direction)
                    {
                        case Direction.Up:
                            snakeParts[i].texture = _content.Load<Texture2D>("head_up");
                            break;
                        case Direction.Down:
                            snakeParts[i].texture = _content.Load<Texture2D>("head_down");
                            break;
                        case Direction.Left:
                            snakeParts[i].texture = _content.Load<Texture2D>("head_left");
                            break;
                        case Direction.Right:
                            snakeParts[i].texture = _content.Load<Texture2D>("head_right");
                            break;
                        default:
                            snakeParts[i].texture = _content.Load<Texture2D>("head_right");
                            break;
                    }
                }
                else if (i == snakeParts.Count - 1) //ponta da cauda
                {
                    switch (snakeParts[i].Direction)
                    {
                        case Direction.Up:
                            snakeParts[i].texture = _content.Load<Texture2D>("tail_down");
                            break;
                        case Direction.Down:
                            snakeParts[i].texture = _content.Load<Texture2D>("tail_up");
                            break;
                        case Direction.Left:
                            snakeParts[i].texture = _content.Load<Texture2D>("tail_right");
                            break;
                        case Direction.Right:
                            snakeParts[i].texture = _content.Load<Texture2D>("tail_left");
                            break;
                        default:
                            snakeParts[i].texture = _content.Load<Texture2D>("tail_left");
                            break;
                    }
                }
                else //resto do corpo
                {
                    switch (snakeParts[i].Direction)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            snakeParts[i].texture = _content.Load<Texture2D>("body_vertical");
                            break;
                        case Direction.Left:
                        case Direction.Right:
                            snakeParts[i].texture = _content.Load<Texture2D>("body_horizontal");
                            break;
                        default:
                            snakeParts[i].texture = _content.Load<Texture2D>("body_horizontal");
                            break;
                    }
                }

                snakeParts[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(border, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(font, "Score: " + currentScore, new Vector2(7, 5), Color.Black);
            spriteBatch.DrawString(font, "LEVEL 1", new Vector2(500, 5), Color.Black);
            spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _game.ChangeState(new GamePaused(_game, _graphicsDevice, _content, backgroundSound, isMuted, 1, currentScore, snakeParts, fruit, null));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                isMuted = !isMuted;
                backgroundSound.SoundState(isMuted);
            }

            if (!isGameOver)
            {
                isGameOver = snakeParts[0].ScreenBorders(borderWidth, textureSize);
                
                foreach (SnakePart s in snakeParts)
                {
                    s.Update(gameTime, velocity);
                }

                snakeParts[0].ChangeDirection(snakeParts);
                snakeParts[0].InputKeyboard();

                if (snakeParts[0].SpriteBox.Intersects(fruit.SpriteBox))
                {
                    if (!isMuted) {
                        soundEffects[0].Play();
                    }

                    fruit.Pos = fruit.GenerateFruitLocation(snakeParts, textureSize, game.screenWidth, game.screenHeight);

                    currentScore += 10;

                    if(currentScore >= 200)
                    {
                        _game.ChangeState(new LevelCompleted(_game, _graphicsDevice, _content, backgroundSound, isMuted, currentScore));
                    }

                    snakeParts[0].GrowSnake(snakeParts, snakePartsTexture, textureSize);
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
                _game.ChangeState(new GameOver(_game, _graphicsDevice, _content, backgroundSound, isMuted, currentScore));
            }
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
    }
}
