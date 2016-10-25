using System.Collections.Generic;
using System;
using UnityEngine;

public class IsingModel {

	// Critical Temperature, God knows what it actually is:
	// NOTE: Doesn't seem to apply to this model! Being close to zero
	// degrees makes the model behave like around a critical temperature though.
	public static double critTemperature = 2d / Math.Log (1d + Math.Sqrt (2d));
	// Number of spins:
	int N;
	// A Dictionary for the spins, contains the number of the spin
	// from 0 to N+1 (see later why - has to do with
	// the free boundary conditions) and its value (+1/-1 or
	// 0 for the 0th and N+1st entries):
	public Dictionary<int, int> spins { get; protected set; }
	// Temperature of the simulation, used for computing physical properties,
	// AND for the boltzmann coefficient, thus changing che probability of
	// a successful flip:
	public double temperature = critTemperature;
	// Number of Monte Carlo steps
	public int monteCarloSteps { get; protected set; }
	// Total energy of the system
	int energy; 
	// A number of successful flips of the spin. Don't know why do we need it,
	// but it was in the pseudo-code I was using.
	int successfulFlips = 0;
	// Used for calculations of physical properties:
	double energyAccumulator = 0d;
	double energySquaredAccumulator = 0d;
	int magnetisation = 0;
	double magnetisationAccumulator = 0d;
	double magnetisationSquaredAccumulator = 0d;
	// An array for the Boltzmann factors, used for changing
	// the probability of a successful flip of the spin:
	double[] w = new double[9]; 

	// Constructor for the class:
	public IsingModel(int N, double temperature)
	{
		this.temperature = temperature;
		spins = new Dictionary<int, int>();
		// Set number of spins
		this.N = N;
		// Since we are going to start at minimal energy, we need
		// all spins to be pointing in the same direction,
		// in our case +1
		for (int i = 0; i < N+2; i++)
		{
			// NOTE: Here, by adding N+2 elements, instead of N, we can then
			// assign to the first and last ones a spin of zero, thus
			// implementing free boundary conditions!
			spins.Add (i, 1);
		}

		//Assigning a spin of zero to boundary elements of spins[]:
		spins[0] = 0;
		spins [N + 1] = 0;

		// ground state energy is equal to -1*the number of spins
		// TODO: In one of the pseudo-codes for 2D Ising it was -2*N. Why?
		energy = -N;
		magnetisation = N;
		// setting two of the boltzmann array elements manually
		// Because of the lack of a magnetic field, only these coefficients,
		// coresponding to their energies, will ever occur, so we can skip the
		// calculation of the others.
		// NOTE: For the 2D Ising Model, the energies +-8, +-4 and 0 will appear, 
		// while for the 1D model, energies of +-4, +-2, and 0 appear.
		//w [8] = Math.Exp (-8d / temperature);
		w [4] = Math.Exp (-4d / temperature);
		w [2] = Math.Exp (-2d / temperature);

		resetData ();
	}

	public double averageMagnetisation()
	{
		return magnetisationAccumulator / monteCarloSteps;
	}

	public double averageEnergyTotal()
	{
		//FIXME This seems completely off!
		return energyAccumulator / monteCarloSteps;
	}
		
	public double specificHeatTotal()
	{

		double averageEnergySquared = energySquaredAccumulator / monteCarloSteps;
		double averageEnergy = energyAccumulator / monteCarloSteps;
		double temp = averageEnergySquared - averageEnergy * averageEnergy;

		return temp / (temperature * temperature);
	}

	public void resetData()
	{
		monteCarloSteps = successfulFlips = 0;
		energyAccumulator = energySquaredAccumulator = 
			magnetisationAccumulator = magnetisationSquaredAccumulator = 0d;
	}

	//TODO: Implement incrementing Temperature and Number of spins, also graphing somehow.

	public void doOneMonteCarloStep()
	{
		System.Random random = new System.Random ();


		// Make sure to do the appropriate number of tries, by making N-2 tries, not N,
		// because of the boundary conditions
		for (int j = 0; j < N-2; j++)
		{
			// Choosing a random spin from the N available ones.
			// NOTE: To finally implement free boundary conditions,
			// we make sure to not pick elements 0 and N+1 in the random
			// generator, because they are assigned a spin of zero!
			int i = random.Next ( 1, N+1);
			//Debug.Log ("spin number: " + i);
			// Calculating the difference between the energy of the
			// neighbourhood before and after a test flip of the spin.

			int dE = 2 * spins[i] * ( spins[i+1] + spins[i-1] );
			//Debug.Log ("dE: " + dE);
			//Debug.Log ("w[dE]: " + w[dE]);
				
			if (dE <= 0 || w[dE] > random.Next (0, 100000001) / 100000000d)
			{				
				//Debug.Log ("random.Next: " + temp);
				// If the difference between the old and new energies
				// of the neighbourhood of spin i is less than zero, or
				// if bigger than zero, but the boltzmann coeff. for this 
				// energy is bigger than a randomly generated number between
				// 0 and 1, then we successfuly flip the spin.
				spins [i] = -spins [i];
				successfulFlips++;
				energy += dE;
				magnetisation += 2 * spins [i];
			}
			// Otherwise nothing changes.
		}
		// At the end of the cycle we just change some variables.
		energyAccumulator += energy;
		energyAccumulator += energy * energy;
		magnetisationAccumulator += magnetisation;
		magnetisationSquaredAccumulator += magnetisation * magnetisation;
		monteCarloSteps++;
		//Debug.Log ("steps: " + monteCarloSteps);
		//Debug.Log( 4/Math.Log( 1d + 4d/ (energyAccumulator / (monteCarloSteps*N)) ));

	}
}
