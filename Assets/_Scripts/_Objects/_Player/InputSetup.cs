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
				Keys.Joy3Axis1Negative, Keys.Joy3Axis4Negative, Keys.Joy3Axis1Positive, Keys.Joy3Axis4Positive, 
				Keys.Joy3Axis2Negative, Keys.Joy3Axis5Negative,Keys.Joy3Axis2Positive, Keys.Joy3Axis5Positive,
				Keys.Joystick3Button7, Keys.Joystick3Button9,
				Keys.Joystick3Button0, Keys.Joystick3Button16,
				Keys.Joystick3Button5, Keys.Joystick3Button14,
				Keys.Joystick3Button4, Keys.Joystick3Button13,
				Keys.Joystick3Button2, Keys.Joystick3Button18,
				Keys.Joystick3Button1, Keys.Joystick3Button17,
			},
			{
				Keys.Joy4Axis1Negative, Keys.Joy4Axis4Negative, Keys.Joy4Axis1Positive, Keys.Joy4Axis4Positive, 
				Keys.Joy4Axis2Negative, Keys.Joy4Axis5Negative,Keys.Joy4Axis2Positive, Keys.Joy4Axis5Positive,
				Keys.Joystick4Button7, Keys.Joystick4Button9,
				Keys.Joystick4Button0, Keys.Joystick4Button16,
				Keys.Joystick4Button5, Keys.Joystick4Button14,
				Keys.Joystick4Button4, Keys.Joystick4Button13,
				Keys.Joystick4Button2, Keys.Joystick4Button18,
				Keys.Joystick4Button1, Keys.Joystick4Button17,
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
			cInput.gravity = 1000000;
			cInput.sensitivity = 1000000;

		}
	}
}
