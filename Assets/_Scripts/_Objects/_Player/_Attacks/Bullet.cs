using UnityEngine;
using System.Collections;

public class Bullet : AliveObject {

	public void unitTakeDamage(Vector4 knockbackPlusDamage){
		float x = rigidbody2D.velocity.x;
		float y = rigidbody2D.velocity.y;
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
		Vector3 newVel = rigidbody2D.velocity;
		switch(currentDirection){
		case Direction.FORWARD:
			newVel.x *= -1;
			break;
		case Direction.UP:
		case Direction.DOWN:
			newVel.y *= -1;
			break;
		}
		rigidbody2D.velocity = newVel;
	}
}
