using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private PlayerController player;

	public GameObject healthPointTemplate;
	private GameObject[] points;
	private GameObject healthPointsHolder;

	private bool showingHealthPoints = false;

	public float delayBeforeHideInSeconds = 3f;

	// Use this for initialization
	void Awake () {
		player = GetComponent<PlayerController> ();

		//create a holder for the health points
		healthPointsHolder = new GameObject ("HealthPoints");
		healthPointsHolder.transform.parent = transform;
		healthPointsHolder.transform.localPosition = new Vector3 (0, 0, 0);


	}
	void Start(){
		init ((int)player.health);
	}

	public void init(int numberOfHitPoints){
		//delete existing health points
		if(points != null){
			foreach(GameObject point in points){
				if(point != null){
					Destroy(point);
				}
			}
		}
		//create all of the health points needed
		Vector3 newPosition = new Vector3(0,0,0);
		points = new GameObject[numberOfHitPoints];
		for(int i=0; i<numberOfHitPoints; i++){
			GameObject healthPoint = (GameObject)Instantiate(healthPointTemplate);
			healthPoint.transform.parent = healthPointsHolder.transform;
			healthPoint.transform.localPosition = newPosition;
			newPosition.x += 1;
			points[i] = healthPoint;
		}

		//center health points
		healthPointsHolder.transform.localPosition = new Vector3 (-numberOfHitPoints/2, 2f, 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void unitTakeDamage(Damager dmg){
		showHealthPoints ();
		for(int i=(int)player.health; i<player.health+dmg.damageAmount; i++){
			if(i >= 0 && i < points.Length)
				points[i].animation.Play("Sprite_FadeOut");
		}
		Invoke ("hideHealthPoints", delayBeforeHideInSeconds);
	}
	
	private void showHealthPoints(){
		CancelInvoke ("showHealthPoints");
		if(!showingHealthPoints){
			for(int i=0; i<player.health; i++){
				points[i].animation.Play ("Sprite_FadeIn");
			}
			showingHealthPoints = true;
		}
	}
	private void hideHealthPoints(){
		CancelInvoke ("hideHealthPoints");
		if(showingHealthPoints){
			for(int i=0; i<player.health; i++){
				points[i].animation.Play ("Sprite_FadeOut");
			}
			showingHealthPoints = false;
		}
	}
}
