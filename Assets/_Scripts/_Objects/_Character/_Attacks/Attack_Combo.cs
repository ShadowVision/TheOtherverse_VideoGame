using UnityEngine;
using System.Collections;

public class Attack_Combo : Attack {
	
	public Attack_ComboMove[] comboAttacks = new Attack_ComboMove[0];
	private  bool canHitNextCombo = true;
	private int currentComboIndex = 0;

	public override void attack ()
	{
		base.attack ();
		if(canAttack){
			runComboAttack();
		}
	}
	private void runComboAttack(){
		if(comboAttacks.Length != 0 && canHitNextCombo){
			Attack_ComboMove nextCombo = comboAttacks[currentComboIndex];
			nextCombo.attack();
			CancelInvoke("enableNextCombo");
			Invoke("enableNextCombo", nextCombo.timeUntilStartInSeconds);
			CancelInvoke("cancelCombo");
			Invoke("cancelCombo", nextCombo.timeUntilEndInSeconds); 
			currentComboIndex++;
			if(currentComboIndex >= comboAttacks.Length){
				currentComboIndex = 0;	
			}
		}
	}
	private void enableNextCombo(){
		canHitNextCombo = true;
	}
	private void cancelCombo(){
		canHitNextCombo = true;
		currentComboIndex = 0;
		
	}
}
