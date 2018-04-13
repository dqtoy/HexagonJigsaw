using lib;

public class EditorVO
{
    public EventDispatcher dispatcher = new EventDispatcher();

    /// <summary>
    /// 当前填充的颜色类型
    /// </summary>
    public Int colorType = new Int();


    private static EditorVO instance;
    public static EditorVO Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EditorVO();
            }
            return instance;
        }
    }
}