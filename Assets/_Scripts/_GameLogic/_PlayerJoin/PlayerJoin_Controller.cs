﻿using UnityEngine;
using System.Collections;

public class PlayerJoin_Controller : MonoBehaviour {
	public PlayerJoin_Box[] boxes;
	public GameObject readyEffect;
	public string nextLevelName = "Game";

	// Use this for initialization
	void Start () {
		slideCount = boxes.Length - 1;
		for(int i=0; i<boxes.Length; i++){
			Invoke("slideInBox", (i+1)*.25f);
		}
	}
	private int slideCount = 0;
	private void slideInBox(){
		boxes [slideCount].animation.Play ();
		slideCount--;
	}
	// Update is called once per frame
	void Update () {
		//check for player start buttons
		for(int i=0; i<boxes.Length; i++){
			if(cInput.GetButtonUp(i+"Start")){
				boxes[i].pressStart();
			}
		}
	}

	//each time a player locks in
	public void playerReady(){
		bool allReady = true;
		STATS.numberOfPlayers = 0;
		foreach(PlayerJoin_Box box in boxes){
			if(box.active){
				STATS.numberOfPlayers++;
				if(!box.ready){
					allReady = false;
					break;
				}
			}
		}

		if(STATS.numberOfPlayers > 1 && allReady){
			finish();
		}else{

		}
	}

	private void finish(){
		Instantiate(readyEffect);
		Invoke ("loadNextLevel", 2f);
	}
	private void loadNextLevel(){
		Application.LoadLevel (nextLevelName);
	}
}
