using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public ItemInfo info;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void attachTo(Transform newParent){
		transform.parent = newParent;	
	}
}
