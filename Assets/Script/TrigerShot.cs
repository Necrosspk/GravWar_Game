using UnityEngine;
using System.Collections;

public class TrigerShot : MonoBehaviour {
	public Transform BoomPrefab;
	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
//		var shotTransform = Instantiate(BoomPrefab) as Transform;
//		
//		// Определите положение
//		shotTransform.position = other.transform.position;
//		Destroy (other.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
