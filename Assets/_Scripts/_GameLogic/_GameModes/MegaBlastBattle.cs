using UnityEngine;
using System.Collections;

public class MegaBlastBattle : GameMode {
	
	public override void OnPlayerLose ()
	{
		base.OnPlayerLose ();
		//TODO: check to see if there is only one player left
		endGame ();
	}
}
