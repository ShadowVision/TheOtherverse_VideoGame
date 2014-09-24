using UnityEngine;
using System.Collections;

public class Gun : Attack {
	float bulletSpeed = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void reset ()
	{
		base.reset ();
		bullets = maxBullets;
	}
	public override void attackPress ()
	{
		base.attackPress ();
	}
	public override void attackRelease ()
	{
		base.attackRelease ();
	}
	public override void attack ()
	{
		if(bullets > 0){
			bullets--;
			base.attack ();
			if(lastSpawnedItem != null){
				Vector3 vel = player.input.dir * bulletSpeed;
				if(vel.magnitude == 0){
					if(player.facingRight){
						vel = new Vector3(1,0,0);
					}else{
						vel = new Vector3(-1,0,0);
					}
				}
				lastSpawnedItem.gameObject.GetComponent<Damager> ().vel = vel;
			}
			lastSpawnedItem = null;
		}
	}
}
