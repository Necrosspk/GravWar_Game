using UnityEngine;
using System.Collections;

public class Prop_Resolution : MonoBehaviour {
	public int w;
	public int h;
	public Renderer rend;
	//private bool enter = false;
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.gray;
		
	}
	// Use this for initialization
	void OnMouseUp () {
		Screen.SetResolution (w, h, true);
	}
	void Update() {
		if (Screen.GetResolution[0].width == w && Screen.GetResolution[0].height == h)
			rend.material.color = Color.white;
		else rend.material.color = Color.gray;
	}
}
