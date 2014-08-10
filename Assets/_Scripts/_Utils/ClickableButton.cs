using UnityEngine;
using System.Collections;

public class ClickableButton : MonoBehaviour {
	private AudioSource sound;
	public AudioClip clickSound;
	public AudioClip hoverSound;

	protected virtual void Awake(){
		sound = GetComponent<AudioSource> ();
	}
	public virtual void click(){
		if(clickSound != null)
			playSound (clickSound);
	}
	public virtual void hover(){
		if(hoverSound != null)
			playSound (hoverSound);
	}
	private void playSound(AudioClip clip){
		sound.clip = clip;
		sound.Play ();
	}
}
