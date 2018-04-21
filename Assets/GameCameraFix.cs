using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameCameraFix : MonoBehaviour {

    public Camera gameCamera;

    public GameCameraFixMode fixMode = GameCameraFixMode.FIX_HEIGHT;

    public int fixSize = 1280;

    private void Awake()
    {
        int width = gameCamera.pixelWidth;
        int height = gameCamera.pixelHeight;
        if(fixMode == GameCameraFixMode.FIX_WIDTH)
        {
            gameCamera.orthographicSize = fixSize * height / (width * 200f);
        }
        else if(fixMode == GameCameraFixMode.FIX_HEIGHT)
        {
            gameCamera.orthographicSize = height / 200f;
        }
    }
}

public enum GameCameraFixMode
{
    FIX_WIDTH = 1,
    FIX_HEIGHT = 2
}