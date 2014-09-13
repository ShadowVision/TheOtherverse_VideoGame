using UnityEngine;
using System.Collections;

public class PlayerJoin_Box : MonoBehaviour {
	[HideInInspector]
	public bool ready = false;
	
	public PlayerController playerTemplate;
	private PlayerController player;
	public Transform levelHolder;
	public PlayerJoin_Controller controller;

	public GameObject[] stages;
	public string idName = "Player1";
	
	private int currentState = 0;

	// Use this for initialization
	void Awake () {
		initState ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pressStart(){
		Debug.Log ("start pressed");
		currentState++;
		initState ();
	}
	private void initState(){
		ready = false;
		if (currentState > 2) {
			currentState = 1;
		}
		switch(currentState){
		case 0:
			//initial state
			Libonati.showAll(stages[0]);
			Libonati.hideAll(stages[1]);
			Libonati.hideAll(stages[2]);
			Libonati.hideAll(stages[3]);
			break;
		case 1:
			//Character selected
			if(player == null){
				//spawn character
				player = (PlayerController)Instantiate (playerTemplate);
				player.transform.parent = levelHolder;
				player.transform.localPosition = Vector3.zero;
				player.spawn();
				player.input.idName = idName;
			}
			
			Libonati.hideAll(stages[0]);
			Libonati.showAll(stages[1]);
			Libonati.showAll(stages[2]);
			Libonati.hideAll(stages[3]);
			break;
		case 2:
			//ready and locked in
			ready = true;
			Libonati.hideAll(stages[0]);
			Libonati.hideAll(stages[1]);
			Libonati.showAll(stages[2]);
			Libonati.showAll(stages[3]);
			controller.playerReady();
			break;
		}
	}
}
