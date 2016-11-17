﻿using UnityEngine;
using System.Collections;
using System;

public class LevelSegment : MonoBehaviour
{
	public enum ActionType
	{
		Tap,
		Hold,
		TapRepeat
	}

	public int segmentLength = 1;

	public bool interactive;

	public float holdLengthMillis;
	public float tapRepeatTarget;

	public ActionType actionType;

	private ActionHint actionHint;

	public float tapRepeatAdd;
	public float tapRepeatDrainPerSec;
	public float tapRepeatGauge;

	private float holdStart = float.NaN;

	public bool actionFinished;

	public void SetupActionHint(int actionId)
	{
		actionHint.gameObject.SetActive(true);
		actionHint.SetActionId(actionId);
	}

	void Update()
	{
		if (!actionFinished) {
			if (actionType == ActionType.Hold && !float.IsNaN(holdStart)) {
				if (Time.realtimeSinceStartup - holdStart > holdLengthMillis) {
					actionFinished = true;
				}
			}

			if (actionType == ActionType.Tap) {
				if (tapRepeatGauge >= tapRepeatTarget) {
					actionFinished = true;
				} else {
					tapRepeatGauge -= tapRepeatDrainPerSec + Time.deltaTime;
				}
			}
		}
	}

	public void OnAction(bool positive)
	{
		if (actionFinished) {
			throw new InvalidOperationException("Action already finished.");
		}

		switch (actionType) {
		case ActionType.Tap:
			actionFinished = true;
			break;
		case ActionType.Hold:
			if (positive) {
				holdStart = Time.realtimeSinceStartup;
			} else {
				holdStart = float.NaN;
			}
			break;
		case ActionType.TapRepeat:
			tapRepeatGauge += tapRepeatAdd;
			break;
		}
	}

	public bool IsActionFinished()
	{
		return actionFinished;
	}

}
