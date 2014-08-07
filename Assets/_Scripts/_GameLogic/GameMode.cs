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
			level.spawnPlayers();
		}else{
			//TODO spawn from selected characters in previous menu
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
