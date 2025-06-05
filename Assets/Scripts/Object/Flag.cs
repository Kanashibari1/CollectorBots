using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}