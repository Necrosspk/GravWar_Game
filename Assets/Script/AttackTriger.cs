using UnityEngine;
using System.Collections;

public class AttackTriger : MonoBehaviour {
	private bool attacked;
	private characterControllerScript playerSCR;
	public int dmg;	
	public AudioSource Punch;
	private EnemyScriptMelle EnSCR;
	private float spddmg;
	private float time = 0;

	void Start()
	{
		playerSCR = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		spddmg = playerSCR.spddmg;
	}


	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log("Trigger: Player Entered");
		if (other.gameObject.tag == "Enemy") 
		{
			EnSCR = other.GetComponent<EnemyScriptMelle> ();
			if (time >= spddmg)
			{
				if(attacked)
				{
					Punch.Play();
					EnSCR.TakeDmg(dmg);
					time = 0;				
					attacked = false;
				}
			}
			//Debug.Log("WOW!");
		}
	}

	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy") 
						attacked = false;				
	}

	void Update()
	{
		time += Time.deltaTime;
		dmg = playerSCR.Damage;		
		spddmg = playerSCR.spddmg;
			if (Input.GetButtonDown("Fire1"))
			{
				attacked = true;
				playerSCR.anim.SetBool("Attack", true);
			}
		else
			playerSCR.anim.SetBool("Attack", false);
	}
}

