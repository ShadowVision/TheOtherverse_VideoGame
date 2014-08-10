using UnityEngine;
using System.Collections;

public class MainMenu_Controller : MonoBehaviour {
	public MainMenu_SlideInAnimation slideInAnimation;

	void Awake(){

	}
	// Use this for initialization
	void Start () {
		Invoke ("startAnimationIntro", 1);
	}
	private void startAnimationIntro(){
		slideInAnimation.startAnimation ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
