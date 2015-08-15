﻿using UnityEngine;
using System.Collections;

public class CreatureDirector : MonoBehaviour {
	//Count
	public int countCreature = 0;
	public int maxCountCreature = 0;

	//portal
	private PortalScript portal;
	//random
	public int randSpawnerCreature = 0;
	//spawner script
	public SpawnerScript spawner;
	private bool hit;
	//Timers
	public float timer = 0f;
	public float[] progressTimers = new float[10]; 
	public float timeToSpawn = 3f;
	private float timer2 = 0f;

	// Use this for initialization
	void Start () 
	{
		maxCountCreature = 0;
		portal = (PortalScript)FindObjectOfType(typeof(PortalScript));
		/*
		progressTimers [0] = 5f;
		progressTimers [1] = 15f;
		progressTimers [2] = 45f;
		progressTimers [3] = 80f;
		progressTimers [4] = 120f;
		progressTimers [5] = 160f;
		progressTimers [6] = 250f;
		progressTimers [7] = 350f;
		progressTimers [8] = 450f;
		progressTimers [9] = 580f;
		progressTimers [10] = 640f;
		*/
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Spawner") 
		{
			hit = true;
			spawner = other.gameObject.GetComponent<SpawnerScript> ();		
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Spawner") 
		{
			hit = false;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		timer2 -= Time.deltaTime;
		switch ((int)timer)
		{
		case 5:
				maxCountCreature = 3;
				timeToSpawn = 5f;
			break;
		case 15:
				maxCountCreature = 5;
				timeToSpawn = 4f;
			break;
		case 45:
				maxCountCreature = 8;
				timeToSpawn = 3f;
			break;
		case 80:
				maxCountCreature = 12;
				timeToSpawn = 2.5f;
			break;
		case 120:
				maxCountCreature = 16;
				timeToSpawn = 2f;
			break;
		case 160:
				maxCountCreature = 17;
				timeToSpawn = 1.5f;
			break;
		case 250:
				maxCountCreature = 18;
				timeToSpawn = 1.1f;
			break;
		case 350:
				maxCountCreature = 19;
				timeToSpawn = 1.1f;
			break;
		case 450:
				maxCountCreature = 21;
				timeToSpawn = 0.9f;
			break;
		case 580:
				maxCountCreature = 22;
				timeToSpawn = 0.7f;
			break;
		case 640:
			maxCountCreature = 23;
				timeToSpawn = 0.4f;
			break;
		}
		if (countCreature < maxCountCreature && !portal.isStarted && !portal.ended && hit && timer2 < 0) 
		{
			spawner.DoSpawn();
			timer2 = timeToSpawn;
		}
		if (countCreature > 0 && portal.ended)
			portal.NPCallive = countCreature;

	}
}