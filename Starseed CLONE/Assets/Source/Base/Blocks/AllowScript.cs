using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowScript : MonoBehaviour {
    public GameObject _myBlock;
    private TestBlock _tbScript;

    public void Start()
    {
        _tbScript = _myBlock.gameObject.GetComponent<TestBlock>();
        _tbScript.enabled = true;
    }

    public void setScript(bool set)
    {
        _tbScript.enabled = set;
    }
}
