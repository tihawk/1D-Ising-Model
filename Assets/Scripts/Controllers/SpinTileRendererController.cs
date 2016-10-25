using UnityEngine;
using System.Collections.Generic;

public class SpinTileRendererController : MonoBehaviour {

	public static SpinTileRendererController Instance;

	public Sprite spinTileSprite;
	public Sprite spinUpSprite;
	public Sprite spinDownSprite;
	// For ordering the tiles in a grid:
	int y = -1;
	int x = 0;

	int n;
	Dictionary<int, int> spinData;

	Dictionary<int, GameObject> spinTileGameObjectMap;
	Dictionary<int, GameObject> spinOrientationGameObjectMap;
	Dictionary<int, SpriteRenderer> spinOrientationSpriteRendererMap;

	// Use this for initialization
	void Start () {

		Instance = this;

		spinTileGameObjectMap = new Dictionary<int, GameObject> ();
		spinOrientationGameObjectMap = new Dictionary<int, GameObject> ();
		spinOrientationSpriteRendererMap = new Dictionary<int, SpriteRenderer> ();

		InitializeEverything ();
	
	}

	// Update is called once per frame
	void LateUpdate () {

		spinData = IsingModelController.Instance.isingModel.spins;

		for (int i = 0; i < spinData.Count; i++)
		{
			if (spinData[i] == 1)
				spinOrientationSpriteRendererMap[i].sprite = spinUpSprite;

			else if (spinData[i] == -1)
				spinOrientationSpriteRendererMap[i].sprite = spinDownSprite;
		}
	}

//	public void DestroyChildren()
//	{
//		for (int i = 0; i < transform.childCount; i++)
//		{
//			Destroy (transform.GetChild (i).gameObject);
//		}
//
//		spinTileGameObjectMap.Clear ();
//		spinOrientationGameObjectMap.Clear ();
//		spinOrientationSpriteRendererMap.Clear ();
//
//		InitializeEverything ();
//	}

	void InitializeEverything()
	{
		spinData = IsingModelController.Instance.isingModel.spins;
		n = spinData.Count;


		// Create a GameObject for each spinTile and assign them
		// both to a dictionary entry, thus giving the sprite the
		// coordinates of the Nth spinTile:
		for (int i = 0; i < n; i++)
		{
			GameObject spinTile_GO = new GameObject ();
			GameObject spinOrientation_GO = new GameObject ();

			spinTileGameObjectMap.Add (i, spinTile_GO);
			spinOrientationGameObjectMap.Add (i, spinOrientation_GO);


			// Orders the tiles in a grid, for easier viewing. Doesn't
			// mean that it's 2D!
			if (i == -y*100)
			{
				y--;
				x = 0;

			}
			x++;

			spinTile_GO.name = "spinTile_" + i;
			spinTile_GO.transform.position = new Vector3 (x, y, 0);
			spinTile_GO.transform.SetParent (this.transform, true);

			spinOrientation_GO.name = "spinOrient_" + i;
			spinOrientation_GO.transform.position = new Vector3 (x, y, 0);
			spinOrientation_GO.transform.SetParent (this.transform, true);


			// Add a sprite renderer to the GameObject
			SpriteRenderer spinTile_sr = spinTile_GO.AddComponent<SpriteRenderer> ();
			spinTile_sr.sprite = spinTileSprite;
			spinTile_sr.sortingLayerName = "spinTile";
			SpriteRenderer spintOrientation_sr = spinOrientation_GO.AddComponent<SpriteRenderer> ();
			spintOrientation_sr.sortingLayerName = "spinOrient";
			spinOrientationSpriteRendererMap.Add (i, spintOrientation_sr);

			if (spinData[i] == 1)
				spinOrientationSpriteRendererMap[i].sprite = spinUpSprite;

			else if (spinData[i] == -1)
				spinOrientationSpriteRendererMap[i].sprite = spinDownSprite;
		}
	}

}
