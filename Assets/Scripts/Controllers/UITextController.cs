using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextController : MonoBehaviour {

	int monteCarloSteps;
	Text energyText;
	Text magnetisationText;

	string energyTextString;
	string magnetisationTextString;

	// Use this for initialization
	void Start () {

		energyText = transform.GetChild (0).GetComponent<Text> ();
		magnetisationText = transform.GetChild (1).GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		monteCarloSteps = IsingModelController.Instance.isingModel.monteCarloSteps;

		energyTextString = ( "average Energy per spin after " + monteCarloSteps + " steps: " + 
			(IsingModelController.Instance.isingModel.averageEnergyTotal()/IsingModelController.Instance.N).ToString("F5") );
		magnetisationTextString = ( "average Magnetisation after " + monteCarloSteps + " steps: " + 
			(IsingModelController.Instance.isingModel.averageMagnetisation()).ToString("F5") );

		energyText.text = energyTextString;
		magnetisationText.text = magnetisationTextString;
	
	}
}
