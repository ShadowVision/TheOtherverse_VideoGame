using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	public Transform teleportTo;
	public bool[] modifyAxis = new bool[3]{true,false,false};

	private Vector3 newPosition;

	void OnCollisionEnter2D(Collision2D coll) {
		newPosition = coll.gameObject.transform.position;
		if(modifyAxis[0]){
			newPosition.x = teleportTo.transform.position.x;
		}
		if(modifyAxis[1]){
			newPosition.y = teleportTo.transform.position.y;
		}
		if(modifyAxis[2]){
			newPosition.z = teleportTo.transform.position.z;
		}
		coll.gameObject.transform.position = teleportTo.transform.position;
	}
}
