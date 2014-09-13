using UnityEngine;
using System.Collections;

public class LinearAnimation : MonoBehaviour {
	public enum AnimationType
		{
			LINEAR,
			EASEINOUT
		}
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float startTime;
	private float durationInSeconds;
	private bool animating = false;
	
	public Transform target;
	public AnimationType animationType = AnimationType.EASEINOUT;

	public delegate void OnFinishFunc();
	public OnFinishFunc OnFinish;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animate ();
	}
	private void animate(){
		if(animating){
			float delta = (Time.time - startTime) / durationInSeconds;
			if(animationType == AnimationType.EASEINOUT){
				delta = Mathf.SmoothStep(0,1f,delta);
			}
			Vector3 newPosition = Vector3.Lerp (startPosition, endPosition, delta);
			target.localPosition = newPosition;

			if(delta >= 1){
				animating = false;

				//finished animation
				if(OnFinish != null){
					OnFinish();
				}
			}
		}
	}
	public void animateTo(Vector3 startPosition, Vector3 endPosition, float durationInSeconds, float delayBeforeStartInSeconds){
		this.startPosition = startPosition;
		this.endPosition = endPosition;
		this.durationInSeconds = durationInSeconds;
		Invoke ("delayedAnimation", delayBeforeStartInSeconds);
	}
	private void delayedAnimation(){
		animateTo (startPosition, endPosition, durationInSeconds);
	}
	public void animateTo(Vector3 startPosition, Vector3 endPosition, float durationInSeconds){
		CancelInvoke ();
		this.startPosition = startPosition;
		this.endPosition = endPosition;
		this.durationInSeconds = durationInSeconds;
		this.startTime = Time.time;

		this.animating = true;
	}
}
