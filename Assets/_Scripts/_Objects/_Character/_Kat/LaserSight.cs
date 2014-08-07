using UnityEngine;
using System.Collections;

public class LaserSight : LineAttachment {

	// Use this for initialization
	void Start () {
		endTransform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		endTransform.position = Input.mousePosition;
	}
}
