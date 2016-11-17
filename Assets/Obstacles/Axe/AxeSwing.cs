using UnityEngine;
using System.Collections;

public class AxeSwing : MonoBehaviour {
	float timer = 0;
	int direction = 1;
	public float Duration = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime / Duration;
		if (timer >= 1) {
			timer = 0;
			direction = -direction;
		}
		float rotation = Mathfx.Hermite (-70, 70, timer);
		transform.rotation = Quaternion.Euler (rotation * direction, 0, 0);
	}
}
