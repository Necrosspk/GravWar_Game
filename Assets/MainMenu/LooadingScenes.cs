using UnityEngine;
using System.Collections;

public class LooadingScenes : MonoBehaviour {
	public string Level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnMouseUp () {
		Application.LoadLevel (Level);
	
	}
}
