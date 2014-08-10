using UnityEngine;
using System.Collections;

public class Player : Unit {
	public InputManager input;
	public enum SnapPoints{
		HEAD,
		TORSO,
		WRIST_R,
		WRIST_L
	}
	public Transform headJoint;
	public Transform torsoJoint;
	private Item[] items;
	// Use this for initialization
	new void Start () {
		base.Start();
		items = new Item[4];
	}
	// Update is called once per frame
	new void Update () {
		base.Update();
	}
	
	public void addItem(Item newItem){
		Transform parent;
		int i=0;
		switch(newItem.info.snapPoint){
			case SnapPoints.HEAD:
				i = 0;
				parent = headJoint;
				break;
			case SnapPoints.TORSO:
				i = 1;
				parent = torsoJoint;
				break;
			default:
				i = 0;
				parent = headJoint;
				break;
		}
		if(items[i] !=null){
			Destroy(items[i].gameObject	);	
		}
		items[i] = (Item)Instantiate(newItem);
		items[i].transform.parent = parent;
		items[i].transform.localPosition = new Vector3(0,0,0);
		items[i].transform.localEulerAngles = new Vector3(90,-90,0);
	}
}
