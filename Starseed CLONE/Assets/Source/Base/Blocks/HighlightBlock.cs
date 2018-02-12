using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBlock : MonoBehaviour {
    public GameObject _highlightBlock;
    public Material _yesPlant;
    public Material _noPlant;
    private bool _isUse;

    public void Start()
    {
        _isUse = true;
    }

    public void Update()
    {
        // Adjust highlighting on the block
        if(_isUse)
        {
            //_highlightBlock.renderer.material = _yesPlant;
        }
        else
        {
            //_highlightBlock.renderer.material = _noPlant;
        }
    }

    public void setUse(bool nU)
    {
        _isUse = nU;
    }
}
