using UnityEngine;
using System.Collections;

public class Movement_Linear : MonoBehaviour {
	public float speed;
	
	// Update is called once per frame
	void Update () {
		if(speed != 0){
			transform.position += speed * transform.right;
		}
	}
}
