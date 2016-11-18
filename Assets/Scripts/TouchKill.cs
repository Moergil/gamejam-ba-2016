using UnityEngine;
using System.Collections;

public class TouchKill : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			GameObject.FindObjectOfType<GameManager>().GameOver();
		}
	}
}
