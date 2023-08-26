using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Planet : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer _planetRebderer;
    
    [SerializeField]
    private PlanetColor _planetColor;
    [SerializeField]
    private TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    public int _numOfShips;
    [SerializeField]
    private float _shipSpwanRate;
    [SerializeField]
    private Transform _spawnPosition;
    
    // CR: 1. These don't need to be public - make them [SerializeField] private.
    //     2. Keep the prefab as 'SpaceShip' instead of 'GameObject', so you don't have to call GetComponent later.
    //        [SerializeField] private SpaceShip _shipPrefab;

    public PlanetState _planetState;
    public GameObject SelectionIndicator;
    public GameObject _shipPrefab;
    public GameObject _EnemyShip;
    public bool _isClicked = false;
    public bool _isFrendly = false; // CR: unused - delete.

    
    private float _spwanNewShipTimer;
    
    private int _startingShips = 100;
    private float _size;


    // CR: 1. you don't need both 'PlanetState' and 'PlanetColor' - delete one of them.
    //     2. Move the enum definition outside of the class, so you can use 'PlanetState.Friendly' in all of your code
    //        (and not 'Planet.PlanetState.Friendly').

    public enum PlanetState
    {
        Friendly,
        Enemy,
        Neutral
    }

    public enum PlanetColor
    {
        Red,
        Blue,
        Nuetral
    }

    private void Update()
    {
        if (_planetState == PlanetState.Friendly)
        {
            SpwanNewShipTimer();
        }
    }

    void SpwanNewShipTimer()
    {
        _spwanNewShipTimer += Time.deltaTime;
        if (_spwanNewShipTimer >= _shipSpwanRate)
        {
            _numOfShips++;
            _numOfshipText.text = _numOfShips.ToString();
            _spwanNewShipTimer = 0;
        }
    }

    void Start()
    {
        _size = GetComponent<Transform>().localScale.x; // CR: just 'transform.localScale.x', no need to GetComponent.
        _shipSpwanRate /= _size;
        _numOfShips = _startingShips;
        _numOfshipText.text = _numOfShips.ToString();
        
    }

    public void SetPlanetVisuals()
    {
        switch (_planetState)
        {
            case PlanetState.Enemy:
                _planetRebderer.material.color = Color.red;
                // CR: like we talked about in the lesson, tags are not a good way to handle state.
                //     You can remove this line.
                this.gameObject.tag = "EnemyPlanet"; 
                _planetState = PlanetState.Enemy; // Set state to enemy for enemy planets
                break;
            case PlanetState.Friendly:
                _planetRebderer.material.color = Color.blue;
                this.gameObject.tag = "FriendlyPlanet";
                _planetState = PlanetState.Friendly; // Set state to friendly for friendly planets
                break;
            case PlanetState.Neutral:
                _planetRebderer.material.color = Color.gray;
                this.gameObject.tag = "NeutralPlanet";
                _planetState = PlanetState.Neutral; // Set state to neutral for neutral planets
                break;
            default:
                break;
        }
    }


    public void DeployShips(Planet targetPlanet)
    {
        
        _numOfShips = _numOfShips / 2;
        _numOfshipText.text = _numOfShips.ToString();
        for (int i = 0; i < _numOfShips; i++)
        {
            // CR: if your prefab is 'SpaceShip' instead of 'GameObject', you can skip the 'GetComponent' call:
            //    SpaceShip ship = Instantiate<SpaceShip>(...);
            GameObject Ship = Instantiate(_shipPrefab, _spawnPosition.position, Quaternion.identity);
            Ship.GetComponent<SpaceShip>()._targetPlanet = targetPlanet;
        }
    }


    private void OnMouseEnter()
    {
        SelectionIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!_isClicked)
        {
            SelectionIndicator.SetActive(false);
        }
    }



    public void SetPlanetFriendlyState()
    {
        _planetState = PlanetState.Friendly;
        SetPlanetVisuals();
    }

    public void IncreaseNumber()
    {
        _numOfShips++;
        _numOfshipText.text = _numOfShips.ToString();
    }

    public void DecreaseNumber()
    {
        _numOfShips--;
        _numOfshipText.text = _numOfShips.ToString();
    }

    // CR: great! I suggest using a readonly-property for function without parameters *that don't change object state*.
    //     public bool isFriendly => _planetState == PlanetState.Friendly;
    public bool isFrendly()
    {
        return _planetState == PlanetState.Friendly;
    }

     public bool isEnemy()
    {
        return _planetState == PlanetState.Enemy;
    }

     public bool isNuetral()
    {
        return _planetState == PlanetState.Neutral;
    }

}
