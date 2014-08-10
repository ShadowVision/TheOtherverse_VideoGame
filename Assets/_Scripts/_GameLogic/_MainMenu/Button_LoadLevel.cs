using UnityEngine;
using System.Collections;

public class Button_LoadLevel : ClickableButton {
	public string levelName = "LEVEL_NAME";

	void OnMouseUp(){
		click ();
	}
	void OnMouseEnter(){
		hover ();
	}
	public override void click ()
	{
		base.click ();
		Application.LoadLevel (levelName);
	}
}
