using UnityEngine;
using System.Collections;

public class Minion_Ball : AliveObject {
	public Sprite idleTexture;
	public Sprite activeTexture;
	public float airFriction;
	public float groundFriction;
	public float initialLaunchDelayInSeconds = -1;
	public float launchStrength;
	public float launchRandomRange;
	public SpriteRenderer sprite;

	private bool isActive = false;
	// Use this for initialization
	new void Start () {
		if(initialLaunchDelayInSeconds != -1){
			Invoke("launch", initialLaunchDelayInSeconds);
		}
	}
	public void launch(){
		float randomRange = launchRandomRange/2f;
		rigidbody2D.velocity = Vector3.up * launchStrength + new Vector3(Random.Range(-randomRange, randomRange),0,0);
	}
	// Update is called once per frame
	new void Update () {
		if(currentState == UnitState.AIR){
			rigidbody2D.velocity *= airFriction;
		}else{
			rigidbody2D.velocity *= groundFriction;
		}
		if(rigidbody2D.velocity.magnitude > .1f){
			setActive();
		}else{
			setIdle();
		}
	}
	new public void die(){

	}
	private void setActive(){
		if(!isActive){
			sprite.sprite = activeTexture;
			isActive = true;
		}
	}
	private void setIdle(){
		if(isActive){
			sprite.sprite = idleTexture;
			isActive = false;
		}
	}
	protected override void permaDie ()
	{
		base.permaDie ();
		launch();
	}
}
