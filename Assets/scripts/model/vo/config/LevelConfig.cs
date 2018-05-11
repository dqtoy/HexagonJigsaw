//Make By CSVCommand. Time 2018.5.11 10:15:44
using System;
using System.Collections.Generic;
using lib;

public class LevelConfig
{
	private List<string> list;
	public int id;
	public List<PieceConfig> pieces = new List<PieceConfig>();
	public List<PieceConfig> pieces2 = new List<PieceConfig>();
	public List<CoordConfig> coords = new List<CoordConfig>();

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
					if (itemList[n].Length == 0) continue;
					int item = (int)StringUtils.ToNumber(itemList[n]);
					pieces.Add(PieceConfig.GetConfig(item));
				}
			}
			if (i == 2)
			{
				List<string> itemList = StringUtils.Split(list[2],',');
				for(int n = 0; n < itemList.Count; n++)
				{
					if (itemList[n].Length == 0) continue;
					int item = (int)StringUtils.ToNumber(itemList[n]);
					pieces2.Add(PieceConfig.GetConfig(item));
				}
			}
			if (i == 3)
			{
				List<string> itemList = StringUtils.Split(list[3],',');
				for(int n = 0; n < itemList.Count; n++)
				{
					if (itemList[n].Length == 0) continue;
					int item = (int)StringUtils.ToNumber(itemList[n]);
					coords.Add(CoordConfig.GetConfig(item));
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
		Configs.Clear();
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

	public static LevelConfig GetConfigWidth(string paramName,object value)
	{
		Type t = typeof(LevelConfig);
		for (int i = 0; i < Configs.Count; i++)
		{
			object val = t.GetField(paramName).GetValue(Configs[i]);
			bool flag = false;
			if (val is string)
			{
				flag = ((string)value).Equals(val);
			}
			else
			{
				flag = val == value;
			}
			if (flag)
			{
				return Configs[i];
			}
		}
		return null;
	}

	public static void DecodeTableItem()
	{
		for(int i = 0,len = Configs.Count; i < len; i++)
		{
			Configs[i].DecodeConfigItem();
		}
	}

}