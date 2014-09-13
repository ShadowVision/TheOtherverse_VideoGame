using UnityEngine;
using System.Collections;

public class PlayerController : Unit {
	private MeshController mesh; 
	private bool attacking = false;
	private LinearAnimation linearAnim;
	private int groundLayerMask;


	public float shieldTimeInSeconds = .25f;
	public bool isAttacking{
		get{
			return attacking;
		}
	}

	[HideInInspector]
	public InputController input;
	public float dodgeTimeInSeconds=1;
	public float dodgeDistance=1;

	public LevelController level;
	public GameMode game;

	public Vector3 middlePosition{
		get{
			return transform.position + new Vector3(0,.5f,0);
		}
	}

	//used by game modes to cusomize the play style
	public void setStats(PlayerStats newStats){
		rigidbody2D.gravityScale = newStats.gravity;
		lives = newStats.maxLives;
		maxHealth = newStats.maxHealth;
		moveSpeed = newStats.groundSpeed;
		airSpeed = newStats.airSpeed;
		maxNumJumps = newStats.maxNumberOfJumps;
		jumpStrength = newStats.jumpStrength;
		jumpStrengthOverTimeMod = newStats.jumpStrengthOverTimeMod;
		maxJumpTicks = newStats.maxJumpTicks;
	}


	// Use this for initialization
	void Start () {
		base.Start();
		linearAnim = gameObject.AddComponent<LinearAnimation> ();
		linearAnim.target = transform;
	}
	new protected void Awake(){
		base.Awake ();
		groundLayerMask = (1 << LayerMask.NameToLayer("Ground"));// | (1 << LayerMask.NameToLayer("Ignore Raycast"));
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

	public void shield(){
		startShield ();
	}
	new protected void startShield(){
		if(!shielding){
			base.startShield();
			input.lockMovement = true;
			Invoke ("stopShield", shieldTimeInSeconds);
		}
	}
	new protected void stopShield(){
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
	
	//dodgeing
	public void dodge(){
		if(!dodgeing){
			dodgeing = true;
			Vector3 dodgeDirection = input.dir;
			Vector3 endPosition = transform.position+dodgeDirection*dodgeDistance;
			//check if we are going to collid into a wall
			RaycastHit2D hit = Physics2D.Raycast(middlePosition,dodgeDirection,dodgeDistance,groundLayerMask);
			if(hit != null && hit.point != Vector2.zero){
				endPosition = new Vector3(hit.point.x,hit.point.y,0);
			}
			rigidbody2D.isKinematic = true;
			input.lockMovement = true;
			linearAnim.OnFinish = endDodge;
			linearAnim.animateTo (transform.position, endPosition, dodgeTimeInSeconds);
		}
	}
	private void endDodge(){
		dodgeing = false;
		rigidbody2D.isKinematic = false;
		input.lockMovement = false;
	}
	public override void scoreKill ()
	{
		base.scoreKill ();
		game.OnPlayerScoreKill ((PlayerController)this);
	}


	public override void kill ()
	{
		base.kill ();
		game.OnPlayerLose ();
	}
}
