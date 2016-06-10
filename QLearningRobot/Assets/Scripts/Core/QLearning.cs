using UnityEngine;
using System.Collections;

public class QLearning
{
	private State[,] _statesMap;
	private State _currentState	= null;
	private State _initialState	= null;
	private State _finalState	= null;

	private int _iterations	= QLData.BASE_ITERATIONS;
	private float alpha		= QLData.BASE_LEARNING_RATE;
	private float delta		= QLData.BASE_DISCOUNT;

	private int _sizeX;
	private int _sizeY;

	public State[,] statesMap {
		get { return _statesMap; }
	}

	public int sizeX {
		get { return _sizeX; }
	}

	public int sizeY {
		get { return _sizeY; }
	}

	public State initialState {
		get { return _initialState; }
	}

	public State finalState {
		get { return _finalState; }
	}

	public State currentState {
		get { return _currentState; }
	}

	public int iterations {
		get { return _iterations; }
		set {
				_iterations = value; 
				if (_iterations < 1) 
					_iterations = 1;
				if (_iterations > QLData.MAX_ITERATIONS) 
					_iterations = QLData.MAX_ITERATIONS;
		}
	}

	public QLearning(){}

	public void CreateMap(
		int sizeX, int sizeY,
		int initialX,int initialY,
		int finalX, int finalY)
	{
		_statesMap = new State[sizeX, sizeY];

		this._sizeX = sizeX;
		this._sizeY = sizeY;

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				State state = new State(x,y);
				state.CreateActions();

				if (x == 0)
				{
					state.GetAction(QLData.Direction.Left)
						.targetState.BlockState();
				}
				else if (x == sizeX - 1)
				{
					state.GetAction(QLData.Direction.Right)
						.targetState.BlockState();
				}

				if (y == 0)
				{
					state.GetAction(QLData.Direction.Up)
						.targetState.BlockState();
				}
				else if (y == sizeY - 1)
				{
					state.GetAction(QLData.Direction.Down)
						.targetState.BlockState();
				}

				if (x == initialX && y == initialY)
				{
					state.InitialState();
					_initialState = state;
					_currentState = _initialState;
				}

				if (x == finalX && y == finalY)
				{
					state.FinalState();
					_finalState = state;
				}

				_statesMap[x,y] = state;
			}
		}

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeY; y++)
			{
				if (x > 0)
				{
					_statesMap[x,y].GetAction(QLData.Direction.Left)
						.targetState = _statesMap[x - 1, y];
				}
				if (x < sizeX - 1)
				{
					_statesMap[x,y].GetAction(QLData.Direction.Right)
						.targetState = _statesMap[x + 1, y];
				}

				if (y > 0)
				{
					_statesMap[x,y].GetAction(QLData.Direction.Up)
						.targetState = _statesMap[x, y - 1];
				}
				if (y < sizeY - 1)
				{
					_statesMap[x,y].GetAction(QLData.Direction.Down)
						.targetState = _statesMap[x, y + 1];
				}
			}
		}
	}

	public void Restart()
	{
		_currentState = _initialState;
	}

	public bool Iterate(bool useExplorationFactor = false, bool canRestart = false)
	{
		if (_currentState == null)
		{
			_currentState = initialState;
		}

		if (_currentState.IsFinalState())
		{
			if (canRestart)	Restart();
			return true;
		}
		else
		{
			Action action = _currentState.ChooseAction(useExplorationFactor);
			if (action != null)
			{
				action.Update(
					alpha, 
					action.targetState.reward,
					delta,
					action.targetState.GreaterQFactor(),
					_currentState);

				_currentState.UpdateAction(action);

				_currentState = action.targetState;

				return false;
			}
			else
			{
				// Blocked
				Debug.Log("Blocked");
				if (canRestart)	Restart();
				return true;
			}
		}
	}

	public void Loop(int iterations, bool useExplorationFactor = false)
	{
		PrintMap("Before loop");

		Restart();
		for(int i = 0; i < iterations; i++)
		{
			Iterate(useExplorationFactor, true);
		}
			
		PrintMap("After "+iterations+" iterations; exploration factor used? "+useExplorationFactor);
	}

	public void Loop()
	{
		PrintMap();
		Loop(this._iterations, true);
		PrintMap();
	}

	public void PrintMap(string title = "")
	{
		string map = "";
		for (int x = 0; x < _sizeX; x++)
		{
			for (int y = 0; y < _sizeY; y++)
			{
				if (_statesMap[x,y].IsBlockState())
				{
					map += "BLOCK\t";
				}
				else if (_statesMap[x,y].IsFinalState())
				{
					map += "FINAL\t";
				}
				else
				{
					map += _statesMap[x,y].GreaterQFactor().ToString("0.00") + "\t";
				}
			}
			map += "\n";
		}
		Debug.Log("Map: "+title+"\n"+map);
	}
}

