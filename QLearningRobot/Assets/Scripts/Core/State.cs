using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class State
{
	private int x	= 0;
	private int y	= 0;

	private List<Action> _actionList	= new List<Action>();
	private float _reward				= Data.BASIC_REWARD;

	public List<Action> actionList {
		get { return _actionList; }
	}
	public float reward {
		get { return _reward; }
	}

	public State(){}

	public State(float reward)
	{
		this._reward = reward;
	}

	public State(List<Action> actionList)
	{
		this._actionList = actionList;
	}

	public State(float reward, List<Action> actionList)
	{
		this._reward = reward;
		this._actionList = actionList;
	}

	public State(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public State(int x, int y, float reward)
	{
		this.x = x;
		this.y = y;
		this._reward = reward;
	}

	public State(int x, int y, float reward, List<Action> actionList)
	{
		this.x = x;
		this.y = y;
		this._reward = reward;
		this._actionList = actionList;
	}

	public void FinalState()
	{
		this._reward = Data.FINAL_REWARD;
	}

	public void NormalState()
	{
		this._reward = Data.BASIC_REWARD;
	}

	public void BlockState(bool border = false)
	{
		this._reward = Data.BLOCK_REWARD;
		if (border)
		{
			x = -1;
			y = -1;
		}
	}

	public void RemoveBlock()
	{
		if (IsBlockState())
		{
			NormalState();
		}
	}

	public bool IsBlockState()
	{
		return this._reward == Data.BLOCK_REWARD;
	}

	public bool IsFinalState()
	{
		return this._reward == Data.FINAL_REWARD;
	}

	public void CreateActions()
	{
		foreach(Data.Direction direction in Data.POSSIBLE_DIRECTIONS)
		{
			_actionList.Add(new Action(new State(), direction));
		}
	}

	public float GreaterQFactor()
	{
		float greaterQ = Data.BASIC_REWARD;
		foreach(Action action in _actionList)
		{
			if (action.qFactor > greaterQ)
			{
				greaterQ = action.qFactor;
			}
		}

		return greaterQ;
	}

	public void UpdateAction(Action action)
	{
		for (int i = 0; i < _actionList.Count; i++)
		{
			if (_actionList[i].targetState == action.targetState)
			{
				_actionList[i] = action;
				break;
			}
		}
	}

	public Action ChooseAction(bool useExplorationFactor = false)
	{
		float greaterQ = Data.BASIC_REWARD;
		Action bestAction = null;
		List<Action> possibleActions = new List<Action>();

		foreach(Action action in _actionList)
		{
			if (action.targetState.IsBlockState() == false)
			{
				possibleActions.Add(action);
				if (action.qFactor > greaterQ)
				{
					greaterQ 	= action.qFactor;
					bestAction 	= action;
				}
			}
		}

		if (possibleActions.Count == 0)
		{
			return null;
		}

		float explorationFactor = Random.Range(0f, 1f);
		if (bestAction == null || (useExplorationFactor &&
			explorationFactor < Data.EXPLORATION_FACTOR))
		{
			return possibleActions[Random.Range(0, possibleActions.Count)];
		}

		if (possibleActions.Count == 0)
		{
			return bestAction;
		}

		float threshold = Data.THRESHOLD_FACTOR * bestAction.qFactor;
		List<Action> thresholdActions = new List<Action>();
		foreach(Action action in _actionList)
		{
			if (action.qFactor >= threshold)
			{
				thresholdActions.Add(action);
			}
		}

		return thresholdActions[Random.Range(0, thresholdActions.Count)];
	}

	public Action GetAction(Data.Direction direction)
	{
		foreach(Action action in _actionList)
		{
			if (action.direction == direction)
			{
				return action;
			}
		}
		return null;
	}
}

