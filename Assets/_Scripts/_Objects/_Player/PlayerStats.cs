using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerStats {

	public int maxHealth = 3;
	public int maxLives = 3;
	public float gravity = .8f;
	public float groundSpeed = 10;
	public float airSpeed = 8;
	public float jumpStrength = 6;
	public float jumpStrengthOverTimeMod = 2;
	public int maxJumpTicks = 10;
	public int maxNumberOfJumps = 2;
}
