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
    public GameObject _shipPrefab;
    [SerializeField]
    private PlanetColor _planetColor;
    [SerializeField]
    private TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    public int _numOfShips;
    [SerializeField]
    private float _shipSpwanRate;
    [SerializeField]
    private Vector3 spawnPosition;
    


    public PlanetState _planetState;
    public GameObject SelectionIndicator;

    
    private float _spwanNewShipTimer;
    public bool _isClicked = false;
    public bool _isFrendly = false;
    private int _startingShips = 100;
    private float _size;


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
        if (_planetState == PlanetState.Friendly || _planetState == PlanetState.Enemy)
            SpwanNewShipTimer();
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
        _size = GetComponent<Transform>().localScale.x;
        _shipSpwanRate /= _size;
        SetPlanetVisuals();
        if(_planetState == PlanetState.Friendly)
        {
        _numOfShips = _startingShips;
        _numOfshipText.text = _numOfShips.ToString();
        }
    }

    public void SetPlanetVisuals()
    {
        switch (_planetState)
        {
            case PlanetState.Enemy:
                _planetRebderer.material.color = Color.red;
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


    public void DeployShips()
    {
        _numOfShips = _numOfShips / 2;
        _numOfshipText.text = _numOfShips.ToString();
        for (int i = 0; i < _numOfShips; i++)
        {
            Instantiate(_shipPrefab, spawnPosition, Quaternion.identity);
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



    public void SetPlanetState()
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

}
