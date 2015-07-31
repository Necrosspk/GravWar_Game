using UnityEngine;
using System.Collections;

public class BorderRight : MonoBehaviour {
	public GameObject playerGO;
	private characterControllerScript player;
	private bool grounded;
	private bool ya;

	void Start(){
		player = playerGO.GetComponent<characterControllerScript> ();
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
								playerGO.rigidbody2D.AddForce (new Vector2 (-180.0f, 0));
								break;
						case 2:
								playerGO.rigidbody2D.AddForce (new Vector2 (0, -180.0f));
								break;
						case 3:
								playerGO.rigidbody2D.AddForce (new Vector2 (300.0f, 0));
								break;
						case 4:
								playerGO.rigidbody2D.AddForce (new Vector2 (0, 300.0f));
								break;
						}
				}
		if (ya && !player.isFacingRight) {
			switch (player.turn) {
			case 1:
				playerGO.rigidbody2D.AddForce (new Vector2 (180.0f, 0));
				break;
			case 2:
				playerGO.rigidbody2D.AddForce (new Vector2 (0, 180.0f));
				break;
			case 3:
				playerGO.rigidbody2D.AddForce (new Vector2 (-300.0f, 0));
				break;
			case 4:
				playerGO.rigidbody2D.AddForce (new Vector2 (0, -300.0f));
				break;
			}
		}

	}
}
