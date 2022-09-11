using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnOnObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameobject;

    public void TurnOn(bool value)
    {
        gameobject.SetActive(value);
    }
}
