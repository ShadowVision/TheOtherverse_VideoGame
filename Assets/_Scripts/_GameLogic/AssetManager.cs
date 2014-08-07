using UnityEngine;
using System.Collections;

public class AssetManager : MonoBehaviour {
	public static AssetManager instance; 
	public PlayerController playerTemplate;
		
	void Awake(){
		AssetManager.instance = (AssetManager)this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public PlayerController spawnPlayer(int id, Transform spawnPoint){
		PlayerController newPlayer = (PlayerController)Instantiate (playerTemplate);
		newPlayer.transform.parent = spawnPoint;
		newPlayer.transform.localPosition = Vector3.zero;
		newPlayer.transform.localRotation = Quaternion.identity;

		return newPlayer;
	}
}
