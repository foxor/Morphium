using UnityEngine;
using System.Collections.Generic;

public abstract class StatBar : MonoBehaviour {
	
	protected abstract StatType BarStat {
		get;
	}
	
	protected abstract Rect DrawLocation {
		get;
	}
	
	protected StatManager manager;
	protected Rect fullRect;
	protected Rect emptyRect;
	
	public Texture fullTex;
	public Texture emptyTex;
	
	public void Awake() {
		manager = GetComponent<StatManager>();
	}
	
	public void OnGUI() {
		Stat barStat = manager.stats[BarStat];
		float pct = ((float)barStat.Current) / ((float)barStat.Max);
		
		fullRect.x = DrawLocation.x;
		fullRect.y = DrawLocation.y;
		fullRect.width = DrawLocation.width * pct;
		fullRect.height = DrawLocation.height;
		
		emptyRect.x = fullRect.xMax;
		emptyRect.y = DrawLocation.y;
		emptyRect.width = DrawLocation.width - fullRect.width;
		emptyRect.height = DrawLocation.height;
		
		GUI.DrawTexture(fullRect, fullTex);
		GUI.DrawTexture(emptyRect, emptyTex);
	}
}