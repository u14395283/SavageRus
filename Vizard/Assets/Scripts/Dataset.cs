//This class will be used to make sure the data is valid

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Dataset {
	public static Dataset dataset = null;

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

		this.title = title;
		this.categories = categories;
		this.series = series;
		this.catTitle = catTitle;
		this.seriesTitle = seriesTitle;
		this.values = values;
		this.categoriesCount = categoriesCount;
		this.seriesCount = seriesCount;
		if(type != Dataset.Types.Null)
			this.type = type;
		else setType ();

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

	public void swithcAxis()
	{
		string temp ;
		temp = catTitle ;
		catTitle = seriesTitle ;
		seriesTitle = temp ;
		float[,] tempValues = new float[categoriesCount,seriesCount];
		for(int i = 0; i < seriesCount; i++)
		{
			for(int x = 0; x < categoriesCount; x++)
			{
				tempValues[x,i] = values[i,x] ;
			}
		}
		values = tempValues ;
		int tempCount;
		tempCount = seriesCount ;
		seriesCount = categoriesCount ;
		categoriesCount = tempCount ;

		string[] tempSeries;
		tempSeries = series;
		series = categories;
		categories = tempSeries;
	}

	public void cycleType()
	{
		if(type == Types.Pie)
		{
			type = Types.Bar ;
			return ;
		}
		if(type == Types.Bar)
		{
			type = Types.Line ;
			return ;
		}
		if(type == Types.Line)
		{
			type = Types.Point ;
			return ;
		}
		if(type == Types.Point)
		{
			type = Types.Pie ;
			return ;
		}
	}

	/*public void setValues(float[,] _values)
	{
		values = _values  ; 
	}*/


	private void setType()
	{
		if (seriesCount > 1) { //possible line graph
			if(checkifDay(categories)) //continuous series of days
			{
				type = Types.Line ;
				return ;
			}
			if(checkIfMonth(categories))//continuous series of months
			{
				type = Types.Line ;
				return ;
			}
			if(checkIfNum(categories)) //continuous series of numbers (years, time, quater1, cycle1, etc..
			{
				type = Types.Line ;
				return ;
			}
			Debug.Log ("series > 1 not line");
		}
		if(checkIfOfTotal(title))
		{
			type = Types.Pie;
			Debug.Log ("ofTotal");
			return;
		}
		if(checkIfOfWhole(title))
		{
			type = Types.Pie;
			Debug.Log ("ofWhole");
			return;
		}
		if(checkIfPercent(title, categoriesCount))
		{
			type = Types.Pie;
			Debug.Log ("ofPercent");
			return;
		}
		if(checkIfPercentSign(title, categoriesCount))
		{
			type = Types.Pie;
			Debug.Log ("ofPercentSign");
			return;
		}
		//int num = Random.Range(1, 100); // creates a number between 1 and 100 for random chance of point cause #yolo
		//if (num > 2) 
		//{
		type = Types.Bar;
		return;
		//}
		//else type = Types.Point ;
		//return;
	}

	private bool checkIfNum(string[] arr)
	{
		//this should cover most cases of line graph eg: "quater 1", "quater 2"; years; times; in cases it is incorrect you can just cycle to wanted graph
		if(Regex.IsMatch(arr[0], @"\d"))
		{
			if(Regex.IsMatch(arr[1], @"\d"))
			{
				return true ;
			}
		}
		return false ;

	}

	private bool checkIfOfTotal(string _title)
	{
		int firstCharacter = title.IndexOf("of total");		
		if(firstCharacter != -1)
		{
			return true;
		}
		return false;
	}

	private bool checkIfOfWhole(string _title)
	{
		int firstCharacter = title.IndexOf("of whole");		
		if(firstCharacter != -1)
		{
			return true;
		}
		return false;
	}

	private bool checkIfPercent(string _title, int arrSize)
	{
		//check for the key word percent then check if the values add to 100 as this would indicate a definite pie chart
		int firstCharacter = (title.ToLower()).IndexOf("percent");		
		if(firstCharacter != -1)
		{
			//float total = 0;
			//for(int i = 0; i < arrSize; i++)
			//{
			//	total = values [i, 1] + total;
			//}
			//if(total > 80 && total < 120)
			//{
				return true ;
			//}
		}
		return false;
	}

	private bool checkIfPercentSign(string _title, int arrSize)
	{
		//check for the percent sign then check if the values add to 100 as this would indicate a definite pie chart
		int firstCharacter = title.IndexOf("%");		
		if(firstCharacter != -1)
		{
			//float total = 0;
			//for(int i = 0; i < arrSize; i++)
			//{
				//total = values [0, 1] + total;
			//}
			//if(total > 80 && total < 120)
			//{
				return true ;
			//}
		}
		return false;
	}

	private bool checkifDay(string[] arr)
	{
		//function looks for continous days in array, if found type is most likely line
		if ((arr [0].ToLower()).IndexOf ("mon") != -1 && (arr [1].ToLower()).IndexOf ("tue") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("tue") != -1 && (arr [1].ToLower()).IndexOf ("wed") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("wed") != -1 && (arr [1].ToLower()).IndexOf ("thur") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("thur") != -1 && (arr [1].ToLower()).IndexOf ("fri") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("fri") != -1 && (arr [1].ToLower()).IndexOf ("sat") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("sat") != -1 && (arr [1].ToLower()).IndexOf ("sun") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("sun") != -1 && (arr [1].ToLower()).IndexOf ("mon") != -1) {
			return true;
		} 
		return false;
	}

	private bool checkIfMonth(string[] arr)
	{
		//function looks for continous months in array, if found type is most likely line
		if ((arr [0].ToLower()).IndexOf ("jan") != -1 && (arr [1].ToLower()).IndexOf ("feb") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("feb") != -1 && (arr [1].ToLower()).IndexOf ("mar") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("mar") != -1 && (arr [1].ToLower()).IndexOf ("apr") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("apr") != -1 && (arr [1].ToLower()).IndexOf ("may") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("may") != -1 && (arr [1].ToLower()).IndexOf ("jun") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("jun") != -1 && (arr [1].ToLower()).IndexOf ("jul") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("jul") != -1 && (arr [1].ToLower()).IndexOf ("aug") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("aug") != -1 && (arr [1].ToLower()).IndexOf ("sep") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("sep") != -1 && (arr [1].ToLower()).IndexOf ("oct") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("oct") != -1 && (arr [1].ToLower()).IndexOf ("nov") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("nov") != -1 && (arr [1].ToLower()).IndexOf ("dec") != -1) {
			return true;
		} else if ((arr [0].ToLower()).IndexOf ("dec") != -1 && (arr [1].ToLower()).IndexOf ("jan") != -1) {
			return true;
		} else return false;
	}
}

//Title = Title of graph
//Values = Two dimentional array of floats containing all graph values
//Categories = string array with names of each category (x-axis)
//Series =  string array with names of each series (z-axis)
//Categories_title = title for categories
//Series_title =  title for series
//Series_Count = number of series
//Category_Count = number of categories