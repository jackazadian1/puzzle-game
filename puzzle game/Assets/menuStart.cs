using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuStart : MonoBehaviour {

    public Text input;
    public Toggle toggle;

    public void ChangeMenuScene(string sceneName)
    {
        PlayerStats.name = input.GetComponent<Text>().text;
        PlayerStats.isGamer = toggle.GetComponent<Toggle>().isOn;
        Application.LoadLevel(sceneName);
    }
}
