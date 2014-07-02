using UnityEngine;
using System.Collections;

public class PlayerController : Unit {
	public KeyboardInputController input;
	private bool dropLock = false;

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		if(!dropLock){
			if(currentState == UnitState.AIR && rigidbody2D.velocity.y > 0){
				gameObject.layer = LayerMask.NameToLayer("Player_NoGround");
			}else{
				gameObject.layer = LayerMask.NameToLayer("Player");
			}
		}
	}
	
	public void dropBelow(){
		if(currentState== UnitState.GROUND){
			enterAir();
			gameObject.layer = LayerMask.NameToLayer("Player_NoGround");
			dropLock = true;
			Invoke("releaseDropLock",.3f);
		}
	}
	private void releaseDropLock(){
		dropLock = false;
	}
	
	new public void startShield(){
		base.startShield();
		input.lockMovement = true;
	}
	new public void stopShield(){
		base.stopShield();
		input.lockMovement = false;
	}
}
