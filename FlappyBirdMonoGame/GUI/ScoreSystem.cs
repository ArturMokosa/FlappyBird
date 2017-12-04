namespace FlappyBird.GUI
{
    public class ScoreSystem
    {
        public int Scores { get; private set; }

        public ScoreSystem()
        {
        }

        public void ResetScore()
        {
            Scores = 0;
        }

        public void AddScore()
        {
            Scores++;
        }

    }
}
