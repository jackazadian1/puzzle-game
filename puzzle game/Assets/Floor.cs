using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject[] adjacents = new GameObject[4];

    public bool active = false;

	// Use this for initialization
	void Start () {

        Vector3 pos = transform.position;
        adjacents[0] = FindAt(new Vector3(pos.x + 2, pos.y, pos.z));
        adjacents[1] = FindAt(new Vector3(pos.x, pos.y, pos.z + 2));
        adjacents[2] = FindAt(new Vector3(pos.x - 2, pos.y, pos.z));
        adjacents[3] = FindAt(new Vector3(pos.x, pos.y, pos.z - 2));

        if(gameObject.tag == "Start")
        {
            active = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private GameObject FindAt(Vector3 pos)
    {
        var cols = Physics.OverlapSphere(pos, 0.1f);

        float dist = Mathf.Infinity;

        GameObject nearest = null;

        foreach(Collider element in cols)
        {
            var d = Vector3.Distance(pos, element.transform.position);
            if (d < dist)
            { // if closer...
                dist = d; // save its distance... 
                nearest = element.gameObject; // and its gameObject
            }
        }

        return nearest;
    }
}
