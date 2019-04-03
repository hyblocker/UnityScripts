using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This script is Attached to a GameObject with a Camera Component
* RenderAspectRatio is a hardcoded string, specifying the aspect ratio.
* Examples: 4:3, 16:9
*/
[RequireComponent(typeof(Camera))]
public class AspectRatio : MonoBehaviour {

	public string RenderAspectRatio = "16:9";

	void Start () {
		SetAspectRatio (RenderAspectRatio);
	}
	
	void Update () {
		SetAspectRatio (RenderAspectRatio);
	}

	public void SetAspectRatio(string Ratio){
		
		float xR = 1;
		float yR = 1;

		int pos = Ratio.IndexOf(":");
		xR = float.Parse(Ratio.Substring(0, pos));
		yR = float.Parse(Ratio.Substring(pos+1));
		//print ("Aspect Ratio:"+xR+" x "+yR);

		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		if (xR > yR) {
			Rect rect = camera.rect;

			rect.width = yR/xR;
			rect.height = 1;
			rect.y = 0;
			rect.x = (1.0f - yR/xR) / 2.0f;

			camera.rect = rect;
		} else {
			Rect rect = camera.rect;

			rect.width = 1;
			rect.height = xR/yR;
			rect.x = 0;
			rect.y = (1.0f - xR/yR) / 2.0f;

			camera.rect = rect;
		}
	}
}
