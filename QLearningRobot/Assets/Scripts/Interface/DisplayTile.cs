using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTile : MonoBehaviour
{
	private static DisplayTile instance = null;

	public Text textUp;
	public Text textRight;
	public Text textDown;
	public Text textLeft;

	private Image image;

	void Start()
	{
		instance	= this;
		image		= GetComponent<Image>();
	}

	public static void SelectTile(State state)
	{
		if (instance)
		{
			instance.Select(state);
		}
		else
		{
			Debug.Log("Instance not set");
		}
	}

	void Select(State state)
	{
		textDown.text	= state.GetAction(QLData.Direction.Up)
			.qFactor.ToString("0.0");
		textRight.text	= state.GetAction(QLData.Direction.Right)
			.qFactor.ToString("0.0");
		textUp.text		= state.GetAction(QLData.Direction.Down)
			.qFactor.ToString("0.0");
		textLeft.text	= state.GetAction(QLData.Direction.Left)
			.qFactor.ToString("0.0");

		image.color		= MapData.tileColors[(int) state.type];
	}
}
