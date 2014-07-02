using UnityEngine;
using System.Collections;

public class Attack_Directional : Attack {
	public Attack[] directionalAttacks = new Attack[5];

	override public void attack(){
		directionalAttacks[(int) getDirection()].attack();
//		Debug.Log("Direction: " + (int) getDirection());
	}
	private Unit.Direction getDirection(){
		return player.currentDirection;
	}
}
