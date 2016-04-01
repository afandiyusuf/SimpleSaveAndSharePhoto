using UnityEngine;
using System.Collections;
using System;

public class ShareAndExperienceDemo : MonoBehaviour {	

	private bool isImmersive = false;
	private SharePlugin sharePlugin;
	private UtilsPlugin utilsPlugin;
	private LocalNotificationPlugin localNotificationPlugin;
	string screenShotName = "";
	string folderPath;
	string path ="";
	public AndroidNativeUIDemo AND;
	
	// Use this for initialization
	void Start (){
		localNotificationPlugin = LocalNotificationPlugin.GetInstance();
		localNotificationPlugin.SetDebug(0);

		int isOpenUsingNotification = localNotificationPlugin.IsOpenUsingNotification();
		Debug.Log("[ShareAndExperienceDemo]: isOpenUsingNotification " + isOpenUsingNotification);

		if(isOpenUsingNotification == 1){
			//do something here
		}else{
			//do something here
		}

		utilsPlugin = UtilsPlugin.GetInstance();
		utilsPlugin.SetDebug(0);

		sharePlugin = SharePlugin.GetInstance();
		sharePlugin.SetDebug(0);
	}
	
	public void ActivateLocalNotification(){
		Debug.Log("ActivateScheduleNotification");
		//schedule notification
		//1000 = 1 sec
		int delay = 3000;
		localNotificationPlugin.ScheduleNotification("my notification title","my notification message","my notification ticker message",delay,true,true);
	}

	public void CancelLocalNotification(){
		localNotificationPlugin.ClearAllScheduledNotification();
	}

	public void ImmersiveToggle(){
		if(!isImmersive){
			utilsPlugin.ImmersiveOn(500);
			isImmersive = true;
		}else{
			utilsPlugin.ImmersiveOff();
			isImmersive = false;
		}
	}

	public void ShareText(){		
		//share link
		sharePlugin.ShareUrl("my subject","my subject content","https://www.urltoshare.com");
	}

	public void ShareImage(){

		//if you want to save on Application.persistentDataPath, file on this path is remove when app is uninstal
		//string screenShotName = "AndroidUltimateScreenShot.png";
		//string path = Application.persistentDataPath + "/" + screenShotName;

		string screenShotName = "AUPScreenShot.jpg";
		string folderPath = utilsPlugin.CreateFolder("MyScreenShots",0);
		string path ="";

		if(!folderPath.Equals("",StringComparison.Ordinal)){
			path = folderPath + "/" + screenShotName;

			//note: we added new required variable to pass which is screenShotName to determined what image format to use
			//jpg or png, if format is not given set default to jpg format
			StartCoroutine(AUP.Utils.TakeScreenshot(path,screenShotName));
			sharePlugin.ShareImage("subject","subjectContent",path);
		}
	}

	public void TakeScreenShot()
	{
		string screenShotName = "IMG_"+UnityEngine.Random.Range(0,100000).ToString()+".jpg";
		string folderPath = utilsPlugin.CreateFolder("GigiPhoto",0);
		string path ="";
		
		if(!folderPath.Equals("",StringComparison.Ordinal)){
			path = folderPath + "/" + screenShotName;
			
			//note: we added new required variable to pass which is screenShotName to determined what image format to use
			//jpg or png, if format is not given set default to jpg format
			StartCoroutine(AUP.Utils.TakeScreenshot(path,screenShotName));
			AND.ShowToastMessage(path);

		}
	}

	public void ShareImageS(string _path)
	{
		sharePlugin.ShareImage("subject","subjectContent",_path);
	}
}