using UnityEngine;
using System.Collections;

public class Attack_Burst : Attack {
	public int numberOfAttacks = 1;
	public float delayBetweenShotsInSeconds = .1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override  void attack ()
	{
		runAttack();
		
	}
	IEnumerator runAttack(){
		for(int i=0; i<numberOfAttacks; i++){
			base.attack ();
			yield return new WaitForSeconds(delayBetweenShotsInSeconds);
		}
	}
}
