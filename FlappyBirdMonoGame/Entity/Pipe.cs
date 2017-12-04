using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entity
{
    class Pipe : IEntity
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public float Span { get; set; }

        public bool ShouldDraw { get; set; }

        public event Action AddScore;

        private float speed;
        private int qty;
        private float distance;

        private int initX;
        private int minY;
        private int maxY;

        private Texture2D upperTexture;
        private Texture2D lowerTexture;

        private float[] xPos;
        private bool[] pipeScored;
        public Rectangle[] destRects { get; private set; }

        private Random random;

        private float speedRec;

        public Pipe(Texture2D upperTexture, Texture2D lowerTexture, int qty, float span, float distance, int minY, int maxY, int initX, int width, int height)
        {
            this.qty = qty;
            Span = span;
            this.minY = minY;
            this.maxY = maxY;

            Width = width;
            Height = height;

            this.distance = distance;

            this.upperTexture = upperTexture;
            this.lowerTexture = lowerTexture;

            this.initX = initX;

            xPos = new float[qty];
            pipeScored = new bool[qty];

            random = new Random();
            destRects = new Rectangle[qty * 2];
            for (int i = 0; i < qty * 2; i += 2)
            {
                int x = initX + (int)(Width + Span) * i;
                int y = minY + random.Next() % (maxY - minY);
                if (i == 0)
                {
                    y = (minY + maxY) / 2;
                }
                xPos[i / 2] = x;
                destRects[i] = new Rectangle(x, y, Width, Height);
                destRects[i + 1] = new Rectangle(x, y + (int)(distance + Height), Width, Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < qty * 2; i += 2)
            {

                xPos[i / 2] -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (xPos[i / 2] < 120 && pipeScored[i / 2] == false)
                {
                    pipeScored[i / 2] = true;
                    AddScore.Invoke();
                }

                if (xPos[i / 2] <= -Width)
                {
                    pipeScored[i / 2] = false;
                    xPos[i / 2] = -Width + (Width + Span) * (qty * 2);
                    int y = minY + random.Next() % (maxY - minY);
                    destRects[i].Y = y;
                    destRects[i + 1].Y = y + (int)(distance + Height);
                }

                destRects[i].X = (int)xPos[i / 2];
                destRects[i + 1].X = (int)xPos[i / 2];
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ShouldDraw)
            {
                for (int i = 0; i < qty * 2; i += 2)
                {
                    spriteBatch.Draw(upperTexture, destRects[i], Color.White);
                    spriteBatch.Draw(lowerTexture, destRects[i + 1], Color.White);
                }
            }
        }

        public void SetSpeed(float speed)
        {
            speedRec = speed;
            this.speed = speed;
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

        public void Reset()
        {
            for (int i = 0; i < qty * 2; i += 2)
            {
                int x = initX + (int)(Width + Span) * i;
                int y = minY + random.Next() % (maxY - minY);
                if (i == 0)
                {
                    y = (minY + maxY) / 2;
                }
                xPos[i / 2] = x;
                pipeScored[i / 2] = false;
                destRects[i] = new Rectangle(x, y, Width, Height);
                destRects[i + 1] = new Rectangle(x, y + (int)(distance + Height), Width, Height);
            }
        }
    }
}
