//Make By CSVCommand. Time 2018.5.11 15:21:32
using System;
using System.Collections.Generic;
using lib;

public class PassScoreConfig
{
	private List<string> list;
	public int id;
	public ModelConfig model;
	public int minTime;
	public int maxTime;
	public LanguageConfig language;
	public int scoreMin;
	public int scoreMax;
	public int effectCount;

	public void Decode(List<string> list)
	{
		this.list = list;
		for(int i = 0; i < list.Count; i++)
		{
			if(i == 0)
			{
				id = (int)StringUtils.ToNumber(list[0]);
			}
			if(i == 2)
			{
				minTime = (int)StringUtils.ToNumber(list[2]);
			}
			if(i == 3)
			{
				maxTime = (int)StringUtils.ToNumber(list[3]);
			}
			if(i == 5)
			{
				scoreMin = (int)StringUtils.ToNumber(list[5]);
			}
			if(i == 6)
			{
				scoreMax = (int)StringUtils.ToNumber(list[6]);
			}
			if(i == 7)
			{
				effectCount = (int)StringUtils.ToNumber(list[7]);
			}
		}
	}

	public void DecodeConfigItem()
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (i == 1)
			{
				model = ModelConfig.GetConfig((int)StringUtils.ToNumber(list[i]));
			}
			if (i == 4)
			{
				language = LanguageConfig.GetConfig((int)StringUtils.ToNumber(list[i]));
			}
		}
		list = null;
	}


	public static List<PassScoreConfig> Configs = new List<PassScoreConfig>();

	public static PassScoreConfig GetConfig(int key)
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
			PassScoreConfig item = new PassScoreConfig();
			item.Decode(list[i]);
			Configs.Add(item);
		}
	}

	public static PassScoreConfig GetConfigWidth(string paramName,object value)
	{
		Type t = typeof(PassScoreConfig);
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