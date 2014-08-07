using UnityEngine;
using System.Collections;

public class CameraFollow_MultiTarget : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the targetTransform can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the targetTransform can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	
	
	public Transform[] targetTransforms;		// Reference to the targetTransform's transform.
	private Vector3 targetPosition;
	
	void Awake ()
	{
		
	}
	
	
	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the targetTransform in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - targetPosition.x) > xMargin;
	}
	
	
	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the targetTransform in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - targetPosition.y) > yMargin;
	}
	
	
	void FixedUpdate ()
	{
		updateTargetPosition ();
		TracktargetTransform();
	}
	private void updateTargetPosition(){
		foreach(Transform t in targetTransforms){
			targetPosition += t.position;
		}
		targetPosition /= targetTransforms.Length;
	}
	
	void TracktargetTransform ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		// If the targetTransform has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the targetTransform's current x position.
			targetX = Mathf.Lerp(transform.position.x, targetPosition.x, xSmooth * Time.deltaTime);
		
		// If the targetTransform has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the targetTransform's current y position.
			targetY = Mathf.Lerp(transform.position.y, targetPosition.y, ySmooth * Time.deltaTime);
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}