using UnityEngine;
using System.Collections;

public class CountdownController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Stop", 12);
	}
	
	void Stop()
	{
		gameObject.SetActive(false);
	}
}
