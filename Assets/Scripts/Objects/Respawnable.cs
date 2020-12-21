using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Vector3 _startingPosition;

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
