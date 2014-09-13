using UnityEngine;
using System.Collections;

public class Libonati : MonoBehaviour {

	static public Vector3 getMousePoint(){
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -Camera.main.transform.position.z;
		return Camera.main.ScreenToWorldPoint(mousePosition);	
	}

	//Text Modification
	static public string padInt(int value, int numberOfDigits){
		return value.ToString ("D" + numberOfDigits.ToString ());
	}
	static public string clockString(int totalSeconds){		
		int minutes = (int)Mathf.Floor(totalSeconds/60);
		int seconds = (int)Mathf.Round(totalSeconds - minutes*60);
		string clock = minutes+":"+Libonati.padInt(seconds,2);
		return clock;
	}
	
	//Game Object Modification
	static public void destroyAllChildren(GameObject parent){
		foreach (Transform child in parent.transform) Destroy(child.gameObject);
	}
	public static Vector2 getPositionFromLinearArray(int i, int width){
		return new Vector2(i%width, Mathf.Floor(i/width));
	}
	
	static public void showAllSprites(GameObject obj){
		toggleAllSpriteVisibility (obj, true);
	}
	static public void hideAllSprites(GameObject obj){
		toggleAllSpriteVisibility (obj, false);
	}
	static public void toggleAllSpriteVisibility(GameObject obj, bool show){
		foreach(SpriteRenderer ren in obj.GetComponentsInChildren<SpriteRenderer>()){
			ren.enabled = show;
		}
	}
	
	static public void showAllMesh(GameObject obj){
		toggleAllMeshVisibility (obj, true);
	}
	static public void hideAllMesh(GameObject obj){
		toggleAllMeshVisibility (obj, false);
	}
	static public void toggleAllMeshVisibility(GameObject obj, bool show){
		foreach(Renderer ren in obj.GetComponentsInChildren<Renderer>()){
			ren.enabled = show;
		}
	}
	
	static public void hideAll(GameObject obj){
		hideAllMesh (obj);
		hideAllSprites (obj);
	}
	static public void showAll(GameObject obj){
		showAllMesh (obj);
		showAllSprites (obj);
	}
}
