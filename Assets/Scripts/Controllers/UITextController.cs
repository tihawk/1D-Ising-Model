using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextController : MonoBehaviour {

	int monteCarloSteps;
	Text text;
	string textString;

	// Use this for initialization
	void Start () {

		text = GetComponent<Text> ();
		text.text = "Average Energy: ";
	
	}
	
	// Update is called once per frame
	void Update () {

		monteCarloSteps = IsingModelController.Instance.isingModel.monteCarloSteps;
		textString = ( "average Energy after " + monteCarloSteps + " steps: " + IsingModelController.Instance.isingModel.averageEnergyTotal() );
		text.text = textString;
	
	}
}
