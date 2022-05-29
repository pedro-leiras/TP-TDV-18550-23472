//fonte: https://github.com/Oyyou/MonoGame_Tutorials
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Trabalho_Pratico
{
    public abstract class State
    {

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;


        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        public abstract void Update(GameTime gameTime);
    }
}
