using System;
using System.IO;
using UnityEngine;

public class StringManipulation
{
	public StringManipulation ()
	{
	}

	static public string sanitizeString(string str){
		str = str.Replace ('B', '8');
		str = str.Replace ('O', '0');
		str = str.Replace ('o', '0');
		str = str.Replace ("%", "");
		str = str.Replace ('E', '8');
		str = str.Replace ('I', '1');
		str = str.Replace ('l', '1');
		str = str.Replace ('A', '4');
		str = str.Replace ('H', '4');
		str = str.Replace ('G', '6');
		str = str.Replace ('b', '6');
		str = str.Replace ('T', '7');
		str = str.Replace ('J', '7');
		str = str.Replace ('S', '5');
		str = str.Replace ("M", "44");
		str = str.Replace ('Q', '9');
		str = str.Replace ("R", "12");
		str = str.Replace ('Z', '2');
		str = str.Replace ('C', '0');
		str = str.Replace ('c', '0');


		float temp = 0;

		//If the string contains any remaining characters (other than a plus or minus at the beginning or end) remove them.
		if(!Single.TryParse (str, out temp)){
			for(int i = 0; i < str.Length; i++){
				if (!Char.IsDigit (str [i])) {
					if (i == 0 || i == str.Length - 1) {
						if (str [i] != '-' && str [i] != '+') {
							str = str.Replace ("" + str [i], "");
						}
					} else {
						str = str.Replace ("" + str[i], "");
					}
				}
			}
		}

		return str;
	}


}

