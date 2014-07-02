using UnityEngine;
using System.Collections;

public class Minion_Jump : Unit {

	// Use this for initialization
	new void Start () {
		base.Start();
		jumpStrength = 15;
		InvokeRepeating("doJump",0,3);
	}
	void doJump(){
		jump();	
	}
	// Update is called once per frame
	new void Update () {
		base.Update();
	}
}
