using UnityEngine;
using System.Collections;

public class Action
{
	private Data.Direction _direction	= Data.Direction.Up;
	private State _targetState			= null;
	private float _qFactor 				= 0;

	public float qFactor {
		get { return _qFactor; }
	}
	public State targetState {
		get { return _targetState; }
		set { _targetState = value; }
	}
	public Data.Direction direction {
		get { return _direction; }
	}

	public Action(){}

	public Action(float qFactor)
	{
		this._qFactor 		= qFactor;	
	}

	public Action(State targetState)
	{
		this._targetState 	= targetState;
	}

	public Action(float qFactor, State targetState)
	{
		this._qFactor 		= qFactor;	
		this._targetState 	= targetState;
	}

	public Action (State targetState, Data.Direction direction)
	{
		this._targetState 	= targetState;
		this._direction 	= direction;
	}

	public Action(float qFactor, State targetState, Data.Direction direction)
	{
		this._qFactor 		= qFactor;	
		this._targetState 	= targetState;
		this._direction 	= direction;
	}

	public void Update(
		float learningRate, 
		float reward, 
		float discount, 
		float utilityValue, 	// Higher qFactor of the next state
		State state)
	{
		_qFactor = _qFactor + learningRate * 
			(reward + (discount * utilityValue) - _qFactor);
		
		//Debug.Log("Qf "+_qFactor);
	}

}

