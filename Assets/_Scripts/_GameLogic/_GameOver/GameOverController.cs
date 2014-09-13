using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {
	public Animator winningPlayer;
	public Animator losingPlayer;

	// Use this for initialization
	void Start () {
		//instantiate players;
		winningPlayer.SetBool ("Win", true);
		losingPlayer.SetBool ("Lose", true);
	}

	// Update is called once per frame
	void Update () {
		//TODO handle all players and go off their custome input
		if(Input.GetButtonUp("Player1Start") || Input.GetButtonUp("Player2Start")){
			Application.LoadLevel("PlayerJoin");
		}
	}
}
