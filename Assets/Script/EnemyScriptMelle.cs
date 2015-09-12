using UnityEngine;
using System.Collections;

public class EnemyScriptMelle : MonoBehaviour {
	//находится ли персонаж на земле или в прыжке?
	private bool isGrounded = false;
	//ссылка на компонент Transform объекта
	//для определения соприкосновения с землей
	public Transform groundCheck;
	//радиус определения соприкосновения с землей
	private float groundRadius = 0.3f;
	//ссылка на слой, представляющий землю
	public LayerMask whatIsGround;
	//переменная для установки макс. скорости персонажа
	public float maxSpeed = 3f; 
	//переменная для определения направления персонажа вправо/влево
	public bool isFacingRight = true;
	//ссылка на компонент анимаций
	private Animator anim;
	//PlayerInfo
	private Transform playerTransform;
	//Turn
	public int turn =4;
	public int moveit =0;
	public int loot;
	public bool lootable;
	public bool attack;
	//blood
	public Transform bloodPrefab;
	//GUI info
	public int hp;
	private TextMesh textdmg;
	public Transform GUIdamage;
	public Transform GUIdamageCritical;
	private TextMesh textdmg2;

	private CameraSmooth camScript;
	private float move;
	public bool ISeeYou=false;
	private characterControllerScript playerscr;
	public int BaseDmg = 5;
	public float AttackCooldown = 1.0f;
	public bool allive; 
	public float Turntimer = 1.0f;
	private bool lootgive = false;

	public AudioSource BreathAU;

	private float smoothtext;

	public bool despawnOn = false;
	public float despawnT = 20f;
	private float scaleDespawn;
	//Debuffs
	private float StunTimer;
	private bool Stun;
	//--
	private float StandTimer;
	private bool Stand;
	private CreatureDirector director;

	public bool critical = false;
	public Transform GoldPrefub;
	public Transform HpPrefub;
	/// <summary>
	/// Начальная инициализация
	/// </summary>
	private void Start()
	{
		smoothtext = 1.0f;
		lootable = true;
		loot = Random.Range (7, 14);
		camScript = (CameraSmooth)FindObjectOfType (typeof(CameraSmooth));
		characterControllerScript player = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		//+ on directorAI
		director = (CreatureDirector)FindObjectOfType (typeof(CreatureDirector));
		director.countCreature++;
		//
		playerTransform = player.transform;
		playerscr = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
		anim = GetComponent<Animator>();
		allive = true;
		scaleDespawn = despawnT;
	}
	
	/// <summary>
	/// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
	/// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
	/// </summary>
	/// 
	/// 
	private void FixedUpdate()
	{
		//определяем, на земле ли персонаж
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
		//устанавливаем соответствующую переменную в аниматоре
		anim.SetBool ("Ground", isGrounded);
		//устанавливаем в аниматоре значение скорости взлета/падения
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		//если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
		if (!isGrounded) {
			if (anim.GetFloat("Speed")>8f)maxSpeed = 1f;
		}
		else
			maxSpeed = 3f;
	}
	
	/// <summary>
	/// Метод для смены направления движения персонажа и его зеркального отражения
	/// </summary>
	
	private void Flip()
	{
		//меняем направление движения персонажа
		isFacingRight = !isFacingRight;
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}
	
	private void Update()
	{
		//if (Input.GetKeyDown (KeyCode.P))
		//	TakeDmg (1);
		
		//используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
		//при стандартных настройках проекта 
		//-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
		//1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
		if (allive)
			Turntimer -= Time.deltaTime;
		if (!ISeeYou && Turntimer<=0) 
		{
			int rand=Random.Range(-2,2);
				move = rand;
			Turntimer=1.0f;
		}

		if (Vector3.Distance (playerTransform.position, this.transform.position) < 7.0f && playerscr.allive)
				ISeeYou = true;

		turn = camScript.rotate;	
		// если дистанция до игрока больше трех метров
		if (ISeeYou && Turntimer<=0  && playerscr.allive) 
		{
			Turntimer=0.3f;
			if (Vector3.Distance (playerTransform.position, this.transform.position) > 0.6f && allive) 
			{
					if (turn == 1) {
							if (this.transform.position.x - playerTransform.position.x > 0.6f)			
									move = -1;
							else
									move = 1;
					}
					if (turn == 3 && ISeeYou) {
							if (this.transform.position.x - playerTransform.position.x > 0.6f)			
									move = 1;
							else
									move = -1;
					}
					if (turn == 2 && ISeeYou) {
							if (this.transform.position.y - playerTransform.position.y > 0.6f)			
									move = -1;
							else
									move = 1;
					}
					if (turn == 4 && ISeeYou) {
							if (this.transform.position.y - playerTransform.position.y > 0.6f)			
									move = 1;
							else
									move = -1;
					}
			} 
			else if (allive  && playerscr.allive) // если меньше или равна трем метрам
			{ 
					move = 0;
					if (AttackCooldown <= 0 && allive) 
					{							
						attack = true;
						anim.SetBool ("Attack", true);
						AttackCooldown = 1.0f;
					}
					else
				{
					anim.SetBool ("Attack", false);
					attack = false;
				}
			} 
			else
			{
				anim.SetBool ("Attack", false);
				attack = false;
			}
		}
		if (allive)
			AttackCooldown -= Time.deltaTime;
		
		//в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
		//приэтом нам нужен модуль значения
		anim.SetFloat("Speed", Mathf.Abs(move));
		
		//if (anim.i) {
		//turnison = false;
		//	}
		
		
		//поворот врага
		if (move > 0 && !isFacingRight) {
			moveit = 1;
			//отражаем врага вправо
			Flip ();
		}
		//обратная ситуация. отражаем врага влево
		else if (move < 0 && isFacingRight) {
			moveit = 0;
			Flip ();
		}

		if (allive) {
			switch (turn) {
			case 1:
					rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), 300f * Time.deltaTime);
					break;
			case 2:
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, move * maxSpeed);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), 300f * Time.deltaTime);
					break;
			case 3:
					rigidbody2D.velocity = new Vector2 (-move * maxSpeed, rigidbody2D.velocity.y);			
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 180), 300f * Time.deltaTime);
					break;
			case 4:
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -move * maxSpeed);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 270), 300f * Time.deltaTime);
					break;
			}
		}

		if (Vector3.Distance (playerTransform.position, this.transform.position) <= 2.2f && allive) 
		{
			if (transform.position.x < playerTransform.position.x && !isFacingRight)
			{
				if (turn == 1)
				{
					Flip ();
				}
			}
			if (transform.position.x > playerTransform.position.x && isFacingRight)
			{
				if (turn == 1)
				{
					Flip ();
				}
			}
			if (transform.position.y < playerTransform.position.y && !isFacingRight)
			{
				if (turn == 2)
				{
					Flip ();
				}
			}
			if (transform.position.y > playerTransform.position.y && isFacingRight)
			{
				if (turn == 2)
				{
					Flip ();
				}
			}
			if (transform.position.x > playerTransform.position.x && !isFacingRight)
			{
				if (turn == 3)
				{
					Flip ();
				}
			}
			if (transform.position.x < playerTransform.position.x && isFacingRight)
			{
				if (turn == 3)
				{
					Flip ();
				}
			}
			if (transform.position.y > playerTransform.position.y && !isFacingRight)
			{
				if (turn == 4)
				{
					Flip ();
				}
			}
			if (transform.position.y < playerTransform.position.y && isFacingRight)
			{
				if (turn == 4)
				{
					Flip ();
				}
			}
		}

		if (hp <= 0)
			allive=false;

		if (!allive && lootable)
		{
			this.tag = "DeadEnemy";
			anim.SetBool ("Attack",false);
			anim.SetBool ("Dead", true);
			this.collider2D.isTrigger = true;
			this.rigidbody2D.isKinematic = true;
			BreathAU.Stop ();
			//playerscr.money+=loot;
			smoothtext = 1.0f;	
			director.countCreature--;
			var gold = Instantiate (GoldPrefub) as Transform;
			gold.position = transform.position;
			gold.GetComponent<GoldFly> ().money = loot;
			//playerscr.money += loot;
			lootable = false;
			if(playerscr.StacksItemsID[9]>0)
			{
				var hptr = Instantiate (HpPrefub) as Transform;
				hptr.position = transform.position;
			}
		}
		if (!allive && smoothtext <=0 && despawnOn) 
		{
			despawnT -= Time.deltaTime;
			Vector3 theScale = transform.localScale;
			this.transform.localScale = new Vector3(theScale.x*despawnT/scaleDespawn,theScale.y,theScale.z);
			if (despawnT <= 0)
				DestroyObject(this.gameObject);
		}

		//DEBUFFS
		//Stun
		if (Stun && StunTimer >= 0) 
		{
			move = 0;
			attack = false;
			StunTimer -= Time.deltaTime;
		} else
		{
			Stun=false;
		}
		//Stand
		if (Stand && StandTimer >= 0) 
		{
			move = 0;
			anim.SetBool ("Attack",false);
			StandTimer -= Time.deltaTime;
		} else
		{
			Stand=false;
		}

	}
	public bool TakeDmg(double Dmg)
	{
		int dmgi = (int)Dmg;
		var bloodTranform = Instantiate (bloodPrefab) as Transform;
		bloodTranform.position = transform.position;
		hp -= dmgi;
		if (!critical) 
		{
			textdmg = GUIdamage.GetComponent<TextMesh> ();
			textdmg.text = "-" + dmgi.ToString ();
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
		//if (!allive && smoothtext>=0)
			//GUI.TextArea (new Rect (Screen.width/2-10,Screen.height/2-5,40,20),"+" + loot.ToString()+"$");
	}

	public void StunD (float time)
	{
		StunTimer = time;
		Stun = true;
		//return true;
	}
	public void StandD (float time)
	{
		StandTimer = time;
		Stand = true;
		//return true;
	}

}
