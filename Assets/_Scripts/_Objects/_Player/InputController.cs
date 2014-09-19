using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {
	private PlayerController player;
	private float xMovement = 0;
	private float yMovement = 0;
	private Dictionary<string,int> key;

	public int playerId = 0;
	public int joystickNumber = 0;
	[HideInInspector]
	public bool lockMovement = false;

	public Vector3 dir;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController>();

	}
	void Awake(){
	}
	public bool keyDown(string name){
		return cInput.GetButtonDown (playerId+name);
	}
	public bool keyHeldDown(string name){
		return cInput.GetButton (playerId+name);
	}
	public bool keyUp(string name){
		return cInput.GetButtonUp (playerId+name);
	}
	// Update is called once per frame
	void Update () {
		//xMovement = Input.GetAxis(idName + "Horizontal");
		//yMovement = Input.GetAxis(idName + "Vertical");
		xMovement = cInput.GetAxis(playerId+"HorizontalMovement");
		yMovement = cInput.GetAxis(playerId+"VerticalMovement");
		dir = new Vector3 (xMovement, yMovement, 0).normalized;
		if(!lockMovement){
			player.moveDir(xMovement);
			if(keyHeldDown("Jump")){
				//if(yMovement<0){
				//	player.dropBelow();
				//}else{
					player.jump();
				//}
			}else{
				player.endJump();
			} 
			//Shielding
			if(keyDown("Block")){
				player.shield();
			}
			//Dodgeing
			if(keyDown("Dodge")){
				player.dodge();
			}
		}
		/*if(Input.GetButtonUp(idName + "Shield")){
			player.stopShield();
		}*/
		if(keyDown("Melee")){
			player.attackPress(0);
		}else if(keyUp("Melee")){
			player.attackRelease(0);
		}
		if(keyDown("Ranged")){
			player.attackPress(1);
		}else if(keyUp("Ranged")){
			player.attackRelease(1);
		}
		/*int numberOfAttackButtons = 4;
		for(int i=0; i<numberOfAttackButtons; i++){
			if(keyDown("Attack"+i)){
				player.attackPress(i);
			}else if(Input.GetButtonUp(idName + "Attack"+i)){
				player.attackRelease(i);
			}
		}*/
		checkDirection();
	}
	private void checkDirection(){
		float threshold = .1f;
		float xAbs = Mathf.Abs(xMovement);
		float yAbs = Mathf.Abs(yMovement);
		if(xAbs < threshold && yAbs < threshold){
			//not aiming anywhere
			player.currentDirection = Unit.Direction.NONE;
		}else{
			if(xAbs > yAbs){
				//X is dominent
				if(xMovement > 0){
					if(player.facingRight){
						player.currentDirection = Unit.Direction.FORWARD;
					}else{
						player.currentDirection = Unit.Direction.BACK;
					}
				}else{
					if(player.facingRight){
						player.currentDirection = Unit.Direction.BACK;
					}else{
						player.currentDirection = Unit.Direction.FORWARD;
					}
				}
			}else{
				//Y is dominent
				if(yMovement > 0){
					player.currentDirection = Unit.Direction.UP;
				}else{
					player.currentDirection = Unit.Direction.DOWN;
				}
			}

	   }
	}

}
