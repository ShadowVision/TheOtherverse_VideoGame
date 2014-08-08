﻿using UnityEngine;
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
	private float maxHealth = 10;
	private HealthBar healthBar;
	public float invulnerabilityAfterHitInSeconds = .5f;
	private bool invulnerable = false;
	//For shielding
	protected bool shielding = false;
	public AliveObject shieldTemplate;
	protected AliveObject shield;
	//For dodgeing
	private bool dodeging = false;

	[HideInInspector]
	public LevelController currentLevel;

	public GameObject spawnEffect;
	
	public bool facingRight{
		get{
			return transform.localScale.x > 0;
		}
	}

	protected void Awake(){
		maxHealth = health;
		healthBar = GetComponent<HealthBar> ();
	}
	public virtual void spawn(){
		Instantiate (spawnEffect, transform.position, transform.rotation);
	}
	public void move(Vector2 strength){
		//rigidbody2D.AddForce(strength);
		Vector2 newVel = rigidbody2D.velocity;
		newVel += strength;
		rigidbody2D.velocity = newVel;
	}
	public void takeDamage(float amount, Vector3 knockBackAmount){
		if(canHit()){
			takeDamage(amount);
			knockback(knockBackAmount);
			SendMessage("playerTakeDamage",amount,SendMessageOptions.DontRequireReceiver);
		}
	}
	public void takeDamage(float amount){
		if(canHit()){
			if(!shielding){
				if(health != -1){
					health -= amount;
					if(health <= 0){
						die();	
					}
				}
			}else if(shield != null){
				shield.takeDamage(amount);
			}
			invulnerable = true;
			Invoke("resetInvulnerable", invulnerabilityAfterHitInSeconds);
		}
	}
	private void resetInvulnerable(){
		invulnerable = false;
	}
	public bool canHit(){
		return (!dodeging && !invulnerable);
	}
	public void knockback(Vector3 amount){
		if(!shielding){
			move(amount);
		}
	}
	public void die(){
		lives--;
		if (lives > 0) {
			respawn();
		}else{
			kill();
		}	
	}
	virtual public void respawn(){
		transform.position = currentLevel.getRandomSpawnPoint ().position;
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.Sleep ();
		health = maxHealth;
		healthBar.init ((int)health);
		spawn ();
	}
	virtual public void kill(){
		Destroy (gameObject);
	}

	public virtual void spawn(Transform spawnPosition){
		Instantiate (spawnEffect, transform.position, Quaternion.identity);
	}
}
