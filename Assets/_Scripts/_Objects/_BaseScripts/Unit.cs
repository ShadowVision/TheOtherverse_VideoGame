using UnityEngine;
using System.Collections;

public class Unit : AliveObject {

	//components
	public AnimationController anim;
	public GameObject jumpEffect;

	//Movemenet
	public Transform groundCheckTop;
	public Transform groundCheckBottom;
	public float moveSpeed = 10;


	//Jumping
	public float jumpStrength = 400;
	protected int numJumpsDone = 0;
	public int maxNumJumps = 2;
	protected bool jumpLock = false;
	protected int jumpTickCount = 0;
	public int maxJumpTicks = 20;
	public float airSpeed = 10;
	public float jumpStrengthOverTimeMod = 1;

	//attacks
	public Attack[] attacks;

	//animation
	private bool canAnimate = true;

	private bool jumping = false;


	// Use this for initialization
	protected void Start () {

	}

	virtual protected void setChildStats(){
		
	}
	// Update is called once per frame
	protected void Update () {
		//limitSpeed();
		checkGround();
		handleMovementAnimation();
	}
	private void checkGround(){
		int layerMask = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Ground_Ghost"));
		//layerMask = 1;
		if(Physics2D.Linecast(groundCheckTop.position, groundCheckBottom.position, layerMask)){
			hitGround();
		}
	}
	private void handleMovementAnimation(){
		if(anim){
			anim.setMoveSpeed(rigidbody2D.velocity.magnitude);
		}
	}

	private void unlockAnimation(){
		canAnimate = true;	
	}
	public void moveDir(float percentage){
		int mod = 1;
		float speed = moveSpeed * percentage;
		if(speed < 0){
			mod = -1;
		}
		if(currentState == UnitState.AIR){
			speed = airSpeed * percentage;	
		}
		//speed = Mathf.Clamp(Mathf.Abs(rigidbody2D.velocity.x)+speed;
		speed -= rigidbody2D.velocity.x;
		if(speed*mod>0){
			move(new Vector2(speed,0));
		}
		if(rigidbody2D.velocity.x > .1f){
			faceRight();
		} else if( rigidbody2D.velocity.x < -.1f){
			faceLeft();	
		}
	}
	public void noMovement(){
		Vector3 vel = rigidbody2D.velocity;
		vel.x *= .90f;
		rigidbody2D.velocity = vel;
	}
	private void faceLeft(){
		Vector3 scale = transform.localScale;
		scale.x = -1;
		transform.localScale = scale;
	}
	private void faceRight(){
		Vector3 scale = transform.localScale;
		scale.x = 1;
		transform.localScale = scale;
	}
	void OnCollisionEnter2D(Collision2D collision) {
	}
	
	public void jump(){
		if(canJump){
			jumping = true;
			jump (jumpStrength);
		}
	}
	public void jump(float strength){
		Vector2 newVel = rigidbody2D.velocity;
		if(newVel.y <0){
			newVel.y = 0;
		}
		rigidbody2D.velocity = newVel;
		if(jumpTickCount == 0){
			if(anim!= null){
				anim.playAnimation(AnimationController.Animations.JUMP);
			}
			Instantiate(jumpEffect,transform.position,Quaternion.identity);
		}
		jumpTickCount++;
		strength-= rigidbody2D.velocity.y / jumpStrengthOverTimeMod;
		if(jumpTickCount > maxJumpTicks){
			maxOutJump();	
		}
		move(new Vector3(0,strength,0));
		enterAir ();
	}
	private void maxOutJump(){
		jumpLock = true;
	}
	public bool canJump{
		get{
			if(!jumpLock){
				if(currentState == UnitState.GROUND || numJumpsDone < maxNumJumps){
					return true;	
				}
			}
			return false;	
		}
	}

	private void hitGround(){
		//Debug.Log ("Hit Ground");
		endJump();
		enterGround();
		numJumpsDone = 0;
	}
	public void endJump(){
		if(jumping){
			jumpLock = false;
			jumpTickCount = 0;
			numJumpsDone++;
			jumping = false;
		}
	}

	public void attackPress(int i){
		attacks[i].attackPress();
	}
	public void attackRelease(int i){
		attacks[i].attackRelease();
	}

	public override void respawn ()
	{
		base.respawn ();
		foreach(Attack att in attacks){
			att.reset();
		}
	}

	public void enterAir(){
		currentState = UnitState.AIR;
		if(anim){
			anim.setAir (true);
		}
	}
	public void enterGround(){
		currentState = UnitState.GROUND;
		if(anim){
			anim.setAir (false);
		}
	}

	//SHIELDING
	protected void startShield(){
		if(!shielding){
			shielding = true;
			shield = (AliveObject)Instantiate(shieldTemplate, transform.position, Quaternion.identity);
			shield.transform.parent = transform;
		}
	}
	protected void stopShield(){
		if(shielding){
			shielding = false;
			Destroy(shield.gameObject);
		}
	}



	
}
