using UnityEngine;
using System.Collections;

public class MeshController : MonoBehaviour {
	public GameObject meshHolder;
	public GameObject debugMeshTemplate;
	[HideInInspector]
	public PlayerController player;

	// Use this for initialization
	void Awake () {
		player = (PlayerController)GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//TODO load mesh from saved player slot
	public void loadMesh(){
		if(debugMeshTemplate != null){
			Debug.Log("Loading Mesh");
			GameObject newMesh = (GameObject)Instantiate(debugMeshTemplate);
			newMesh.transform.parent = meshHolder.transform;
			newMesh.transform.localPosition = Vector3.zero;
			player.anim = newMesh.GetComponentInChildren<AnimationController>();
		}
	}
}
