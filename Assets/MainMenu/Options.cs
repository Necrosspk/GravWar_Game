using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	public Camera camera;
	public bool op;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = camera.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void OnMouseUp () {
		anim.SetBool ("Options", op);	
	}
}
