using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	private Animator anim;
	public enum Animations{
		INTRO,
		JUMP,
		ATTACK
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playAnimation(Animations animationType){
		switch(animationType){
		case Animations.INTRO:
			
			break;
		case Animations.ATTACK:
			anim.SetBool("Attack", true);
			break;
		case Animations.JUMP:
			anim.SetBool("Jump", true);
			break;
		}

	}
	public void setMoveSpeed(float newSpeed){
		if(anim!=null){
			anim.SetFloat("Vel",newSpeed);
		}
	}
	public void setAir(bool inAir){
		anim.SetBool ("InAir", inAir);
		if(!inAir){	
			anim.SetBool("Jump", false);
		}
	}
}
