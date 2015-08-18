﻿using UnityEngine;
using System.Collections;

public class ItemFromChest : MonoBehaviour {
	public characterControllerScript player;
	public float TimeToGo = 0.5f;
	public float TimeStop = 0.7f;
	public float Timer = 0f;
	// Use this for initialization
	void Start () {
		this.gameObject.collider2D.enabled = false;
		player = (characterControllerScript)FindObjectOfType (typeof(characterControllerScript));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
			DestroyObject (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		if (Timer <= TimeToGo)
			transform.Translate(new Vector3(0,Time.deltaTime*1f,0)) ;
		if (Timer >= TimeStop) 
		{
			Vector3 dir = player.transform.position;
			transform.position = Vector3.Lerp(transform.position,dir,0.1f);
			this.gameObject.collider2D.enabled = true;
		}

	}
}