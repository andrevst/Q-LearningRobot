using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public MapTile objectMapTile;
	public Robot objectRobot;

	public Transform parentSearching;
	public Transform parentDelivering;

	public void CreateMap(QLearning qLSearching, QLearning qLDelivering)
	{
		List<MapTile> mapSearching = new List<MapTile>();

		for(int x = 0; x < qLSearching.sizeX; x++)
		{
			for(int y = 0; y < qLSearching.sizeY; y++)
			{
				MapTile newMapTile = Instantiate<MapTile>(objectMapTile);

				newMapTile.transform.SetParent(this.parentSearching, false);
				newMapTile.Create(qLSearching.statesMap[x,y]);

				mapSearching.Add(newMapTile);
			}
		}

		List<MapTile> mapDelivering = new List<MapTile>();

		for(int x = 0; x < qLDelivering.sizeX; x++)
		{
			for(int y = 0; y < qLDelivering.sizeY; y++)
			{
				MapTile newMapTile = Instantiate<MapTile>(objectMapTile);

				newMapTile.transform.SetParent(this.parentDelivering, false);
				newMapTile.Create(qLDelivering.statesMap[x,y]);

				mapDelivering.Add(newMapTile);
			}
		}

		Robot robot = Instantiate<Robot>(objectRobot);
		robot.transform.SetParent(this.transform, false);
		robot.Create(
			mapSearching, mapDelivering,
			parentSearching, parentDelivering,
			qLSearching, qLDelivering);
	}
}

