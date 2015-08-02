using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	private WeaponScript weapon;

	// Use this for initialization
	void Aweke () 
	{
		//получить оружие только один раз
		weapon = this.GetComponent<WeaponScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//автоматическая стрельба
		if (weapon != null && weapon.CanAttack) 
		{
			weapon.Attack(true);
		}
	}
}
