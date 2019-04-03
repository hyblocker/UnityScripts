using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour {

	public GameObject ScrollObject;
	public static bool CanScroll = true;
	public GameObject PreviousScreen;

	public Vector3 MaxUp=new Vector3(0,0,0);
	public Vector3 MaxDown=new Vector3(0,0,0);

	public AudioSource Back;

	void Update () {
		if(ScrollObject.transform.localPosition==MaxUp){
			ScrollObject.transform.localPosition = MaxDown;
		}
		if(ScrollObject.transform.localPosition==new Vector3(MaxDown.x, MaxDown.y-5, MaxDown.z)){
			ScrollObject.transform.localPosition = new Vector3(MaxUp.x, MaxUp.y-5, MaxUp.z);
		}
		if(CanScroll){
			if(Input.GetButton("Up2")){
			  ScrollObject.transform.localPosition = ScrollObject.transform.localPosition + new Vector3 (0,5,0);
			}
			if(Input.GetButton("Down2")){
			  ScrollObject.transform.localPosition = ScrollObject.transform.localPosition - new Vector3 (0,5,0);
			}
		}
		if(Input.GetButton("Cancel")){
			PreviousScreen.SetActive (true);
			CanScroll = false;
			Back.Play ();
			GetComponent<Image>().gameObject.SetActive (false);
		}
	}
}
