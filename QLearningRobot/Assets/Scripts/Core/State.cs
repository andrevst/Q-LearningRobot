using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class State
{
	private int _x	= 0;
	private int _y	= 0;

	private List<Action> _actionList	= new List<Action>();
	private float _reward				= QLData.BASIC_REWARD;

	private QLData.StateType _type	= QLData.StateType.Normal;

	public List<Action> actionList {
		get { return _actionList; }
	}
	public float reward {
		get { return _reward; }
	}

	public QLData.StateType type {
		get { return _type; }
	}

	public int x {
		get { return _x; }
	}
	public int y {
		get { return _y; }
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
		this._x = x;
		this._y = y;
	}

	public State(int x, int y, float reward)
	{
		this._x = x;
		this._y = y;
		this._reward = reward;
	}

	public State(int x, int y, float reward, List<Action> actionList)
	{
		this._x = x;
		this._y = y;
		this._reward = reward;
		this._actionList = actionList;
	}

	public void FinalState()
	{
		this._reward 	= QLData.FINAL_REWARD;
		this._type		= QLData.StateType.Final;
	}

	public void NormalState()
	{
		this._reward 	= QLData.BASIC_REWARD;
		this._type		= QLData.StateType.Normal;
	}

	public void BlockState(bool border = false)
	{
		this._reward 	= QLData.BLOCK_REWARD;
		this._type		= QLData.StateType.Block;
		if (border)
		{
			_x = -1;
			_y = -1;
		}
	}

	public void InitialState()
	{
		this._reward 	= QLData.BASIC_REWARD;
		this._type		= QLData.StateType.Initial;
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
		return this._type == QLData.StateType.Block;
	}

	public bool IsFinalState()
	{
		return this._type == QLData.StateType.Final;
	}

	public bool IsInitialState()
	{
		return this._type == QLData.StateType.Initial;
	}

	public void CreateActions()
	{
		foreach(QLData.Direction direction in QLData.POSSIBLE_DIRECTIONS)
		{
			_actionList.Add(new Action(new State(), direction));
		}
	}

	public float GreaterQFactor()
	{
		float greaterQ = QLData.BASIC_REWARD;
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
		float greaterQ = QLData.BASIC_REWARD;
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
			explorationFactor < QLData.EXPLORATION_FACTOR))
		{
			return possibleActions[Random.Range(0, possibleActions.Count)];
		}

		if (possibleActions.Count == 0)
		{
			return bestAction;
		}

		float threshold = QLData.THRESHOLD_FACTOR * bestAction.qFactor;
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

	public Action GetAction(QLData.Direction direction)
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

