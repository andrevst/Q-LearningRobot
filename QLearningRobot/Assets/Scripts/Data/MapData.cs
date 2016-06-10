using UnityEngine;
using System.Collections;

public static class MapData
{
	public static float tileDistanceMultpiplier = 100f;
	public static float tileDistanceOffset		= 50f;

	public static Color tileOutlineNormalColor		= 
		new Color(0.5f, 0.5f, 0.5f, 0.5f);
	public static Color tileOutlineSelectedColor	= 
		new Color(0f, 1f, 1f, 1f);

	public static Color tileImageNormalColor		= 
		new Color(1f, 1f, 1f, 1f);
	public static Color tileImageInitialColor	= 
		new Color(0.5f, 0.5f, 1f, 1f);
	public static Color tileImageFinalColor		= 
		new Color(0.5f, 1f, 0.5f, 1f);
	public static Color tileImageBlockColor		= 
		new Color(1f, 0.5f, 0.5f, 1f);

	public static Color [] tileColors 			= {
		tileImageNormalColor, tileImageInitialColor,
		tileImageFinalColor, tileImageBlockColor
	};
}

