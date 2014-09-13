using UnityEngine;
using System.Collections;

public class Attack_AimedLinear : Attack {
	private Vector3 savedKnockback;
	private	Damager dmg;

	private int maxBullets = 0;
	public int bullets = 5;

	new void Awake(){
		base.Awake ();
		maxBullets = bullets;
	}
	// Use this for initialization
	void Start () {
		base.Start ();
		savedKnockback = selfknockback;
	}
	public override void reset ()
	{
		base.reset ();
		bullets = maxBullets;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void attack ()
	{
		if(bullets >0 || bullets == -1){
			if(bullets != -1){
				bullets--;
			}
			dmg = null;
			switch(player.currentDirection){
			case Unit.Direction.FORWARD:
			case Unit.Direction.BACK:
			case Unit.Direction.NONE:
				selfknockback = savedKnockback;
				base.attack();
				break;
			case Unit.Direction.UP:
				selfknockback = new Vector3(savedKnockback.y,savedKnockback.x,savedKnockback.z);
				base.attack();
				lastSpawnedItem.transform.localEulerAngles = new Vector3(0,0,90);
				dmg = lastSpawnedItem.GetComponent<Damager>();
				if(dmg != null){
					dmg.knockbackAmount = new Vector3(dmg.knockbackAmount.y,dmg.knockbackAmount.x,dmg.knockbackAmount.z);
					dmg.vel = new Vector3(dmg.vel.y,dmg.vel.x,dmg.vel.z);
				}
				break;
			case Unit.Direction.DOWN:
				//if(player.currentState != AliveObject.UnitState.GROUND){
					selfknockback = new Vector3(savedKnockback.y,-savedKnockback.x,savedKnockback.z);
					base.attack ();
					lastSpawnedItem.transform.localEulerAngles = new Vector3(0,0,-90);
					dmg = lastSpawnedItem.GetComponent<Damager>();
					if(dmg != null){
						dmg.knockbackAmount = new Vector3(dmg.knockbackAmount.y,-dmg.knockbackAmount.x,dmg.knockbackAmount.z);
						dmg.vel = new Vector3(dmg.vel.y,-dmg.vel.x,dmg.vel.z);
					}
				//}
				break;
			}
		}
	}
}
