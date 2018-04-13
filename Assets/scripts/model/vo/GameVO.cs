public class GameVO
{

    public float Width;
    public float Height;
    public float PixelWidth;
    public float PixelHeight;

    public LevelVO level;

    private static GameVO instance;

    public static GameVO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameVO();
            }
            return instance;
        }
    }
}