using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Trabalho_Pratico
{
    public class MainMenu : State
    {
        private List<Component> _components;
        Texture2D backGround;
        SpriteFont fontText, fontButton;
        public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            this.fontText = _content.Load<SpriteFont>("fontMenu");
            this.fontButton = _content.Load<SpriteFont>("font");
            this.backGround = _content.Load<Texture2D>("mainMenuBackground");

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

            spriteBatch.DrawString(fontText, "The SNAKE Game", new Vector2(150, 120), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
