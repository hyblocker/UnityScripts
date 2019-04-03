using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour {

    public Vector2 ScrollSpeed = new Vector2 (0.5f, 0.5f);
	
	// Update is called once per frame
	void Update () {
        float OffsetX = Time.time * ScrollSpeed.x;
        float OffsetY = Time.time * ScrollSpeed.y;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
