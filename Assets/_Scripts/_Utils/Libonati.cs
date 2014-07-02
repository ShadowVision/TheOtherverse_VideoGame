using UnityEngine;
using System.Collections;

public class Libonati : MonoBehaviour {

	static public Vector3 getMousePoint(){
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -Camera.main.transform.position.z;
		return Camera.main.ScreenToWorldPoint(mousePosition);	
	}
}
