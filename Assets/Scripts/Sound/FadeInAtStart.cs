using UnityEngine;
using System.Collections;

public class FadeInAtStart : MonoBehaviour {
	
	public float Delay = 1;
	public float Duration = 1;
	public float VolumeScale = 1;
	
	void Start() {
		audio.volume = 0;
	 	StartCoroutine(FadeInSound());
	}
	
	IEnumerator FadeInSound()
	{
		audio.volume = 0;
		
		yield return new WaitForSeconds(Delay);
		
		float start = Time.time;
		float end = start + Duration;
		while (Time.time < end)
		{
			audio.volume = VolumeScale * Mathf.Clamp((Time.time - start) / Duration, 0, 1);
			yield return new WaitForEndOfFrame();
		}
		
		audio.volume = VolumeScale;
	}
}
