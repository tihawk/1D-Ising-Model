using UnityEngine;
using System.Collections;
using AwokeKnowing.GnuplotCSharp;

public class PlotController {

	public PlotController ()
	{
		GnuPlot.Plot ("sin(x) + 2");
	}

}
