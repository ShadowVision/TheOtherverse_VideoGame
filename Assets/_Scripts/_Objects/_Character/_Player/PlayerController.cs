using UnityEngine;
using System.Collections;

public class PlayerController : Unit {
	private bool dropLock = false;
	private MeshController mesh; 

	[HideInInspector]
	public InputController input;


	// Use this for initialization
	void Start () {
		base.Start();
	}
	new protected void Awake(){
		base.Awake ();
		input = GetComponent<InputController> ();
		mesh = GetComponent<MeshController> ();
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
	public override void spawn ()
	{
		base.spawn ();
		mesh.loadMesh ();
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
