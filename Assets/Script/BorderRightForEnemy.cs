using UnityEngine;
using System.Collections;

public class BorderRightForEnemy : MonoBehaviour {
	public GameObject enemyGO;
	private characterControllerScript player;
	private EnemyScriptMelle enemy;
	private bool grounded;
	private bool ya;
	
	void Start(){
		player = (characterControllerScript)FindObjectOfType(typeof(characterControllerScript));
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "ground") 
		{
			ya=true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		ya = false;
	}
	
	void Update()
	{
		if (ya && player.isFacingRight) {
			switch (player.turn) {
			case 1:
				enemyGO.rigidbody2D.AddForce (new Vector2 (-180.0f, 0));
				break;
			case 2:
				enemyGO.rigidbody2D.AddForce (new Vector2 (0, -180.0f));
				break;
			case 3:
				enemyGO.rigidbody2D.AddForce (new Vector2 (300.0f, 0));
				break;
			case 4:
				enemyGO.rigidbody2D.AddForce (new Vector2 (0, 300.0f));
				break;
			}
		}
		if (ya && !player.isFacingRight) {
			switch (player.turn) {
			case 1:
				enemyGO.rigidbody2D.AddForce (new Vector2 (180.0f, 0));
				break;
			case 2:
				enemyGO.rigidbody2D.AddForce (new Vector2 (0, 180.0f));
				break;
			case 3:
				enemyGO.rigidbody2D.AddForce (new Vector2 (-300.0f, 0));
				break;
			case 4:
				enemyGO.rigidbody2D.AddForce (new Vector2 (0, -300.0f));
				break;
			}
		}
		
	}
}
