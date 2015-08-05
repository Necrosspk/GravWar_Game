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
	public float spddmg = 0.5f;
	public float jumpheight = 500;
	public bool allive;
	public bool attack;
	//GUI
	public Transform GUIdamage;
	//Camrta
	public GameObject camera1;
	private CameraSmooth camScript;
	//blood
	public Transform bloodPrefab;
	public bool ouch = false;
	//GUI info
	public int hp;
	public int Maxhp = 100;
	public int money;
	public int startmoney = 500;
	
	// ITEMS
	public int item_id = 0;
	public int item_gui = 0;
	private float item_gui_time = 2f;
	private float item_gui_timerstop = 0f;
	private bool item_gui_timer_on = false;
	
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
	}
	
	/// <summary>
	/// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
	/// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
	/// </summary>
	/// 
	/// 
	private void FixedUpdate()
	{
		if (allive) {
			//определяем, на земле ли персонаж
			isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); 
			//устанавливаем соответствующую переменную в аниматоре
			anim.SetBool ("Ground", isGrounded);
			//устанавливаем в аниматоре значение скорости взлета/падения
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			//если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
			//if (!isGrounded) {
			//		if (anim.GetFloat ("Speed") > 8f)
			//			maxSpeed = maxSpeedlittle;
			//} 
			//else
			//		maxSpeed = maxSpeedstandart;
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
		if (allive) {
			if (hp > Maxhp)
				hp = Maxhp;
			// == DEBUG 
			//if (Input.GetKeyDown (KeyCode.P))
			//		TakeDmg (1);
			// == 
			//используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1
			//при стандартных настройках проекта
			//-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
			//1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
			float move = Input.GetAxis ("Horizontal");
			
			
			//в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
			//приэтом нам нужен модуль значения
			anim.SetFloat ("Speed", Mathf.Abs (move));
			
			//if (anim.i) {
			//turnison = false;
			//	}
			
			
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
			
			//		// 5 - Стрельба
			//		bool shoot = Input.GetButtonDown("Fire1");
			//		shoot |= Input.GetButtonDown("Fire2");
			//		// Замечание: Для пользователей Mac, Ctrl + стрелка - это плохая идея
			//		
			//		if (shoot)
			//		{
			//			WeaponScript weapon = GetComponent<WeaponScript>();
			//			if (weapon != null)
			//			{
			//				// ложь, так как игрок не враг
			//				weapon.Attack(false);
			//			}
			//		}
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
			item_gui = 1;
			Maxhp = HpItem_id_1(Maxhp);
			hp = Maxhp;
			item_gui_timer_on = true;
			break;
		case 2:
			item_gui = 2;
			Damage = DmgItem_id_2(Damage);
			item_gui_timer_on = true;
			break;
		case 3:
			item_gui = 3;
			spddmg = SpdDmgItem_id_3(spddmg);
			item_gui_timer_on = true;
			break;
		case 4:
			item_gui = 4;
			maxSpeed = SpdItem_id_4(maxSpeed);
			item_gui_timer_on = true;
			break;
		case 5: 
			item_gui = 5;
			KDturn = RotateKDItem_id_5(KDturn);
			item_gui_timer_on = true;
			break;
		case 6:
			item_gui = 6;
			jumpheight = JumpHeightItem_id_6(jumpheight);
			item_gui_timer_on = true;
			break;
		}
		// TIMER FOR GUI
		if (item_gui_timer_on)
			item_gui_timerstop += Time.deltaTime;
		else 
			item_gui_timerstop = 0f;
		//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	}
	public bool TakeDmg(int Dmg)
	{
		var bloodTranform = Instantiate (bloodPrefab) as Transform;
		bloodTranform.position = transform.position;
		hp -= Dmg;
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
		spddmg -= 0.03f;
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
	// +jump count
	// +red
	// +white
	// +black
	// +st
	// +spell
	// +
	
	
}

























