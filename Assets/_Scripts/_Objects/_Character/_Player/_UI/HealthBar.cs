using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private PlayerController player;

	public GameObject healthPointTemplate;
	private GameObject[] points;

	// Use this for initialization
	void Awake () {
		player = GetComponent<PlayerController> ();
	}
	void Start(){
		init ((int)player.health);
	}

	public void init(int numberOfLives){
		GameObject healthPoints = new GameObject ("HealthPoints");
		healthPoints.transform.parent = transform;
		healthPoints.transform.localPosition = new Vector3 (0, 0, 0);

		Vector3 newPosition = new Vector3(0,0,0);
		points = new GameObject[numberOfLives];
		for(int i=0; i<numberOfLives; i++){
			GameObject healthPoint = (GameObject)Instantiate(healthPointTemplate);
			healthPoint.transform.parent = healthPoints.transform;
			healthPoint.transform.localPosition = newPosition;
			newPosition.x += 1;
			points[i] = healthPoint;
		}

		healthPoints.transform.localPosition = new Vector3 (-numberOfLives/2, 2f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playerTakeDamage(float amount){
		int damage = (int)amount;
		for(int i=(int)player.health; i<player.health+damage; i++){
			Destroy(points[i].gameObject);
		}
	}
}
