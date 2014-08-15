using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	protected PlayerController player;
	public string attackName = "NoName";
	public bool attackOnPress = true;
	public float energyCost = 0;
	public float cooldownInSeconds = 1;
	public float playerLockTimeInSeconds = 1;
	public Vector3 selfknockback = new Vector3(0,0,0);
	public GameObject[] spawnItems;
	protected GameObject lastSpawnedItem;
	protected bool canAttack = true;
	public bool spawnAttached = true;
	public int friendlyId = -1;
	// Use this for initialization
	protected void Awake () {
		player = transform.parent.parent.gameObject.GetComponent<PlayerController>();
		if(player == null){
			player = transform.parent.parent.parent.gameObject.GetComponent<PlayerController>();
		}
	}
	protected void Start(){

	}
	
	// Update is called once per frame
	protected void Update () {
	
	}
	virtual public void attackPress(){
		if(attackOnPress){
			attack ();
		}
	}
	virtual public void attackRelease(){
		if(!attackOnPress){
			attack ();
		}
	}
	virtual public void attack(){
		if(canAttack){
			canAttack = false;
			Invoke("resetAttack", cooldownInSeconds);
			doSpawnItems();
			player.knockback(getSelfKnockback());
			lockPlayer();
		}
	}
	protected Vector3 getSelfKnockback(){
		Vector3 skb = selfknockback;
		if(!player.facingRight){
			skb.x *= -1;
		}
		return skb;
	}
	protected void lockPlayer(){
		if(player && playerLockTimeInSeconds > 0){
			player.startAttacking(attackName);
			Invoke("resetPlayerLock", playerLockTimeInSeconds);
		}
	}
	private void resetPlayerLock(){
		player.stopAttacking (attackName);
	}
	private void resetAttack(){
		canAttack = true;	
	}
	private void doSpawnItems(){
		for(int i=0; i<spawnItems.Length; i++){
			lastSpawnedItem = (GameObject)Instantiate(spawnItems[i]);
			lastSpawnedItem.transform.position += transform.position;
			if(spawnAttached){
				lastSpawnedItem.transform.parent = transform;
			}
			Damager damager = lastSpawnedItem.GetComponent<Damager>();
			if(damager != null){
				if(!player.facingRight){
					damager.knockbackAmount.x *= -1;
				}
				damager.owner = player;
				damager.updateDirection(player.facingRight);
			}
			Projectile newProjectile = lastSpawnedItem.GetComponent<Projectile>();
			if(newProjectile != null){
				newProjectile.friendlyId = friendlyId;	
				//newProjectile.owner = player;	
			}
		}
	}
}
