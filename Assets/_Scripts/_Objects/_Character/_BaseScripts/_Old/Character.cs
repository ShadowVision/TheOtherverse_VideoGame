using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour{
	public float jumpStrength = 400;
	public int jumpTicks = 40;
	public float moveSpeed = 10;
	public float maxMoveSpeed = 10;
	public float airSpeed = 10;
	public float health = 10;
	private Attack[] attacks;
	public Attack xAttack;
	public Attack yAttack;
	public Attack bAttack;
	public Attack x2Attack;
	public Attack y2Attack;
	public Attack b2Attack;
	public Attack grabAttack;
	public Attack xAirOverrideAttack;
	public Attack yAirOverrideAttack;
	public Attack bAirOverrideAttack;
	public Attack x2AirOverrideAttack;
	public Attack y2AirOverrideAttack;
	public Attack b2AirOverrideAttack;
	public Attack grabAirOverrideAttack;
	
	private Player player;
	// Use this for initialization
	void Start () {
		
	}
	public void setPlayer(Player newPlayer){
		player = newPlayer;
		setAttacks();
		//player.setStats(jumpStrength,jumpTicks, moveSpeed,maxMoveSpeed,airSpeed,health,attacks);
	}
	private void setAttacks(){
		attacks = new Attack[14];
		attacks[0] = xAttack;
		attacks[1] = yAttack;
		attacks[2] = bAttack;
		attacks[3] = x2Attack;
		attacks[4] = y2Attack;
		attacks[5] = b2Attack;
		attacks[6] = grabAttack;
		if(xAirOverrideAttack != null){
			attacks[7] = xAirOverrideAttack;
		}else{
			attacks[7] = xAttack;
		}
		if(yAirOverrideAttack != null){
			attacks[8] = yAirOverrideAttack;
		}else{
			attacks[8] = yAttack;
		}
		if(bAirOverrideAttack != null){
			attacks[9] = bAirOverrideAttack;
		}else{
			attacks[9] = bAttack;
		}
		if(x2AirOverrideAttack != null){
			attacks[10] = x2AirOverrideAttack;
		}else{
			attacks[10] = x2Attack;
		}
		if(y2AirOverrideAttack != null){
			attacks[11] = y2AirOverrideAttack;
		}else{
			attacks[11] = y2Attack;
		}
		if(b2AirOverrideAttack != null){
			attacks[12] = b2AirOverrideAttack;
		}else{
			attacks[12] = b2Attack;
		}
		if(grabAirOverrideAttack != null){
			attacks[13] = grabAirOverrideAttack;
		}else{
			attacks[13] = grabAttack;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
