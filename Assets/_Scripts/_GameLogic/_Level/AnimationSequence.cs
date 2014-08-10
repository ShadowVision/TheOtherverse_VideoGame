using UnityEngine;
using System.Collections;

public interface AnimationSequence {
	void startAnimation ();
	void stopAnimation ();
	void stepAnimation();
	Transform target{
		get;
		set;
	}
}
