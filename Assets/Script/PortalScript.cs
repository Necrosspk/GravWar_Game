using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public Transform NPCcommon;
	public int place;
	public float MainTimer=0f; //60s
	public float WavesTimer=0f; // 20,15,12,7,6 s
	public int WavesNumber; // 5s
	public int RandSpawnNum; //8-10
	public int MonsterCount;
	private bool isStarted;
	private bool entered;
	private bool ended = false;

	private Vector3[] Places1;
	private Vector3[] Places2;
	private Vector3[] Places3;




	// Use this for initialization
	void Start () {
		entered = false;
		isStarted = false;
		place = Random.Range (1, 4);	
		Places1 = new Vector3[9];
		Places2 = new Vector3[9];
		Places3 = new Vector3[7];
		Places1 [0] = new Vector3 (3.12588f, 23.4562f, 0f);
		Places1 [1] = new Vector3 (5.69314f, 23.3982f, 0f);
		Places1 [2] = new Vector3 (10.1604f, 23.3837f, 0f);
		Places1 [3] = new Vector3 (10.0589f, 22.0493f, 0f);
		Places1 [4] = new Vector3 (11.2773f, 19.3805f, 0f);
		Places1 [5] = new Vector3 (8.53599f, 19.1630f, 0f);
		Places1 [6] = new Vector3 (6.07025f, 17.9446f, 0f);
		Places1 [7] = new Vector3 (3.70605f, 17.9011f, 0f);
		Places1 [8] = new Vector3 (8.76806f, 22.7310f, 0f);

		Places2 [0] = new Vector3 (5.93804f, -3.4702f, 0f);
		Places2 [1] = new Vector3 (3.48480f, -3.4702f, 0f);
		Places2 [2] = new Vector3 (1.86878f, -1.5232f, 0f);
		Places2 [3] = new Vector3 (4.45831f, 8.42603f, 0f);
		Places2 [4] = new Vector3 (5.84069f, 6.61530f, 0f);
		Places2 [5] = new Vector3 (9.83208f, 4.35676f, 0f);
		Places2 [6] = new Vector3 (8.50811f, 1.82564f, 0f);
		Places2 [7] = new Vector3 (11.4004f, -0.9315f, 0f);
		Places2 [8] = new Vector3 (9.95624f, 1.86802f, 0f);

		Places3 [0] = new Vector3 (8.73180f, 10.0987f, 0f);
		Places3 [1] = new Vector3 (8.70958f, 8.74337f, 0f);
		Places3 [2] = new Vector3 (7.13206f, 8.58783f, 0f);
		Places3 [3] = new Vector3 (10.6648f, 10.0320f, 0f);
		Places3 [4] = new Vector3 (8.73180f, 12.4316f, 0f);
		Places3 [5] = new Vector3 (8.64293f, 7.34358f, 0f);
		Places3 [6] = new Vector3 (11.4647f, 13.1871f, 0f);


		if (place == 1)																		
			this.transform.position = new Vector3 (10.2315f, 19.35334f, 0f);				
		else if (place == 2)
			this.transform.position = new Vector3 (9.950826f, 1.925393f, 0f);				
		else 	
			this.transform.position = new Vector3 (9.732812f, 8.756865f, 0f); 
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") 
			entered=true;						
		}
	
	// Update is called once per frame
	void Update () {

		if (entered)
			if(!ended)
			if (Input.GetKeyDown (KeyCode.F)) 
				StartEscapeEvent ();

		WavesTimer -= Time.deltaTime;//
		MainTimer -= Time.deltaTime; //
		///////////////////////////////

					

		if(isStarted)
		{
			if (WavesTimer <= 0)
			{
				switch (WavesNumber) {	
				case 1:
					WavesTimer = 20f;
					WavesNumber++;
					MonsterCount = 3;
					break;
				case 2:
					WavesTimer = 15f;
					WavesNumber++;
					MonsterCount = 4;
					break;
				case 3:
					WavesTimer = 12f;
					WavesNumber++;
					MonsterCount = 5;
					break;
				case 4:
					WavesTimer = 7f;
					WavesNumber++;
					MonsterCount = 4;
					break;
				case 5:
					WavesTimer = 6f;
					WavesNumber++;
					MonsterCount = 3;
					break;
				}
			}
			switch (place) {
			case 1:
				if (MonsterCount>0)
				{
				RandSpawnNum=Random.Range(0,9);	
				var NPC1 = Instantiate(NPCcommon) as Transform;
				NPC1.position = Places1[RandSpawnNum];
				MonsterCount--;
				}
				break;
			case 2:
				if (MonsterCount>0)
				{
					RandSpawnNum=Random.Range(0,9);	
					var NPC1 = Instantiate(NPCcommon) as Transform;
					NPC1.position = Places2[RandSpawnNum];
					MonsterCount--;
				}
				break;
			case 3:
				if (MonsterCount>0)
				{
					RandSpawnNum=Random.Range(0,7);	
					var NPC1 = Instantiate(NPCcommon) as Transform;
					NPC1.position = Places3[RandSpawnNum];
					MonsterCount--;
				}
				break;
			}

		}
	}
	void StartEscapeEvent()
	{
		Debug.Log ("StartEscapeEvent!");
		isStarted = true;
		MainTimer = 60f;
		WavesTimer = 0f;
		WavesNumber = 1;
		MonsterCount = 3;
		this.collider2D.enabled = false;
	}

	void OnGUI(){
		if (WavesNumber >= 6 && MainTimer>0) {
			ended = true;
			if (Input.GetKeyDown (KeyCode.F)) 
			{
				GUI.Box (new Rect (Screen.width / 2 - 140, Screen.height / 2 - 45, 280, 60), "!!You complite first stage!!");
				if (GUI.Button (new Rect (Screen.width / 2 - 130, Screen.height / 2 - 20, 260, 20), "Vse huinya Misha, Davai zanovo))"))
						Application.LoadLevel ("testStage");
			}
		}
		if (!isStarted && entered)
				GUI.Box (new Rect (Screen.width / 2 - 140, Screen.height / 2 - 45, 280, 25), "Press 'F' to activate Portal (and preapre to DIE)");
		if (entered && ended)
			GUI.Box (new Rect (Screen.width / 2 - 140, Screen.height / 2 - 45, 280, 25), "Press 'F' to next level");
	
	
		if (isStarted && MainTimer>0) {
				GUI.Box (new Rect (Screen.width / 2 - 140, Screen.height / 2 - 200, 280, 20), "Time to open portal is :" + MainTimer.ToString () + "sec.");
		}
	}

}
