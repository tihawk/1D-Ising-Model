using UnityEngine;
using System.Collections;
using AwokeKnowing.GnuplotCSharp;

public class IsingModelController : MonoBehaviour {

	public static IsingModelController Instance { get; protected set; }
	public IsingModel isingModel { get; protected set; }
	public static int N = 3;
	public static double temperature = 0;

	// Use this for initialization
	void OnEnable () {

		Instance = this;
		if (temperature == 0)	temperature = IsingModel.critTemperature;
		isingModel = new IsingModel (N, temperature);

	}
	
	// Update is called once per frame
	void Update () {

		isingModel.doOneMonteCarloStep ();

	}
}
