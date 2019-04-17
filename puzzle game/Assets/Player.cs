using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoGraph;
using MonoGraph.Algorithms;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Player : MonoBehaviour {

    CharacterController cc;

    public float speed = 10f;

    //gravity stuff
    float ySpeed = 0;
    float gravity = -15f;

    public Transform fpsCamera;

    float pitch = 0f;
    AdjacencyListGraph<string, Edge<string>> floorGraph;

    GameObject lastHit = null;

    bool hasWon = false;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();

        floorGraph = new AdjacencyListGraph<string, Edge<string>>();
        floorGraph.AddVertex("Start");
    }
	
	// Update is called once per frame
	void Update () {

        float xInput = Input.GetAxis("Horizontal") * speed;
        float zInput = Input.GetAxis("Vertical") * speed;

        Vector3 move = new Vector3(xInput, 0, zInput);
        move = Vector3.ClampMagnitude(move, speed);
        move = transform.TransformVector(move);

        if(cc.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                ySpeed = 15f;
            }
            else
            {
                ySpeed = gravity*Time.deltaTime;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        cc.Move((move + new Vector3(0,ySpeed,0)) * Time.deltaTime);

        float xMouse = Input.GetAxis("Mouse X")* 2f;

        transform.Rotate(0, xMouse, 0);

        pitch -= Input.GetAxis("Mouse Y") * 2f;
        pitch = Mathf.Clamp(pitch,25f,60f);

        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);

        fpsCamera.localRotation = camRotation;
        
        if(!hasWon)
            checkWin();
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject body = hit.gameObject;
        if(lastHit == body)
        {
            return;
        }
        lastHit = body;
        if (body.tag == "Floor" || body.tag == "End")
        {
            Floor floor = body.GetComponent<Floor>();
            
            if (floor.active == false)
                foreach (GameObject element in floor.adjacents)
                {
                    if (element != null)
                    {
 
                        Floor adjacent = element.GetComponent<Floor>();
                        if (adjacent.active == true)
                        {
                            floorGraph.AddVertex(body.name);
                            floorGraph.AddBidirectionalEdge(new Edge<string>(body.name, element.name));
                            floor.active = true;
                            Material mat = body.GetComponent<Renderer>().material;
                            if(body.tag == "Floor")
                                mat.color = new Color(255, 255, 0);
                            else
                                mat.color = new Color(0, 255, 0);

                            break;
                        }
                    }
                }

        }
        else if(body.tag == "Switch")
        {
            body.SendMessage("StartSwitch");
        }

        
    }

    void checkWin()
    {
        if (floorGraph.ContainsVertex("End"))
        {
            hasWon = true;
            String time = Time.realtimeSinceStartup +"";

            //addData(PlayerStats.name, time.Substring(0,5), PlayerStats.isGamer + "");

            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }

    public void addData(string ID, string time, string isGamer)
    {
        WWWForm form = new WWWForm();
        form.AddField("IDPost",ID+ Application.loadedLevel);
        form.AddField("timespentPost", time);
        form.AddField("isGamerPost", isGamer);

        WWW www = new WWW("https://gsnd-6320.000webhostapp.com/gsnd_6320.php", form);
    }

}
