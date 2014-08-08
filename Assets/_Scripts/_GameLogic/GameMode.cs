using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {
	private AssetManager assets;

	public bool debugMode = false;
	public LevelController level;

	// Use this for initialization
	void Start () {
		assets = AssetManager.instance;
		if(debugMode){

		}else{

		}
		level.spawnPlayers();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F5)){
			Application.LoadLevel(Application.loadedLevel);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
