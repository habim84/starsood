using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Currently simple level creation where a large array of cubes created as a big cube*/

public class TemporaryLevelSetup : MonoBehaviour {
    public int _blockSize;
    public int _blockCountX;
    public int _blockCountY;
    public int _blockCountZ;
    public GameObject _block;
    public GameObject _spawnPosition;
    // Use this for initialization
    void Start ()
    {
        _blockCountX = 0;
        _blockCountY = 0;
        _blockCountZ = 0;
        _blockSize = Random.Range(4, 10);
        Vector3 _pos = _spawnPosition.transform.position;
        _block.transform.localScale = new Vector3(2,2,2);
        // z-axis
        for(int k = 0; k < _blockSize; k++)
        {
            _pos.z = (_blockCountZ * _block.transform.localScale.z);
            _blockCountZ++;
            _blockCountY = 0;
            // y-axis
            for(int j = 0; j < _blockSize; j++)
            {
                _pos.y = (_blockCountY * _block.transform.localScale.y) * -1;
                _blockCountY++;
                _blockCountX = 0;
                // x-axis
                for (int i = 0; i < _blockSize; i++)
                {
                    _pos.x = (_blockCountX * _block.transform.localScale.x);
                    Instantiate(_block, _pos, _spawnPosition.transform.rotation);
                    _blockCountX++;
                }
            }
        }
	}
}
