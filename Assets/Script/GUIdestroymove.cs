using UnityEngine;
using System.Collections;

public class GUIdestroymove : MonoBehaviour {
	// Use this for initialization
	private float yyy ;
	private float xxx ;
	private CameraSmooth cam;
	void Start () {		
		cam = (CameraSmooth)FindObjectOfType (typeof(CameraSmooth));
		yyy = this.transform.position.y;
		xxx = this.transform.position.x;
		DestroyObject (gameObject,1.4f);
	}
	
	// Update is called once per frame
	void Update () {
		if (cam.rotate == 1) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), 300f * Time.deltaTime);
			transform.position = new Vector3 (transform.position.x, yyy += Time.deltaTime, 0f);
		}
		if (cam.rotate == 2) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), 300f * Time.deltaTime);
			transform.position = new Vector3 (xxx -= Time.deltaTime,transform.position.y,0f);
		}
		if (cam.rotate == 3) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 180), 300f * Time.deltaTime);
			transform.position = new Vector3 (transform.position.x, yyy -= Time.deltaTime, 0f);
		}
		if (cam.rotate == 4) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 270), 300f * Time.deltaTime);
			transform.position = new Vector3 (xxx += Time.deltaTime, transform.position.y,0f);
		}
	}
}
