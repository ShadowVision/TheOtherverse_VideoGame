using UnityEngine;
using System.Collections;

public class Minion_Standard : Unit {
	private float lastPercentage = 0;
	private float percentage = 0;
	private float oddsOfChanging = .05f;
	
	// Use this for initialization
	new void Start () {
		base.Start();
		changeDir();
		moveSpeed = 20f;
		//InvokeRepeating("think",0, .3f);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		moveDir(percentage);	
	}
	
	private void think(){
		lastPercentage = percentage;
		if(Random.Range(0f,1f) < oddsOfChanging){
			changeDir();
		}
	}
	private void changeDir(){
		percentage = Random.Range(-1f,1f);
	}
}
