using UnityEngine;
using System.Collections;

public class Bullet : AliveObject {
	Damager dmg;

	new void Awake(){
		base.Awake ();
		dmg = GetComponent<Damager> ();
	}
	public void Update(){

	}

	public void unitTakeDamage(Damager dmg){
		Debug.Log ("Bullet taking damage");
		float x = dmg.vel.x;
		float y = dmg.vel.y;
		float yAbs = Mathf.Abs (y);
		float xAbs = Mathf.Abs (x);
		if(yAbs > xAbs){
			if(y >0){
				currentDirection = Direction.UP;
			}else{
				currentDirection = Direction.DOWN;
			}
		}else{
			if(x > 0){
				currentDirection = Direction.FORWARD;
			}else{
				currentDirection = Direction.FORWARD;
			}
		}
		Vector3 newVel = dmg.vel;
		switch(currentDirection){
		case Direction.FORWARD:
			newVel.x *= -1;
			break;
		case Direction.UP:
		case Direction.DOWN:
			newVel.y *= -1;
			break;
		}
		dmg.vel = newVel;
	}
}
