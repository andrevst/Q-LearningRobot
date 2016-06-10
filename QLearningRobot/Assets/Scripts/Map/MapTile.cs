using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapTile : MonoBehaviour
{
	public Sprite [] tileSprites;

	private Outline outline;
	private Image image;
	private Text displayText;

	private State state;
	private int x, y;

	public void Create(State state)
	{
		this.x		= state.x;
		this.y		= state.y;
		this.state	= state;

		UpdatePosition();

		displayText 	= GetComponentInChildren<Text>();
		outline 		= GetComponent<Outline>();
		image 			= GetComponent<Image>();

		image.sprite		= tileSprites[(int) state.type];
		image.color			= MapData.tileColors[(int) state.type];
		outline.effectColor = MapData.tileOutlineNormalColor;

		displayText.text	= state.GreaterQFactor().ToString("0.0");
	}

	public void UpdateTile(QLearning qLearning)
	{
		state 				= qLearning.statesMap[x, y];

		if (state.IsFinalState())
		{
			displayText.text	= "F";
		}
		else if (state.IsBlockState())
		{
			displayText.text	= "B";
		}
		else
		{
			displayText.text 	= state.GreaterQFactor().ToString("0.0");
		}

		if (!state.IsInitialState() &&
			!state.IsFinalState() &&
			!state.IsBlockState())
		{
			float c				= state.GreaterQFactor() / QLData.FINAL_REWARD;
			image.color			= new Color(1f, 1f, 1-c, 1f);
		}
	}

	void UpdatePosition()
	{
		float screenMultiplier	= Screen.height / ScreenData.screenHeightBase;

		float realX		= (float)
			(x * MapData.tileDistanceMultpiplier + 
				MapData.tileDistanceOffset) * screenMultiplier;

		float realY		= (float)
			(y * MapData.tileDistanceMultpiplier +  
				MapData.tileDistanceOffset) * screenMultiplier;

		transform.position = new Vector2(realX, realY);
	}

	public void Select()
	{
		DisplayTile.SelectTile(state);
	}
}

