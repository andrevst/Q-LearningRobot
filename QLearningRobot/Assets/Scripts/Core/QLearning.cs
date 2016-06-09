using UnityEngine;
using System.Collections;

public class QLearning
{
	private State[,] statesMap;
	private State currentState	= null;
	private State initialState	= null;
	private State finalState	= null;

	private int iterations	= Data.BASE_ITERATIONS;
	private float alpha		= Data.BASE_LEARNING_RATE;
	private float delta		= Data.BASE_DISCOUNT;

	private int sizeX;
	private int sizeY;

	private int initialX;
	private int initialY;

	public QLearning(){}

	public void CreateMap(
		int sizeX, int sizeY,
		int initialX,int initialY,
		int finalX, int finalY)
	{
		statesMap = new State[sizeX, sizeY];

		this.sizeX = sizeX;
		this.sizeY = sizeY;
		this.initialX = initialX;
		this.initialY = initialY;

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				State state = new State(x,y);
				state.CreateActions();

				if (x == 0)
				{
					state.GetAction(Data.Direction.Left)
						.targetState.BlockState();
				}
				else if (x == sizeX - 1)
				{
					state.GetAction(Data.Direction.Right)
						.targetState.BlockState();
				}

				if (y == 0)
				{
					state.GetAction(Data.Direction.Up)
						.targetState.BlockState();
				}
				else if (y == sizeY - 1)
				{
					state.GetAction(Data.Direction.Down)
						.targetState.BlockState();
				}

				if (x == initialX && y == initialY)
				{
					initialState = state;
				}

				if (x == finalX && y == finalY)
				{
					state.FinalState();
					finalState = state;
				}

				statesMap[x,y] = state;
			}
		}

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				if (x > 0)
				{
					statesMap[x,y].GetAction(Data.Direction.Left)
						.targetState = statesMap[x - 1, y];
				}
				if (x < sizeX - 1)
				{
					statesMap[x,y].GetAction(Data.Direction.Right)
						.targetState = statesMap[x + 1, y];
				}

				if (y > 0)
				{
					statesMap[x,y].GetAction(Data.Direction.Up)
						.targetState = statesMap[x, y - 1];
				}
				if (y < sizeY - 1)
				{
					statesMap[x,y].GetAction(Data.Direction.Down)
						.targetState = statesMap[x, y + 1];
				}

				// REMOVE THIS
				if (Random.value < 0.1f && statesMap[x,y].IsFinalState() == false)
				{
					statesMap[x,y].BlockState();
				}
			}
		}
	}

	public void Restart()
	{
		currentState = initialState;
	}

	public void Iterate(bool useExplorationFactor = false)
	{
		if (currentState.IsFinalState())
		{
			Restart();
		}
		else
		{
			Action action = currentState.ChooseAction(useExplorationFactor);
			if (action != null)
			{
				action.Update(
					alpha, 
					action.targetState.reward,
					delta,
					action.targetState.GreaterQFactor(),
					currentState);

				currentState.UpdateAction(action);

				currentState = action.targetState;
			}
			else
			{
				// Blocked
				Debug.Log("Blocked");
				Restart();
			}
		}
	}

	public void Loop(int iterations, bool useExplorationFactor = false)
	{
		PrintMap("Before loop");

		Restart();
		for(int i = 0; i < iterations; i++)
		{
			Iterate(useExplorationFactor);
		}
			
		PrintMap("After "+iterations+" iterations; exploration factor used? "+useExplorationFactor);
	}

	public void Loop()
	{
		PrintMap();
		Loop(this.iterations, true);
		PrintMap();
	}

	public void PrintMap(string title = "")
	{
		string map = "";
		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				if (statesMap[x,y].IsBlockState())
				{
					map += "BLOCK\t";
				}
				else if (statesMap[x,y].IsFinalState())
				{
					map += "FINAL\t";
				}
				else
				{
					map += statesMap[x,y].GreaterQFactor().ToString("0.00") + "\t";
				}
			}
			map += "\n";
		}
		Debug.Log("Map: "+title+"\n"+map);
	}
}

