using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_VN : MonoBehaviour
{
	// 3 spacer and 7, 7, 7
	/*
	 * PUBLIC VARIABLES 
	*/ 
	// ------------------------------------------------

	// Cell prefab
	[Header("Cell Prefab")]
	public GameObject cell;

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

	// Text object to track the number of iterations in the current sim
	[Header("Iteration Text object")]
	public Text iterationText;

	/* 
	 * PRIVATE VARIABLES
	*/
	// -------------------------------------------------

	// List of cells 
	private GameObject[] cells;

	// Mirror list of cells' alive state 
	private int[] mirrorAliveState;

	// Gap between the outer cube and the inner cube
	private float gapToCenter;

	private int totalCells;

	// Random class for populating cells 
	private System.Random rand = new System.Random();

	// Counter variable to track the array indices
	private int counter = 0;

	// Iteration variable to send to the Text element
	private int iteration = 0;

	// Raycast output variables: Front, Back, Left, Right, Up, Down
	private int F, B, L, R, U, D; 

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

		// Begin my Update loop
		StartCoroutine(updateGrid());
    }

	IEnumerator updateGrid() {

		iteration++;

		// Iterate through all the cells 
		for (int i = 0; i < cells.Length; i++) {

			cells[i].GetComponent<BoxCollider>().enabled = false;

			F = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.forward, 1f));
			B = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.back, 1f));
			L = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.left, 1f));
			R = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.right, 1f));
			U = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.up, 1f));
			D = Convert.ToInt32(Physics.Raycast(cells[i].transform.position, Vector3.down, 1f));

			cells[i].GetComponent<BoxCollider>().enabled = true;

			totalCells = F + B + L + R + U + D;

			if (totalCells == 0) {
				continue;
			}

			/*
			 * Enforce the GOL rules here:
			*/

			if (!cells[i].activeSelf) {

				if (totalCells == 1 || totalCells == 3) {
					mirrorAliveState[i] = 1;
				} else {
					mirrorAliveState[i] = 0;
				}

			} else {

				if (totalCells >= 0 && totalCells <= 6) {
					mirrorAliveState[i] = 1;
				} else {
					mirrorAliveState[i] = 0;
				}

			}
		}

		// Pause 
		yield return new WaitForSeconds(0.3f);

		// A mirror array is used to ensure the cells aren't updating as I am iterating
		// through them
		for (int i = 0; i < cells.Length; i++) {
			if (mirrorAliveState[i] == 1) {
				cells[i].SetActive(true);
				cells[i].GetComponent<BoxCollider>().enabled = true;
			}
			else if (mirrorAliveState[i] == 0) {
				cells[i].SetActive(false);
				cells[i].GetComponent<BoxCollider>().enabled = false;
			}
		}

		iterationText.text = "Iteration: " + iteration;

		if (GameObject.FindGameObjectsWithTag("cell").Length != 0) {
			StartCoroutine(updateGrid());
		} else {
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("SampleScene");
		}

	}

	/*
	void OnDrawGizmos() {
		Gizmos.color = Color.green;

		Gizmos.DrawWireCube(transform.position, new Vector3(3, 1, 1));
	}
	*/
}
