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

// CR: [discuss] private vs public.

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
    [SerializeField]
    private SpaceShip _shipPrefab;
    
    // CR: [discuss] state
    public GameObject SelectionIndicator;

    public PlanetState _planetState;
    private SpriteRenderer _spriteRenderer;
    public bool isClicked = false;
    private float _spwanNewShipTimer;
    private int _startingShips = 100; // CR: no defaults in the code - use [SerializeField]
    private float _size;

    private void Update()
    {
        ShipSpwanUpdate();
    }

    void SpwanNewShipTimer()
    {
        // CR: [discuss] rate vs time.
        _spwanNewShipTimer += Time.deltaTime;
        if (_spwanNewShipTimer >= _shipSpwanRate)
        {
            _numOfShips++;
            _numOfshipText.text = _numOfShips.ToString();
            _spwanNewShipTimer = 0;
        }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ShipUpdate();
    }

    void ShipUpdate()
    {
        if (isFrendly || isEnemy)
        {
            // CR: "GetComponent<Transform>().localScale" is slower than "transform.localScale". 
            _size = GetComponent<Transform>().localScale.x;
            _shipSpwanRate /= _size; // CR: [discuss]
            _numOfShips = _startingShips;
            _numOfshipText.text = _numOfShips.ToString();
        }
        else
        {
            _numOfShips = Random.Range(10, 25);
            _numOfshipText.text = _numOfShips.ToString();
        }

    }
    public void DeployShips(Planet targetPlanet)
    {
        // CR: [discuss]
        int _numShipTmp = _numOfShips / 2;
        _numOfShips -= _numShipTmp;
        _numOfshipText.text = _numOfShips.ToString();
        for (int i = 0; i < _numOfShips; i++)
        {
            SpaceShip Ship = Instantiate(_shipPrefab, _spawnPosition.position, Quaternion.identity);
            Ship._spriteRenderer.color = this._spriteRenderer.color;
            Ship.GetComponent<SpaceShip>()._targetPlanet = targetPlanet;
        }
    }

    private void OnMouseEnter()
    {
        SelectionIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!isClicked)
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

    public void SetPlanetSettings(PlanetState planetState)
    {

        switch (_planetState)
        {
            case PlanetState.Enemy:
                _planetState = PlanetState.Enemy;
                _spriteRenderer.color = Color.red;
                // CR:  [discuss] tags
                tag = "EnemyPlanet";
                // Set state to enemy for enemy planets
                break;
            case PlanetState.Friendly:
                _planetState = PlanetState.Friendly;
                _spriteRenderer.color = Color.blue;
                tag = "FriendlyPlanet";
                // Set state to friendly for friendly planets
                break;
            case PlanetState.Neutral:
                _planetState = PlanetState.Neutral;
                _spriteRenderer.color = Color.gray;
                tag = "NeutralPlanet";
                // Set state to neutral for neutral planets
                break;
        }
    }
    public void SetPlanetFreindlyState()
    {
        _planetState = PlanetState.Friendly;
        _spriteRenderer.color = Color.blue;
        tag = "FriendlyPlanet";
    }

    public void SetPlanetEnemyState()
    {
        _planetState = PlanetState.Enemy;
        _spriteRenderer.color = Color.red;
        tag = "EnemyPlanet";
    }

    public PlanetState GetPlanetStateByShip(SpaceShip ship)
    {
        if (ship._spriteRenderer.color == Color.blue)
        {
            _planetState = PlanetState.Friendly;
            SetPlanetSettings(_planetState);
        }
        else if (ship._spriteRenderer.color == Color.red)
        {
            _planetState = PlanetState.Enemy;
            SetPlanetSettings(_planetState);
        }
        return _planetState;
    }



}
