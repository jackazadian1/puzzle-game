using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject start;
    public GameObject end;

    public GameObject[] floors;

    public Vector3 startPos;

    bool notSwitchedYet = true;
    bool isChanging = false;
    bool atEnd = false;
	// Use this for initialization
	void Start () {
        floors = GameObject.FindGameObjectsWithTag("Floor");
        startPos = start.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(!notSwitchedYet)
        {
            if (!atEnd)
            {
                if (Vector3.Distance(start.transform.position, end.transform.position) >= 0.1f && isChanging)
                {
                    start.transform.position = Vector3.MoveTowards(start.transform.position, end.transform.position, 5f * Time.deltaTime);
                }
                else
                {
                    isChanging = false;
                    notSwitchedYet = true;
                    atEnd = !atEnd;

                    foreach (GameObject floor in floors)
                        floor.SendMessage("GetAdjacents");
                }
            }
            else
            {

                if (Vector3.Distance(start.transform.position, startPos) >= 0.1f && isChanging)
                {

                    start.transform.position = Vector3.MoveTowards(start.transform.position, startPos, 5f * Time.deltaTime); 
                }
                else
                {
                    isChanging = false;
                    notSwitchedYet = true;
                    atEnd = !atEnd;
                    foreach (GameObject floor in floors)
                        floor.SendMessage("GetAdjacents");
                }
            }
        }
        


	}

    public void StartSwitch()
    {
        isChanging = true;
        notSwitchedYet = false;
    }
}
