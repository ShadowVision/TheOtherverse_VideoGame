using UnityEngine;
using System.Collections;

public class Movement_Vel : MonoBehaviour {
	public Vector3 vel;
	void Start(){
		if(transform.localScale.x < 0){
			vel.x *=-1;
		}
	}
	// Update is called once per frame
	void Update () {
		transform.position += vel;
	}
}
