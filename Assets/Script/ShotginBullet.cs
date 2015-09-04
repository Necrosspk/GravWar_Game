using UnityEngine;
using System.Collections;

public class ShotginBullet : MonoBehaviour {
	public bool massdmg = false;
	public bool spell4 = false;
	public float timeShot = 1f; 
	public float timer = 0.3f;
	public GameObject boooom; 
	private EnemyScriptMelle melle;
	private ArcherWar archer;
	private characterControllerScript player;
	private bool hit;
	private bool flip=false;
	// 1 - Designer variables
	
	/// <summary>
	/// Скорость объекта
	/// </summary>
	public Vector2 speed = new Vector2(2, 2);
	
	/// <summary>
	/// Направление движения
	/// </summary>
	public Vector2 direction;
	
	private Vector2 movement;
	
	void Start()
	{
		timer = 0.3f;
		this.renderer.enabled = false;
		player = (characterControllerScript)FindObjectOfType (typeof(characterControllerScript));
	}
	
	void OnTriggerEnter2D( Collider2D other)
	{
		//DestroyObject (this);
		//Debug.Log ("boob bay");
		if (other.gameObject.tag == "EnemyMelle") 
		{
			hit = true;
			melle = other.GetComponent<EnemyScriptMelle>();
			if (!spell4)
				melle.TakeDmg(player.Damage);
			if (spell4)
				melle.TakeDmg(player.Damage *2);
			melle.StunD(0.5f);
			//var shotTransform = Instantiate(boooom) as Transform;
			//shotTransform.position = transform.position;
			if (!massdmg)
			{
				this.renderer.enabled = true;
				DestroyObject(gameObject, 0.2f);
				speed = new Vector2(0,0);
			}
		}
		if (other.gameObject.tag == "EnemyArcher") 
		{
			hit = true;
			archer = other.GetComponent<ArcherWar>();
			if (!spell4)
				archer.TakeDmg(player.Damage);
			if (spell4)
				archer.TakeDmg(player.Damage *2);
			archer.StunD(0.5f);
			//var shotTransform = Instantiate(boooom) as Transform;
			//shotTransform.position = transform.position;
			if (!massdmg)
			{
				this.renderer.enabled = true;
				DestroyObject(gameObject, 0.2f);
				speed = new Vector2(0,0);
			}
		}
		if (other.gameObject.tag == "ground")
		{
			this.renderer.enabled = true;
			hit = true;			
			DestroyObject(gameObject, 0.2f);
			speed = new Vector2(0,0);
			//Destroy(this);
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		hit = false;
	}
	
	void Update()
	{
		timer -= Time.deltaTime;
		if (timeShot <= 0) 
		{
			this.renderer.enabled = true;
			speed = new Vector2(0,0);
			DestroyObject(gameObject, 0.2f);
			timeShot = 1f;
		}
		
		timeShot -= Time.deltaTime;
		
		// 2 - Перемещение
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
		
		switch (player.turn) {
		case 1:
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), 300f * Time.deltaTime);
			break;
		case 2:
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), 300f * Time.deltaTime);
			break;
		case 3:
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 180), 300f * Time.deltaTime);
			break;
		case 4:
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 270), 300f * Time.deltaTime);
			break;
		}
		if (player.isFacingRight == true) 
		{
			direction = player.transform.right; // в двухмерном пространстве это будет справа от спрайта
			flip = false;
		}
		if (player.isFacingRight == false && !flip) 
		{
			Vector3 theScale = transform.localScale;
			//зеркально отражаем персонажа по оси Х)
			theScale.x *= -1;
			//задаем новый размер персонажа, равный старому, но зеркально отраженный
			transform.localScale = theScale;
			direction = -player.transform.right; // в двухмерном пространстве это будет справа от спрайта
			flip = !flip;
		}
	}
	
	void FixedUpdate()
	{
		// Применить движение к Rigidbody
		if (timer <=0)
			rigidbody2D.velocity = movement;
	}
}
