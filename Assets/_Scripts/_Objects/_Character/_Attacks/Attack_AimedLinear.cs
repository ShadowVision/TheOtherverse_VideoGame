using UnityEngine;
using System.Collections;

public class Attack_AimedLinear : Attack {
	protected Vector3 savedKnockback;
	private	Damager dmg;

	protected Unit.Direction dir = Unit.Direction.NONE;

	new void Awake(){
		base.Awake ();
		savedKnockback = selfknockback;
	}
	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	public override void reset ()
	{
		base.reset ();
		bullets = maxBullets;
	}

	public override void attack ()
	{
		if(bullets >0 || bullets == -1){
			if(bullets != -1){
				bullets--;
			}
			dmg = null;
			Unit.Direction newDir = dir;
			if(newDir == AliveObject.Direction.NONE){
				newDir = player.currentDirection;
			}
			switch(newDir){
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
