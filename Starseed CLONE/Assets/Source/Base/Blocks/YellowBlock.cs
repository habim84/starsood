using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO - Repair grow block mechanism

public class YellowBlock : MonoBehaviour {
    public GameObject _highlight;
    public GameObject _newBlock;
    public GameObject _particle;
    public float _growSpeed;
    public int _stackNo;
    private bool _isGrow;

    // public
    public float _growTimeTaken;
    public float _startSize;
    public int _noSeedDrop;
    public LayerMask _layers;
    // private
    private GameObject _mainBlock;
    public int _seed;
    private GameObject _player;
    private float _blockSize;
    private bool _spaceTop;
    private bool _spaceBottom;
    private bool _spaceLeft;
    private bool _spaceRight;
    private bool _spaceFront;
    private bool _spaceBack;
    private bool _growDone;
    private BlockManager _blockManager;
    private IEnumerator _coroutine;
    private IEnumerator _ycoroutine;
    private Vector3 _startScale;
    private Vector3 _finalScale;
    private Vector3 _nPosition;
    private Vector3 _fPosition;
    private int _blockCounter;

    public void Start()
    {
        _player = GameObject.Find("FPSController");
        _mainBlock = this.gameObject;
        _blockManager = _player.GetComponent<BlockManager>();
        _blockSize = _mainBlock.transform.localScale.x;
        _startScale = new Vector3(_blockSize, _startSize, _blockSize);
        _finalScale = new Vector3(_blockSize, _blockSize, _blockSize);
        _spaceTop = true;
        _spaceBottom = true;
        _spaceLeft = true;
        _spaceRight = true;
        _spaceFront = true;
        _spaceBack = true;
        _blockCounter = 0;
        startGrow();
    }

    private void startGrow()
    {
        Vector3 _startPosition = getBlockPosition();
        GameObject oldBlock = _mainBlock;
        int counter = 0;
        while (counter < _stackNo)
        {
            checkBlock(oldBlock.transform.position, 1);
            checkBlock(oldBlock.transform.position, 2);
            checkBlock(oldBlock.transform.position, 3);
            checkBlock(oldBlock.transform.position, 4);
            checkGrow(oldBlock);
            if(_spaceFront && _spaceRight && _spaceLeft && _spaceBack && _spaceTop && _growDone)
            {
                oldBlock = growBlockTop(_startPosition);
                _startPosition.y += _blockSize;
                counter++;
            }
            else if(_spaceTop && _spaceRight && _spaceFront)
            {
                oldBlock = growBlockDiagonalFR(_startPosition);
                _startPosition.x += _blockSize;
                _startPosition.z += _blockSize; 
                counter++;
            }
            else if(_spaceTop && _spaceLeft && _spaceFront)
            {
                oldBlock = growBlockDiagonalFL(_startPosition);
                _startPosition.x -= _blockSize;
                _startPosition.z += _blockSize;
                counter++;
            }
            else if(_spaceTop && _spaceRight && _spaceBack)
            {
                oldBlock = growBlockDiagonalBR(_startPosition);
                _startPosition.x += _blockSize;
                _startPosition.z -= _blockSize;
                counter++;
            }
            else if(_spaceTop && _spaceLeft && _spaceBack)
            {
                oldBlock = growBlockDiagonalBL(_startPosition);
                _startPosition.x -= _blockSize;
                _startPosition.z -= _blockSize;
                counter++;
            }
        }
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
        // TODO - Do something that informs the player that this block is unbreakable
    }

    private Vector3 getBlockPosition()
    {
        return _mainBlock.transform.position;
    }

    private GameObject growBlockTop(Vector3 sPos)
    {
        GameObject spawnedBlock = Instantiate(_newBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = sPos;
        _fPosition = getBlockPosition();
        _nPosition.y += (_blockSize / 2);
        _fPosition.y += _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock, _nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
        return spawnedBlock;
    }

    private GameObject growBlockDiagonalFR(Vector3 sPos)
    {
        // TODO - Front Right Diagonal Direction
        GameObject spawnedBlock = Instantiate(_newBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = sPos;
        _fPosition = getBlockPosition();
        _nPosition.x += (_blockSize / 2);
        _fPosition.x += _blockSize;
        _nPosition.z += (_blockSize / 2);
        _fPosition.z += _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock, _nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
        return spawnedBlock;
    }

    private GameObject growBlockDiagonalFL(Vector3 sPos)
    {
        // TODO - Front Left Diagonal Direction
        GameObject spawnedBlock = Instantiate(_newBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = sPos;
        _fPosition = getBlockPosition();
        _nPosition.x -= (_blockSize / 2);
        _fPosition.x -= _blockSize;
        _nPosition.z += (_blockSize / 2);
        _fPosition.z += _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock, _nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
        return spawnedBlock;
    }

    private GameObject growBlockDiagonalBR(Vector3 sPos)
    {
        // TODO - Back Right Diagonal Direction
        GameObject spawnedBlock = Instantiate(_newBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = sPos;
        _fPosition = getBlockPosition();
        _nPosition.x += (_blockSize / 2);
        _fPosition.x += _blockSize;
        _nPosition.z -= (_blockSize / 2);
        _fPosition.z -= _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock, _nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
        return spawnedBlock;
    }

    private GameObject growBlockDiagonalBL(Vector3 sPos)
    {
        // TODO - Back Left Diagonal Direction
        GameObject spawnedBlock = Instantiate(_newBlock, _nPosition, _mainBlock.transform.rotation);
        _nPosition = sPos;
        _fPosition = getBlockPosition();
        _nPosition.x -= (_blockSize / 2);
        _fPosition.x -= _blockSize;
        _nPosition.z -= (_blockSize / 2);
        _fPosition.z -= _blockSize;
        spawnedBlock.transform.localScale = _startScale;
        _coroutine = GrowScale(spawnedBlock, _startScale, _finalScale);
        _ycoroutine = GrowY(spawnedBlock, _nPosition, _fPosition);
        StartCoroutine(_coroutine);
        StartCoroutine(_ycoroutine);
        return spawnedBlock;
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
            yield return null;
        }
    }

    private void checkBlock(Vector3 blockPos, int myDir)
    {
        float np;
        Vector3 rayPos;
        Vector3 dir;
        Ray ray;
        RaycastHit hit;
        switch(myDir)
        {
            // front
            case 1:
                np = blockPos.z + (_blockSize / 2);
                rayPos = new Vector3(blockPos.x, blockPos.y, np);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if(Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceFront = false;
                }
                else
                {
                    _spaceFront = true;
                }
                break;
            // back
            case 2:
                np = blockPos.z - (_blockSize / 2);
                rayPos = new Vector3(blockPos.x, blockPos.y, np);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if (Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceBack = false;
                }
                else
                {
                    _spaceBack = true;
                }
                break;
            // left
            case 3:
                np = blockPos.x - (_blockSize / 2);
                rayPos = new Vector3(np, blockPos.y, blockPos.z);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if (Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceLeft = false;
                }
                else
                {
                    _spaceLeft = true;
                }
                break;
            // right
            case 4:
                np = blockPos.x + (_blockSize / 2);
                rayPos = new Vector3(np, blockPos.y, blockPos.z);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if (Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceRight = false;
                }
                else
                {
                    _spaceRight = true;
                }
                break;
            // top
            case 5:
                np = blockPos.y + (_blockSize / 2);
                rayPos = new Vector3(blockPos.x, np, blockPos.z);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if (Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceTop = false;
                }
                else
                {
                    _spaceTop = true;
                }
                break;
            // bottom
            case 6:
                np = blockPos.y - (_blockSize / 2);
                rayPos = new Vector3(blockPos.x, np, blockPos.z);
                dir = rayPos - blockPos;
                ray = new Ray(rayPos, dir);
                if (Physics.Raycast(ray, out hit, 10, _layers))
                {
                    _spaceBottom = false;
                }
                else
                {
                    _spaceBottom = true;
                }
                break;
            default:
                Debug.Log("WHAT DIRECTION ARE YOU LOOKING AT?????");
                break;
        }
    }

    private void checkGrow(GameObject curBlock)
    {
        if (curBlock.transform.localScale == _finalScale)
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
        _bControl.changePlantBool(false);
    }
}
