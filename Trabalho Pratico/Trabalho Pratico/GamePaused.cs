using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    public class GamePaused : State
    {
        private List<Component> _components;
        private Texture2D backGround;
        private SpriteFont fontText, fontButton;
        private int currentScore;
        private List<SnakePart> snakeParts;
        private List<Cactus> cactus;
        private Fruit fruit;
        private int level;
        private bool isMuted;
        private Sounds backgroundSound;

        public GamePaused(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Sounds backgroundSound, bool isMuted, int level, int score, List<SnakePart> snakeParts, Fruit fruit, List<Cactus> cactus) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            fontText = _content.Load<SpriteFont>("fontMenu");
            fontButton = _content.Load<SpriteFont>("font");
            backGround = _content.Load<Texture2D>("mainMenuBackground");
            currentScore = score;
            this.snakeParts = snakeParts;
            this.fruit = fruit;
            this.level = level;
            this.backgroundSound = backgroundSound;
            this.cactus = cactus;
            this.isMuted = isMuted;

            var resumeButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 300),
                Text = "Resume",
            };

            resumeButton.Click += resumeButton_Click;

            var mainMenuButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 400),
                Text = "Main Menu",
            };

            mainMenuButton.Click += mainMenuButton_Click;

            _components = new List<Component>()
            {
                resumeButton,
                mainMenuButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(-10, 0), Color.White);

            spriteBatch.DrawString(fontText, "Game Paused", new Vector2(170, 100), Color.Black);
            spriteBatch.DrawString(fontText, "Current Score: " + currentScore, new Vector2(115, 180), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                isMuted = !isMuted;
                backgroundSound.SoundState(isMuted);
            }

            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            if(level == 1) {
                _game.ChangeState(new Level1(_game, _graphicsDevice, _content, backgroundSound, isMuted, currentScore, snakeParts, fruit));
            }
            else if (level == 2)
            {
                _game.ChangeState(new Level2(_game, _graphicsDevice, _content, backgroundSound, isMuted, currentScore, snakeParts, fruit, cactus));
            }
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenu(_game, _graphicsDevice, _content, backgroundSound, isMuted));
        }
    }
}
