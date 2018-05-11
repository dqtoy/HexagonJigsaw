//Make By CSVCommand. Time 2018.5.11 15:21:32
using UnityEngine;

public class ConfigDecode
{
	public static void Decode()
	{
		CoordConfig.DecodeTable(Resources.Load("config/Coord") + "");
		LanguageConfig.DecodeTable(Resources.Load("config/Language") + "");
		LanguageTypeConfig.DecodeTable(Resources.Load("config/LanguageType") + "");
		LevelConfig.DecodeTable(Resources.Load("config/Level") + "");
		ModelConfig.DecodeTable(Resources.Load("config/Model") + "");
		PassScoreConfig.DecodeTable(Resources.Load("config/PassScore") + "");
		PieceConfig.DecodeTable(Resources.Load("config/Piece") + "");
		CoordConfig.DecodeTableItem();
		LanguageConfig.DecodeTableItem();
		LanguageTypeConfig.DecodeTableItem();
		LevelConfig.DecodeTableItem();
		ModelConfig.DecodeTableItem();
		PassScoreConfig.DecodeTableItem();
		PieceConfig.DecodeTableItem();
	}
}