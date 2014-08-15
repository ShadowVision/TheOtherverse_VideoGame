using UnityEngine;
using System.Collections;

public class LevelIntroSequence : MonoBehaviour, AnimationSequence {
	public Animation lightboxAnim;
	public Camera effectsCamera;
	public Transform target{
		get{
			return _target;
		}
		set{
			_target = value;
			effectsCamera.transform.parent = _target;
			effectsCamera.transform.localPosition = new Vector3(-10,0,-10);
		}
	}
	private Transform _target;
	private int step = 0;

	public void startAnimation(){
		lightboxAnim.Play("LightboxAnimation");
	}
	public void stopAnimation(){
		lightboxAnim.Play ("LightboxAnimationOff");
		effectsCamera.enabled = false;
	}
	public void stepAnimation(){
		playFocusStrip ();
		step++;
	}
	private void playFocusStrip(){
		lightboxAnim.Stop ();
		lightboxAnim.Play("FocusStrip");
		effectsCamera.animation.Stop ();
		effectsCamera.animation.Play ("FocusObject");
	}
}
