using UnityEngine;
using System.Collections;

public class characterControllerScript : MonoBehaviour {
	//находится ли персонаж на земле или в прыжке?
	private bool isGrounded = false;
	//ссылка на компонент Transform объекта
	//для определения соприкосновения с землей
	public Transform groundCheck;
	//радиус определения соприкосновения с землей
	private float groundRadius = 0.01f;
	//ссылка на слой, представляющий землю
	public LayerMask whatIsGround;
	//переменная для установки макс. скорости персонажа
	public float maxSpeed = 2.5f; 
	public float maxSpeedlittle = 5f; 
	public float maxSpeedstandart = 5f; 
	//переменная для определения направления персонажа вправо/влево
	public bool isFacingRight = true;
	//ссылка на компонент анимаций
	public Animator anim;
	//Turn
	public int turn = 4;
	public int moveit = 0;
	public float KDturn = 3f;//3 sec
	public float timerturn = 0f;
	//Base
	public int Damage = 8;
	public float spddmg = 0.5f; //MainAttack
	public float spddmg2 = 7f;//Spell 1
	public float spddmg3 = 8f;//Spell 2
	public float spddmg4 = 21f;//Spell 3
	public float jumpheight = 500;
	public bool allive;
	public bool attack;
	private bool fall = false;
	private float falldmgvelocity;
	//GUI
	private TextMesh textdmg;
	public Transform GUIdamage;
	//Camrta
	public GameObject camera1;
	private CameraSmooth camScript;
	//blood
	public Transform bloodPrefab;
	public bool ouch = false;
	public bool isAttack = false;
	//GUI info
	public int hp;
	public int Maxhp = 100;
	public int money;
	public int startmoney = 500;
	//public GameObject UIMoney;
	//private TextMesh UIMoneyText;
	
	// ITEMS
	public int item_id = 0;
	public int item_gui = 0;
	private float item_gui_time = 2f;
	private float item_gui_timerstop = 0f;
	private bool item_gui_timer_on = false;
	public int[] StacksItemsID = new int [30];
	private float item7Timer;

	//Stairs
	private bool OnStairs = false;
	
	/// <summary>
	/// Начальная инициализация
	/// </summary>
	private void Start()
	{
		allive = true;
		Damage = 10;
		hp = Maxhp;
		money = startmoney;
		Physics2D.gravity = new Vector2 (0, -30f);
		anim = GetComponent<Animator>();
		
		camScript=camera1.GetComponent<CameraSmooth>();		
		camScript.SetKD(KDturn);

//		UIMoneyText = UIMoney.GetComponent<TextMesh> ();
	}
	
	/// <summary>
	/// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
	/// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
	/// </summary>
	/// 
	/// 
	void OnTriggerEnter2D (Collider2D other)
	{
		//Лестницы
		if (other.gameObject.tag == "StairsV" && (turn == 1 || turn == 3) && (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S))) 
		{
			//Debug.Log ("Stairs id here");
			rigidbody2D.isKinematic = true;
			OnStairs = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		//Лестницы
		if (other.gameObject.tag == "StairsV") 
		{
			//Debug.Log ("Stairs id here");
			rigidbody2D.isKinematic = false;
			OnStairs = false;
		}
	}
	private void FixedUpdate()
	{
		//Physics2D.IgnoreCollision(
		if (allive && !OnStairs) {
			//определяем, на земле ли персонаж
			isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); 
			//устанавливаем соответствующую переменную в аниматоре
			anim.SetBool ("Ground", isGrounded);
			//устанавливаем в аниматоре значение скорости взлета/падения
			if (turn== 1 || turn == 3)
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			if (turn== 2 || turn == 4)
				anim.SetFloat ("vSpeed", rigidbody2D.velocity.x);
			//если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
			//if (!isGrounded) {
			//		if (anim.GetFloat ("Speed") > 8f)
			//			maxSpeed = maxSpeedlittle;
			//} 
			//else
			//		maxSpeed = maxSpeedstandart;

			if (turn== 1 || turn == 3)
				if (rigidbody2D.velocity.y > 16f && !isGrounded)
				{
					fall = true;
					falldmgvelocity = rigidbody2D.velocity.y-12;
				}
			if (turn== 1 || turn == 3)
				if (rigidbody2D.velocity.y < -16f && !isGrounded)
				{
					fall = true;
					falldmgvelocity = -rigidbody2D.velocity.y-12;
				}
			if (turn== 2 || turn == 4)
				if (rigidbody2D.velocity.x > 16f && !isGrounded)
				{
					fall = true;
					falldmgvelocity = rigidbody2D.velocity.x-12;
				}
			if (turn== 2 || turn == 4)
				if (rigidbody2D.velocity.x < -16f && !isGrounded)
				{
					fall = true;
					falldmgvelocity = -rigidbody2D.velocity.x-12f;
				}

			if (turn== 1 || turn == 3)
				if (isGrounded && fall)
				{
					TakeDmg((int)(Maxhp/50)*(int)falldmgvelocity);
					fall = false;
				}
			if (turn== 1 || turn == 3)
				if (isGrounded && fall)
				{
					TakeDmg(-(int)(Maxhp/50)*(int)falldmgvelocity);
					fall = false;
				}
			if (turn== 2 || turn == 4)
				if (isGrounded && fall)
				{
					TakeDmg((int)(Maxhp/50)*(int)falldmgvelocity);
					fall = false;
				}
			if (turn== 2 || turn == 4)
				if (isGrounded && fall)
				{
					TakeDmg(-(int)(Maxhp/(Maxhp/2))*(int)falldmgvelocity);
					fall = false;
				}

		}
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
		if (allive && OnStairs) 
		{
			//Jump down from stairs
			if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
			{
				rigidbody2D.isKinematic=false;
				OnStairs=false;
			}
			if (Input.GetKey(KeyCode.W))
			{
				if (turn == 1)
					transform.position = new Vector2(transform.position.x,transform.position.y + 0.05f);
				else if (turn == 2)
					transform.position = new Vector2(transform.position.x-0.05f,transform.position.y);
				else if (turn == 3)
					transform.position = new Vector2(transform.position.x,transform.position.y - 0.05f);
				else if (turn == 4)
					transform.position = new Vector2(transform.position.x+0.05f,transform.position.y);
			}
			if (Input.GetKey(KeyCode.S))
			{
				if (turn == 1)
					transform.position = new Vector2(transform.position.x,transform.position.y - 0.05f);
				else if (turn == 2)
					transform.position = new Vector2(transform.position.x+0.05f,transform.position.y);
				else if (turn == 3)
					transform.position = new Vector2(transform.position.x,transform.position.y + 0.05f);
				else if (turn == 4)
					transform.position = new Vector2(transform.position.x-0.05f,transform.position.y);
			}
		}

		if (allive) 
		{
			if (hp > Maxhp)
				hp = Maxhp;
			if (hp < 0)
				hp=0;

			//-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
			//1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
			float move = Input.GetAxis ("Horizontal");
			
			//в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
			//приэтом нам нужен модуль значения
			anim.SetFloat ("Speed", Mathf.Abs (move));
			
			//если нажали клавишу для перемещения вправо, а персонаж направлен влево
			if (move > 0 && !isFacingRight) {
				moveit = 1;
				//отражаем персонажа вправо
				Flip ();
			}
			//обратная ситуация. отражаем персонажа влево
			else if (move < 0 && isFacingRight) {
				moveit = 0;
				Flip ();
			}
			timerturn += Time.deltaTime;
			if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.E)) {
				turn = camScript.rotate;
				//if (timerturn >= KDturn)
				//{
				switch (turn) {
				case 1:
					Physics2D.gravity = new Vector2 (0, -30f);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), 300f * Time.deltaTime);
					break;
				case 2:
					Physics2D.gravity = new Vector2 (30f, 0);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), 300f * Time.deltaTime);
					break;
				case 3:
					Physics2D.gravity = new Vector2 (0, 30f);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 180), 300f * Time.deltaTime);
					break;
				case 4:
					Physics2D.gravity = new Vector2 (-30f, 0);
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 270), 300f * Time.deltaTime);
					break;
				}
				timerturn = 0;
				//}
			}
			
			switch (turn) {
			case 1:
				rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
				break;
			case 2:
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, move * maxSpeed);
				break;
			case 3:
				rigidbody2D.velocity = new Vector2 (-move * maxSpeed, rigidbody2D.velocity.y);
				break;
			case 4:
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -move * maxSpeed);
				break;
			}
			//если персонаж на земле и нажат пробел...
			if (isGrounded && Input.GetKeyDown (KeyCode.Space)) {
				//устанавливаем в аниматоре переменную в false
				anim.SetBool ("Ground", false);
				//прикладываем силу вверх, чтобы персонаж подпрыгнул
				if (turn == 1)
					rigidbody2D.AddForce (new Vector2 (0, jumpheight));
				else if (turn == 2)
					rigidbody2D.AddForce (new Vector2 (-jumpheight, 0));
				else if (turn == 3)
					rigidbody2D.AddForce (new Vector2 (0, -jumpheight));
				else if (turn == 4)
					rigidbody2D.AddForce (new Vector2 (jumpheight, 0));
			}
		}
		if (hp <= 0) {
			allive = false;
			anim.SetBool("Dead",true);
		}

		// /////////////////////////////////
		// ITEMS switcher-------------------
		// /////////////////////////////////
		switch(item_id)
		{
		case 0:
			break;
		case 1:
			StacksItemsID[1]++;
			item_gui = 1;
			Maxhp = HpItem_id_1(Maxhp);
			hp = Maxhp;
			item_gui_timer_on = true;
			break;
		case 2:
			StacksItemsID[2]++;
			item_gui = 2;
			Damage = DmgItem_id_2(Damage);
			item_gui_timer_on = true;
			break;
		case 3:
			StacksItemsID[3]++;
			item_gui = 3;
			spddmg = SpdDmgItem_id_3(spddmg);
			item_gui_timer_on = true;
			break;
		case 4:
			StacksItemsID[4]++;
			item_gui = 4;
			maxSpeed = SpdItem_id_4(maxSpeed);
			item_gui_timer_on = true;
			break;
		case 5: 
			StacksItemsID[5]++;
			item_gui = 5;
			KDturn = RotateKDItem_id_5(KDturn);
			item_gui_timer_on = true;
			break;
		case 6:
			StacksItemsID[6]++;
			item_gui = 6;
			jumpheight = JumpHeightItem_id_6(jumpheight);
			item_gui_timer_on = true;
			break;
		case 7:
			StacksItemsID[7]++;
			item_gui = 7;
			RegenerateItem_id_7();
			item_gui_timer_on = true;
			item7Timer=6f/StacksItemsID[7];
			break;
		case 8:
			StacksItemsID[8]++;
			item_gui = 8;
			CritItem_id_8 ();
			break;
		case 9:
			StacksItemsID[9]++;
			item_gui = 9;
			HpDropItem_id_9 ();
			break;
		case 10:
			StacksItemsID[10]++;
			item_gui = 10;
			ArmorItem_id_10 ();
			break;
		}
		//
		//Timers for pereodic items
		//
		if (StacksItemsID [7] > 0) 
		{
			item7Timer -= Time.deltaTime;
			if (item7Timer <= 0)
			{
				hp+=1;
				item7Timer=6f/StacksItemsID[7];
			}
		}
		//

		// TIMER FOR GUI
		if (item_gui_timer_on)
			item_gui_timerstop += Time.deltaTime;
		else 
			item_gui_timerstop = 0f;
		//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

		// GUI INFO
		//UIMoneyText.text = money.ToString ();
	}
	public bool TakeDmg(int Dmg)
	{
		var bloodTranform = Instantiate (bloodPrefab) as Transform;
		bloodTranform.position = transform.position;
		if (StacksItemsID [10] > 0 && StacksItemsID [10] < 5)
			Dmg = (int)(Dmg * (1- (0.07 * StacksItemsID [10])));
		if (StacksItemsID [10] >= 5)
			Dmg = (int)(Dmg * 0.65);
		hp -= Dmg;
		textdmg = GUIdamage.GetComponent<TextMesh> ();
		textdmg.text = "-"+Dmg.ToString ();
		var GUID = Instantiate(GUIdamage) as Transform;
		GUID.transform.position = transform.position;
		ouch = true;
		return true;
	}

	public bool FallDmg(int Dmg)
	{
		var bloodTranform = Instantiate (bloodPrefab) as Transform;
		bloodTranform.position = transform.position;
		hp -= Dmg;
		textdmg = GUIdamage.GetComponent<TextMesh> ();
		textdmg.text = "-"+Dmg.ToString ();
		var GUID = Instantiate(GUIdamage) as Transform;
		GUID.transform.position = transform.position;
		ouch = true;
		return true;
	}
	
	void OnGUI(){
		switch (item_gui) {
		case 0:
			break;
		case 1:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You hp increase on 10%!");
			else 
				item_gui_timer_on = false;
			break;
		case 2:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You dmg increase on 2!");
			else 
				item_gui_timer_on = false;
			break;			
		case 3:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You attack increase speed 10%!");
			else 			
				item_gui_timer_on = false;
			break;
		case 4:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You speed increase on 10%!");
			else 
				item_gui_timer_on = false;
			break;
		case 5:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You rotate KD decrease on 0.2 sec!");
			else 
				item_gui_timer_on = false;
			break;
		case 6:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"You jump height increase on 10%!");
			else 
				item_gui_timer_on = false;
			break;
		case 7:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"Slowly regenerate hp!");
			else 
				item_gui_timer_on = false;
			break;
		case 8:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"+10% chance to double damage!");
			else 
				item_gui_timer_on = false;
			break;
		case 9:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"NPC now drop HP!");
			else 
				item_gui_timer_on = false;
			break;
		case 10:
			if(item_gui_timerstop <= item_gui_time && item_gui_timer_on == true)
				GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,20),"Decrease damage to you on 10%!");
			else 
				item_gui_timer_on = false;
			break;
		}
	}
	//------------------------------------------------------------------------- \\
	//----------------------------------ITEMS---------------------------------- \\
	//------------------------------------------------------------------------- \\
	
	// +hp
	private int HpItem_id_1(int base_hp) // Increase hp on  10%
	{
		double a = base_hp * 1.1;
		base_hp = (int)a;
		item_id = 0;
		Debug.Log("Hp increase to: " + base_hp + "!");
		return base_hp;
	}
	// +dmg
	private int DmgItem_id_2 (int base_dmg) // increase dmg on 2 pt
	{
		base_dmg += 2;
		item_id = 0;
		Debug.Log("Damage increase to: " + base_dmg + "!");
		return base_dmg;
	}
	// +spd dmg
	private float SpdDmgItem_id_3 (float spddmg)
	{
		spddmg -= 0.1f;
		item_id = 0;
		Debug.Log("Attack speed increase to: " + spddmg + "!");
		return spddmg;
	}
	// +spd
	private float SpdItem_id_4 (float spd)
	{
		spd *= 1.1f;
		item_id = 0;
		Debug.Log("Speed increase to: " + spd + "!");
		return spd;
	}
	// +rotate kd
	private float RotateKDItem_id_5 (float KDrotate)
	{
		KDrotate -= 0.3f;
		item_id = 0;
		Debug.Log("Cooldown of rotate decrease to: " + KDrotate + "!");
		camScript.SetKD(KDturn);
		return KDrotate;
	}
	// +jump height
	private float JumpHeightItem_id_6 (float jumpheight) // +5%
	{
		jumpheight *= 1.05f;
		item_id = 0;
		Debug.Log("Jump height increase to: " + jumpheight + "!");
		return jumpheight;
	}
	private void RegenerateItem_id_7 () // +5%
	{
		item_id = 0;
		Debug.Log("Regeneration!");
	}
	private void CritItem_id_8 () // +5%
	{
		item_id = 0;
		Debug.Log("+10% double damage!");
	}

	private void HpDropItem_id_9 () // +5%
	{
		item_id = 0;
		Debug.Log("NPC now when die drop HP!");
	}

	private void ArmorItem_id_10 () // +5%
	{
		item_id = 0;
		Debug.Log("Decrease damage to you on 10%");
	}
	// +jump count
	// +red
	// +white
	// +black
	// +st
	// +spell
	// +
	
	
}

























