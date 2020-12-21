using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldBlock : MonoBehaviour
{
    public GameObject GameObject => gameObject;
    public Vector3 Position { get { return gameObject.transform.position; } set { gameObject.transform.position = value; } }

    private Respawnable[] _respawnables;

    private void Awake()
    {
        _respawnables = GetComponentsInChildren<Respawnable>();
    }

    private void OnEnable()
    {
        Respawn();
    }

    private void Respawn()
    {
        foreach (var respanable in _respawnables)
            respanable.Respawn();
    }
}
