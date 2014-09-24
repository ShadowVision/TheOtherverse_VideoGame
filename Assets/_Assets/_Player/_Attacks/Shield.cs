using UnityEngine;
using System.Collections;

public class Shield : AliveObject {

	public void getHit(Damager dmg){
		dmg.clash ();
	}
}
