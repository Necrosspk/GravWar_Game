using UnityEngine;
using System.Collections;

public class Prop_Antialiasing : MonoBehaviour {
	public int quality;
	public Renderer rend;
	//private bool enter = false;
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.gray;
		
	}
	// Use this for initialization
	void OnMouseUp () {
		QualitySettings.antiAliasing = quality;
	}
	void Update() {
		if (QualitySettings.antiAliasing == quality)
			rend.material.color = Color.white;
		else rend.material.color = Color.gray;
	}
}
