//Make By CSVCommand. Time 2018.5.11 10:15:44
using System;
using System.Collections.Generic;
using lib;

public class LanguageConfig
{
	private List<string> list;
	public int id;
	public string uipanel;
	public string position;
	public string des;
	public string zh_cn;
	public string en_us;
	public string zh_tw;
	public string ja_jp;
	public string ko_kr;
	public string de_de;
	public string es_la;

	public void Decode(List<string> list)
	{
		this.list = list;
		for(int i = 0; i < list.Count; i++)
		{
			if(i == 0)
			{
				id = (int)StringUtils.ToNumber(list[0]);
			}
			if(i == 1)
			{
				uipanel = list[1];
			}
			if(i == 2)
			{
				position = list[2];
			}
			if(i == 3)
			{
				des = list[3];
			}
			if(i == 4)
			{
				zh_cn = list[4];
			}
			if(i == 5)
			{
				en_us = list[5];
			}
			if(i == 6)
			{
				zh_tw = list[6];
			}
			if(i == 7)
			{
				ja_jp = list[7];
			}
			if(i == 8)
			{
				ko_kr = list[8];
			}
			if(i == 9)
			{
				de_de = list[9];
			}
			if(i == 10)
			{
				es_la = list[10];
			}
		}
	}

	public void DecodeConfigItem()
	{
		for (int i = 0; i < list.Count; i++)
		{
		}
		list = null;
	}


	public static List<LanguageConfig> Configs = new List<LanguageConfig>();

	public static LanguageConfig GetConfig(int key)
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
			LanguageConfig item = new LanguageConfig();
			item.Decode(list[i]);
			Configs.Add(item);
		}
	}

	public static LanguageConfig GetConfigWidth(string paramName,object value)
	{
		Type t = typeof(LanguageConfig);
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