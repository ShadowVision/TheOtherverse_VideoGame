using UnityEngine;
using System.Collections;

public class MultiCharacterFollowCamera : MonoBehaviour {
	public Transform[] targets;
	public Vector3 offset = Vector3.zero;
	public float deltaMultiplier = .5f;
	private Vector3 targetPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	public void setTargets(Transform[] newTargets){
		targets = newTargets;
	}
	void FixedUpdate(){
		transform.position = Vector3.Lerp(transform.position, targetPosition, deltaMultiplier * Time.deltaTime);	
	}
	// Update is called once per frame
	void Update () {
		if(targets != null){
			Vector3 p1 = new Vector3(0,0,0);
			Vector3 p2 = new Vector3(0,0,0);
			foreach(Transform target in targets){
				p1.x = Mathf.Min(target.position.x, p1.x);
				p2.x = Mathf.Max(target.position.x, p2.x);	
				p1.y = Mathf.Min(target.position.y, p1.y);
				p2.y = Mathf.Max(target.position.y, p2.y);	
			}
			Vector3 distance = p2 - p1;
			distance.z = -distance.magnitude;
			Vector3 centerPosition = p1 + distance/2;
			targetPosition = new Vector3(centerPosition.x, centerPosition.y, distance.z) + (offset * distance.magnitude);
		}
	}
}
