using lib;
using System;

public class Language
{
    public static Language instance = new Language();

    public string GetLanguage(int languageType,int languageId)
    {
        LanguageConfig lc = LanguageConfig.GetConfig(languageId);
        if (lc == null) return "";
        Type t = typeof(LanguageConfig);
        return (string)t.GetField(LanguageTypeConfig.GetConfig(languageType).name).GetValue(lc);
    }
}