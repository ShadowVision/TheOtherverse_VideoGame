using UnityEngine;
using System.Collections;

public class AliveObject : MonoBehaviour {
	public enum Direction{
		UP,DOWN,FORWARD,BACK,NONE	
	}
	public enum UnitState{
		GROUND,
		AIR,
		STUN
	}
	public UnitState currentState = UnitState.AIR;
	public Direction currentDirection = Direction.NONE;

	//Health
	public int lives = -1;
	public float health = 10;
	protected float maxHealth = 10;
	public float respawnDelayInSeconds = 3;
	private HealthBar healthBar;
	public float invulnerabilityAfterHitInSeconds = .5f;
	private bool invulnerable = false;
	//For shielding
	protected bool shielding = false;
	public Shield shieldTemplate;
	protected Shield shield;
	//For dodgeing
	protected bool dodgeing = false;

	[HideInInspector]
	public LevelController currentLevel;

	public GameObject spawnEffect;
	

	private int numberOfKills = 0;
	[HideInInspector]
	public Damager lastDamager;
	private bool isDead = false;

	public bool dead{
		get{
			return isDead;
		}
	}
	public bool facingRight{
		get{
			return transform.localScale.x > 0;
		}
	}

	protected void Awake(){
		maxHealth = health;
		healthBar = GetComponent<HealthBar> ();
	}
	protected void Start(){
		currentLevel = LevelController.instance;
	}
	public virtual void spawn(){
		rigidbody2D.WakeUp ();
		Instantiate (spawnEffect, transform.position, transform.rotation);
		isDead = false;
	}
	public void move(Vector2 strength){
		if(rigidbody2D != null){
			//rigidbody2D.AddForce(strength);
			Vector2 newVel = rigidbody2D.velocity;
			newVel += strength;
			rigidbody2D.velocity = newVel;
		}
	}
	
	public virtual void scoreKill(){
		numberOfKills++;
	}
	
	public int getNumberOfKills(){
		return numberOfKills;
	}
	public void takeDamage(Damager dmg, float amount, Vector3 knockBackAmount){
		if(canHit()){
			if(takeDamage(dmg)){
				knockback(knockBackAmount);
				lastDamager = dmg;
			}else{
				amount = 0;
			}
			SendMessage("unitTakeDamage",dmg,SendMessageOptions.DontRequireReceiver);
		}
	}
	//returns true if it actually does damage
	public bool takeDamage(Damager dmg){
		bool tookDamage = false;
		if(canHit()){
			if(!shielding){
				if(health != -1){
					health -= dmg.damageAmount;
					if(health <= 0){
						die();	
					}
					tookDamage = true;
				}
			}else if(shield != null){
				tookDamage = false;
				shield.getHit(dmg);
			}
			invulnerable = true;
			Invoke("resetInvulnerable", invulnerabilityAfterHitInSeconds);
		}
		return tookDamage;
	}
	private void resetInvulnerable(){
		invulnerable = false;
	}
	public bool canHit(){
		return (!dodgeing && !invulnerable && !isDead);
	}
	public void knockback(Vector3 amount){
		if(!shielding){
			move(amount);
		}
	}
	protected virtual void die(){
		lives--;
		isDead = true;
		if(lastDamager != null){
			lastDamager.owner.owner.scoreKill();
		}
		if (lives > 0) {
			triggerRespawn();
		}else{
			permaDie();
		}	
	}
	virtual protected void triggerRespawn(){
		Invoke ("respawn", respawnDelayInSeconds);
	}
	virtual protected void respawn(){
		if(currentLevel != null){
			transform.position = currentLevel.getRandomSpawnPoint ().position;
		}
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.Sleep ();
		health = maxHealth;
		healthBar.init ((int)health);
		spawn ();
	}
	virtual protected void permaDie(){
	}

	public virtual void spawn(Transform spawnPosition){
		Instantiate (spawnEffect, transform.position, Quaternion.identity);
	}

	public void forceDie(){
		if(!isDead){
			die ();
		}
	}
}
