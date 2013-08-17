using UnityEngine;
using System.Collections;

/// <summary>
/// For now the actionbar is a solid block with tiles representing 
/// actions.
/// 
/// TODO.D: 
/// - Hookup actions with actionbar
/// - Expand text/icons
/// - Display cooldown (as countdown?)
/// </summary>


public class ActionBar : MonoBehaviour
{

	public Color backgroundColor = Color.black;
	public Color abilityColor = Color.gray;
		
	public string labelText = "";

	// Placeholder for actual character abilities
	public string[] abilities = {
			"Q", "W", "E",
		};
	
	// visual delimination between abilities
	public int padding = 5;
		
	// The main reference rectangle for the action bar.
	private Rect box;

	private Texture2D background;
	private Texture2D foreground;
		
	private Rect bgRect;
		
	private Rect[] abilityRects;
		
	private int abilityWidth = 60;
	private int barHeight = 60;
		
	
	
	void Start ()
	{
		int barWidth = 
				// The bar will be at least as long as the abilities
				(abilities.GetLength(0) * abilityWidth) +
				// plus the padding between abilities
				((abilities.GetLength(0) - 1) * padding) + 
				// and pad between the abilities and the ends of the bar
				padding * 2;
		
		// Orients the bar such that the center of bar == center of screen.
		int barX = (Screen.width / 2) - (barWidth / 2);
		
		// Top of scren - bottom of screen
		int barY = 0; // Screen.height - barHeight;
		
		box = new Rect(barX, barY, barWidth, barHeight);
				
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		background.SetPixel(0, 0, backgroundColor);
		background.Apply();
			
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground.SetPixel(0, 0, abilityColor);
		foreground.Apply();
			
		bgRect = new Rect(0f, 0f, box.width, box.height);
			
		abilityRects = new Rect[abilities.GetLength(0)];
		
		for(int i = 0; i < abilities.GetLength(0); i++){
			abilityRects[i] = new Rect(
					bgRect.xMin + (i * padding) + (abilityWidth * i) + padding,
					bgRect.yMin, abilityWidth, bgRect.height);
		}


	}


	public void OnGUI()
	{

		GUI.BeginGroup(box);
		{
			GUI.DrawTexture(bgRect, background, ScaleMode.StretchToFill);
			for(int i = 0; i < abilities.GetLength(0); i++){
				GUI.DrawTexture(abilityRects[i], foreground, ScaleMode.StretchToFill);
				GUI.Label(abilityRects[i], abilities[i]);
			}
		}
		GUI.EndGroup();

	}


}
