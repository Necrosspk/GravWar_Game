using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject player;
	private characterControllerScript info;
	public float maxHP;
	public float currentHP;
	public Vector3 size;
	// Use this for initialization
	void Start () {
		info = player.GetComponent<characterControllerScript> ();
		maxHP = (float)info.Maxhp;
		size = this.transform.localScale;
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		maxHP = (float)info.Maxhp;	
		currentHP = (float)info.hp;
		this.transform.localScale = new Vector3((currentHP / maxHP * size.x),size.y,size.z);
		if (info.ouch) 
		{
			particleSystem.Play ();
			info.ouch = false;
		}
	}
	
}
