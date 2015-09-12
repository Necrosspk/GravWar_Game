using UnityEngine;
using System.Collections;

public class Chests : MonoBehaviour {
	public bool entered;
	public GameObject character;
	private characterControllerScript script;
	public int price;
	private int item;
	private int rare;	
	public GUIStyle chestGUI;
	private OnGUIs GGUUII;

	public Transform CommonItem;
	public Transform RareItem;
	public Transform UnusualItem;
	// Use this for initialization
	void Start () {
		GGUUII = (OnGUIs)(FindObjectOfType (typeof(OnGUIs)));
		int rand = Random.Range (1, 20);
		if (rand > 9)
			Destroy (this.gameObject);
		script = character.GetComponent<characterControllerScript> ();
		int randPrice = Random.Range (1, 4);
		switch (randPrice) {
		case 1:
		{
				price = 50;
				rare = 1;
				break;
		}
		case 2:
		{
				price = 100;
				rare = 2;
				break;
		}
		case 3:
		{
				price = 200;
				rare = 3;
				break;
		}
		}
		item = Random.Range (0, 11);
		//item = Random.Range (1, 5) * rare + Random.Range(0,3); // RARE - 1 (common) №1-10, RARE - 2 (rare) №11-20, RARE - 3 (unusial) № 21-30
		if (item > 10)
			item = 10;
		if (item == 0)
			item = 3;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log("Trigger: Player Entered");
		if(other.gameObject.tag == "Player")
				entered=true;	
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
						entered = false;
						GGUUII.price = 0;
				}
	}

	void Update () {
		if (entered == true)
		{
			GGUUII.price = price;
			if (Input.GetKeyDown (KeyCode.F))
			{ 
				if(script.money<price)
				{
					Debug.Log("Not enough money!");
				}
				else
				{					
					if (item <11)
					{
						var Common = Instantiate (CommonItem) as Transform;
						Common.position = transform.position;
						Common.rotation = transform.rotation;
					}
					if (item >=11 && item <21)
					{
						var Rare = Instantiate (RareItem) as Transform;
						Rare.position = transform.position;
						Rare.rotation = transform.rotation;
					}
					if (item >=21)
					{
						var Unusual = Instantiate (UnusualItem) as Transform;
						Unusual.position = transform.position;
						Unusual.rotation = transform.rotation;
					}
					script.item_id = item;
					script.money -=price;
					GGUUII.price=0;
					//Debug.Log("You took a: " + item + "!");
						Destroy (this.gameObject);
				}
			}
		}
	}

}
