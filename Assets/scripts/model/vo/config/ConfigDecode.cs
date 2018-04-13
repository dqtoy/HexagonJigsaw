//Make By CSVCommand. Time 2018.4.13 14:50:24
using UnityEngine;

public class ConfigDecode
{
	public static void Decode()
	{
		CoordConfig.DecodeTable(Resources.Load("config/Coord") + "");
		LevelConfig.DecodeTable(Resources.Load("config/Level") + "");
		PieceConfig.DecodeTable(Resources.Load("config/Piece") + "");
		CoordConfig.DecodeTableItem();
		LevelConfig.DecodeTableItem();
		PieceConfig.DecodeTableItem();
	}
}