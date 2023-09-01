using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum PlanetState
{
    Friendly,
    Enemy,
    Neutral
}

public class Planet : MonoBehaviour
{


    [SerializeField]
    public TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    public int _numOfShips;
    [SerializeField]
    private float _shipSpwanRate;
    [SerializeField]
    private Transform _spawnPosition;

    private PlanetState _planetState;
    private SpriteRenderer _planetRebderer;



    // CR: 1. These don't need to be public - make them [SerializeField] private.
    //     2. Keep the prefab as 'SpaceShip' instead of 'GameObject', so you don't have to call GetComponent later.
    //        [SerializeField] private SpaceShip _shipPrefab;


    public GameObject SelectionIndicator;
    public SpaceShip _shipPrefab;

    public bool _isClicked = false;
    private float _spwanNewShipTimer;

    private int _startingShips = 100;
    private float _size;


    private void Update()
    {
        ShipSpwanUpdate();
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

        ShipUpdate();

        _size = transform.localScale.x; // CR: just 'transform.localScale.x', no need to GetComponent.
        _shipSpwanRate /= _size;
        _numOfShips = _startingShips;
        _numOfshipText.text = _numOfShips.ToString();
    }



    void ShipUpdate()
    {
        if (isFrendly || isEnemy)
        {
            _size = GetComponent<Transform>().localScale.x;
            _shipSpwanRate /= _size;
            _numOfShips = _startingShips;
            _numOfshipText.text = _numOfShips.ToString();
        }
        else
        {
            _numOfShips = Random.Range(10, 50);
            _numOfshipText.text = _numOfShips.ToString();
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
            SpaceShip Ship = Instantiate(_shipPrefab, _spawnPosition.position, Quaternion.identity);
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
    public bool isFrendly => _planetState == PlanetState.Friendly;


    public bool isEnemy => _planetState == PlanetState.Enemy;


    public bool isNuetral => _planetState == PlanetState.Neutral;


    void ShipSpwanUpdate()
    {
        if (_planetState == PlanetState.Friendly || _planetState == PlanetState.Enemy)
        {
            SpwanNewShipTimer();
        }
    }

    public void SetPlanetVisuals()
    {
        switch (_planetState)
        {
            case PlanetState.Enemy:
                _planetRebderer.material.color = Color.red;
                _planetState = PlanetState.Enemy; // Set state to enemy for enemy planets
                break;
            case PlanetState.Friendly:
                _planetRebderer.material.color = Color.blue;
                _planetState = PlanetState.Friendly; // Set state to friendly for friendly planets
                break;
            case PlanetState.Neutral:
                _planetRebderer.material.color = Color.gray;
                _planetState = PlanetState.Neutral; // Set state to neutral for neutral planets
                break;
            default:
                break;
        }
    }


    public void SetPlanetFriendlyState()
    {
        _planetState = PlanetState.Friendly;
        SetPlanetVisuals();
    }

     public void SetPlanetEnemyState()
     {
      _planetState = PlanetState.Enemy;
      SetPlanetVisuals();
     }
}
