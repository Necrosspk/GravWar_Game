using UnityEngine;
using System.Collections;

public class HelthbarArcher : MonoBehaviour {

	public GameObject enemy;
	private ArcherWar info;
	public float maxHP;
	public float currentHP;
	public Vector3 size;
	// Use this for initialization
	void Start () {
		info = enemy.GetComponent<ArcherWar> ();
		maxHP = (float)info.hp;
		size = this.transform.localScale;
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		//maxHP = (float)info.Maxhp;	
		currentHP = (float)info.hp;
		this.transform.localScale = new Vector3((currentHP / maxHP * size.x),size.y,size.z);
		if (!info.alliveArcher)
			DestroyObject (this.gameObject);
		
	}
}
