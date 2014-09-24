using UnityEngine;
using System.Collections;

public class PlayerController : Unit {
	private MeshController mesh; 
	private bool attacking = false;
	private LinearAnimation linearAnim;
	private int groundLayerMask;


	public float shieldTimeInSeconds = .25f;
	public float shieldCooldownInSeconds = 4;
	private bool canShield = true;
	public bool isAttacking{
		get{
			return attacking;
		}
	}

	[HideInInspector]
	public InputController input;
	public GameObject clashEffectTemplate;

	//Dodgeing
	public int maxDodgesInAir = 2;
	private int numberOfDodgesDone = 0;
	public float dodgeTimeInSeconds=1;
	public float dodgeDistance=1;
	public float dodgeCooldownInSeconds = 1;
	private bool canDodge = true;
	
	//wall jumping
	public float wallSlideSpeed = .2f;
	private bool isOnWall = false;
	public float wallJumpHorizontalSpeed;

	public LevelController level;
	public GameMode game;

	public SpriteRenderer colorSprite;

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
		attacks [1].maxBullets = attacks [1].bullets = newStats.numberOfBulletsPerLife;
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
		if(canShield){
			startShield ();
		}
	}
	new protected void startShield(){
		if(!shielding){
			canShield = false;
			base.startShield();
			input.lockPlayerMovement(shieldTimeInSeconds);
			Invoke("stopShield",shieldTimeInSeconds);
			anim.setShield(true);
		}
	}
	new protected void stopShield(){
		base.stopShield();
		anim.setShield(false);
		Invoke ("resetShield", shieldCooldownInSeconds);
	}
	private void resetShield(){
		canShield = true;
	}

	public void startAttacking(string attackName = "NoName"){
		if(attackName == "Sword"){
			shielding = true;
		}
		attacking = true;
		anim.setAttack (true);
		input.lockMovement = true;
	}
	public void stopAttacking(string attackName = "NoName"){
		if(attackName == "Sword"){
			shielding = false;
		}
		attacking = false;
		anim.setAttack (false);
		input.lockMovement = false;
	}
	
	//dodgeing
	public void dodge(){
		if(!dodgeing && canDodge){
			if(currentState == UnitState.AIR){
				if(numberOfDodgesDone >= maxDodgesInAir){
					return;
				}
			}
			dodgeing = true;
			anim.setDodge(true);
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
			enterAir();
			numberOfDodgesDone++;
		}
	}
	private void endDodge(){
		dodgeing = false;
		rigidbody2D.isKinematic = false;
		input.lockMovement = false;
		anim.setDodge (false);
		canDodge = false;
		Invoke ("resetDodge", dodgeCooldownInSeconds);
	}
	private void resetDodge(){
		canDodge = true;
	}
	public override void scoreKill ()
	{
		base.scoreKill ();
		game.OnPlayerScoreKill ((PlayerController)this);
	}
	protected override void die ()
	{
		base.die ();
	}

	protected override void permaDie ()
	{
		base.permaDie ();
		game.OnPlayerLose ();
	}


	//WALL JUMPING
	//detect collisions
	void OnCollisionEnter2D(Collision2D coll){
		if(currentState == UnitState.AIR && LayerMask.LayerToName(coll.gameObject.layer) == "Ground"){
			hitWall (coll);
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if(currentState == UnitState.AIR && LayerMask.LayerToName(coll.gameObject.layer) == "Ground"){
			onWall(coll);
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		if(currentState == UnitState.AIR && LayerMask.LayerToName(coll.gameObject.layer) == "Ground"){
			leaveWall (coll);
		}
	}

	public void hitWall(Collision2D coll){
		Vector2 dir = coll.contacts[0].normal;
		if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y)){
			//to the left or right
			if(dir.x > 0){
				//wall on the left
				jumpDirModifier = new Vector3(wallJumpHorizontalSpeed,0,0);
			}else{
				//wall on the right
				jumpDirModifier = new Vector3(-wallJumpHorizontalSpeed,0,0);
			}
		}else{
			//above or below, don't trigger wall jump
			return;
		}

		isOnWall = true;
		endJump ();
		numJumpsDone = 0;
	}
	private void onWall(Collision2D coll){
		//rigidbody2D.AddForce(new Vector2(0,-wallSlideSpeed));
		move (new Vector2 (0, -wallSlideSpeed));
		endJump ();
	}
	private void leaveWall(Collision2D coll){
		if(isOnWall){
			isOnWall = false;
			jumpDirModifier = new Vector3(0,0,0);
		}
	}
	protected override void childJump (float strength)
	{
		if(isOnWall){
			input.lockPlayerMovement(.35f);
			Debug.Log("wall jumping: " + jumpDirModifier);
			move (jumpDirModifier);
		}
	}

	protected override void triggerRespawn ()
	{
		base.triggerRespawn ();
		input.lockMovement = true;
		anim.lose ();
	}
	protected override void respawn ()
	{
		input.lockMovement = false;
		mesh.releaseMesh ();
		base.respawn ();
	}

	protected override void hitGround ()
	{
		base.hitGround ();
		numberOfDodgesDone = 0;
	}
	public void unitTakeDamage (Damager dmg){
		if(isAttacking){
			Debug.Log ("CLASH");
			if(dmg.canKnockbackOnClash){
				knockback (dmg.knockbackAmount*10);
			}
			dmg.clash();
			Instantiate(clashEffectTemplate,transform.position,transform.rotation);
		}
	}

	public void setColor(Color newColor){
		colorSprite.color = newColor;
	}

	private void disableColliders(){
		Collider2D[] col = GetComponents<Collider2D>();
		foreach(Collider2D c in col){
			c.enabled = false;
		}
	}
}
