using UnityEngine;
using System.Collections;

public class Prop_Quality : MonoBehaviour {
	public int quality;
	public Renderer rend;
	//private bool enter = false;
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.gray;
		
	}
	// Use this for initialization
	void OnMouseUp () {
		QualitySettings.SetQualityLevel (quality);
		}
	void Update() {
		if (QualitySettings.GetQualityLevel() == quality)
			rend.material.color = Color.white;
		else rend.material.color = Color.gray;
	}
}
