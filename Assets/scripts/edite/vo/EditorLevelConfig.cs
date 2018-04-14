using lib;
using System.Collections.Generic;

/// <summary>
/// 关卡配置信息
/// </summary>
public class EditorLevelConfig
{
    /// <summary>
    /// 片的信息
    /// </summary>
    public List<EditorLevelPiece> pieces = new List<EditorLevelPiece>();

    /// <summary>
    /// 干扰片信息
    /// </summary>
    public List<EditorLevelPiece> otherPieces = new List<EditorLevelPiece>();

}