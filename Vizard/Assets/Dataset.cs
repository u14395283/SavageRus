//This class will be used to make sure the data is valid

using UnityEngine;
using System.Collections;

public class Dataset {
	public enum Types{
		Pie,
		Bar,
		Point,
		Line,
		Null //Used to initialise variable
	}

	public Dataset(
		Types type, 
		string title, 
		string[] categories,
		string[] series,
		string catTitle,
		string seriesTitle,
		float[,] values,
		int categoriesCount,
		int seriesCount){

		this.type = type;
		this.title = title;
		this.categories = categories;
		this.series = series;
		this.catTitle = catTitle;
		this.seriesTitle = seriesTitle;
		this.values = values;
		this.categoriesCount = categoriesCount;
		this.seriesCount = seriesCount;

	}
		
	public Types type;
	public string title;
	public string[] categories;
	public string[] series;
	public string catTitle;
	public string seriesTitle;
	public float[,] values;
	public int categoriesCount;
	public int seriesCount;

}

//Title = Title of graph
//Values = Two dimentional array of floats containing all graph values
//Categories = string array with names of each category (x-axis)
//Series =  string array with names of each series (z-axis)
//Categories_title = title for categories
//Series_title =  title for series
//Series_Count = number of series
//Category_Count = number of categories