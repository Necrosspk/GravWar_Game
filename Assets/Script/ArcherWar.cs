﻿using UnityEngine;
using System.Collections;

public class ArcherWar : MonoBehaviour 
{
	// хп лучника
	public int hp;
	// атака лучника
	public int damageArcher = 5;
	// скорость передвижения
	public int speedArcher = 5;
	// скорость атаки
	public int speedDamageArcher = 5;
	// проверка стоит ли у него на земле
	private bool stayGroundArcher = false;
	//Turn - поворот
	public int turnArcher =1;
	public int moveitArcher =0;
	public int lootArcher;
	public bool lootableArcher;
	public bool attackArcher;
	//переменная для определения направления персонажа вправо/влево
	public bool isFacingRightArcher = true;
	//ссылка на компонент анимаций
	private Animator animArcher;
	//для определения соприкосновения с землей
	public Transform groundCheckArcher;
	//радиус определения соприкосновения с землей
	private float groundRadiusArcher = 0.3f;
	//ссылка на слой, представляющий землю
	public LayerMask whatIsGroundArcher;

	private float AttackCooldown;
	private CameraSmooth camScriptArcher;
	private float moveArcher;
	public bool ISeeYouArcher =false;
	private characterControllerScript playerscrArcher;
	public bool alliveArcher;
	private float Turntimer = 0.3f;
	public Transform playerTransformArcher;
	private float smoothText;
	public Transform bloodPrefab;
	//GUI
	private TextMesh textdmg;
	private TextMesh textdmg2;
	public Transform GUIdamage;
	public Transform GUIdamageCritical;

	public bool despawnOn = false;
	public float despawnT = 20f;
	private float scaleDespawn;
	//Debuffs
	private float StunTimer;
	private bool Stun;
	private CreatureDirector director;

	public bool critical = false;
	public Transform GoldPrefub;
	public Transform HpPrefub;
	// Use this for initialization
	void Start () 
	{
		lootableArcher = true;
		lootArcher = Random.Range (10, 18);
		camScriptArcher = (CameraSmooth)FindObjectOfType (typeof(CameraSmooth));
		characterControllerScript player = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		//+ on directorAI
		director = (CreatureDirector)FindObjectOfType (typeof(CreatureDirector));
		director.countCreature++;
		//
		playerTransformArcher = player.transform;
		playerscrArcher = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		animArcher = GetComponent<Animator>();
		scaleDespawn = despawnT;
		alliveArcher = true;	
	}

	/// <summary>
	/// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
	/// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
	/// </summary>
	private void FixedUpdate()
	{
		//определяем, на земле ли персонаж
		stayGroundArcher = Physics2D.OverlapCircle(groundCheckArcher.position, groundRadiusArcher, whatIsGroundArcher); 
		//устанавливаем соответствующую переменную в аниматоре
		animArcher.SetBool ("Ground", stayGroundArcher);
		//устанавливаем в аниматоре значение скорости взлета/падения
		animArcher.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		//если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
		if (!stayGroundArcher) 
		{
			if (animArcher.GetFloat("Speed")>8f)moveArcher = 1f;
		}
		//else
			//moveArcher = speedArcher;
	}

	/// <summary>
	/// Метод для смены направления движения персонажа и его зеркального отражения
	/// </summary>
	private void Flip()
	{
		//меняем направление движения персонажа
		isFacingRightArcher = !isFacingRightArcher;
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (alliveArcher)
			Turntimer -= Time.deltaTime;
		if (!ISeeYouArcher && Turntimer<=0) 
		{
			int rand=Random.Range(-2,2);
			moveArcher = rand;
			Turntimer=1.0f;
			if (rand >1)
				rand=1;
			if (rand <-1)
				rand=-1;
		}
		
		if (Vector3.Distance (playerTransformArcher.position, this.transform.position) < 7.0f && playerscrArcher.allive)
			ISeeYouArcher = true;
		
		turnArcher = camScriptArcher.rotate;	
		// если дистанция до игрока больше трех метров
		if (ISeeYouArcher && Turntimer<=0 && playerscrArcher.allive) 
		{
			Turntimer=0.3f;
			if (Vector3.Distance (playerTransformArcher.position, this.transform.position) > 2.2f && alliveArcher) 
			{
				if (turnArcher == 1) {
					if (this.transform.position.x - playerTransformArcher.position.x > 2.2f)			
						moveArcher = -1;
					else
						moveArcher = 1;
				}
				if (turnArcher == 3 && ISeeYouArcher) {
					if (this.transform.position.x - playerTransformArcher.position.x > 2.2f)			
						moveArcher = 1;
					else
						moveArcher = -1;
				}
				if (turnArcher == 2 && ISeeYouArcher) {
					if (this.transform.position.y - playerTransformArcher.position.y > 2.2f)			
						moveArcher = -1;
					else
						moveArcher = 1;
				}
				if (turnArcher == 4 && ISeeYouArcher) {
					if (this.transform.position.y - playerTransformArcher.position.y > 2.2f)			
						moveArcher = 1;
					else
						moveArcher = -1;
				}
			} 
			else if (alliveArcher && playerscrArcher.allive) // если меньше или равна трем метрам
			{ 
				moveArcher = 0;
				if (AttackCooldown <= 0) 
				{							
					attackArcher = true;
					animArcher.SetBool ("Attack", true);
					AttackCooldown = 1.0f;
				}
				else
				{
					animArcher.SetBool ("Attack", false);
					attackArcher = false;
				}
				if (Vector3.Distance (playerTransformArcher.position, this.transform.position) <= 2.2f && alliveArcher) 
				{
					if (transform.position.x < playerTransformArcher.position.x && !isFacingRightArcher)
					{
						if (turnArcher == 1)
						{
							Flip ();
						}
					}
					if (transform.position.x > playerTransformArcher.position.x && isFacingRightArcher)
					{
						if (turnArcher == 1)
						{
							Flip ();
						}
					}
					if (transform.position.y < playerTransformArcher.position.y && !isFacingRightArcher)
					{
						if (turnArcher == 2)
						{
							Flip ();
						}
					}
					if (transform.position.y > playerTransformArcher.position.y && isFacingRightArcher)
					{
						if (turnArcher == 2)
						{
							Flip ();
						}
					}
					if (transform.position.x > playerTransformArcher.position.x && !isFacingRightArcher)
					{
						if (turnArcher == 3)
						{
							Flip ();
						}
					}
					if (transform.position.x < playerTransformArcher.position.x && isFacingRightArcher)
					{
						if (turnArcher == 3)
						{
							Flip ();
						}
					}
					if (transform.position.y > playerTransformArcher.position.y && !isFacingRightArcher)
					{
						if (turnArcher == 4)
						{
							Flip ();
						}
					}
					if (transform.position.y < playerTransformArcher.position.y && isFacingRightArcher)
					{
						if (turnArcher == 4)
						{
							Flip ();
						}
					}
				}
			} 
			else
			{
				animArcher.SetBool ("Attack", false);
				attackArcher = false;
			}
		}
		if (alliveArcher)
			AttackCooldown -= Time.deltaTime;

		if (attackArcher == true && alliveArcher) 
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				weapon.Attack(true);
			}
		}
		
		//в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
		//приэтом нам нужен модуль значения
		animArcher.SetFloat("Speed", Mathf.Abs(moveArcher));
				
		//поворот врага
		if (moveArcher > 0 && !isFacingRightArcher) 
		{
			moveitArcher = 1;
			//отражаем врага вправо
			Flip ();
		}
		//обратная ситуация. отражаем врага влево
		else if (moveArcher < 0 && isFacingRightArcher) 
		{
			moveitArcher = 0;
			Flip ();
		}
		
		if (alliveArcher) 
		{
			switch (turnArcher) 
			{
			case 1:
				rigidbody2D.velocity = new Vector2 (moveArcher * speedArcher, rigidbody2D.velocity.y);
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), 300f * Time.deltaTime);
				break;
			case 2:
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveArcher * speedArcher);
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), 300f * Time.deltaTime);
				break;
			case 3:
				rigidbody2D.velocity = new Vector2 (-moveArcher * speedArcher, rigidbody2D.velocity.y);			
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 180), 300f * Time.deltaTime);
				break;
			case 4:
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -moveArcher * speedArcher);
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 270), 300f * Time.deltaTime);
				break;
			}
		}

		if (hp <= 0)
			alliveArcher=false;
		
		if (!alliveArcher && lootableArcher) 
		{
			this.tag = "DeadEnemy";
			animArcher.SetBool ("Attack",false);
			animArcher.SetBool ("Dead",true);
			this.collider2D.isTrigger=true;
			this.rigidbody2D.isKinematic=true;
			//playerscrArcher.money += lootArcher;
			smoothText=1.0f;
			director.countCreature--;
			var gold = Instantiate (GoldPrefub) as Transform;
			gold.position = transform.position;
			gold.GetComponent<GoldFly>().money = lootArcher;
			//playerscrArcher.money += lootArcher;
			ISeeYouArcher = false;
			lootableArcher=false;
			if(playerscrArcher.StacksItemsID[9]>0)
			{
				var hptr = Instantiate (HpPrefub) as Transform;
				hptr.position = transform.position;
			}
		}

		if (!alliveArcher && smoothText <=0 && despawnOn) 
		{
			despawnT -= Time.deltaTime;
			Vector3 theScale = transform.localScale;
			this.transform.localScale = new Vector3(theScale.x*despawnT/scaleDespawn,theScale.y,theScale.z);
			if (despawnT <= 0)
				DestroyObject(this.gameObject);
		}
		if (Stun && StunTimer >= 0) 
		{
			moveArcher = 0;
			attackArcher = false;
			animArcher.SetBool ("Attack",false);
			StunTimer -= Time.deltaTime;
		} else
		{
			Stun=false;
		}
	}

	public bool TakeDmg(double Dmg)
	{
		int dmgint = (int)Dmg;
		var bloodTranform = Instantiate (bloodPrefab) as Transform;
		bloodTranform.position = transform.position;
		hp -= dmgint;
		if (!critical) 
		{
			textdmg = GUIdamage.GetComponent<TextMesh> ();
			textdmg.text = "-" + dmgint.ToString ();
			var GUID = Instantiate (GUIdamage) as Transform;
			GUID.transform.position = transform.position;
		} 
		if (critical)
		{
			textdmg2 = GUIdamageCritical.GetComponent<TextMesh> ();
			textdmg2.text = "-" + Dmg.ToString ();
			var GUID = Instantiate (GUIdamageCritical) as Transform;
			GUID.transform.position = transform.position;
			critical = false;
		}
		return true;
	}
	
	void OnGUI()
	{
		//if (!alliveArcher && smoothText>=0) 
		//{
		//	GUI.TextArea (new Rect (Screen.width/2-10,Screen.height/2-5,40,20),"+" + lootArcher.ToString()+"$");
		//}  
	}

	public void StunD (float time)
	{
		StunTimer = time;
		Stun = true;
		//return true;
	}
}
