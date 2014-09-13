using UnityEngine;
using System.Collections;

public class HonorableDeathMatch : GameMode {
	public int numberOfKillsRequired = 10;

	public override void OnPlayerScoreKill (PlayerController player)
	{
		base.OnPlayerScoreKill (player);
		if(player.getNumberOfKills() >= numberOfKillsRequired){
			endGame();
		}
	}
}
