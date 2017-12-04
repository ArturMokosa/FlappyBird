using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entity
{
    class Background : IEntity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        private float speed;
        private Texture2D texture;
        public Rectangle[] destRects { get; private set; }

        private float[] xPos;
        private float speedRec;

        public Background(Texture2D texture, float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            this.texture = texture;

            xPos = new float[2];
            xPos[0] = x;
            xPos[1] = x + width;
            destRects = new Rectangle[2];
            for (int i = 0; i < destRects.Length; i++)
            {
                destRects[i] = new Rectangle((int)xPos[i], (int)y, (int)width, (int)height);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < xPos.Length; i++)
            {
                xPos[i] -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (xPos[i] <= -Width)
                {
                    xPos[i] += Width * 2;
                }
            }

            for (int i = 0; i < destRects.Length; i++)
            {
                destRects[i].X = (int)xPos[i];
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < destRects.Length; i++)
            {
                spriteBatch.Draw(texture, destRects[i], Color.White);
            }
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
            speedRec = speed;
        }

        public void Pause()
        {
            speed = 0;
        }

        public void Resume()
        {
            speed = speedRec;
        }

        public bool Intersect(Rectangle rectangle)
        {
            for (int i = 0; i < destRects.Length; i++)
            {
                if (destRects[i].Intersects(rectangle))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
