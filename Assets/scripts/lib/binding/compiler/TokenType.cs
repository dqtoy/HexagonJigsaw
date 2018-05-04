﻿using System.Collections.Generic;

namespace lib
{
    public class TokenType
    {
        public static Dictionary<string,object> Type = JSON.Parse(@"{'endSign':'$','public':'public','private':'private','protected':'protected','final':'final','dynamic':'dynamic','internal':'internal','class':'class','interface':'interface','extends':'extends','implements':'implements','import':'import','var':'var','static':'static','const':'const','function':'function','override':'override','void':'void','return':'return','package':'package','flashProxy':'flash_proxy','namespace':'namespace','finally':'finally','new':'new','as':'as','is':'is','get':'get','set':'set','Vector':'Vector','op':'op','id':'id','valueInt':'CInt','valueOxInt':'OXCInt','valueNumber':'CNumber','valueString':'CString','valueRegExp':'RegExp','null':'null','true':'true','false':'false','if':'if','else':'else','for':'for','each':'each','in':'in','do':'do','while':'while','switch':'switch','case':'case','default':'default','continue':'continue','break':'break'}") as Dictionary<string, object>;
        public static Dictionary<string, object> TokenTrans = JSON.Parse(@"{'op': true}") as Dictionary<string, object>;
    }
}