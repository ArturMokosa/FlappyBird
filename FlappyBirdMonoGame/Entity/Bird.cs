using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird.Entity
{
    public class Bird : IEntity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ShouldDraw { get; set; }

        public Rectangle destRect;
        private Rectangle sourceRect;

        private Texture2D texture;
        private int animIndex;
        private bool flapping;
        private int spriteWidth;
        private int spriteHeight;
        private float velocity;
        private readonly float gravity = 320f;

        public float animInterval { get; set; }
        private float animTimeLeft;

        private float initY;
        private bool paused;
        private bool gameOver;
        private float rotation;
        private Vector2 origin;

        private Game1 game;

        public Bird(Game1 game, Texture2D texture, float x, float y, int width, int height)
        {
            this.game = game;
            initY = y;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            this.texture = texture;
            destRect = new Rectangle((int)X, (int)Y, Width, Height);

            animIndex = 1;
            spriteWidth = 18;
            spriteHeight = 12;

            velocity = 120f;
            animInterval = 0.06f;
            sourceRect = new Rectangle(1, 4, spriteWidth, spriteHeight);
            origin = new Vector2(0.5f, 0.5f);
        }

        public void Update(GameTime gameTime)
        {
            if (paused)
            {
                return;
            }

            if (game.Status == GameStatus.Play)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !flapping)
                {
                    flapping = true;
                    Flap();
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    flapping = false;
                }
            }

            if (animIndex > 0)
            {
                animTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animTimeLeft <= 0)
                {
                    animTimeLeft = animInterval;
                    animIndex++;

                    animIndex = animIndex % 3;
                }
            }

            sourceRect.X = animIndex * (2 + spriteWidth) + 1;

            velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * gravity;
            Y += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            destRect.Y = (int)Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ShouldDraw)
            {
                spriteBatch.Draw(texture, destinationRectangle: destRect, sourceRectangle: sourceRect, color: Color.White, rotation: rotation, origin: origin);
            }
        }

        private void Flap()
        {
            animTimeLeft = animInterval;
            velocity = -220f;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Resume()
        {
            paused = false;
        }

        public bool Intersect(Rectangle rectangle)
        {
            return destRect.Intersects(rectangle);
        }

        public void SetGameOver(bool gameOver)
        {
            this.gameOver = gameOver;
        }

        public void Reset()
        {
            Y = initY;
            rotation = 0;
            velocity = 120f;

            gameOver = false;
        }
    }
}
