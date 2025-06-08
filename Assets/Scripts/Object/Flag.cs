using UnityEngine;

public class Flag : MonoBehaviour
{
    public void TurnOnActive()
    {
        gameObject.SetActive(true);
    }

    public void TurnOffInactive()
    {
        gameObject.SetActive(false);
    }
}