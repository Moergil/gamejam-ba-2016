using UnityEngine;
using System.Collections;

public class ActionHint : MonoBehaviour
{
	public int actionId;
	private float gauge;

	void Update()
	{
		// flash stuff
	}

	public void SetActionId(int actionId)
	{
		this.actionId = actionId;
	}

	public void runFlash()
	{

	}

	public void setGauge(float gauge)
	{
		this.gauge = gauge;
	}

}
