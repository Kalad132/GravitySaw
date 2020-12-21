using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject _template;
    [SerializeField] private float _parallaxSpeed;
    [SerializeField] private Game _game;

    private float _offset = 82;
    private List<Transform> _blocks;
    private int _blocksAmount = 4;

    private void Awake()
    {
        _blocks = new List<Transform>();
        for (int i = 0; i < _blocksAmount; i++)
            AddBlock();   
    }

    private void Update()
    {
        if (_camera.transform.position.x - _offset * 2> _blocks[0].position.x)
            Extend();
    }

    public void FollowCamera(Vector3 distanse)
    {
            transform.Translate(-distanse * _parallaxSpeed * Time.deltaTime);
    }

    private Vector3 GetNewBlockPosition()
    {
        Vector3 position = new Vector3(-_offset, 0, 0);
        if (_blocks.Count == 0)
            return new Vector3(-_offset, 0, 0);
        else
            return new Vector3(_offset, 0, 0) + _blocks[_blocks.Count - 1].position;
    }

    private void AddBlock()
    {
        _blocks.Add(Instantiate(_template, GetNewBlockPosition(), Quaternion.identity, transform).transform);
    }

    private void Extend()
    {
        var tempBlock = _blocks[0];
        _blocks.RemoveAt(0);
        tempBlock.position = GetNewBlockPosition();
        _blocks.Add(tempBlock);
    }

}
