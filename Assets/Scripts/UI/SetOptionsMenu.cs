using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SetOptionsMenu : MonoBehaviour {

	int newN = IsingModelController.N;
	double newTemperature = IsingModelController.temperature;

	public void OnClick()
	{

		IsingModelController.Instance.isingModel.resetData (IsingModelController.N, IsingModelController.temperature);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

		IsingModelController.N = newN;
		IsingModelController.temperature = newTemperature;

	}

	public void SetN(string inputFieldString)
	{

		int.TryParse (inputFieldString, out newN);

	}

	public void SetTemperature(string inputFieldString)
	{
		
		double.TryParse (inputFieldString, out newTemperature);

	}

}
