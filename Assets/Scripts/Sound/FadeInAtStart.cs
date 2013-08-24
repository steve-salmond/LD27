using UnityEngine;
using System.Collections;

public class FadeInAtStart : MonoBehaviour {
	
	public float Delay = 1;
	public float Duration = 1;
	
	void Awake () {
	 StartCoroutine(FadeInSound());
	}
	
	IEnumerator FadeInSound()
	{
		AudioListener.volume = 0;
		
		yield return new WaitForSeconds(Delay);
		
		float start = Time.time;
		float end = start + Duration;
		while (Time.time < end)
		{
			AudioListener.volume = Mathf.Clamp((Time.time - start) / Duration, 0, 1);
			yield return new WaitForEndOfFrame();
		}
		
		AudioListener.volume = 1;
	}
}
