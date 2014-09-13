using UnityEngine;
using System.Collections;

public class CustomInput : MonoBehaviour {
	public enum KeyNames{
		START,
		JUMP,
		BLOCK,
		DODGE,
		MELEE,
		RANGED
	}
	public bool showKeyInput = false;
	public int[] keys;

	void Update(){
		if(showKeyInput && Input.anyKeyDown){
			Debug.Log("KeyDown: " + Input.inputString);
		}
	}

	public void setKey(KeyNames name, int keycode){
		keys[(int)name] = keycode;
	}

	public int getKeycode(KeyNames name){
		return keys[(int)name];
	}


}
