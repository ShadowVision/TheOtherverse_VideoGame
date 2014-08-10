using UnityEngine;
using System.Collections;

public class MainMenu_SlideInAnimation : MonoBehaviour , AnimationSequence {
	public float delayBetweenSlides = .1f;
	public GameObject[] slideElements;

	public Transform target{
		get{return null;}
		set{}
	}
	private int step = 0;
	public void startAnimation(){
		for(int i=0; i<slideElements.Length; i++){
			Invoke("stepAnimation", delayBetweenSlides * i);
		}
	}
	public void stopAnimation(){

	}
	public void stepAnimation(){
		slideElements[step].animation.Play();
				step++;
	}

}
