using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToWorld : MonoBehaviour
{
	
	private Transform t;
	
	private const float radius = 20;
	
	public void Start()
	{
		t = transform;
		if (Application.isPlaying)
			enabled = false;
	}
	
	public void Update()
	{
		if (!t)
			t = transform;
		
		t.position = t.position.normalized * radius;
		t.LookAt(t.position - new Vector3(0, 0, 1), t.position);
	}
	
}
