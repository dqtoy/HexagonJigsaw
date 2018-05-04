using UnityEngine;
using UnityEditor;
public class MyEditor : EditorWindow
{

    [MenuItem("Tools/window")]
    static void AddWindow()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 500, 500);
        MyEditor window = (MyEditor)EditorWindow.GetWindowWithRect(typeof(MyEditor), wr, true, "widow name");
        window.Show();
    }
}