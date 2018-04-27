//Make By CSVCommand. Time 2018.4.24 19:0:51
using UnityEngine;

public class ConfigDecode
{
	public static void Decode()
	{
		CoordConfig.DecodeTable(Resources.Load("config/Coord") + "");
		LanguageConfig.DecodeTable(Resources.Load("config/Language") + "");
		LanguageTypeConfig.DecodeTable(Resources.Load("config/LanguageType") + "");
		LevelConfig.DecodeTable(Resources.Load("config/Level") + "");
		PieceConfig.DecodeTable(Resources.Load("config/Piece") + "");
		CoordConfig.DecodeTableItem();
		LanguageConfig.DecodeTableItem();
		LanguageTypeConfig.DecodeTableItem();
		LevelConfig.DecodeTableItem();
		PieceConfig.DecodeTableItem();
	}
}