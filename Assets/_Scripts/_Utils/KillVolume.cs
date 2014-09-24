using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour {
	public Transform spawnPoint;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collision) {
		Unit unit = collision.collider.gameObject.GetComponent<Unit>();
		if (unit){
			unit.forceDie();
		}
	}
}
