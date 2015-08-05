using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour 
{
	private float timeShot = 1f; 
	public GameObject boooom; 
	private characterControllerScript player;
	private bool hit;
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
		player = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
	}

	void OnTriggerEnter2D( Collider2D other)
	{
		//DestroyObject (this);
		//Debug.Log ("boob bay");
		if (other.gameObject.tag == "Player") 
		{
			hit = true;
			player = other.GetComponent<characterControllerScript>();
			player.TakeDmg(3);
			//var shotTransform = Instantiate(boooom) as Transform;
			//shotTransform.position = transform.position;
			DestroyObject(gameObject, 0.01f);
		}
		if (other.gameObject.tag == "ground")
		{
			hit = true;


			DestroyObject(gameObject, 0.01f);
			//Destroy(this);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		hit = false;
	}

	void Update()
	{
		if (timeShot <= 0) 
		{
			DestroyObject(gameObject, 0.01f);
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
	}
	
	void FixedUpdate()
	{
		// Применить движение к Rigidbody
		rigidbody2D.velocity = movement;
	}
}
