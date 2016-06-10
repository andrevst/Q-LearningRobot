using UnityEngine;
using System.Collections;

public class VisualTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		QLearning qlSearch = new QLearning();
		QLearning qlDeliver = new QLearning();

		qlSearch.CreateMap(10,10,0,0,9,9);
		qlDeliver.CreateMap(10,10,9,9,0,0);

		qlSearch.Loop(4572, true);
		qlDeliver.Loop(4572, true);

		GetComponent<Map>().CreateMap(qlSearch, qlDeliver);

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

