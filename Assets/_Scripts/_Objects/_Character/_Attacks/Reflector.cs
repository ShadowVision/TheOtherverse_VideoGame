using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {
	private PlayerController player;

	public GameObject clashEffectTemplate;

	void Awake(){
		player = GetComponent<PlayerController> ();
	}
	public void unitTakeDamage (Vector4 knockbackPlusDamage){
		if(player.isAttacking){
			//Debug.Log ("CLASH. Damage: " + knockbackPlusDamage);
			Vector3 knockback = new Vector3(knockbackPlusDamage.x,knockbackPlusDamage.y,knockbackPlusDamage.z);
			player.knockback (knockback*10);
			Instantiate(clashEffectTemplate,transform.position,transform.rotation);
		}
	}
}
