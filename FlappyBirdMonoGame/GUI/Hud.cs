using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.GUI
{
    class Hud
    {
        private Game1 game;
        private ScoreSystem scoreSystem;

        private Texture2D[] numberTextures;
        private Texture2D pauseIconTexture;
        private Texture2D gameOverTexture;
        private Texture2D getReadyTexture;
        private Texture2D instuctionTexture;

        public Hud()
        {
            scoreSystem = new ScoreSystem();
        }

        public void setTextures(Game1 game, Texture2D[] numberTextures, Texture2D pauseIconTexture, Texture2D gameOverTexture, Texture2D getReadyTexture, Texture2D instuctionTexture)
        {
            this.game = game;
            this.numberTextures = numberTextures;
            this.pauseIconTexture = pauseIconTexture;
            this.gameOverTexture = gameOverTexture;
            this.getReadyTexture = getReadyTexture;
            this.instuctionTexture = instuctionTexture;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (game.Status)
            {
                case GameStatus.Instruction:
                    spriteBatch.Draw(instuctionTexture, new Rectangle(180, 200, 120, 110), Color.White);
                    break;
                case GameStatus.GetReady:
                case GameStatus.Play:
                    break;
                case GameStatus.GameOver:
                    spriteBatch.Draw(gameOverTexture, new Rectangle(165, 240, 150, 42), Color.White);
                    break;
                case GameStatus.Pause:
                    spriteBatch.Draw(pauseIconTexture, new Rectangle(game.bird.destRect.X + 40, game.bird.destRect.Y - 20, 20, 20), Color.White);
                    break;
            }

            if (game.Status != GameStatus.Instruction)
            {
                DrawScores(spriteBatch, scoreSystem.Scores, game.bird.destRect.X + 20, game.bird.destRect.Y - 28);
            }
        }

        private void DrawScores(SpriteBatch spriteBatch, int scores, int x, int y)
        {
            int width = 16;
            Rectangle destRect = new Rectangle(x, y, 16, 20);
            Rectangle srcRect = new Rectangle(2, 0, 16, 20);

            int digits = (int)Math.Log10(Math.Max(1, scores)) + 1;
            x += (int)(width * (digits / 2f));

            for (int i = 0; i < digits; i++)
            {
                destRect.X = x - width * i;
                spriteBatch.Draw(numberTextures[scores % 10], destRect, srcRect, Color.White);
                scores /= 10;
            }

        }

        public void ResetScore()
        {
            scoreSystem.ResetScore();
        }

        public void AddScore()
        {
            scoreSystem.AddScore();
        }
    }
}
