using UnityEngine;
using System.Collections;

public class Point
{
	private int _x = 0;
	private int _y = 0;

	public int x {
		get { return _x; }
	}

	public int y {
		get { return _y; }
	}

	public Point(){}

	public Point(int x, int y)
	{
		this._x = x;
		this._y = y;
	}

	public Point (Point point)
	{
		this._x = point.x;
		this._y = point.y;
	}

	public bool Equal(int x, int y)
	{
		return this._x == x && this._y == y;
	}

	public bool Equal(Point point)
	{
		return Equal(point._x, point._y);
	}

	public Point Up()
	{
		return new Point(this._x, this._y + 1);
	}

	public Point Down()
	{
		return new Point(this._x, this._y - 1);
	}

	public Point Left()
	{
		return new Point(this._x - 1, this._y);
	}

	public Point Right()
	{
		return new Point(this._x + 1, this._y);
	}

	public void Print()
	{
		Debug.Log("Point ("+this._x+","+this._y+")");
	}
}