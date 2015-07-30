using UnityEngine;
using System.Collections;

public class EnemyMelleAttackTriger : MonoBehaviour {

	public int dmg;
	public GameObject girl;
	private bool attacked;
	private bool allive;
	private bool TimeToAttack;
	private characterControllerScript playerSCR;
	private EnemyScriptMelle EnSCR;
	
	void Start()
	{
		EnSCR = girl.GetComponent<EnemyScriptMelle> ();
		dmg = EnSCR.BaseDmg;
		allive = EnSCR.allive;
	}
	
	
	void OnTriggerEnter2D (Collider2D other) 
	{
		attacked = true;
		if (other.gameObject.tag == "Player") 
		{
			if(attacked && allive && TimeToAttack)
			{
				EnSCR.audio.Play();
				playerSCR = other.GetComponent<characterControllerScript>();
				playerSCR.TakeDmg(dmg);
				attacked = false;
				EnSCR.attack = false;
			}
			//Debug.Log("WOW!");
		}
	}
	
	
	void OnTriggerExit2D (Collider2D other) {
		//Debug.Log("Trigger: Player Entered");
		if (other.gameObject.tag == "Player") 
			attacked = false;				
	}
	
	void Update()
	{
		if (EnSCR.attack)
						TimeToAttack = true;
				else
						TimeToAttack = false;
		allive = EnSCR.allive;
	}
}
