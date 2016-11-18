using UnityEngine;
using System.Collections;

public class AxeSwing : MonoBehaviour
{
	float timer = 0;
	int direction = 1;
	public float Duration = 2;

	public float slowDownModifier;

	void Update()
	{
		timer += Time.deltaTime / Duration;
		if (timer >= 1) {
			timer = 0;
			direction = -direction;
		}
		float rotation = Mathfx.Hermite(-30, 30, timer);
		transform.rotation = Quaternion.Euler(rotation * direction, 0, 0);
	}

	public void SlowDownAxe(float completeness)
	{
		Duration *= slowDownModifier;
	}
}
