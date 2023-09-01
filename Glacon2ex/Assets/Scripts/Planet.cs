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
    [SerializeField]
    private SpaceShip _shipPrefab;

    public PlanetState _planetState;
    public Color _planetColor;
    private SpriteRenderer _spriteRenderer;

    public GameObject SelectionIndicator;
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

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

        ShipUpdate();

        _size = transform.localScale.x;
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
                _planetColor = Color.red;
                _spriteRenderer.color = _planetColor;
                tag = "EnemyPlanet";
                // Set state to enemy for enemy planets
                break;
            case PlanetState.Friendly:
                _planetState = PlanetState.Friendly;
                _planetColor = Color.blue;
                _spriteRenderer.color = _planetColor;
                tag = "FriendlyPlanet";
                // Set state to friendly for friendly planets
                break;
            case PlanetState.Neutral:
                _planetState = PlanetState.Neutral;
                _planetColor = Color.gray;
                _spriteRenderer.color = _planetColor;
                tag = "NeutralPlanet";
                // Set state to neutral for neutral planets
                break;
        }
    }

    public void SetPlanetFriendlyState()
    {
        _planetState = PlanetState.Friendly;
        SetPlanetSettings(PlanetState.Friendly);
    }

    public void SetPlanetEnemyState()
    {
        _planetState = PlanetState.Enemy;
        SetPlanetSettings(PlanetState.Enemy);
    }
}
