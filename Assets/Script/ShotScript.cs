using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {
	//--------------------------------
	// 1 – Переменная дизайнера
	//--------------------------------
	
	/// <summary>
	/// Причиненный вред
	/// </summary>
	public int damage = 1;

	/// <summary>
	/// Префаб снаряда для стрельбы
	/// </summary>
	public Transform shortPrefab;

	///<summary>
	/// Время перезарядки в сек
	/// </summary>
	public float shootingRate = 0.15f;

	//----------------------------------
	//	2 - Перезарядка
	//----------------------------------

	private float shootCooldown;

	/// <summary>
	/// Снаряд наносит повреждения игроку или врагам?
	/// </summary>
	public bool isEnemyShot = false;
	
	void Start()
	{
		// Ограниченное время жизни, чтобы избежать утечек
		Destroy(gameObject, 0.2f); // 0.1 секунд
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0 )
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	//---------------------------------------
	// 3 - Стрельба из другого скрипта
	//---------------------------------------

	///<summary
	/// Создайте новый снаряд, если это возможно
	///</summaru>
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			//просто сохрани))
			shootCooldown = shootingRate;

			// новый выстрел
			var shotTransform = Instantiate(shortPrefab) as Transform;
			// определение местоположения
			shotTransform.position = transform.position;
			// свойство врага
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}
			// Cделать так, чтобы выстрел всегда был направлен на него
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if(move != null)
			{
				move.direction = this.transform.right; // справо от спрайта
			}
		}
	}

	/// <summary>
	/// Готово ли оружие выпустить свой снаряд
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
