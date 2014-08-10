using UnityEngine;
using System.Collections;

public class Attack_Kat_Snipe : Attack {
	public LaserSight laserSightTemplate;
	private LaserSight laserSight;
	public GameObject bulletTemplate;
	private GameObject bullet;

	public float timeUntilLoadInSeconds = .3f;
	private bool loaded = false;
	private bool cancelShot = true;

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if(laserSight != null){
			laserSight.transform.position = transform.position;
		}	
	}
	public override void attackPress ()
	{
		cancelShot = false;
		Invoke("loadGun", timeUntilLoadInSeconds);
		player.input.lockMovement = true;
	}
	public void loadGun(){
		if(!cancelShot){
			laserSight = (LaserSight)Instantiate(laserSightTemplate, Vector3.zero, Quaternion.identity);
			loaded = true;
		}
	}
	public override void attackRelease ()
	{
		if(loaded){
			base.attackRelease ();
			Destroy(laserSight.gameObject);
			bullet = (GameObject)Instantiate(bulletTemplate, transform.position, Quaternion.identity);
			bullet.transform.LookAt(Input.mousePosition);
			InstantHitBullet hit = bullet.GetComponent<InstantHitBullet>();
			if(!player.facingRight){
				hit.knockbackAmount.x *= -1;
			}
			hit.owner = player;
			hit.direction = (Input.mousePosition - transform.position).normalized;
			hit.distance = 100;
			hit.fire();
		}
		loaded = false;
		cancelShot = true;
		player.input.lockMovement = false;
	}
}
