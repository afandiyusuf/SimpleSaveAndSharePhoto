using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace AUP{
	public class Utils{
		public static void Message(string tag, string message){
			Debug.LogWarning(tag + message);
		}
		
		//take screen shot then share via intent
		public static IEnumerator TakeScreenshot(string screenShotPath,string screenShotName){
			yield return new WaitForEndOfFrame();
			
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			
			// Read screen contents into the texture
			tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			
			tex.Apply();

			byte[] screenshot;

			string[] pathParts = screenShotName.Split('.');
			
			if(pathParts.Length > 1){
				string format = pathParts.GetValue(1).ToString();
				Debug.Log( " format " + format );
				if(format.Equals("png",StringComparison.Ordinal)){
					screenshot = tex.EncodeToPNG();
					Debug.Log( "screen shot set to png format");
				}else if(format.Equals("jpg",StringComparison.Ordinal)){
					screenshot = tex.EncodeToJPG();
					Debug.Log( "screen shot set to jpg format");
				}else{
					screenshot = tex.EncodeToJPG();
					Debug.Log( "screen shot unknown format default to jpg");
				}
			}else{
				screenshot = tex.EncodeToJPG();
				Debug.Log( "screen shot no format default to jpg");
			}

			//saving to phone storage
			System.IO.File.WriteAllBytes(screenShotPath,screenshot);
		}
	}
}