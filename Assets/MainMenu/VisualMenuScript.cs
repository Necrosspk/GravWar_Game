using UnityEngine;
using System.Collections;

public class VisualMenuScript : MonoBehaviour {
	public Renderer rend;
	//private bool enter = false;
	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.gray;

	}
	void OnMouseEnter() {
		rend.material.color = Color.white;
		transform.localScale += new Vector3 (0.1f,0.1f,0);
		//enter = true;
	}
	void OnMouseDown() {
		transform.localScale -= new Vector3 (0.05f ,0.05f,0);
	}
	void OnMouseUp() {
		transform.localScale += new Vector3 (0.05f ,0.05f,0);
	}
	void OnMouseExit() {
		rend.material.color = Color.gray;
		transform.localScale -= new Vector3 (0.1f,0.1f,0);
	}

	void Update()
	{
		}
}
