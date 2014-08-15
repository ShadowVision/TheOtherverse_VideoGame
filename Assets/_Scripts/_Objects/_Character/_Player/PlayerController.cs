using UnityEngine;
using System.Collections;

public class PlayerController : Unit {
	private MeshController mesh; 
	private bool attacking = false;
	public bool isAttacking{
		get{
			return attacking;
		}
	}

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
	}
	public override void spawn ()
	{
		base.spawn ();
		mesh.loadMesh ();
	}
	public void dropBelow(){
		if(currentState== UnitState.GROUND){
			enterAir();
			//TODO find the piece of ground we are standing on, then tell it not to collide with us until we land again
		}
	}
	
	new public void startShield(){
		base.startShield();
		input.lockMovement = true;
	}
	new public void stopShield(){
		base.stopShield();
		input.lockMovement = false;
	}

	public void startAttacking(string attackName = "NoName"){
		if(attackName == "Sword"){
			startShield();
		}
		attacking = true;
		input.lockMovement = true;
	}
	public void stopAttacking(string attackName = "NoName"){
		if(attackName == "Sword"){
			stopShield();
		}
		attacking = false;
		input.lockMovement = false;
	}
	
}
