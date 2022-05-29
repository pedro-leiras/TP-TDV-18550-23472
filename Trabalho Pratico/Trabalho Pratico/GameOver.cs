using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    public class GameOver : State
    {
        private List<Component> _components;
        private Texture2D backGround;
        private SpriteFont fontText, fontButton;
        private int score;
        private bool isMuted;
        private Sounds backgroundSound;

        public GameOver(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Sounds backgroundSound, bool isMuted, int score) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            fontText = _content.Load<SpriteFont>("fontMenu");
            fontButton = _content.Load<SpriteFont>("font");
            backGround = _content.Load<Texture2D>("mainMenuBackground");
            this.score = score;
            this.isMuted = isMuted;
            this.backgroundSound = backgroundSound;

            var restartButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 300),
                Text = "Restart",
            };

            restartButton.Click += restartButton_Click;

            var mainMenuButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 400),
                Text = "Main Menu",
            };

            mainMenuButton.Click += mainMenuButton_Click;

            _components = new List<Component>()
            {
                restartButton,
                mainMenuButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(-10, 0), Color.White);

            spriteBatch.DrawString(fontText, "Game Over", new Vector2(200, 100), Color.Black);
            spriteBatch.DrawString(fontText, "Score: "+ score, new Vector2(210, 180), Color.Black);

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

        private void restartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Level1(_game, _graphicsDevice, _content, backgroundSound, isMuted, -1, null, null));
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenu(_game, _graphicsDevice, _content, backgroundSound, isMuted));
        }
    }
}
