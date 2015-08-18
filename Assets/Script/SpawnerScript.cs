using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	//NPCs
	public Transform NPCcommon;
	public Transform NPCarcher;
	//Player	
	public Transform player;
	// Use this for initialization
	void Start () {
		//randCreature = Random.Range (0, 100);
	}
	
	// Update is called once per frame
	void Update () {		

	}

	public bool DoSpawn()
	{
		int randCreature = Random.Range (0, 100);
		if (Vector3.Distance (player.position, this.transform.position) < 6.2f) 
		{
			if (randCreature < 30) 
			{
				var NPC = Instantiate (NPCarcher) as Transform;
				NPC.position = transform.position;
			} 
			else
			{
				var NPC = Instantiate (NPCcommon) as Transform;
				NPC.position = transform.position;
			}
			return true;
		} else
			return false;
	}
}
