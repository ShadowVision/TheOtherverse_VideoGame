using UnityEngine;
using System.Collections;

public class Projectile : FreeObject {
	public PlayerController owner;
	public float damageAmount;
	public float knockbackAmount;
	public bool deathOnTouch = false;
	public bool lockDir = false;
	// Use this for initialization
	new protected void Start () {
		base.Start();
		vel = dir.normalized * speed;
		transform.LookAt(transform.position+vel);
	}
	private Vector3 dir{
		get{
			Vector3 angle = Vector3.zero;
/*			if(owner.characterInfo.controllerType ==STATS.controllerType.XBOX ){
				angle = owner.input.lastDirection;
			} else if(owner.characterInfo.controllerType==STATS.controllerType.MOUSE){
				Vector3 mousePoint = Libonati.getMousePoint();
				angle = mousePoint - transform.position;	
			}*/
			if(lockDir){
				
			}
			return angle;
		}
	}
	// Update is called once per frame
	new void Update () {
		base.Update();
		RaycastHit hitInfo;
		if(Physics.Raycast(transform.position,vel.normalized,out hitInfo,vel.magnitude)){
			damage (hitInfo.collider.gameObject);
		}
			
	}
	
	void OnCollisionEnter(Collision collision) {
		damage(collision.collider.gameObject);
	}
	private void damage(GameObject target){
		Unit hitUnit = (Unit) target.GetComponent("Unit");	
		if(hitUnit != null && hitUnit.canHit()){
			hitUnit.takeDamage(damageAmount);	
			hitUnit.knockback(vel * knockbackAmount);
		}
		
		if(deathOnTouch){
			Destroy (gameObject);	
		}
	}
}
