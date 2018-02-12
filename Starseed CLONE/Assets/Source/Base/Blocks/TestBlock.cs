using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Simple block that adds 1 block up z-axis */

public class TestBlock : MonoBehaviour {
    // public
    //public GameObject _highlight;
    //public GameObject _particle;
    public float _growTimeTaken;
    public float _startSize;
    public int _noSeedDrop;
    public LayerMask _layers;
    // private
    private GameObject _mainBlock;
    public int _seed;
    private GameObject _player;
    private float _blockSize;
    private bool _space;
    private bool _growDone;
    private BlockManager _blockManager;
    private IEnumerator _coroutine;
    private IEnumerator _ycoroutine;
    private Vector3 _startScale;
    private Vector3 _finalScale;
    private Vector3 _nPosition;
    private Vector3 _fPosition;

    public void Start()
    {
        _player = GameObject.Find("FPSController");
        _mainBlock = this.gameObject;
        _blockManager = _player.GetComponent<BlockManager>();
        //_highlight.SetActive(false);
        _blockSize = _mainBlock.transform.localScale.x;
        _startScale = new Vector3(_blockSize, _startSize, _blockSize);
        _finalScale = new Vector3(_blockSize, _blockSize, _blockSize);
        _space = true;
    }

    public void showHightlight()
    {
        //_highlight.SetActive(true);
    }

    public void hideHighlight()
    {
        //_highlight.SetActive(false);
    }

    public void breakBlock()
    {
        BlockControl _sManager = _player.GetComponent<BlockControl>();
        _mainBlock.SetActive(false);
        //Instantiate(_particle, _mainBlock.transform.position, _mainBlock.transform.rotation);
        _sManager.addSeed(_noSeedDrop);
        Destroy(this);
    }

	private Vector3 getBlockPosition()
	{
		return _mainBlock.transform.position;
	}

    public void plantSeed(int s)
    {
        _seed = s;
        growBlock(s);
    }

    private void growBlock(int sn)
    {
        GameObject nBlock = _blockManager.getBlock(sn);
        GameObject spawnedBlock = Instantiate(nBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = getBlockPosition();
        _fPosition = getBlockPosition();
        _nPosition.y += (_blockSize / 2);
        _fPosition.y += _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock,_nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
    }

    private IEnumerator GrowY(GameObject gBlock, Vector3 sy, Vector3 fy)
    {
        float i = 0f;
        float rate = 1.0f / _growTimeTaken;
        BlockControl bControl = _player.GetComponent<BlockControl>();
        while (i < 1)
        {
            bControl._checkPlant = false;
            i += Time.deltaTime * rate;
            gBlock.transform.position = Vector3.Lerp(sy, fy, i);
            //Debug.Log("i value is: " + i.ToString() + " and the position is: " + gBlock.transform.position.ToString());
            yield return null;
        }
        bControl._checkPlant = true;
    }

    private IEnumerator GrowScale(GameObject gBlock, Vector3 sScale, Vector3 fScale)
    {
        float i = 0f;
        float rate = 1.0f / _growTimeTaken;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            gBlock.transform.localScale = Vector3.Lerp(sScale, fScale, i);
            //Debug.Log("i value is: " + i.ToString() + " and the scale is: " + gBlock.transform.localScale.ToString() );
            yield return null;
        }
    }

    public int getSeed()
    {
        return _seed;
    }
    
    private void checkBlock()
    {
        float ny = _mainBlock.transform.position.y + (_blockSize / 2);
        Vector3 rayPos = new Vector3(_mainBlock.transform.position.x, ny, _mainBlock.transform.position.z);
        Vector3 dir = rayPos - _mainBlock.transform.position;
        Ray ray = new Ray(rayPos, dir);
        RaycastHit hit;
        _mainBlock.layer = 9;
        //Debug.DrawRay(rayPos * 10, dir, Color.green);
        if(Physics.Raycast(ray, out hit, 10, _layers))
        {
            _space = false;
            Debug.Log("It hit " + hit.transform.ToString());
        }
        else
        {
            _space = true;
        }
        _mainBlock.layer = 8;
        //Debug.Log("checkBlock() method went well. The _space bool is at" + _space.ToString());
    }

    private void checkGrow()
    {
        if (_mainBlock.transform.localScale == _finalScale)
        {
            _growDone = true;
        }
        else
        {
            _growDone = false;
        }
    }

    public void canPlant()
    {
        BlockControl _bControl = _player.GetComponent<BlockControl>();
        //Debug.Log("Checking if this block could plant seeds...");
        checkBlock();
        checkGrow();
        if(_space && _growDone)
        {
            _bControl.changePlantBool(true);
        }
        else
        {
            _bControl.changePlantBool(false);
        }
    }
}
