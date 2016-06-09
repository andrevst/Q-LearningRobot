using UnityEngine;
using System.Collections;

public class BasicTest : MonoBehaviour
{
	void Start ()
	{
		QLearning robot = new QLearning();

		robot.CreateMap(10,10,0,0,9,9);

		robot.Loop(6381, true);
	}
	
	void Update ()
	{
	
	}
}

