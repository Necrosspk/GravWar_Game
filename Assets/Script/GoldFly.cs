using UnityEngine;
using System.Collections;

public class GoldFly : MonoBehaviour {
	//public CheckGoldEmpty GO;
	public characterControllerScript player;
	public int money;
	private bool loot = false;
	private bool destroy = false;
	public float TimeToGo = 0.5f;
	public float TimeStop = 0.7f;
	public float Timer = 0f;
	// Use this for initialization
	void Start () 
	{
		this.gameObject.collider2D.enabled = false;
		loot = false;
		destroy = false;
		//GO = (CheckGoldEmpty)FindObjectOfType (typeof (CheckGoldEmpty));
		player = (characterControllerScript)FindObjectOfType (typeof(characterControllerScript));
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			loot = true;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		Timer += Time.deltaTime;
		if (Timer <= TimeToGo)
			transform.Translate(new Vector3(0,Time.deltaTime*1f,0)) ;
		if (Timer >= TimeStop) 
		{
			Vector3 dir = player.transform.position;
			transform.position = Vector3.Lerp(transform.position,dir,0.2f);
			this.gameObject.collider2D.enabled = true;
		}
		if (loot) 
		{
			player.money += money;
			destroy = true;
			loot = false;
		}
		if (destroy)
			DestroyObject (this.gameObject);
	}
}
