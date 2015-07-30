using UnityEngine;
using System.Collections;

public class Prop_Volume : MonoBehaviour {
	public float Volume;
	public Renderer rend;
	//private bool enter = false;
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.gray;
		
	}
	// Use this for initialization
	void OnMouseUp () {
		AudioListener.volume =Volume;
	}
	void Update() {
		if (AudioListener.volume  == Volume)
			rend.material.color = Color.white;
		else rend.material.color = Color.gray;
	}
}
