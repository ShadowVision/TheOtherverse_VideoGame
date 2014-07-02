using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {
	public Unit owner;
	public float damageAmount;
	public Vector3 knockbackAmount;
	public bool deathOnFirstTouch = false;

	private GameObject[] alreadyDamaged;
	private int alreadyDamagedIndex = 0;
	// Use this for initialization
	protected void Start () {
		alreadyDamaged = new GameObject[10];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col) {
		AliveObject unit = col.gameObject.GetComponent<AliveObject>();
		hitUnit(unit);
	}
	protected void hitUnit(AliveObject unit){
		if(unit != null && unit != owner && !alreadyHit(unit.gameObject)){
			unit.takeDamage(damageAmount,knockbackAmount);
			alreadyDamaged[alreadyDamagedIndex] = unit.gameObject;
			alreadyDamagedIndex++;
			if(deathOnFirstTouch){
				die();
			}
		}
	}
	private bool alreadyHit(GameObject tester){
		foreach(GameObject obj in alreadyDamaged){
			if(obj != null && obj == tester){
				return true;
			}
		}
		return false;
	}
	private void die(){
		Destroy(gameObject);
	}
}
