using UnityEngine;
using System.Collections;

public class ShootingStarController : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		particleSystem.enableEmission = false;
		particleSystem.renderer.enabled = false;
		Invoke("Show", 1);
	}
	
	void Show () {
		particleSystem.enableEmission = true;
		particleSystem.renderer.enabled = true;
	}
}
