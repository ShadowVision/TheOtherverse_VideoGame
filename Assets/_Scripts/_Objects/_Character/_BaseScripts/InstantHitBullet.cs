using UnityEngine;
using System.Collections;

public class InstantHitBullet : Damager {
	public Vector2 direction;
	public float distance;
	// Use this for initialization
	void Start () {
		base.Start();
	}
	public void fire(){	
		base.Start();
		checkCollision();
		Destroy(this);
	}
	private void checkCollision(){
		RaycastHit2D[] hitObjects = Physics2D.RaycastAll(transform.position,direction,distance);
		if(hitObjects != null){
			foreach(RaycastHit2D hitInfo in hitObjects){
				AliveObject unit = hitInfo.collider.gameObject.GetComponent<AliveObject>();
				hitUnit(unit);
			}
		}
	}
}
