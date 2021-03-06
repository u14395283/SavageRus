using System;
using System.IO;
using UnityEngine;

public class Helpers
{
	public Helpers ()
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

	static public Color32[] RotateMatrix(Color32[] matrix, int w, int h) {
		/*Texture2D text = new Texture2D (w, h, TextureFormat.RGB24,true);
		text.SetPixels32 (matrix);

		var byets = text.EncodeToPNG ();
		File.WriteAllBytes (Application.dataPath + "/original.png", byets);*/


		Color32[] ret = new Color32[w * h];

		Color32[,] temp = new Color32[h,w];

		for(int i = 0; i < h; i++){
			for(int x = 0; x < w; x++){
				temp[i,x] = matrix[i*w+x];
			}
		}

		Debug.Log ("###---ROTATING IMAGE FOR OCR---###");

		Color32[,] temp2 = new Color32[w,h];

		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				temp2[j,i] = temp[i,j];
			}
		}

		Color32[,] temp3 = new Color32[w,h];

		for (int i = 0; i < w; i++) {
			for (int j = 0; j < h; j++) {
				temp3[i,j] = temp2[i,(h-1)-j];
			}
		}

		for(int i = 0; i < w; i++){
			for(int x = 0; x < h; x++){
				ret[i*h+x] = temp3[i,x] ;
			}
		}

		Debug.Log ("###---ROTATION SUCCESS---###");

		/*Texture2D tex = new Texture2D (h, w, TextureFormat.RGB24,true);
		tex.SetPixels32 (ret);

		var bytes = tex.EncodeToPNG ();
		File.WriteAllBytes (Application.dataPath + "/sample.png", bytes);*/

		return ret;
	}
}

