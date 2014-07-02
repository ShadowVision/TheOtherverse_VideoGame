using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public Player player;
	public STATS.controllerType controllerType = STATS.controllerType.MOUSE;
	private string prefix; 
	public float movementHorizontal; 
	public float movementVertical;
	public Vector2 lastDirection = Vector2.zero;
	private float axisFilter  =.32f;
	private bool holdMovement = false;
	// Use this for initialization
	void Start () {
		if(controllerType == STATS.controllerType.XBOX){
			prefix = "X1_";
		} else if(controllerType == STATS.controllerType.MOUSE){
			prefix = "M_";
		}
	}
	
	void OnGUI(){
		//if(controllerType == STATS.controllerType.MOUSE)
			//GUILayout.Label("HOLD: " + Input.GetAxis(prefix+"Hold"));
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp (prefix+"Jump")){
			player.endJump();	
		}
		/*if(Input.GetButtonUp(prefix+"Attack1")){
			player.attack(0);	
		}
		if(Input.GetButtonUp(prefix+"Attack2")){
			player.attack(1);	
		}
		if(Input.GetButtonUp(prefix+"Attack3")){
			player.attack(2);	
		}
		if(Input.GetButtonUp(prefix+"Attack4")){
			player.attack(3);	
		}
		if(Input.GetButtonUp(prefix+"Attack5")){
			player.attack(4);	
		}
		if(Input.GetButtonUp(prefix+ "Attack6")){
			player.attack(5);	
		}*/	
	}
	private void FixedUpdate(){
		move();
		
		if(Input.GetButton(prefix+"Jump")){
			player.jump();	
		}
	}
	
	private void move(){		
		movementHorizontal = Input.GetAxis(prefix+"Horizontal");
		movementVertical = Input.GetAxis(prefix+"Vertical");
		setDirection();
		
		if(Mathf.Abs(Input.GetAxis(prefix+"Hold")) == 0){
			if(Mathf.Abs(movementHorizontal) > axisFilter){
				player.moveDir(movementHorizontal);
			}else{
				player.noMovement();	
			}
		}
	}
	private void setDirection(){
		if(Mathf.Abs(movementHorizontal) > axisFilter || Mathf.Abs(movementVertical) > axisFilter){
			lastDirection.x = movementHorizontal;
			lastDirection.y = -movementVertical;
		}
		
		if(Mathf.Abs(movementHorizontal) == 0 && Mathf.Abs(movementVertical) == 0) {
			player.currentDirection = Unit.Direction.NONE;
		}else{
			if(Mathf.Abs(movementHorizontal) > Mathf.Abs(movementVertical)){
				if(movementHorizontal > 0){
					player.currentDirection = Unit.Direction.FORWARD;
				}else{
					player.currentDirection = Unit.Direction.BACK;
				}
			} else {
				if(movementVertical > 0){
					player.currentDirection = Unit.Direction.UP;
				}else if (movementVertical < 0){
					player.currentDirection = Unit.Direction.DOWN;
				} 
			}
		}
	}
}
