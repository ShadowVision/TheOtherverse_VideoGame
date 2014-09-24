using UnityEngine;
using System.Collections;

public class MeshController : MonoBehaviour {
	public GameObject meshHolder;
	public GameObject debugMeshTemplate;
	[HideInInspector]
	public PlayerController player;

	private GameObject meshObject;

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
			Libonati.destroyAllChildren(meshHolder);
			meshObject = (GameObject)Instantiate(debugMeshTemplate);
			meshObject.transform.parent = meshHolder.transform;
			meshObject.transform.localPosition = Vector3.zero;
			player.anim = meshObject.GetComponentInChildren<AnimationController>();
		}
	}
	public void releaseMesh(){
		meshHolder.transform.parent = null;
		meshHolder = new GameObject ("Mesh");
		meshHolder.transform.parent = transform;
		meshHolder.transform.localPosition = Vector3.zero;
	}













}
