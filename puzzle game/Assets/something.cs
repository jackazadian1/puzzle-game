using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class something : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LineRenderer lr = GetComponent<LineRenderer>();
        Vector3[] dots = new Vector3[3];
        dots[0] = Vector3.zero;
        dots[1] = Vector3.up;
        dots[2] = Vector3.right;
        lr.SetPositions(dots);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
