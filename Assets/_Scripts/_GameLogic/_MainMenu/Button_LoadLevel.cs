using UnityEngine;
using System.Collections;

public class Button_LoadLevel : ClickableButton {
	public string levelName = "LEVEL_NAME";
	public GameObject fadeOutTemplate;

	void OnMouseUp(){
		click ();
	}
	void OnMouseEnter(){
		hover ();
	}
	public override void click ()
	{
		base.click ();
		Instantiate (fadeOutTemplate);
		Invoke ("nextLevel", 1.5f);
	}
	private void nextLevel(){	
		Application.LoadLevel (levelName);
	}
}
