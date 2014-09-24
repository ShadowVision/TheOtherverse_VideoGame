using UnityEngine;
using System.Collections;

public class Sword : Attack_AimedLinear {
	private float startTime;
	private float secondsCharged = 0;
	
	public float minDamage = 1;
	public float maxDamage = 2;
	public float chargeTimeInSeconds = 1;
	public float minKnockbackMod = .25f;

	private Vector3 permSavedKnockback;

	// Use this for initialization
	new void Start () {
		base.Start ();
		attackOnPress = false;
		permSavedKnockback = savedKnockback;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	public override void attackPress ()
	{
		base.attackPress ();
		startCharge ();
	}
	public void startCharge(){
		startTime = Time.time;
		player.anim.charge ();
		player.input.lockMovement = true;
		dir = player.currentDirection;
	}
	public override void attackRelease ()
	{
		player.input.lockMovement = false;

		float timeCharged = Mathf.Min(Time.time - startTime, chargeTimeInSeconds);
		float d = timeCharged / chargeTimeInSeconds;
		float damage = Mathf.Lerp (minDamage, maxDamage, d);
		damage = Mathf.Floor (damage);

		savedKnockback = Vector3.Lerp(permSavedKnockback*minKnockbackMod, permSavedKnockback,d);

		attack ();
		Damager dmg = lastSpawnedItem.GetComponent<Damager> ();
		if(dmg != null){
			dmg.damageAmount = damage;
		}
	}
}
