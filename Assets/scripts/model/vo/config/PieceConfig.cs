//Make By CSVCommand. Time 2018.5.11 15:21:32
using System;
using System.Collections.Generic;
using lib;

public class PieceConfig
{
	private List<string> list;
	public int id;
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
					coords.Add(CoordConfig.GetConfig(item));
				}
			}
		}
		list = null;
	}


	public static List<PieceConfig> Configs = new List<PieceConfig>();

	public static PieceConfig GetConfig(int key)
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
			PieceConfig item = new PieceConfig();
			item.Decode(list[i]);
			Configs.Add(item);
		}
	}

	public static PieceConfig GetConfigWidth(string paramName,object value)
	{
		Type t = typeof(PieceConfig);
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