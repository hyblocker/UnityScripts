using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Utilities/Text Shadow Coloriser")]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Shadow))]
public class ShadowText : MonoBehaviour {

	private Shadow thisShadow;
	private Text thisText;

	public float DarkenFactor = 100;

	public  float updateInterval = 0.05F;

	private float timeleft; // Left time for current interval

	// Use this for initialization
	void Start () {
		thisShadow = GetComponent<Shadow> ();
		thisText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeleft -= Time.deltaTime;

		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
		{
			float H, S, V;
			Color.RGBToHSV (thisText.color, out H, out S, out V);
			V -= DarkenFactor/255;
			if (V<=0) {
				V = 0;
			}
			thisShadow.effectColor = Color.HSVToRGB (H,S,V);
			//	DebugConsole.Log(format,level);
			timeleft = updateInterval;
		}
	}
}
