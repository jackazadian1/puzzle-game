using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject[] adjacents = new GameObject[4];

    public bool active = false;

	// Use this for initialization
	void Start () {

        GetAdjacents();
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

    public void GetAdjacents()
    {
        Vector3 pos = transform.position;
        GameObject temp = null;
        temp = FindAt(new Vector3(pos.x + 2, pos.y, pos.z));
        if (temp != null)
        {
            if (temp.tag != "Metal" && temp.tag!="Switch")
                adjacents[0] = temp;
            else
                adjacents[0] = null;
        }
        else
            adjacents[0] = null;

        temp = FindAt(new Vector3(pos.x, pos.y, pos.z + 2));
        if (temp != null)
        {
            if (temp.tag != "Metal" && temp.tag != "Switch")
                adjacents[1] = temp;
            else
                adjacents[1] = null;
        }
        else
            adjacents[1] = null;

        temp = FindAt(new Vector3(pos.x - 2, pos.y, pos.z));
        if (temp != null)
        {
            if (temp.tag != "Metal" && temp.tag != "Switch")
                adjacents[2] = temp;
            else
                adjacents[2] = null;
        }
        else
            adjacents[2] = null;

        temp = FindAt(new Vector3(pos.x, pos.y, pos.z - 2));
        if (temp != null)
        {
            if (temp.tag != "Metal" && temp.tag != "Switch")
                adjacents[3] = temp;
            else
                adjacents[3] = null;
        }
        else
            adjacents[3] = null;
    }
}
