using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	private PlayerController[] players;
	private AssetManager assets;
	private GameObject playerHolder;

	public CameraFollow_MultiTarget levelCamera;
	public Transform[] spawnPoints;
	
	void Awake(){
		playerHolder = new GameObject ("Players");
		playerHolder.transform.parent = transform;
		playerHolder.transform.localPosition = Vector3.zero;
	}

	// Use this for initialization
	void Start () {
		assets = AssetManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void spawnPlayers(){
		//TODO spawn characters from however many players are playing

		assets = AssetManager.instance;
		players = new PlayerController[2]{
			assets.spawnPlayer(0, getRandomSpawnPoint()),
			assets.spawnPlayer(1, getRandomSpawnPoint())
		};

		Transform[] t = new Transform[players.Length];
		int i = 0;
		foreach(PlayerController player in players){
			player.transform.parent = playerHolder.transform;
			player.currentLevel = (LevelController)this;
			player.spawn();
			player.input.idName = "Player" + (i+1);

			t[i] = player.transform;
			i++;
		}
		levelCamera.targetTransforms = t;
	}

	public Transform getRandomSpawnPoint(){
		return spawnPoints[Random.Range(0,spawnPoints.Length)];
	}
}
