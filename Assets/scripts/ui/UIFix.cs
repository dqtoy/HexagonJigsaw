
using UnityEngine;

public class UIFix {

	public static float GetY(float y)
    {
        return y*GameVO.Instance.PixelHeight/1280.0f;
    }

    public static void SetPosition(RectTransform trans)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, GetY(trans.localPosition.y), trans.localPosition.z);
    }

    public static void SetDistanceToTop(RectTransform trans)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + GameVO.Instance.PixelHeight * 0.5f - 1280 * 0.5f, trans.localPosition.z);
    }

    public static void SetDistanceToBottom(RectTransform trans)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - GameVO.Instance.PixelHeight * 0.5f + 1280 * 0.5f, trans.localPosition.z);
    }

    public static float GetDistanceToTop()
    {
        return GameVO.Instance.PixelHeight * 0.5f - 1280 * 0.5f;
    }

    public static float GetDistanceToBottom()
    {
        return -GameVO.Instance.PixelHeight * 0.5f + 1280 * 0.5f;
    }

}
