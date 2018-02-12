using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	
	public List<GameObject> _blockObj = new List<GameObject>();
	public GameObject[] _blockPrefabs = new GameObject[100];
    private int _blockCount;

	// Use this for initialization
	void Start ()
    {
        _blockCount = 0;
	}
	
	public void addBlock(GameObject newBlock)
	{
		_blockObj.Add(newBlock);
        _blockCount++;
	}

    public GameObject getBlock(int t)
    {
        GameObject returnBlock;
        returnBlock = _blockPrefabs[t];
        return returnBlock;
    }
}
