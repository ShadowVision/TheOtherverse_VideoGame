using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	private PlayerController[] players;
	private AssetManager assets;
	private GameObject playerHolder;

	public CameraFollow_MultiTarget levelCamera;
	public Camera effectsCamera;
	public AnimationSequence introSequence;
	public Transform[] spawnPoints;
	
	void Awake(){
		playerHolder = new GameObject ("Players");
		playerHolder.transform.parent = transform;
		playerHolder.transform.localPosition = Vector3.zero;

		introSequence = GetComponentInChildren<LevelIntroSequence> ();
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

		StartCoroutine ("SpawnEachPlayer");
	}
	private IEnumerator SpawnEachPlayer(){
		//begin the animation sequence for players spawning into map
		introSequence.startAnimation ();

		yield return new WaitForSeconds(.4f);

		assets = AssetManager.instance;
		Transform[] ts = getRandomSpawnPoints (2);
		players = new PlayerController[2]{
			assets.spawnPlayer(0, ts[0]),
			assets.spawnPlayer(1, ts[1])
		};

		Transform[] t = new Transform[players.Length];
		int i = 0;
		foreach(PlayerController player in players){
			//Setup player
			player.transform.parent = playerHolder.transform;
			player.currentLevel = (LevelController)this;
			player.input.idName = "Player" + (i+1);
			player.spawn();

			//set camera to focus on player
			levelCamera.targetTransforms = new Transform[1]{player.transform};

			//play intro animation
			introSequence.stepAnimation();
			introSequence.target = player.transform;

			t[i] = player.transform;
			i++;
			yield return new WaitForSeconds(3f);
		}
		levelCamera.targetTransforms = t;
		introSequence.stopAnimation ();
	}
	public Transform[] getRandomSpawnPoints(int number = 2){
		Transform[] transforms = new Transform[number];
		for (int i=0; i<number; i++) {
			bool redo = true;
			while(redo){
				transforms[i] = getRandomSpawnPoint();
				redo = false;
				for(int j=0; j<i; j++){
					if(transforms[i].transform.position == transforms[j].transform.position){
						redo = true;
						break;
					}
				}
			}
		}
		return transforms;
	}
	public Transform getRandomSpawnPoint(){
		return spawnPoints[Random.Range(0,spawnPoints.Length)];
	}
}
