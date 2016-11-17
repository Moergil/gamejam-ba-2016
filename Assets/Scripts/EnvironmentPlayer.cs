using UnityEngine;
using System.Collections;
using System;

public class EnvironmentPlayer : MonoBehaviour
{

	public string[] actionButtons;
	public int[] actionIds;

	public RunPathSpawner runPathSpawner;

	void Update()
	{
		for (int i = 0; i < actionButtons.Length; i++) {
			string actionButton = actionButtons[i];
			int actionId = actionIds[i];
			if (Input.GetButtonDown(actionButton)) {
				runPathSpawner.OnAction(actionId, true);
			} else if (Input.GetButtonUp(actionButton)) {
				runPathSpawner.OnAction(actionId, false);
			}
		}
		
	}

}
