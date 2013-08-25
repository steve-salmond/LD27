using UnityEngine;
using System.Collections;

public class EffectCleanup : MonoBehaviour {

	void Update () {
		if (particleSystem != null && particleSystem.IsAlive())
			return;
		if (audio != null && audio.isPlaying)
			return;
		
		Destroy(gameObject);
	}
}
