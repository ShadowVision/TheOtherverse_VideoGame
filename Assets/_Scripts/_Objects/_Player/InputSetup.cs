using UnityEngine;
using System.Collections;

public class InputSetup : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		//Initialize keys
		setupDefaultKeys ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	key = new Dictionary<string,int> ();
	key.Add ("Start",0);
	key.Add ("Jump",1);
	key.Add ("Block",2);
	key.Add ("Dodge",3);
	key.Add ("Melee",4);
	key.Add ("Ranged",5);
	 */

	private void setupDefaultKeys(){
		string[,] defaultPlayerKeys = new string[,]{
			{
				Keys.A, Keys.Joy1Axis1Negative, Keys.D, Keys.Joy1Axis1Positive,
				Keys.W, Keys.Joy1Axis2Negative, Keys.S, Keys.Joy1Axis2Positive,
				Keys.Enter, 		Keys.Joystick1Button9,
				Keys.Space, 		Keys.Joystick1Button16,
				Keys.Semicolon,		Keys.Joystick1Button14,
				Keys.L, 			Keys.Joystick1Button13,
				Keys.J, 			Keys.Joystick1Button18,
				Keys.K, 			Keys.Joystick1Button17
			},
			{
				Keys.Joy2Axis1Negative, Keys.Joy2Axis4Negative, Keys.Joy2Axis1Positive, Keys.Joy2Axis4Positive, 
				Keys.Joy2Axis2Negative, Keys.Joy2Axis5Negative,Keys.Joy2Axis2Positive, Keys.Joy2Axis5Positive,
				Keys.Joystick2Button7, Keys.Joystick2Button9,
				Keys.Joystick2Button0, Keys.Joystick2Button16,
				Keys.Joystick2Button5, Keys.Joystick2Button14,
				Keys.Joystick2Button4, Keys.Joystick2Button13,
				Keys.Joystick2Button2, Keys.Joystick2Button18,
				Keys.Joystick2Button1, Keys.Joystick2Button17,
			},
			{
				Keys.Joy1Axis1Negative, Keys.Joy1Axis4Negative, Keys.Joy1Axis1Positive, Keys.Joy1Axis4Positive, 
				Keys.Joy1Axis2Negative, Keys.Joy1Axis5Negative,Keys.Joy1Axis2Positive, Keys.Joy1Axis5Positive,
				Keys.JoystickButton7, Keys.JoystickButton9,
				Keys.JoystickButton0, Keys.JoystickButton16,
				Keys.JoystickButton5, Keys.JoystickButton14,
				Keys.JoystickButton4, Keys.JoystickButton13,
				Keys.JoystickButton2, Keys.JoystickButton18,
				Keys.JoystickButton1, Keys.JoystickButton17,
			},
			{
				Keys.Joy1Axis1Negative, Keys.Joy1Axis4Negative, Keys.Joy1Axis1Positive, Keys.Joy1Axis4Positive, 
				Keys.Joy1Axis2Negative, Keys.Joy1Axis5Negative,Keys.Joy1Axis2Positive, Keys.Joy1Axis5Positive,
				Keys.JoystickButton7, Keys.JoystickButton9,
				Keys.JoystickButton0, Keys.JoystickButton16,
				Keys.JoystickButton5, Keys.JoystickButton14,
				Keys.JoystickButton4, Keys.JoystickButton13,
				Keys.JoystickButton2, Keys.JoystickButton18,
				Keys.JoystickButton1, Keys.JoystickButton17,
			},
		};
		string[,] dpk = defaultPlayerKeys;

		int numberOfPossiblePlayers = 4;
		for(int i=0; i<numberOfPossiblePlayers; i++){
			// this creates a new key called "Left" and binds A and the Left Arrow to the key
			cInput.SetKey(i+"Left", dpk[i,0],dpk[i,1]);
			cInput.SetKey(i+"Right",  dpk[i,2],dpk[i,3]);
			cInput.SetKey(i+"Up",  dpk[i,4],dpk[i,5]);
			cInput.SetKey(i+"Down",  dpk[i,6],dpk[i,7]);
			// uses "Left" and "Right" inputs previously defined with SetKey to create a new axis
			cInput.SetAxis(i+"HorizontalMovement", i+"Left", i+"Right");
			cInput.SetAxis(i+"VerticalMovement", i+"Down", i+"Up");

			
			cInput.SetKey(i+"Start",   dpk[i,8],dpk[i,9]);
			cInput.SetKey(i+"Jump",    dpk[i,10],dpk[i,11]);
			cInput.SetKey(i+"Block",   dpk[i,12],dpk[i,13]);
			cInput.SetKey(i+"Dodge",   dpk[i,14],dpk[i,15]);
			cInput.SetKey(i+"Melee",   dpk[i,16],dpk[i,17]);
			cInput.SetKey(i+"Ranged",  dpk[i,18],dpk[i,19]);
		}
	}
}
