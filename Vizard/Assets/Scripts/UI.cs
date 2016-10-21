using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

public class UI : MonoBehaviour {

	public GameObject Form;
	public GameObject Grid;
	public GameObject Row;
	public GameObject Cell;
	public GameObject Category_title_Inputfield;
	public GameObject Series_title_Inputfield;
	public GameObject datagrid_x_size_inputfield;
	public GameObject datagrid_z_size_Inputfield;
	public GameObject type_box;

	private string[,] datagrid_values;
	private string category_title = "";
	private string series_title = "";
	private int datagrid_x_size = 3;
	private int datagrid_z_size = 3;
	private GameObject[] row_components = null;
	private Dataset.Types type = Dataset.Types.Null;

	private Dataset dataset;

	public void initialiseForm(Dataset _dataset){
		dataset = _dataset;

		type = dataset.type;

		type_box.GetComponent<InputField> ().text = type.ToString ();

		datagrid_z_size_Inputfield.GetComponent<InputField> ().text = dataset.seriesCount.ToString();
		datagrid_x_size_inputfield.GetComponent<InputField> ().text = dataset.categoriesCount.ToString();

		int z = int.Parse(datagrid_z_size_Inputfield.GetComponent<InputField> ().text) + 1;
		int x = int.Parse(datagrid_x_size_inputfield.GetComponent<InputField> ().text) + 1;

		datagrid_values = null;
		datagrid_values = new string[z,x];

		for (int x_counter = 0; x_counter < dataset.values.GetLength(1); x_counter++) {
			for (int z_counter = 0; z_counter < dataset.values.GetLength(0); z_counter++) {
				string s = dataset.values [z_counter, x_counter].ToString() ;
				datagrid_values [z_counter + 1, x_counter + 1] = s;
			}
		}

		for (int z_counter = 0; z_counter < dataset.values.GetLength(0); z_counter++) {
			string s = dataset.series[z_counter].ToString();
			datagrid_values [z_counter + 1, 0] = s;
		}
		for (int x_counter = 0; x_counter < dataset.values.GetLength(1); x_counter++) {
			string s = dataset.categories[x_counter].ToString();
			datagrid_values [0, x_counter + 1] = s;
		}

		Series_title_Inputfield.GetComponent<InputField> ().text = dataset.seriesTitle;
		Category_title_Inputfield.GetComponent<InputField> ().text = dataset.catTitle;

		datagrid_z_size = z;
		datagrid_x_size = x;

		drawGrid();
	}

	private void drawGrid(){
		if (row_components != null) {
			foreach (GameObject r in row_components) {
				GameObject.Destroy (r);
			}
		}

		row_components = new GameObject[datagrid_x_size];

		for (int rows = 0; rows < datagrid_x_size; rows++) {
			var row = GameObject.Instantiate (Row);
			row.name = "Row_" + rows.ToString ();

			row.transform.SetParent (Grid.transform);
			row.SetActive (true);

			for (int columns = 0; columns < datagrid_z_size; columns++) {
				var cell = GameObject.Instantiate (Cell);
				cell.name = "[" + rows.ToString() + ":" + columns.ToString() + "]";

				if (rows == 0 || columns == 0) {
					cell.GetComponent<UnityEngine.UI.Image> ().color = new Color32 (200, 200, 255, 255);
				}

				cell.AddComponent<Cell_ID> ();
				cell.GetComponent<Cell_ID> ().cell = columns;
				cell.GetComponent<Cell_ID> ().row = rows;

				if (datagrid_values != null && datagrid_values [columns, rows] != null) {
					cell.GetComponent<InputField> ().text = datagrid_values [columns, rows];
				}
				cell.GetComponent<InputField>().onValueChanged.AddListener (
					delegate {
						setValue ( cell );
					}
				);
				cell.transform.SetParent (row.transform);
				cell.SetActive (true);
			}
				
			row_components [rows] = row;
		}


	}

	public void switchAxis()
	{
		dataset.swithcAxis ();
		initialiseForm(dataset);
	}

	public void cycleGraph()
	{
		dataset.cycleType ();
		initialiseForm(dataset);
	}

	public void setSize(){
		try{
			int x = int.Parse(datagrid_x_size_inputfield.GetComponent<InputField> ().text) + 1;
			int z = int.Parse(datagrid_z_size_Inputfield.GetComponent<InputField> ().text) + 1;

			string[,] old_values = (string[,])datagrid_values.Clone();

			datagrid_values = new string[z,x];
			for (int z_counter = 0; z_counter < z; z_counter++){
				if(z_counter < datagrid_z_size){
					for (int x_counter = 0; x_counter < x; x_counter++)
					{
						if(x_counter < datagrid_x_size){
							string s = old_values[z_counter, x_counter];
							datagrid_values[z_counter, x_counter] = s;
						}
						else 
							datagrid_values[z_counter, x_counter] = "";
					}
				}
				else {
					for (int x_counter = 0; x_counter < x; x_counter++)
					{
						datagrid_values[z_counter, x_counter] = "";
					}
				}
			}

			datagrid_z_size = z;
			datagrid_x_size = x;

			drawGrid();
		}
		catch(UnityException e) {
			print (e.Data);
		}
	}

	public void setSeriesTitle(){
		series_title = Series_title_Inputfield.GetComponent<InputField> ().text;
	}

	public void setCategoryTitle(){
		category_title = Category_title_Inputfield.GetComponent<InputField> ().text;
	}

	public void setValue(GameObject cell)
	{
		datagrid_values[cell.GetComponent<Cell_ID> ().cell, cell.GetComponent<Cell_ID> ().row] = cell.GetComponent<InputField>().text;
	}

	public TrackableBehaviour theTrackable;
	private GameObject trackableGameObject;
		
	public void Button_Clicked () {

		int series_count = datagrid_z_size - 1;
		int category_count = datagrid_x_size - 1;

		float[,] float_values = new float[series_count, category_count];

		for (int c = 0; c < category_count; c++) {
			for (int s = 0; s < series_count; s++){
				float f = float.Parse(datagrid_values[s + 1,c + 1]);
				float_values [s, c] = f;
			}
		}

		string[] categories = new string[category_count];
		string[] series = new string[series_count];

		for (int c = 0; c < category_count; c++) {
			categories [c] = datagrid_values [0, c + 1];
		}

		for (int s = 0; s < series_count; s++) {
			series [s] = datagrid_values [s + 1, 0];
		}

		trackableGameObject = theTrackable.gameObject;

		foreach (Transform child in trackableGameObject.transform)
			GameObject.Destroy (child.gameObject);
			
		Dataset.dataset = new Dataset (type, "", categories, series, category_title, series_title, float_values, category_count, series_count);

		Form.SetActive (false);
	}

}
