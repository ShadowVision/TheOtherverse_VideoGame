using UnityEngine;
using System.Collections;

public class Movement_Rot : MonoBehaviour {
	public Vector3 rot;
	void Start(){
		if(transform.localScale.x < 0){
			transform.localScale *=-1;
		}
	}
	// Update is called once per frame
	void Update () {
		Quaternion currentRot = transform.rotation;
		currentRot.eulerAngles += rot;
		transform.rotation = currentRot;
	}
}
