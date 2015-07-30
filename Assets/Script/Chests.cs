using UnityEngine;
using System.Collections;

public class Chests : MonoBehaviour {
	public bool entered;
	public GameObject character;
	private characterControllerScript script;
	public int price;
	private int item;
	// Use this for initialization
	void Start () {
		int rand = Random.Range (1, 20);
		if (rand > 7)
			Destroy (this.gameObject);
		script = character.GetComponent<characterControllerScript> ();
		item = Random.Range (1, 7);
		int randPrice = Random.Range (1, 4);
		switch (randPrice) {
		case 1:
				price = 50;
				break;
		case 2:
				price = 100;
				break;
		case 3:
				price = 200;
				break;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log("Trigger: Player Entered");
		if(other.gameObject.tag == "Player")
				entered=true;	
	}

	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject.tag == "Player")
			entered=false;
	}

	void Update () {
		if (entered == true)
		{
			if (Input.GetKeyDown (KeyCode.F)){ 
				if(script.money<price)
				{
					Debug.Log("Not enough money!");

				}
				else
				{					
					script.item_id = item;
					script.money -=price;
					//Debug.Log("You took a: " + item + "!");
						Destroy (this.gameObject);
				}
			}
		}
	}

	void OnGUI(){
		if (entered)
			GUI.Box (new Rect (400, 150, 100, 40), "Price\n of chest: " + price + "$");
		}

}
