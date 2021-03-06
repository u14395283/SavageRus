using System;
using System.IO;
using UnityEngine;

public class ImageManipulation
{
	public ImageManipulation ()
	{
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

