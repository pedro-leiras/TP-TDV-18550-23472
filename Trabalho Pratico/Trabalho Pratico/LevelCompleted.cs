using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    public class LevelCompleted : State
    {
        private List<Component> _components;
        private Texture2D backGround;
        private SpriteFont fontText, fontButton;
        private int currentScore;
        private bool isMuted;
        private Sounds backgroundSound;

        public LevelCompleted(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Sounds backgroundSound, bool isMuted, int score) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            fontText = _content.Load<SpriteFont>("fontMenu");
            fontButton = _content.Load<SpriteFont>("font");
            backGround = _content.Load<Texture2D>("mainMenuBackground");
            currentScore = score;
            this.backgroundSound = backgroundSound;
            this.isMuted = isMuted;

            var nextLevelButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 300),
                Text = "Next Level",
            };

            nextLevelButton.Click += nextLevelButton_Click;

            var mainMenuButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 400),
                Text = "Main Menu",
            };

            mainMenuButton.Click += mainMenuButton_Click;

            _components = new List<Component>()
            {
                nextLevelButton,
                mainMenuButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(-10, 0), Color.White);

            spriteBatch.DrawString(fontText, "Level 1 Completed", new Vector2(125, 100), Color.Black);
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

        private void nextLevelButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Level2(_game, _graphicsDevice, _content, backgroundSound, isMuted, currentScore, null, null, null));
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenu(_game, _graphicsDevice, _content, backgroundSound, isMuted));
        }
    }
}
