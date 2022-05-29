using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Trabalho_Pratico
{
    public class Sprite
    {
        public Texture2D texture;
        protected Rectangle screen;

        protected Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        protected Direction direction;
        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Rectangle SpriteBox { get { return new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height); } }

        public Sprite(Texture2D texture, Vector2 pos, Direction direction, Rectangle screen)
        {
            this.texture = texture;
            this.pos = pos;
            this.direction = direction;
            this.screen = screen;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }
    }
}
