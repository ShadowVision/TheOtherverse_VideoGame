using UnityEngine;
using System.Collections;

public class LineAttachment : MonoBehaviour {
	public Transform endTransform;
	public GameObject mesh;
	public bool lockScale;
	public float lockScaleAmount = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		if(endTransform != null && mesh != null){
			mesh.transform.LookAt(endTransform.position);
			Vector3 newScale = mesh.transform.localScale;
			newScale.z = (endTransform.position - transform.position).magnitude;
			if(lockScale){
				newScale.z = lockScaleAmount;
			}
			mesh.transform.localScale = newScale;
		}
	}
}
