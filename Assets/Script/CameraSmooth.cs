using UnityEngine;
using System.Collections;

public class CameraSmooth : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Camera camera1;
	public int rotate = 1;
	public float rotateTime = 0.5f;
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y/*+0.75f*/,target.position.z));
			Vector3 delta = new Vector3(target.position.x, target.position.y/*+0.75f*/,target.position.z) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			
			
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			rotate++;
			if (rotate > 4)
					rotate = 1;
		}

		if (Input.GetKeyDown (KeyCode.E)) 
		{
			rotate--;
			if (rotate < 1)
				rotate = 4;
		}

		switch (rotate)
		{
		case 1:
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), rotateTime * Time.deltaTime);
			break;
		}
		case 2:
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 90), rotateTime * Time.deltaTime);
			break;
		}
		case 3:
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 180), rotateTime * Time.deltaTime);
			break;
		}
		case 4:
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 270), rotateTime * Time.deltaTime);
			break;
		}
		}
		
	}
}