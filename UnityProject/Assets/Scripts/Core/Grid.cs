using UnityEngine;
using System.Collections;

public class Grid
{
	private State [][] states;
	private int sizeX = 0;
	private int sizeY = 0;

	public Grid(int sizeX, int sizeY)
	{
		this.sizeX = sizeX;
		this.sizeY = sizeY;

		this.states = new State[sizeX][sizeY];
	}

	public State Value(Point point)
	{
		return states[point.x, point.y];
	}

	public void RestartStates()
	{
		foreach(State s in states)
		{
			s.Normal();
		}
	}

	public bool valid(int x, int y)
	{
		if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
		{
			return false;
		}
		return true;
	}

	public bool valid(Point point)
	{
		return valid(point.x, point.y);
	}
}

