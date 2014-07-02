using UnityEngine;
using System.Collections;

public class SelfKill : MonoBehaviour {
	public float timeUntilDeathInSeconds = 1;
	// Use this for initialization
	void Start () {
		Invoke("die", timeUntilDeathInSeconds);
	}
	private void die(){
		Destroy (gameObject);
	}
}
