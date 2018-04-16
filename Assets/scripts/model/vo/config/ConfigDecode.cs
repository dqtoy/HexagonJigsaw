//Make By CSVCommand. Time 2018.4.14 16:34:42
using UnityEngine;

public class ConfigDecode
{
	public static void Decode()
	{
		CoordConfig.DecodeTable(Resources.Load("config/Coord") + "");
		LevelConfig.DecodeTable(Resources.Load("config/Level") + "");
		PieceConfig.DecodeTable(Resources.Load("config/Piece") + "");
        Resources.UnloadUnusedAssets();
        CoordConfig.DecodeTableItem();
		LevelConfig.DecodeTableItem();
		PieceConfig.DecodeTableItem();
	}
}