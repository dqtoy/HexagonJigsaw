//Make By CSVCommand. Time 2018.4.13 14:50:24
using System.Collections.Generic;
using lib;

public class LevelConfig
{
	private List<string> list;
	public int id;
	public List<CoordConfig> coords = new List<CoordConfig>();
	public List<PieceConfig> pieces = new List<PieceConfig>();

	public void Decode(List<string> list)
	{
		this.list = list;
		for(int i = 0; i < list.Count; i++)
		{
			if(i == 0)
			{
				id = (int)StringUtils.ToNumber(list[0]);
			}
		}
	}

	public void DecodeConfigItem()
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (i == 1)
			{
				List<string> itemList = StringUtils.Split(list[1],',');
				for(int n = 0; n < itemList.Count; n++)
				{
					int item = (int)StringUtils.ToNumber(itemList[n]);
					coords.Add(CoordConfig.GetConfig(item));
				}
			}
			if (i == 2)
			{
				List<string> itemList = StringUtils.Split(list[2],',');
				for(int n = 0; n < itemList.Count; n++)
				{
					int item = (int)StringUtils.ToNumber(itemList[n]);
					pieces.Add(PieceConfig.GetConfig(item));
				}
			}
		}
		list = null;
	}


	public static List<LevelConfig> Configs = new List<LevelConfig>();

	public static LevelConfig GetConfig(int key)
	{
		for(int i = 0; i < Configs.Count; i++)
		{
			if(Configs[i].id == key)
			{
				return Configs[i];
			}
		}
		return null;
	}

	public static void DecodeTable(string str)
	{
		str = StringUtils.Replace(str, '\r', '\n');
		str = StringUtils.Replace(str, "\n\n", '\n');
		List<List<string>> list = CSV.Parse(str);
		for(int i = 2,len = list.Count; i < len; i++)
		{
			LevelConfig item = new LevelConfig();
			item.Decode(list[i]);
			Configs.Add(item);
		}
	}

	public static void DecodeTableItem()
	{
		for(int i = 0,len = Configs.Count; i < len; i++)
		{
			Configs[i].DecodeConfigItem();
		}
	}

}