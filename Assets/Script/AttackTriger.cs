using UnityEngine;
using System.Collections;

public class AttackTriger : MonoBehaviour {
	private bool attacked;
	private bool attacked2;
	private bool attacked3;
	private bool attacked4;
	private characterControllerScript playerSCR;
	public int dmg;	
	public int dmg2;
	public AudioSource Punch;
	public AudioSource Punch2;
	public AudioSource Punch3;
	public AudioSource Punch4;
	private EnemyScriptMelle EnSCR;
	private float spddmg;
	private float spddmg2;
	private float spddmg3;
	private float spddmg4;
	public float time = 0;
	public float time2 = 1;
	public float time3 = 1;
	public float time4 = 1;
	private bool critical = false;
	public Transform Fire1;
	public Transform Fire4;

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
				if (!critical)
					EnSCR.TakeDmg(dmg);	
				if (critical)
				{
					EnSCR.critical = true;
					EnSCR.TakeDmg(dmg*2);
					critical = false;
				}
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
				if (!critical)
					EnSCR.TakeDmg(dmg*1.7);
				if (critical)
				{
					EnSCR.critical = true;
					EnSCR.TakeDmg(dmg*2*1.7);	
					critical = false;
				}
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
				if (!critical)
					ArcherScript.TakeDmg(dmg);	
				if (critical)
				{
					ArcherScript.critical = true;
					ArcherScript.TakeDmg(dmg*2);
					critical=false;
				}
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
				if (!critical)
					ArcherScript.TakeDmg(dmg*2*1.7);
				if (critical)
				{
					ArcherScript.critical = true;
					ArcherScript.TakeDmg(dmg*1.7);
					critical=false;
				}
				attacked4= false;
			}
			//Debug.Log("WOW!");
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "EnemyMelle") 
		{
			attacked = false;
			attacked2 = false;
			attacked4 = false;
		}
		if (other.gameObject.tag == "EnemyArcher") 
		{
			attacked = false;
			attacked2 = false;
			attacked4 = false;
		}	
	}

	void FixedUpdate()
	{
		if (Input.GetButton ("Fire1") && time <= 0 && !playerSCR.isAttack && playerSCR.allive) 
		{
			if (playerSCR.StacksItemsID [8] > 0) 
			{
				if (Random.Range(0,(100-10*playerSCR.StacksItemsID[8]))<10)
					critical = true;
			}
			attacked = true;			
			Punch.Play();
			playerSCR.anim.SetBool ("Attack", true);
			playerSCR.isAttack=true;
			time= playerSCR.spddmg;
		} else
		{
			//attacked = false;
			playerSCR.anim.SetBool ("Attack", false);
		}
		
		//2
		if (Input.GetButton("Fire2") && time2<=0 && !playerSCR.isAttack && playerSCR.allive)
		{
			attacked2 = true;
			playerSCR.anim.SetBool("Attack2", true);
			playerSCR.isAttack=true;
			time2= playerSCR.spddmg2;
		}
		else
		{
			//attacked2 = false;
			playerSCR.anim.SetBool ("Attack2", false);
		}
		
		//3
		/*
		if (Input.GetButton("Fire3"))
		{
			attacked3 = true;
			playerSCR.anim.SetBool("Attack3", true);
		}
		else
		{
			//attacked3 = false;
			playerSCR.anim.SetBool ("Attack3", false);
		}
		*/
		//4
		if (Input.GetButton("Fire4") && time4<=0 && !playerSCR.isAttack && playerSCR.allive)
		{
			Punch4.Play();
			if (playerSCR.StacksItemsID [8] > 0) 
			{
				if (Random.Range(0,(100-10*playerSCR.StacksItemsID[8]))<10)
					critical = true;
			}
			attacked4 = true;
			playerSCR.anim.SetBool("Attack4", true);
			playerSCR.isAttack=true;
			time4= playerSCR.spddmg4;
		}
		else
		{
			//attacked4 = false;
			playerSCR.anim.SetBool ("Attack4", false);
		}	
	}
	void Update()
	{
		if (time > 0)
			time -= Time.deltaTime;
		if (time2 > 0)
			time2 -= Time.deltaTime;
		if (time3 > 0)
			time3 -= Time.deltaTime;
		if (time4 > 0)
			time4 -= Time.deltaTime;

		dmg = playerSCR.Damage;
		spddmg = playerSCR.spddmg;
		spddmg2 = playerSCR.spddmg2;
		spddmg3 = playerSCR.spddmg3;
		spddmg4 = playerSCR.spddmg4;
		//1

		if (attacked) 
		{
			var shotTransform = Instantiate(Fire1) as Transform;			
			// Определите положение
			shotTransform.position = transform.position;
			attacked=false;
		}
		if (attacked4) 
		{
			var shotTransform = Instantiate(Fire4) as Transform;			
			// Определите положение
			shotTransform.position = transform.position;
			attacked4=false;
		}

		if (!playerSCR.isAttack) 
		{
			attacked=false;
			attacked2=false;
			attacked3=false;
			attacked4=false;
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

