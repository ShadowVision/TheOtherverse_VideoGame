using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {
	private AssetManager assets;

	public bool debugMode = false;
	public LevelController level;
	public PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		assets = AssetManager.instance;
		if(debugMode){

		}else{

		}
		level.gameMode = (GameMode)this;
		level.spawnPlayers();
		updatePlayerStats();
	}
	public void OnIntroDone(){

	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F5)){
			Application.LoadLevel(Application.loadedLevel);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		if(debugMode){
			updatePlayerStats();
		}
	}

	public void updatePlayerStats(){
		foreach(PlayerController player in level.currentPlayers){
			player.setStats(playerStats);
		}
	}
	public virtual void endGame(){
		Application.LoadLevel ("GameOverScreen");
	}
	
	//when a player scores a kill
	public virtual void OnPlayerScoreKill(PlayerController player){

	}
	//when a player runs out of lives
	public virtual void OnPlayerLose(){

	}

}
