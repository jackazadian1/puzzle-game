using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject start;
    public GameObject end;

    public GameObject[] floors;

    bool notSwitchedYet = true;
    bool isChanging = false;
	// Use this for initialization
	void Start () {
        floors = GameObject.FindGameObjectsWithTag("Floor");
	}
	
	// Update is called once per frame
	void Update () {
        if(!notSwitchedYet)
        {
            if (Vector3.Magnitude(start.transform.position - end.transform.position) >= 0.1 && isChanging)
                start.transform.Translate(Vector3.down * 10 * Time.deltaTime);
            else
            {
                Debug.Log("Hello");
                isChanging = false;
                notSwitchedYet = true;
                foreach(GameObject floor in floors)
                    floor.SendMessage("GetAdjacents");
            }
        }
        


	}

    public void StartSwitch()
    {
        isChanging = true;
        notSwitchedYet = false;
    }
}
