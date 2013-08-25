using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	public Texture Text;
	
	public Rect[] NumberBounds;
	public Rect PlaceBounds;
	public Rect LapBounds;
	public Rect SuffixStBounds;
	public Rect SuffixNdBounds;
	public Rect SuffixRdBounds;
	public Rect SuffixThBounds;
	
	void OnGUI()
	{
		// Only draw textures during the GUI repaint phase.
		if (!Event.current.type.Equals(EventType.Repaint))
			return;
		
		// Get screen dimensions.
		float w = Screen.width;
		float h = Screen.height;
		
		// Draw place text.
		int place = PlayerController.Instance.Place;
		Graphics.DrawTexture(new Rect(30, h - 110, 100, 100), Text, PlaceBounds, 0, 0, 0, 0);
		Graphics.DrawTexture(new Rect(120, h - 110, 100, 100), Text, NumberBounds[place], 0, 0, 0, 0);
		
		// Draw place suffix (1st,2nd, 3rd etc.)
		if (place == 1)
			Graphics.DrawTexture(new Rect(200, h - 100, 50, 50), Text, SuffixStBounds, 0, 0, 0, 0);
		else if (place == 2)	
			Graphics.DrawTexture(new Rect(200, h - 100, 50, 50), Text, SuffixNdBounds, 0, 0, 0, 0);
		else if (place == 3)	
			Graphics.DrawTexture(new Rect(200, h - 100, 50, 50), Text, SuffixRdBounds, 0, 0, 0, 0);
		else if (place == 4)	
			Graphics.DrawTexture(new Rect(200, h - 100, 50, 50), Text, SuffixThBounds, 0, 0, 0, 0);
		
		// Draw current lap.
		int lap = PlayerController.Instance.Lap;
		Graphics.DrawTexture(new Rect(w - 230, h - 110, 100, 100), Text, LapBounds, 0, 0, 0, 0);
		Graphics.DrawTexture(new Rect(w - 140, h - 110, 100, 100), Text, NumberBounds[lap], 0, 0, 0, 0);
		
	}
}
