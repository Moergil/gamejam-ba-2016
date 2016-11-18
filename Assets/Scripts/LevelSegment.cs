using UnityEngine;
using System.Collections;
using ByteSheep.Events;
using System.Collections.Generic;

public class LevelSegment : MonoBehaviour
{
	[System.Serializable]
	public class LevelSegmentActionEvent : AdvancedEvent<object>
	{

	}

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

	public ActionHint actionHint;

	public float tapRepeatAdd;
	public float tapRepeatDrainPerSec;
	public float tapRepeatGauge;

	private float holdStart = float.NaN;

	public bool actionFinished;

	public LevelSegmentActionEvent actionEvent;

	public void SetupActionHint(int actionId)
	{
		actionHint.gameObject.SetActive(true);
		actionHint.SetActionId(actionId);
	}

	void Start()
	{
		// Najdi mince
		foreach (Transform child in transform) {
			if (child.gameObject.tag == "Minca") {
				GameObject tmp = Instantiate(GameManager.Instance.MincaPrefab, Vector3.zero, Quaternion.identity, child) as GameObject;
				tmp.transform.localPosition = new Vector3(Random.Range(-.5f, .5f), 0.4f, Random.Range(-.5f, .5f));
			}
		}
	}

	void Update()
	{
		if (!actionFinished) {
			checkActionProgress();
		}
	}

	private void checkActionProgress()
	{
		bool actionJustFinished = false;
		if (!actionFinished) {
			if (actionType == ActionType.Hold && !float.IsNaN(holdStart)) {
				if (Time.realtimeSinceStartup - holdStart > holdLengthMillis) {
					actionJustFinished = true;
				}
			}

			if (actionType == ActionType.TapRepeat) {
				if (tapRepeatGauge >= tapRepeatTarget) {
					actionJustFinished = true;
				} else {
					tapRepeatGauge -= tapRepeatDrainPerSec + Time.deltaTime;
				}
			}
		}

		if (actionJustFinished) {
			actionEvent.Invoke(null);
			actionFinished = true;
		}
	}

	public void OnAction(bool positive)
	{
		if (actionFinished) {
			throw new System.InvalidOperationException("Action already finished.");
		}

		switch (actionType) {
		case ActionType.Tap:
			if (positive) {
				actionEvent.Invoke(null);
				actionFinished = true;
			}
			break;
		case ActionType.Hold:
			if (positive) {
				holdStart = Time.realtimeSinceStartup;
			} else {
				holdStart = float.NaN;
			}
			break;
		case ActionType.TapRepeat:
			if (positive) {
				tapRepeatGauge += tapRepeatAdd;
			}
			break;
		}
	}

	public bool IsActionFinished()
	{
		return actionFinished;
	}

}
