using UnityEngine;
using System.Collections;

public class STATS : MonoBehaviour {
	public enum Character{
		EZREAL,
		UNICORN,
		SPARTAN,
		NONE
	}
	public enum Level{
		TEST
	}
	public enum controllerType{
		XBOX,
		MOUSE
	}
	static public CharacterInfo[] playerInfo;
	static public Level levelSelection;
	static private int nextId = -1;
	static public int getNextId(){
		nextId ++;
		return nextId;
	}
	static public void init(int numPlayers){
		playerInfo = new CharacterInfo[numPlayers];
		for(int i=0; i<playerInfo.Length; i++){
			playerInfo[i] = new CharacterInfo();
			if(i==1){
				playerInfo[1].controllerType = controllerType.XBOX;
			}
		}
	}
}
