using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_Pratico
{
    public class MainMenu : State
    {
        private List<Component> _components;
        private Texture2D backGround;
        private SpriteFont fontText, fontButton;
        private bool isMuted = false;
        private Sounds backgroundSound;

        public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Sounds backgroundSound, bool isMuted) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            fontText = _content.Load<SpriteFont>("fontMenu");
            fontButton = _content.Load<SpriteFont>("font");
            backGround = _content.Load<Texture2D>("mainMenuBackground");
            this.backgroundSound = backgroundSound;
            this.isMuted = isMuted;

            var playButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 250),
                Text = "Play",
            };

            playButton.Click += playButton_Click;

            var quitGameButton = new Button(buttonTexture, fontButton)
            {
                Position = new Vector2(210, 350),
                Text = "Quit Game",
            };

            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>()
            {
                playButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(-10, 0), Color.White);

            spriteBatch.DrawString(fontText, "la serpiente azul", new Vector2(120, 120), Color.Black);

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

        private void playButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Level1(_game, _graphicsDevice, _content, backgroundSound, isMuted, - 1, null, null));
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
