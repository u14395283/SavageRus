/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * And TheSavageRu's
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
using System;
using System.IO;
using AssemblyCSharp;
using UnityEngine.SceneManagement;


public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    #region PUBLIC_MEMBERS
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour that is instanciated for augmentations of new user defined targets.
    /// </summary>
    public ImageTargetBehaviour ImageTargetTemplate;
    public GameObject takePhoto;
	public GameObject backBtn;
    public GameObject viewGraphs;
    public GameObject editGraph;
    public GameObject shareGraph;
	public GameObject form;
	public GameObject UI;
	public GameObject loadScreen;
	public GameObject frameCanvas;

	private bool isProcessing = false;
	public string message = "Shared muthafucker";

    protected bool editClicked = false;
    public int LastTargetIndex
    {
        get { return (mTargetCounter - 1) % MAX_TARGETS; }
    }
    #endregion PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS
    private const int MAX_TARGETS = 1;
    private UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;
    private QualityDialog mQualityDialog;
    private ObjectTracker mObjectTracker;


    // DataSet that newly defined targets are added to
    private DataSet mBuiltDataSet;
    
    // Currently observed frame quality
    private ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;
    
    // Counter used to name newly created targets
    private int mTargetCounter;

    private TrackableSettings mTrackableSettings;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    public void Start()
    {
        takePhoto = GameObject.Find("takePhoto");
        viewGraphs = GameObject.Find("viewGraphs");
        editGraph = GameObject.Find("editGraph");
        shareGraph = GameObject.Find("shareGraph");
		backBtn = GameObject.Find("backBtn");
		form = GameObject.Find("Form");
		loadScreen = GameObject.Find ("LoadScreen");

		backBtn.SetActive (false);
		form.SetActive (false);
        shareGraph.SetActive(false);
        editGraph.SetActive(false);
		loadScreen.SetActive (false);

        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (mTargetBuildingBehaviour)
        {
            mTargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log("Registering User Defined Target event handler.");
        }

        mTrackableSettings = FindObjectOfType<TrackableSettings>();
        mQualityDialog = FindObjectOfType<QualityDialog>();
        if (mQualityDialog)
        {
            mQualityDialog.gameObject.SetActive(false);
        }
            
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region IUserDefinedTargetEventHandler implementation
    /// <summary>
    /// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
    /// </summary>
    public void OnInitialized ()
    {
        mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (mObjectTracker != null)
        {
            // Create a new dataset
            mBuiltDataSet = mObjectTracker.CreateDataSet();
            mObjectTracker.ActivateDataSet(mBuiltDataSet);
        }
    }

    /// <summary>
    /// Updates the current frame quality
    /// </summary>
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        mFrameQuality = frameQuality;
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
        {
            Debug.Log("Low camera image quality");
        }
    }

	/// <summary>
	/// Finds non-digit characters in str parameter and replaces them with nearest estimate digits.
	/// </summary>
		
	public void editDataClicked(){
		UI.GetComponent<UI> ().initialiseForm (Dataset.dataset);
		form.SetActive (true);
	}

    /// <summary>
    /// Takes a new trackable source and adds it to the dataset
    /// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
    /// </summary>
	public void OnNewTrackableSource(TrackableSource trackableSource)
	{
		mTargetCounter++;

		// Deactivates the dataset first
		mObjectTracker.DeactivateDataSet(mBuiltDataSet);

		// Destroy the oldest target if the dataset is full or the dataset 
		// already contains five user-defined targets.
		if (mBuiltDataSet.HasReachedTrackableLimit() || mBuiltDataSet.GetTrackables().Count() >= MAX_TARGETS)
		{
			IEnumerable<Trackable> trackables = mBuiltDataSet.GetTrackables();
			foreach (Trackable trackable in trackables)
			{
				mBuiltDataSet.Destroy(trackable, true);
			}
		}


		// Get predefined trackable and instantiate it       -----  Add more targets
		/*ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
		imageTargetCopy.gameObject.name = "UserDefinedTarget-" + mTargetCounter;*/

		// Add the duplicated trackable to the data set and activate it
		mBuiltDataSet.CreateTrackable(trackableSource, ImageTargetTemplate.gameObject);

		// Activate the dataset again
		mObjectTracker.ActivateDataSet(mBuiltDataSet);

		// Extended Tracking with user defined targets only works with the most recently defined target.
		// If tracking is enabled on previous target, it will not work on newly defined target.
		// Don't need to call this if you don't care about extended tracking.
		StopExtendedTracking();
		mObjectTracker.Stop();
		mObjectTracker.ResetExtendedTracking();
		mObjectTracker.Start();
	}
    #endregion IUserDefinedTargetEventHandler implementation

    //function called from a button
	public void ButtonShare ()
	{
		shareGraph.SetActive(false);
		if(!isProcessing){
			StartCoroutine( ShareScreenshot() );
		}
	}
	public IEnumerator ShareScreenshot()
	{
		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ message);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;
		shareGraph.SetActive(true);
	}

	public void resetApp(){
		ModelInstantiator.resetGraph ();

		int scene = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (scene, LoadSceneMode.Single);
	}

	IEnumerator UploadFileCo(string uploadURL)
	{
		//Take screen shot
		yield return new WaitForEndOfFrame();
		Texture2D screenTexture = new Texture2D((int)(FrameCoordinates.size.x - (FrameCoordinates.borderWidth * 2)), (int)(FrameCoordinates.size.y - (FrameCoordinates.borderWidth * 2)), TextureFormat.RGB24, true);

		//screenTexture.ReadPixels(new Rect(FrameCoordinates.left, FrameCoordinates.bottom, FrameCoordinates.size.x, FrameCoordinates.size.y),0,0);
		screenTexture.ReadPixels(new Rect(FrameCoordinates.left + FrameCoordinates.borderWidth, FrameCoordinates.bottom + FrameCoordinates.borderWidth, FrameCoordinates.size.x - (FrameCoordinates.borderWidth * 2), FrameCoordinates.size.y - (FrameCoordinates.borderWidth * 2)),0,0);
		//screenTexture.ReadPixels(new Rect(0f, 0f+150, Screen.width, Screen.height-150),0,0);

		screenTexture.Apply();

		//Rotate pictures taken in landscape
		if (!(Input.deviceOrientation == DeviceOrientation.Portrait))
		{
			Debug.Log ("###---ORIENTATION SWITCHED TO LANDSCAPE---###");
			Texture2D screenTexture2 = new Texture2D((int)(FrameCoordinates.size.y - (FrameCoordinates.borderWidth * 2)), (int)(FrameCoordinates.size.x - (FrameCoordinates.borderWidth * 2)), TextureFormat.RGB24,true);
			Color32[] pixels = screenTexture.GetPixels32();
			pixels = Helpers.RotateMatrix(pixels, (int)(FrameCoordinates.size.x - (FrameCoordinates.borderWidth * 2)), (int)(FrameCoordinates.size.y - (FrameCoordinates.borderWidth * 2)));
			screenTexture2.SetPixels32(pixels); 
			screenTexture = screenTexture2;
		}

		//Convert image to bytes
		byte[] pData2 = screenTexture.EncodeToJPG();
		//UnityEngine.Object.Destroy(screenTexture);

		loadScreen.SetActive (true);
		//Create post request
		WWWForm postForm = new WWWForm();

		string postData = null;

		bool sw = false;

		if (!sw) {
			postData = "--09sad98as09dhidp0a98soakscajbva12\n"+
				"Content-Type: application/json; charset=UTF-8\n\n"+
				"{\"engine\":\"tesseract\"}\n\n"+
				"--09sad98as09dhidp0a98soakscajbva12\n"+
				"Content-Type: image/jpeg\n"+
				"Content-Disposition: attachment; filename=\"attachment.txt\". \n\n";
		} else {
			postData = "--09sad98as09dhidp0a98soakscajbva12\n"+
			"Content-Type: application/json; charset=UTF-8\n\n"+
			"{\"engine\":\"tesseract\", \"preprocessors\":[\"stroke-width-transform\"]}\n\n"+
			"--09sad98as09dhidp0a98soakscajbva12\n"+
			"Content-Type: image/jpeg\n"+
			"Content-Disposition: attachment; filename=\"attachment.txt\". \n\n";
		}
			

		byte[] pData1 = System.Text.Encoding.UTF8.GetBytes(postData.ToCharArray());

		string postDataEnd = "\n--09sad98as09dhidp0a98soakscajbva12--\n";

		byte[] pData3 = System.Text.Encoding.UTF8.GetBytes(postDataEnd.ToCharArray());

		//combine different parts of request into byte array
		byte[] pDataCom = new byte[pData1.Length + pData2.Length + pData3.Length];
		System.Buffer.BlockCopy(pData1, 0, pDataCom, 0, pData1.Length);
		System.Buffer.BlockCopy(pData2, 0, pDataCom, pData1.Length, pData2.Length);
		System.Buffer.BlockCopy(pData3, 0, pDataCom, pData1.Length + pData2.Length, pData3.Length);

		System.Collections.Generic.Dictionary<string, string> headers = new System.Collections.Generic.Dictionary<string, string>();
		headers.Add("Content-Type", "multipart/related;boundary=09sad98as09dhidp0a98soakscajbva12");

		byte[] pData = System.Text.Encoding.UTF8.GetBytes(postData.ToCharArray());

		//Create connection
		WWW upload = new WWW(uploadURL, pDataCom,headers);
		yield return upload;
		if (upload.error == null)
		{
			Debug.Log("\n\n"+System.DateTime.Now.ToString()+": OCR RESULT: "+upload.text);
			string result = upload.text.Trim();
			result += "\n";

			MakeDataset (result);
			backBtn.SetActive(true);
			shareGraph.SetActive(true);
			editGraph.SetActive(true);
			viewGraphs.SetActive(false);
			takePhoto.SetActive(false);
			frameCanvas.SetActive (false);
		}
		else
		{
			Debug.Log("WWW Error: "+ upload.error);
			Debug.Log("WWW Error: "+ upload.text);
			loadScreen.SetActive (false);
		}


	}
	void UploadFile(string uploadURL)
	{	
		 StartCoroutine(UploadFileCo(uploadURL));
	}

	//Generates a Dataset from the OCR result
	void MakeDataset(string text){
		Dataset.Types type = Dataset.Types.Null;
		string firstline = text.Substring(0, text.IndexOf("\n"));
		try{	
			text = text.Replace(firstline, null);
		} catch {
			resetApp ();
		}
		Debug.Log ("Title: " + firstline+"\n");
		text = text.TrimStart();

		string ser = text.Substring(0, text.IndexOf("\n"));
		try{
			text = text.Replace(ser, "");
		} catch {
			resetApp ();
		}
		string[] tempSer = ser.Split (' ');

		string[] series = new string[tempSer.Length - 1];

		for(int i = 1; i < tempSer.Length; i++){
			series [i - 1] = tempSer [i];
		}

		Debug.Log ("Series: " + ser+"\n");
		text = text.TrimStart();



		System.Collections.Generic.List<string[]> tableList = new System.Collections.Generic.List<string[]>();
		System.Collections.Generic.List<string> categoryList = new System.Collections.Generic.List<string>();
		string temp = "";
		bool success = true;

		const int MIN_LENGTH = 1;
		while(text.Length > MIN_LENGTH && text.IndexOf("\n") > 0){
			temp = text.Substring(0, text.IndexOf("\n"));
			text = text.Replace(temp, "");
			string catName = "";

			for(int x = 0; x < text.Length; x++){
				if (!Char.IsDigit (text [x])) {
					catName += text [x];
				} else {
					break;
				}
			}

			try{
				text = text.Replace(catName, "");
			} catch {
				resetApp ();
			}

			categoryList.Add (catName);

			//Removes series entries from rows
			string[] rowAndCategory = temp.Split(' ');
			string[] row = new string[rowAndCategory.Length-1];



			for(int i = 0; i < rowAndCategory.Length; i++){
				if (!rowAndCategory [i].All (char.IsDigit)) {
					row [i - 1] = Helpers.sanitizeString( rowAndCategory [i] );
				} else {
					row [i - 1] = rowAndCategory [i];
				}
			}

			tableList.Add(row);
			Debug.Log ("Row: " + string.Join("",tableList.ElementAt(tableList.Count-1))+"\n");
			text = text.TrimStart();
		}
		string[] categories = categoryList.ToArray ();

		float[,] table = new float[series.Length, tableList.Count];
		for(int i = 0; i < tableList.Count; i++){
			for(int x = 0; x < series.Length; x++){
				float tempFloat = 0;

				try{	
					if (Single.TryParse (tableList.ElementAt (i) [x], out tempFloat)) {
					} else if (Single.TryParse (Helpers.sanitizeString (tableList.ElementAt (i) [x]), out tempFloat)) {
					} else {
						tempFloat = 0;
						success = false;
					}
				}catch (Exception e){
					tempFloat = 0;
					success = false;
				}

				table [x, i] = tempFloat;
			}
		}



		string dbg = "";
		for(int i = 0; i < tableList.Count; i++){
			dbg = "";
			dbg += categories [i] + " ";
			for(int x = 0; x < series.Length; x++){
				dbg += " : " + table[x,i];
			}
			Debug.Log (dbg);
		}

		//Send to form
		Dataset dataset = new Dataset (type, firstline, categories, series, "", "", table, categories.Length, series.Length);
		UI.GetComponent<UI> ().initialiseForm (dataset);
		loadScreen.SetActive (false);
		form.SetActive (true);

		//Create graph directly
		//Dataset.dataset = new Dataset (type, firstline, categories, series, tempSer[0], firstline, table, categories.Length, series.Length);
	}



    #region PUBLIC_METHODS
    /// <summary>
    /// Instantiates a new user-defined target and is also responsible for dispatching callback to 
    /// IUserDefinedTargetEventHandler::OnNewTrackableSource
    /// </summary>

    public void BuildNewTarget()
    {
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM || 
            mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            // create the name of the next target.
            // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
            string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, mTargetCounter);

            // generate a new target:
            mTargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
			UploadFile("http://105.255.168.115:9292/ocr-file-upload");

        }
        else
        {
            Debug.Log("Cannot build new target, due to poor camera image quality");
            if (mQualityDialog)
            {
                mQualityDialog.gameObject.SetActive(true);
            }
        }
    }

    public void CloseQualityDialog()
    {
        if (mQualityDialog)
            mQualityDialog.gameObject.SetActive(false);
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    /// <summary>
    /// This method only demonstrates how to handle extended tracking feature when you have multiple targets in the scene
    /// So, this method could be removed otherwise
    /// </summary>
    /// 
    private void StopExtendedTracking()
    {
        // If Extended Tracking is enabled, we first disable it for all the trackables
        // and then enable it only for the newly created target
        bool extTrackingEnabled = mTrackableSettings && mTrackableSettings.IsExtendedTrackingEnabled();
        if (extTrackingEnabled)
        {
            StateManager stateManager = TrackerManager.Instance.GetStateManager();

            // 1. Stop extended tracking on all the trackables
            foreach(var tb in stateManager.GetTrackableBehaviours())
            {
                var itb = tb as ImageTargetBehaviour;
                if (itb != null)
                {
                    itb.ImageTarget.StopExtendedTracking();
                }
            }
    
            // 2. Start Extended Tracking on the most recently added target
            List<TrackableBehaviour> trackableList =  stateManager.GetTrackableBehaviours().ToList();
            ImageTargetBehaviour lastItb = trackableList[LastTargetIndex] as ImageTargetBehaviour;
            if (lastItb != null)
            {
                if (lastItb.ImageTarget.StartExtendedTracking())
                    Debug.Log("Extended Tracking successfully enabled for " + lastItb.name);
            }
        }
    }


    #endregion //PRIVATE_METHODS
}



