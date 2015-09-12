using UnityEngine;
using System.Collections;

public class OnGUIs : MonoBehaviour {
	public GameObject character;
	private characterControllerScript script;
	public int price =0;
	public int hp;
	public int maxhp;
	public Texture hpbar;
	public GUIStyle HPB;
	public GUIStyle MONEYB;
	public GUIStyle chestGUI;
	private int money;
	private int damage;
	private float attackspped;
	private float kdturn;
	private bool exit = false;
	// Use this for initialization
	void OnGUI(){
		script = character.GetComponent<characterControllerScript>();
		hp = script.hp;
		maxhp = script.Maxhp;
		money = script.money;
		damage = script.Damage;
		attackspped = script.spddmg;
		kdturn = script.KDturn;

		//GUI.Box (new Rect (0, 0, 100, 20), "HP: "+ hp + "%");
		//GUI.Box (new Rect (0, 20, 100, 20), "Money: "+money+"$");
		GUI.TextArea (new Rect (Screen.width - 120, 20, 100, 30), money + " $",MONEYB);
		GUI.Box (new Rect (0, 40, 100, 20), "DMG: "+damage+"$");
		GUI.Box (new Rect (0, 60, 100, 20), "Attack speed: "+attackspped+"$");
		GUI.Box (new Rect (0, 80, 100, 20), "KD spin: "+kdturn+"$");
		GUI.TextArea (new Rect (Screen.width / 2, Screen.height - 45, 100, 20), hp + "/" + maxhp,HPB);
		if (price !=0)
			GUI.TextArea (new Rect (Screen.width/2-50, Screen.height/2-15, 100, 30), "Price of chest: " + price + "$",chestGUI); 
		/*
		GUI.BeginGroup ( new Rect(100, 100, hp/maxhp*100*2.93f, 35)); //где 2.93 - ширина текстуры/100
		GUI.DrawTexture( new Rect(0, 0, 293, 50), hpbar);  //293 - ширина текстуры
		GUI.EndGroup();
*/
		if(script.allive==false)
		{
			// Make a background box
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-45,200,60), "You are DEAD.");
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(Screen.width/2-90,Screen.height/2-20,80,20), "Try Again!")) {
				Application.LoadLevel("Level_1");
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(Screen.width/2,Screen.height/2-20,80,20), "Quit")) {
				Application.LoadLevel("Menu");
			}
		}
		if (exit) {
			Time.timeScale = 0;
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 45, 200, 60), "You really want to quit?.");
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 2 - 20, 80, 20), "NOOOOO")) {
				//Application.LoadLevel ("Level_1");
				Time.timeScale = 1;
				exit= false;
			}							 
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 20, 80, 20), "Quit")) {
				Application.LoadLevel ("Menu");
				Time.timeScale = 1;
				exit= false;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
						exit = true;
	}
}
