using UnityEngine;
using System.Collections;

public class MincaRotation : MonoBehaviour {
	float rotationSpeed = 0;
	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range(80,120);
		transform.Rotate (Vector3.up, Random.Range(0,1024));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up, Time.deltaTime * rotationSpeed);
	}
}
