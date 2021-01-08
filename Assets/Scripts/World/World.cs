using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private WorldBlock _firstBlock;
    [SerializeField] [Min(4)] private int _maxActiveBlocksAmount;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private Game _game;

    private System.Random _random = new System.Random();
    private List<WorldBlock> _blocks;
    private List<WorldBlock> _unusedBlocks;
    private Vector3 _newBlockOffset = new Vector3(121.6f, 0, 0);
    private float _newBlockDistance = 150;
    private bool _isNewBlockNeeded { get => _player.position.x + _newBlockDistance > _blocks[_blocks.Count - 1].Position.x; }

    private void OnEnable()
    {
        _game.Restarted.AddListener(Restart);
    }
    private void OnDisable()
    {
        _game.Restarted.RemoveListener(Restart);
    }

    private void Start()
    {
        _blocks = new List<WorldBlock>();
        _blocks.Add(_firstBlock);
        _unusedBlocks = new List<WorldBlock>();
        foreach (GameObject template in _templates) 
        {
            var block = Instantiate(template, transform).GetComponent<WorldBlock>();
            block.GameObject.SetActive(false);
            _unusedBlocks.Add(block);
        }
    }


    private void Update()
    {
        if (_isNewBlockNeeded)
        {
            if (_unusedBlocks.Count == 0 || _blocks.Count == _maxActiveBlocksAmount)
                DeactivateOldestBlock();
            ActivateRandomBlock();
        }
    }

    private void Restart()
    {
        int blocks = _blocks.Count;
        for (int i = 0; i < blocks; i++)
            DeactivateOldestBlock();
        _firstBlock.gameObject.SetActive(true);
        _blocks.Add(_firstBlock);
    }

    private void DeactivateOldestBlock()
    {
        _blocks[0].GameObject.SetActive(false);
        if (_blocks[0] != _firstBlock)
            _unusedBlocks.Add(_blocks[0]);
        _blocks.RemoveAt(0);
    }

    private void ActivateRandomBlock()
    {
        int index = _random.Next(0, _unusedBlocks.Count);
        var block = _unusedBlocks[index];
        block.GameObject.SetActive(true);
        block.Position = GetNewBlockPosition();
        _blocks.Add(block);
        _unusedBlocks.RemoveAt(index);
    }

    private Vector3 GetNewBlockPosition()
    {
        return _blocks[_blocks.Count - 1].Position + _newBlockOffset;
    }

}
