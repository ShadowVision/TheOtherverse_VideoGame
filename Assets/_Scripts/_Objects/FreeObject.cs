using UnityEngine;
using System.Collections;

public class FreeObject : MonoBehaviour {
	public float velAmplify = 100;
	public Vector3 vel;
	public int friendlyId;
	public float speed = 1;
	// Use this for initialization
	protected void Start () {
	
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {
		rigidbody2D.AddForce(vel*Time.deltaTime * velAmplify);
	}
	protected void Update(){
		
	}
}
