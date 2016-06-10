using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
	private int x, y;
	private QLearning qLSearching;
	private QLearning qLDelivering;
	private List<MapTile> mapSearching;
	private List<MapTile> mapDelivering;
	private Transform parentSearching;
	private Transform parentDelivering;

	private QLearning activeQLearning;

	private bool searching				= true;
	private bool useExplorationFactor	= false;
	private bool playing				= false;

	public void Create(
		List<MapTile> mapSearching, List<MapTile> mapDelivering,
		Transform parentSearching, Transform parentDelivering,
		QLearning qLSearching, QLearning qLDelivering)
	{
		this.mapSearching 		= mapSearching;
		this.mapDelivering 		= mapDelivering;
		this.parentSearching	= parentSearching;
		this.parentDelivering	= parentDelivering;
		this.qLSearching		= qLSearching;
		this.qLDelivering		= qLDelivering;

		parentDelivering.gameObject.SetActive(false);

		activeQLearning = qLSearching;
		searching		= true;
		playing			= false;

		this.x 	= qLSearching.initialState.x;
		this.y 	= qLSearching.initialState.y;
		UpdatePosition();

		// remove
		useExplorationFactor	= true;
		playing = true;
	}

	void Update()
	{
		if (playing)
		{
			UpdateRobot();
		}
	}

	void UpdateRobot()
	{
		bool final 	= activeQLearning.Iterate(useExplorationFactor);

		UpdatePosition();
		UpdateMaps();

		if (final)
		{
			SwapMap();
			Restart();
		}
	}

	void UpdateMaps()
	{
		if (searching)
		{
			foreach(MapTile mapTile in mapSearching)
			{
				mapTile.UpdateTile(activeQLearning);
			}
		}
		else
		{
			foreach(MapTile mapTile in mapDelivering)
			{
				mapTile.UpdateTile(activeQLearning);
			}
		}
	}

	void UpdatePosition()
	{
		float screenMultiplier	= Screen.height / ScreenData.screenHeightBase;

		this.x 		= activeQLearning.currentState.x;
		this.y 		= activeQLearning.currentState.y;

		float realX		= (float)
			(x * MapData.tileDistanceMultpiplier + 
				MapData.tileDistanceOffset) * screenMultiplier;

		float realY		= (float)
			(y * MapData.tileDistanceMultpiplier +  
				MapData.tileDistanceOffset) * screenMultiplier;

		transform.position = new Vector2(realX, realY);
	}

	void Restart()
	{
		activeQLearning.Restart();
		UpdatePosition();
	}

	void SwapMap()
	{
		if (searching)
		{
			searching 		= false;
			activeQLearning = qLDelivering;

			parentDelivering.gameObject.SetActive(true);
			parentSearching.gameObject.SetActive(false);
		}
		else
		{
			searching		= true;
			activeQLearning = qLSearching;

			parentDelivering.gameObject.SetActive(false);
			parentSearching.gameObject.SetActive(true);
		}
	}
}

