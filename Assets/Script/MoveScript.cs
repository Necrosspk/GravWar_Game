using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
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
	
	void Update()
	{
		 
		// 2 - Перемещение
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}
	
	void FixedUpdate()
	{
		// Применить движение к Rigidbody
		rigidbody2D.velocity = movement;
	}
}
