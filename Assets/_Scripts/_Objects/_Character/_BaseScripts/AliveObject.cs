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
	public float invulnerabilityAfterHitInSeconds = .5f;
	private bool invulnerable = false;
	//For shielding
	private bool shielding = false;
	public AliveObject shieldTemplate;
	private AliveObject shield;
	//For dodgeing
	private bool dodeging = false;
	
	
	public bool facingRight{
		get{
			return transform.localScale.x > 0;
		}
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
		Destroy(gameObject);	
	}
	virtual public void kill(){
		
	}
	//SHIELDING
	public void startShield(){
		if(!shielding){
			shielding = true;
			shield = (AliveObject)Instantiate(shieldTemplate, transform.position, Quaternion.identity);
			shield.transform.parent = transform;
		}
	}
	public void stopShield(){
		if(shielding){
			shielding = false;
			Destroy(shield.gameObject);
		}
	}
}
