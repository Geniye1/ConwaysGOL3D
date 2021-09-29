using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

	// Enum for automaton rules
	public enum Rule {
		Amoeba,
		L445,
		L678,
		L1021,
		L3,
		L23,
		L5655,
		L5766,
		Builder,
		Clouds1,
		Clouds2,
		Pyroclastic,
		SlowDecay,
		SpikyGrowth,
		Experimenting
	};


	/*
	 * PUBLIC VARIABLES 
	*/ 
	// ------------------------------------------------

	// Cell prefab
	[Header("Cell Prefab")]
	public GameObject cell;

	// Script to enforce the given rule
	[Header("Enforcer object")]
	public Enforcer enforcerObj;

	// Dimensions of the polygon 
	[Header("Size of the Universe for the simulation")]
	public float sizeOfUniverse;

	// Size of the inner cube 
	[Header("Size of the inner primordial soup")]
	public int innerCubeSize;

	// Position vector for the starting position of the cells
	[Header("Starting point for cell population")]
	public Vector3 pos;

	// Space between cells
	[Header("Space between cells")]
	public int spacer;

	// Rule for the current simulation
	[Header("Automaton rule")]
	public Rule simulationRule;

	// Text object to track the number of iterations in the current sim
	[Header("Iteration Text object")]
	public Text iterationText;

	 

	/* 
	 * PRIVATE VARIABLES
	*/
	// -------------------------------------------------

	// List of cells 
	private GameObject[] cells;

	private Func<GameObject, int, int> enforcerFunctionPtr = null;

	// Mirror list of cells' alive state 
	private int[] mirrorAliveState;

	// Gap between the outer cube and the inner cube
	private float gapToCenter;

	// Total cell count stored during the sim
	private int totalCells;

	// Random class for populating cells 
	private System.Random rand = new System.Random();

	// Counter variable to track the array indices
	private int counter = 0;

	// Iteration variable to send to the Text element
	private int iteration = 0;

	// Array to store the amount of neighboring cells that are around a single cell
	private Collider[] hitCollisions;

	// Size of the OverlapBox when doing the above ^
	private Vector3 colliderBox = new Vector3(1, 1, 1);

	// Bool to track if the program is inside the starting cube
	private bool isInsideStartingCube;

	/* 
	 * FUNCTIONS 
	*/ 
	// --------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
		gapToCenter = (sizeOfUniverse - innerCubeSize) / 2;

		cells = new GameObject[ (int) Mathf.Pow(sizeOfUniverse, 3) ];
		mirrorAliveState = new int[ (int) Mathf.Pow(sizeOfUniverse, 3) ];

		// Loop through the depth, height, and width to instantiate cell clones
		for (int i = 0; i < sizeOfUniverse; i++) {
			for (int j = 0; j < sizeOfUniverse; j++) {
				for (int k = 0; k < sizeOfUniverse; k++) {
					// Instantiate clone
					GameObject cellClone = Instantiate(cell, pos, Quaternion.identity) as GameObject;

					isInsideStartingCube = ((pos.x >= gapToCenter + 1 && pos.x <= gapToCenter + innerCubeSize) &&
						(pos.y >= gapToCenter + 1 && pos.y <= gapToCenter + innerCubeSize) &&
						(pos.z >= gapToCenter + 1 && pos.z <= gapToCenter + innerCubeSize));

					if (isInsideStartingCube) {
						// Randomly generate the cell's alive state
						cellClone.GetComponent<Cell>().changeAliveState(rand.Next(0, 2));
					} else {
						// Spawn a dead cell
						cellClone.GetComponent<Cell>().changeAliveState(0);
					}
						
					// Add cell to the list
					cells[counter] = cellClone;
					// Add space for next cycle
					pos.x += spacer;
					counter++;
				}
				pos.x = 0;
				pos.y += spacer;
			}
			pos.x = 0;
			pos.y = 0;
			pos.z += spacer;
		}

		// Set the enforcer function
		switch (simulationRule)
		{
			case Rule.Amoeba:
				enforcerFunctionPtr = enforcerObj.EnforceAmoeba;
				break;
			case Rule.L445:
				enforcerFunctionPtr = enforcerObj.EnforceL445;
				break;
			case Rule.L678:
				enforcerFunctionPtr = enforcerObj.EnforceL678;
				break;
			case Rule.L1021:
				enforcerFunctionPtr = enforcerObj.EnforceL1021;
				break;
			case Rule.L3:
				enforcerFunctionPtr = enforcerObj.EnforceL3;
				break;
			case Rule.L23:
				enforcerFunctionPtr = enforcerObj.EnforceL23;
				break;
			case Rule.L5655:
				enforcerFunctionPtr = enforcerObj.EnforceL5655;
				break;
			case Rule.L5766:
				enforcerFunctionPtr = enforcerObj.EnforceL5766;
				break;
			case Rule.Builder:
				enforcerFunctionPtr = enforcerObj.EnforceBuilder;
				break;
			case Rule.Clouds1:
				enforcerFunctionPtr = enforcerObj.EnforceClouds1;
				break;
			case Rule.Clouds2:
				enforcerFunctionPtr = enforcerObj.EnforceClouds2;
				break;
			case Rule.Pyroclastic:
				enforcerFunctionPtr = enforcerObj.EnforcePyroclastic;
				break;
			case Rule.SlowDecay:
				enforcerFunctionPtr = enforcerObj.EnforceSlowDecay;
				break;
			case Rule.SpikyGrowth:
				enforcerFunctionPtr = enforcerObj.EnforceSpikyGrowth;
				break;
			case Rule.Experimenting:
				enforcerFunctionPtr = enforcerObj.EnforceExperimenting;
				break;
			default:
				break;
		}

		// Begin my Update loop
		StartCoroutine(updateGrid());
    }

	IEnumerator updateGrid() {

		iteration++;

		// Iterate through all the cells 
		for (int i = 0; i < cells.Length; i++) {
			// Cast a box 7 units in length from the current cell
			hitCollisions = Physics.OverlapBox(cells[i].transform.position, 
				colliderBox, 
				Quaternion.identity);

			// Get the amount of objects hit and subtract one as I am not using a mask and Unity will
			// count the cell itself in the collision
			totalCells = hitCollisions.Length - 1;

			if (totalCells == -1) {
				continue;
			}

			/*
			 * Enforce the GOL rules here:
			*/
			mirrorAliveState[i] = enforcerFunctionPtr(cells[i], totalCells);
		}

		// Pause 
		yield return new WaitForSeconds(0.3f);

		// A mirror array is used to ensure the cells aren't updating as I am iterating
		// through them
		for (int i = 0; i < cells.Length; i++) {
			if (mirrorAliveState[i] == 1) {
				cells[i].SetActive(true);
				cells[i].GetComponent<BoxCollider>().enabled = true;
				cells[i].GetComponent<Rigidbody>().isKinematic = true;
			}
			else if (mirrorAliveState[i] == 0) {
				cells[i].SetActive(false);
				cells[i].GetComponent<BoxCollider>().enabled = false;
				cells[i].GetComponent<Rigidbody>().isKinematic = false;
			}
		}

		iterationText.text = "Iteration: " + iteration;

		if (GameObject.FindGameObjectsWithTag("cell").Length != 0) {
			StartCoroutine(updateGrid());
		} else {
			yield return new WaitForSeconds(0.3f);
			SceneManager.LoadScene("SampleScene");
		}

	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
			SceneManager.LoadScene("SampleScene");
		}
    }

}
