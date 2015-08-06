using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static class XCodeSaveMarker {
	
	[DllImport ("__Internal")]
	private static extern void _SaveVRImage (string type);
	
	public static void SaveMarkerToAlbum () {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			_SaveVRImage("");
		}	
	}
}