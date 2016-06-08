using UnityEngine;
using System.Collections;

public class State
{
	private float _up;
	private float _right;
	private float _down;
	private float _left;

	public float up {
		get { return _up; }
	}
	public float right {
		get { return _right; }
	}
	public float down {
		get { return _down; }
	}
	public float left {
		get { return _left; }
	}

	public enum Direction {
		Up = 0, Right = 1, Down = 2, Left = 3
	}

	public enum Type {
		Normal = 0, Obstacle = -1, Goal = 1
	}

	public State(Type type = Type.Normal)
	{
		Convert(type);
	}

	private float Value(Type type)
	{
		return (float) type;
	}

	private void Convert(Type type)
	{
		this._up 	= Value(type);
		this._right = Value(type);
		this._down 	= Value(type);
		this._left 	= Value(type);
	}

	public void Obstacle()
	{
		Convert(Type.Obstacle);
	}

	public void Normal()
	{
		Convert(Type.Normal);
	}

	public void Goal()
	{
		Convert(Type.Goal);
	}

	public Direction BestChoice()
	{
		float value = -1f;
		Direction direction = Direction.Up;

		if (_up > value)
		{
			value = _up;
			direction = Direction.Up;
		}

		if (_right > value)
		{
			value = _right;
			direction = Direction.Right;
		}

		if (_down > value)
		{
			value = _down;
			direction = Direction.Down;
		}

		if (_left > value)
		{
			value = _left;
			direction = Direction.Left;
		}
	}
}

