using UnityEngine;
using System.Collections;

public static class Data
{
	public enum Direction {
		Up = 0, Right = 1, Down = 2, Left = 3
	}

	public static Direction [] POSSIBLE_DIRECTIONS = {
		Direction.Up, Direction.Right,
		Direction.Down, Direction.Left
	};

	public const float QFACTOR_BASE	= 0f;

	public const float BASIC_REWARD = 0f;
	public const float FINAL_REWARD = 100f;
	public const float BLOCK_REWARD = -100f;

	public const float EXPLORATION_FACTOR	= 0.5f;
	public const float THRESHOLD_FACTOR		= 0.95f;

	public const float BASE_LEARNING_RATE	= 0.3f;
	public const float BASE_DISCOUNT		= 0.8f;

	public const int BASE_ITERATIONS		= 50000;
}

