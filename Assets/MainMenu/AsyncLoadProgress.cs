using UnityEngine;
using System.Collections;

public class AsyncLoadProgress : MonoBehaviour {
	
	[ExecuteInEditMode]
	private GUIText gui;
	
	
	private AsyncOperation async = null;
	private bool isLoading = false;
	public string levelName = "";
	public float cutoff = 0.2f;
	
	private IEnumerator _Start(){
		Debug.Log( "Loading... " );
		async = Application.LoadLevelAsync( levelName );
		while( !async.isDone ){
			Debug.Log( string.Format( "Loading {0}%", async.progress*100 ) );
			yield return null;
		}
		Debug.Log( "Loading complete" );
		isLoading = false;
		yield return async;
	}
	
	private void OnGUI(){
		if( !isLoading ){
			if( GUI.Button( new Rect( ( Screen.width*0.5f ) - 60, Screen.height*0.6f, 120, 40 ), "press to continue" ) ){
				isLoading = true;
				StartCoroutine( "_Start" ); //код согласия
			}
		}
	}
	
	private void Update(){
		if( isLoading ){
			// 1 параметр макс значение 2 параметр ноль или отправная величина, третий параметр текущее значение.    
			renderer.material.SetFloat( "_Cutoff", Mathf.InverseLerp( 0, 1, async.progress ) );
			Debug.Log( string.Format( "=> isLoading {0}%", async.progress*100 ) );
		} else{
			// 1 параметр макс значение 2 параметр ноль или отправная величина, третий параметр текущее значение.    
			renderer.material.SetFloat( "_Cutoff", Mathf.InverseLerp( 0, 1, cutoff ) );
		}
		
		if (!gui) {
			GameObject go = new GameObject("FPS Display", typeof(GUIText));
			go.hideFlags = HideFlags.HideAndDontSave;
			go.transform.position = new Vector3(0, 0, 0);
			gui = go.guiText;
			gui.pixelOffset = new Vector2(5, 45);
		}
		gui.text = string.Format( "cutoff = {0}", cutoff );
		
	}
	
	private void OnDisable() {
		if (gui)
			DestroyImmediate(gui.gameObject);
	}
}