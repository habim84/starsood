using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class BlockControl : MonoBehaviour {
    // public
    public int _distanceCast;
    public int _maxSeedsHeld;
    public int _maxSeedTypes;
    public Camera _cam;
    public Text _textView;
    public Text _canPlantUI;
    public bool _checkPlant;
    //private
    private Transform _selectedBlock;
    private RaycastHit _hit;
    private Transform _oldBlock;
    private BlockControl _bControl;
    private bool _plantable;
    public int[] _seedArr;
    private int _curNoSeed;
    private SlowBlock _blockCode;
    private GameObject _player;
    private FirstPersonController _controller;
    [SerializeField] private int _slowSpeed;
    [SerializeField] private int _normalSpeed;
    [SerializeField] private int _highJumpSpeed;
    [SerializeField] private int _normalJumpSpeed;


    public void Start()
    {
        _player = GameObject.Find("FPSController");
        _controller = _player.GetComponent<FirstPersonController>();
        _seedArr = new int[_maxSeedsHeld];
        _curNoSeed = 5;
        _checkPlant = false;
        for (int i = 0; i < _curNoSeed; i++)
        {
            _seedArr[i] = Random.Range(0, _maxSeedTypes);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "slow_block")
        {
            Debug.Log("I'm on top of the green block!");
            _blockCode = collision.gameObject.GetComponent<SlowBlock>();
            _blockCode.onSurface();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _blockCode.exitSurface();
    }*/

    public void changePlantBool(bool nBool)
    {
        _checkPlant = nBool;
    }

    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        _textView.text = _curNoSeed.ToString();
        if(Physics.Raycast(ray, out _hit))
        {
            _oldBlock = _hit.transform;
            //Debug.Log("Current Object Selected: " + _oldBlock.ToString());
            //_oldBlock.SendMessageUpwards("showHighlight");
            _oldBlock.SendMessageUpwards("canPlant");
            if(_checkPlant)
            {
                //Debug.Log("checkPlant is true :)");
                _canPlantUI.gameObject.SetActive(true);
            }
            else
            {
                //Debug.Log("checkPlant is false :(");
                _canPlantUI.gameObject.SetActive(false);
            }
            /*if(Input.GetButtonDown("USE"))
            {
                _oldBlock.SendMessageUpwards("interact");
            }*/
            // EDIT HERE
            if (Input.GetButtonDown("PlantSeed") && _checkPlant && _curNoSeed > 0)
            {
                _selectedBlock = _oldBlock;
                //Debug.Log("selectedBlock = " + _selectedBlock.ToString());
                _selectedBlock.SendMessageUpwards("plantSeed", _seedArr[0]);
                getNewSeed();
            }
            else if(Input.GetMouseButtonDown(0))
            {
                _oldBlock.SendMessageUpwards("breakBlock");
            }
        }
        else if(Physics.Raycast(ray, out _hit) == false)
        {
            //_oldBlock.SendMessageUpwards("hideHighlight");
            _oldBlock = null;
            _selectedBlock = null;
            _checkPlant = false;
            _canPlantUI.gameObject.SetActive(false);
        }
    }

    public Transform getBlock()
    {
        return _oldBlock;
    }

    private void getNewSeed()
    {
        if(_curNoSeed > 0)
        {
            for (int i = 0; i < _curNoSeed; i++)
            {
                _seedArr[i] = _seedArr[i+1];
            }
            _curNoSeed--;
        }
    }

    public void addSeed(int n)
    {
        // TODO: When n > 1
        int _oldPos = _curNoSeed - 1;
        //Debug.Log("oldPos = " + _oldPos.ToString() + " _curNoSeed = " + _curNoSeed.ToString());
        _curNoSeed += n;
        if(_oldPos > 0)
        {
            for (int i = _oldPos; i < _curNoSeed; i++)
            {
                _seedArr[i] = Random.Range(0, _maxSeedTypes);
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                _seedArr[i] = Random.Range(0, _maxSeedTypes);
            }
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "slow_block")
        {
            //Debug.Log("Touching Slow Block");
            _controller.setSpeed(_slowSpeed);
        }

        else if(hit.gameObject.tag == "jump_block")
        {
            _controller.setJumpSpeed(_highJumpSpeed);
        }

        else
        {
            //Debug.Log("Not Touching Slow Block");
            _controller.setSpeed(_normalSpeed);
            _controller.setJumpSpeed(_normalJumpSpeed);
        }
    }
}