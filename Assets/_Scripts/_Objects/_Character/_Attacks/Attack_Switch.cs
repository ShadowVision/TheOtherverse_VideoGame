using UnityEngine;
using System.Collections;

public class Attack_Switch : Attack {
	public Attack_SwitchController switchController; 
	public Attack[] switchAttacks = new Attack[2];
	// Use this for initialization
	void Start () {
		
	}
	public override void attack ()
	{
		base.attack ();
		switchAttacks[switchController.attackIndex].attack();
	}
}
