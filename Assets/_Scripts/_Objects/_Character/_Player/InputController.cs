using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	private PlayerController player;
	private float xMovement = 0;
	private float yMovement = 0;

	public string idName = "Player1";
	public bool lockMovement = false;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		xMovement = Input.GetAxis(idName + "Horizontal");
		yMovement = Input.GetAxis(idName + "Vertical");
		if(!lockMovement){
			player.moveDir(xMovement);
			if(Input.GetButton(idName + "Jump")){
				if(yMovement<0){
					player.dropBelow();
				}else{
					player.jump();
				}
			} else if(Input.GetButtonUp(idName + "Jump")){
				player.endJump();
			} 
			if(Input.GetButtonDown(idName + "Shield")){
				player.startShield();
			}
		}
		if(Input.GetButtonUp(idName + "Shield")){
			player.stopShield();
		}
		int numberOfAttackButtons = 4;
		for(int i=0; i<numberOfAttackButtons; i++){
			if(Input.GetButtonDown(idName + "Attack"+i)){
				player.attackPress(i);
			}else if(Input.GetButtonUp(idName + "Attack"+i)){
				player.attackRelease(i);
			}
		}
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
