using UnityEngine;
using System.Collections;

public class ForestGamp_Bullet : MonoBehaviour 
{
	public GameObject forestGamp;
	private ArcherWar Gamp;
	private float flyTime;
	private float fly = 1f;

	// Use this for initialization
	void Start () 
	{
		renderer.enabled = false;
		Gamp = forestGamp.GetComponent<ArcherWar>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Gamp.attackArcher) 
		{
			renderer.enabled = true;
			rigidbody2D.AddForce(new Vector2 (10f, 0));
		}
		if (flyTime <= 0) 
		{
			transform.position = forestGamp.transform.position;
			flyTime = fly;
		}
		flyTime -= Time.deltaTime;
	}
}