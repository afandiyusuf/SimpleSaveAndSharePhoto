using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;
public class PreviewAndSave : MonoBehaviour {

	Texture2D tex;
	public VuforiaBehaviour camera;
	public GameObject takeButton;
	public GameObject saveButton;
	public GameObject shareButton;
	public GameObject retakeButton;
	CaptureAndSave snapShot ;
	public RawImage previewer;
	public GameObject canvas;
	public ShareAndExperienceDemo SAE;
	public AndroidNativeUIDemo AND;
	public bool isSave = false;
	public string path;
	public GIGI gigi;
	public GIGI banner;


	void Start()
	{
		snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
		takeButton.SetActive(true);
		//retakeButton.SetActive(false);
		shareButton.SetActive(false);
		saveButton.SetActive(false);
	}

	void OnEnable()
	{
		CaptureAndSaveEventListener.onError += OnError;
		CaptureAndSaveEventListener.onSuccess += OnSuccess;
		CaptureAndSaveEventListener.onScreenShotInvoker += OnScreenShot;
	}

	void OnDisable()
	{
		CaptureAndSaveEventListener.onError -= OnError;
		CaptureAndSaveEventListener.onSuccess -= OnSuccess;
		CaptureAndSaveEventListener.onScreenShotInvoker -= OnScreenShot;
	}

	void OnError(string error)
	{
		Debug.Log ("Error : "+error);
		AND.ShowAlertPopup(error);
	}

	void OnSuccess(string msg)
	{
		path = msg;
		AND.ShowToastMessage("Image berhasil di save di "+msg);
		AND.ShowToastMessage("Saving image ");
		saveButton.SetActive(false);
		shareButton.SetActive(true);
	}

	void OnScreenShot(Texture2D tex2D)
	{
		AND.ShowToastMessage("Take image success");
		camera.enabled = false;
		previewer.gameObject.SetActive(true);
		tex = tex2D;
		previewer.texture = tex;
		canvas.SetActive(true);
	}
	public void TakeCamera()
	{
		canvas.SetActive(false);
		previewer.gameObject.SetActive(true);
		snapShot.GetFullScreenShot();

		takeButton.SetActive(false);
		retakeButton.SetActive(true);
		saveButton.SetActive(true);
		isSave = false;
	}

	public void SaveScreenShotToGallery()
	{
		snapShot.SaveTextureToGallery(tex);


	}
	public void EnableButton()
	{

		isSave = true;
	}
	public void Share()
	{
		SAE.ShareImageS(path);
	}

	public void RetakeCamera()
	{
		previewer.gameObject.SetActive(false);
		shareButton.SetActive(false);
		takeButton.SetActive(true);
		//retakeButton.SetActive(false);
		saveButton.SetActive(false);
		camera.enabled = true;

	}
}
