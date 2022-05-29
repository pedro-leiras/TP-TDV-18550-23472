using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Trabalho_Pratico
{
    public class GameOver : State
    {
        private List<Component> _components;
        Texture2D backGround;
        SpriteFont fontText, fontButton;
        int score;
        public GameOver(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int score) : base(game, graphicsDevice, content)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            this.fontText = _content.Load<SpriteFont>("fontMenu");
            this.fontButton = _content.Load<SpriteFont>("font");
            this.backGround = _content.Load<Texture2D>("mainMenuBackground");
            this.score = score;

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
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenu(_game, _graphicsDevice, _content));
        }
    }
}
