using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {
	private PlayerController player;
	public Attack owner;
	public float damageAmount;
	public Vector3 knockbackAmount;
	public bool deathOnFirstTouch = false;
	public bool canReflect = false;
	public bool canKnockbackOnClash = true;
	[HideInInspector]
	public Vector3 vel = Vector3.zero;

	private GameObject[] alreadyDamaged;
	private int alreadyDamagedIndex = 0;

	private int layerMask = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Ground_Ghost"));
	// Use this for initialization
	protected void Start () {
		alreadyDamaged = new GameObject[10];
		player = owner.owner.gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position,vel.normalized,vel.magnitude,layerMask);
		if(hit.collider != null){
			GameObject unit = hit.collider.gameObject;
			if(unit != null && unit != player && !alreadyHit(unit.gameObject)){
				//Debug.Log("Hit: " + unit.name);
				string hitLayer = LayerMask.LayerToName(unit.gameObject.layer);
				switch(hitLayer){
					case "Ground":
						if(deathOnFirstTouch){
							die();
						}
					break;
				}
			}
		}
		transform.position += vel;
	}
	void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log ("Hit: " + col.gameObject.name);
		AliveObject unit = col.gameObject.GetComponent<AliveObject>();
		hitUnit(unit);
	}
	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log ("Hit2: " + col.gameObject.name);
		if(deathOnFirstTouch){
			die ();
		}
	}

	protected void hitUnit(AliveObject unit){
		if(unit != null && unit != player && !alreadyHit(unit.gameObject)){
			Debug.Log("Dealing Damage: " + damageAmount);
			unit.takeDamage((Damager)this,damageAmount,knockbackAmount);
			alreadyDamaged[alreadyDamagedIndex] = unit.gameObject;
			alreadyDamagedIndex++;
			if(deathOnFirstTouch){
				die ();
			}
		}
	}
	private bool alreadyHit(GameObject tester){
		foreach(GameObject obj in alreadyDamaged){
			if(obj != null && obj == tester){
				return true;
			}
		}
		return false;
	}
	private void die(){
		Destroy(gameObject);
	}

	public void updateDirection(bool facingRight){
		if(!facingRight){
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
			vel.x *= -1;
		}	
	}
	public virtual void clash(){
		if(canReflect){
			reflect();
		}
	}
	private void reflect(){
		Debug.Log (name + " reflecting");
		vel*=-1;
		knockbackAmount *= -1;
		player = null;
	}
}
