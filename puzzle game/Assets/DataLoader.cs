using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		WWW playerData = new WWW("http://localhost/gsnd_6320/gsnd_6320.php");
        yield return playerData;
        string data = playerData.text;
        Debug.Log(data);
    }
	
}
