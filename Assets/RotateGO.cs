using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGO : MonoBehaviour
{

	public int rotationRate;

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0, rotationRate * Time.deltaTime, 0);
    }
}
