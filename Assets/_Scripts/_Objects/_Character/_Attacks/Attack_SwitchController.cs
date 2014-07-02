using UnityEngine;
using System.Collections;

public class Attack_SwitchController : Attack {
	public int attackIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void attack ()
	{
		base.attack ();
		if(attackIndex>0){
			attackIndex = 0;
		}else{
			attackIndex = 1;	
		}
	}
}
