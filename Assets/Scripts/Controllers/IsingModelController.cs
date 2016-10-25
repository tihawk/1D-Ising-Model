using UnityEngine;
using System.Collections;
using AwokeKnowing.GnuplotCSharp;

public class IsingModelController : MonoBehaviour {

	public static IsingModelController Instance { get; protected set; }
	public IsingModel isingModel { get; protected set; }
	public int N;
	public double temperature;

	// Temporary implementation of GnuPlot
	double[] specificHeat;

	// Use this for initialization
	void OnEnable () {

		Instance = this;
		if (temperature == 0)	temperature = IsingModel.critTemperature - 0.1*IsingModel.critTemperature;
		isingModel = new IsingModel (N, temperature);
		specificHeat = new double[1000];

	}
	
	// Update is called once per frame
	void Update () {

		isingModel.doOneMonteCarloStep ();
	
//		if (isingModel.monteCarloSteps <= 10*N)
//		{
//			
//		}
//		else
//		{
//			isingModel.resetData ();
//			isingModel.doOneMonteCarloStep ();
//		}

//		Debug.Log ("Total Specific Heat: " + isingModel.specificHeatTotal ());
//		Debug.Log ("Total Average Energy: " + isingModel.averageEnergyTotal());
//		Debug.Log ("Temperature: " + isingModel.temperature);

		// Temporary implementation of GnuPlot:
//		for (int i = 0; i < 1000; i++)
//		{
//			
//			isingModel.doOneMonteCarloStep ();
//			specificHeat [i] = isingModel.specificHeatTotal ();
//
//		}
//
//		GnuPlot.HoldOn ();
//		GnuPlot.Plot (specificHeat);

	}
}
