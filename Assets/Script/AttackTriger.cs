using UnityEngine;
using System.Collections;

public class AttackTriger : MonoBehaviour {
	private bool attacked;
	private bool attacked2;
	private bool attacked3;
	private bool attacked4;
	private characterControllerScript playerSCR;
	public int dmg;	
	public AudioSource Punch;
	private EnemyScriptMelle EnSCR;
	private float spddmg;
	private float spddmg2;
	private float spddmg3;
	private float spddmg4;
	public float time = 0;
	public float time2 = 1;
	public float time3 = 1;
	public float time4 = 1;

	private ArcherWar ArcherScript;

	void Start()
	{
		playerSCR = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		spddmg = playerSCR.spddmg;
		spddmg2 = playerSCR.spddmg2;
		spddmg3 = playerSCR.spddmg3;
		spddmg4 = playerSCR.spddmg4;
	}


	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log("Trigger: Player Entered");
		if (other.gameObject.tag == "EnemyMelle") 
		{
			EnSCR = other.GetComponent<EnemyScriptMelle> ();
			if(attacked2 || attacked3 || attacked4)
				attacked = false;
			if(attacked)
			{
				Punch.Play();
				EnSCR.TakeDmg(dmg);			
				attacked = false;
			}
			if(attacked || attacked3 || attacked4)
				attacked2 = false;
			if(attacked2)
			{
				EnSCR.StunD(0.5f);
				if(EnSCR.turn == 1 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(3000f,400f));
				if(EnSCR.turn == 2 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-400f,3000f));
				if(EnSCR.turn == 3 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-3000f,-400f));
				if(EnSCR.turn == 4 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(400f,-3000f));
				
				if(EnSCR.turn == 1 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-3000f,400f));
				if(EnSCR.turn == 2 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-400f,-3000f));
				if(EnSCR.turn == 3 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(3000f,-400f));
				if(EnSCR.turn == 4 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(400f,3000f));
				attacked2 = false;
			}
			if(attacked2 || attacked3 || attacked)
				attacked4 = false;
			if(attacked4)
			{
				Punch.Play();
				EnSCR.TakeDmg((int)(dmg*1.5));		
				attacked = false;
			}
			//Debug.Log("WOW!");
		}
		if (other.gameObject.tag == "EnemyArcher") 
		{
			ArcherScript = other.GetComponent<ArcherWar> ();
			if(attacked2 || attacked3 || attacked4)
				attacked = false;
			if(attacked)
			{
				Punch.Play();
				ArcherScript.TakeDmg(dmg);		
				attacked = false;
			}
			if(attacked || attacked3 || attacked4)
				attacked2 = false;
			if(attacked2)
			{
				ArcherScript.StunD(0.5f);
				if(ArcherScript.turnArcher == 1 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(3000f,400f));
				if(ArcherScript.turnArcher == 2 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-400f,3000f));
				if(ArcherScript.turnArcher == 3 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-3000f,-400f));
				if(ArcherScript.turnArcher == 4 && playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(400f,-3000f));
				
				if(ArcherScript.turnArcher == 1 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-3000f,400f));
				if(ArcherScript.turnArcher == 2 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(-400f,-3000f));
				if(ArcherScript.turnArcher == 3 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(3000f,-400f));
				if(ArcherScript.turnArcher == 4 && !playerSCR.isFacingRight)
					other.rigidbody2D.AddForce(new Vector2(400f,3000f));
				attacked2 = false;
			}

			if(attacked2 || attacked3 || attacked)
				attacked4 = false;
			if(attacked4)
			{
				Punch.Play();
				ArcherScript.TakeDmg((int)(dmg*1.5));		
				attacked = false;
			}
			//Debug.Log("WOW!");
		}
	}

	
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "EnemyMelle") 
		{
			attacked = false;
			attacked2 = false;
		}
		if (other.gameObject.tag == "EnemyArcher") 
		{
			attacked = false;
			attacked2 = false;
		}	
	}

	void Update()
	{
		time -= Time.deltaTime;
		time2 -= Time.deltaTime;
		time3 -= Time.deltaTime;
		time4 -= Time.deltaTime;
		dmg = playerSCR.Damage;		
		spddmg = playerSCR.spddmg;
		spddmg2 = playerSCR.spddmg2;
		spddmg3 = playerSCR.spddmg3;
		spddmg4 = playerSCR.spddmg4;
		//1
		if (Input.GetButtonDown ("Fire1") && time <= 0) 
		{
			attacked = true;
			playerSCR.anim.SetBool ("Attack", true);			
			time = spddmg;	
		} else
		{
			//attacked = false;
			playerSCR.anim.SetBool ("Attack", false);
		}

		//2
		if (Input.GetButtonDown("Fire2") && time2<0)
		{
			attacked2 = true;
			playerSCR.anim.SetBool("Attack2", true);			
			time2 = spddmg2;
		}
		else
		{
			//attacked2 = false;
			playerSCR.anim.SetBool ("Attack2", false);
		}

		//3
		if (Input.GetButtonDown("Fire3"))
		{
			attacked3 = true;
			playerSCR.anim.SetBool("Attack3", true);
		}
		else
		{
			//attacked3 = false;
			playerSCR.anim.SetBool ("Attack3", false);
		}

		//4
		if (Input.GetButtonDown("Fire4") && time4<0)
		{
			attacked4 = true;
			playerSCR.anim.SetBool("Attack4", true);
			time4 = spddmg4;
		}
		else
		{
			//attacked4 = false;
			playerSCR.anim.SetBool ("Attack4", false);
		}
	}

	void OnGUI()
	{		
			//GUI.Box (new Rect (0, 0, 100, 20), "HP: "+ hp + "%");
		if(time >0)
			GUI.Box (new Rect (0, 100, 100, 20), "KD spell 1: " + time);
		else
			GUI.Box (new Rect (0, 100, 100, 20), "KD spell 1: Ready");
		if(time2 >0)
			GUI.Box (new Rect (0, 125, 100, 20), "KD spell 2: " + time2);
		else
			GUI.Box (new Rect (0, 125, 100, 20), "KD spell 2: Ready");
		if(time3 >0)
			GUI.Box (new Rect (0, 150, 100, 20), "KD spell 3: " + time3);
		else
			GUI.Box (new Rect (0, 150, 100, 20), "KD spell 3: Ready");
		if(time4 >0)
			GUI.Box (new Rect (0, 175, 100, 20), "KD spell 4: " + time4);
		else
			GUI.Box (new Rect (0, 175, 100, 20), "KD spell 4: Ready");
	}
}

