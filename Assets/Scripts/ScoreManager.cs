using System;

public static class ScoreManager
{
    public class ScoreData
    {
        public int Points
        {
            get => points;
            set
            {
                points = value;
                OnDataChanged?.Invoke(this);
            }
        }

        private int points;

        public int Sets
        {
            get => sets;
            set
            {
                sets = value;
                OnDataChanged?.Invoke(this);
            }
        }

        private int sets;

        public Action<ScoreData> OnDataChanged;
    }

    public static readonly ScoreData RedScore = new ScoreData();
    public static readonly ScoreData BlueScore = new ScoreData();

    public static void ResetPoints() => RedScore.Points = BlueScore.Points = 0;

    public static void Reset()
    {
        ResetPoints();
        RedScore.Sets = BlueScore.Sets = 0;
    }
}