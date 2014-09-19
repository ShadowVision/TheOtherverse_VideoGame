using UnityEngine;
using System.Collections;

public class Sword : Attack_AimedLinear {
	private float startTime;
	private float secondsCharged = 0;
	
	public float minDamage = 1;
	public float maxDamage = 2;
	public float chargeTimeInSeconds = 1;

	// Use this for initialization
	void Start () {
		attackOnPress = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void attackPress ()
	{
		base.attackPress ();
		startCharge ();
	}
	public void startCharge(){
		startTime = Time.time;
	}
	public override void attackRelease ()
	{
		attack ();
		float timeCharged = Mathf.Min(Time.time - startTime, chargeTimeInSeconds);
		float damage = Mathf.Lerp (minDamage, maxDamage, timeCharged / chargeTimeInSeconds);
		damage = Mathf.Floor (damage);
		lastSpawnedItem.GetComponent<Damager> ().damageAmount = damage;
	}
}
