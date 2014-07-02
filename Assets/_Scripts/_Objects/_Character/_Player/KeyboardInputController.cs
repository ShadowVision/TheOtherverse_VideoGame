using UnityEngine;
using System.Collections;

public class KeyboardInputController : MonoBehaviour {
	private PlayerController player;
	public bool lockMovement = false;
	float xMovement = 0;
	float yMovement = 0;
	static public Vector3 mousePosition{
		get{
			Vector3 mouse = Input.mousePosition;
			mouse.z = -Camera.main.transform.position.z;
			return Camera.main.ScreenToWorldPoint(mouse);
		}
	}

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		xMovement = Input.GetAxis("Horizontal");
		yMovement = Input.GetAxis("Vertical");
		if(!lockMovement){
			player.moveDir(xMovement);
			if(Input.GetButton("Jump")){
				if(yMovement<0){
					player.dropBelow();
				}else{
					player.jump();
				}
			} else if(Input.GetButtonUp("Jump")){
				player.endJump();
			} 
			if(Input.GetButtonDown("Shield")){
				player.startShield();
			}
		}
		if(Input.GetButtonUp("Shield")){
			player.stopShield();
		}
		int numberOfAttackButtons = 4;
		for(int i=0; i<numberOfAttackButtons; i++){
			if(Input.GetButtonDown("Attack"+i)){
				player.attackPress(i);
			}else if(Input.GetButtonUp("Attack"+i)){
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
