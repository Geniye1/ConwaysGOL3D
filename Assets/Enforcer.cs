using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enforcer : MonoBehaviour
{
 	
	public int EnforceAmoeba(GameObject cell, int totalCells) {
		
		if (!cell.activeSelf) {

			switch (totalCells) {
			case 5: case 6: case 7: case 12: case 13: case 15:
				return 1;
			default:
				return 0;
			}

		} else {

			if (totalCells >= 9 && totalCells <= 26) {
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforceL445(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells == 4) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {

			if (totalCells == 4) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceL678(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells >= 6 && totalCells <= 8) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {

			if (totalCells >= 6 && totalCells <= 8) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceL1021(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells >= 10 && totalCells <= 21) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {

			if (totalCells >= 10 && totalCells <= 21) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceL3(GameObject cell, int totalCells) {
		
		if (!cell.activeSelf) {
			
			if (totalCells == 3) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {

			if (totalCells == 3) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceL23(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells == 3 ) {
				return 1;
			} else {
				return 0;
			}

		} else {

			if (totalCells == 2 || totalCells == 3 ) {
				return 1;
			} else {
				return 0;
			}

		}

	}

	public int EnforceL5655(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells == 5 || totalCells == 6) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {

			if (totalCells == 5) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceL5766(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells == 6) { 
				return 1; 
			} else { 
				return 0; 
			}

		} else {
			
			if (totalCells >= 5 || totalCells <= 7) { 
				return 1; 
			} else { 
				return 0; 
			}

		}
	}

	public int EnforceBuilder(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			switch (totalCells) {
				case 4: case 6: case 8: case 9:
					return 1;
				default:
					return 0;
			}

		} else {

			switch (totalCells) {
				case 2: case 6: case 9:
					return 1;
				default:
					return 0;
			}

		}
	}

	public int EnforceClouds1(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			switch (totalCells) {
				case 13: case 14: case 17: case 18: case 19:
					return 1;
				default:
					return 0;
			}

		} else {

			if (totalCells >= 13 && totalCells <= 26) {
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforceClouds2(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			switch (totalCells) {
			case 13: case 14: 
				return 1;
			default:
				return 0;
			}

		} else {

			if (totalCells >= 13 && totalCells <= 26) {
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforcePyroclastic(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells >= 6 && totalCells <= 8) {
				return 1;
			} else {
				return 0;
			}

		} else {

			if (totalCells >= 4 && totalCells <= 7) {
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforceSlowDecay(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells >= 13 && totalCells <= 26) {
				return 1;
			} else {
				return 0;
			}

		} else {

			if (totalCells == 1 || totalCells == 4 || totalCells == 8 || 
							(totalCells >= 13 && totalCells <= 26)) {
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforceSpikyGrowth(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if ( (totalCells == 4) || (totalCells == 13) || (totalCells == 17) ||
				(totalCells >= 20 && totalCells <= 24) || (totalCells == 26) ) {
				return 1;
			} else {
				return 0;
			}

		} else {

			if ( (totalCells >= 0 && totalCells <= 3) || (totalCells >= 7 && totalCells <= 9) ||
				(totalCells >= 11 && totalCells <= 13) || (totalCells == 18) || 
				(totalCells >= 21 && totalCells <= 22) || (totalCells == 24) ||
				(totalCells == 26) ) {
				
				return 1;
			} else {
				return 0;
			}

		}
	}

	public int EnforceExperimenting(GameObject cell, int totalCells) {

		if (!cell.activeSelf) {

			if (totalCells == 3) {
				return 1;
			} else {
				return 0;
			}

		} else {

			if (totalCells == 3 || totalCells == 15 ) {
				return 1;
			} else {
				return 0;
			}

		}
	}

}
