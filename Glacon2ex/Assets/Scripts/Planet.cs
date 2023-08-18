using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Planet : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _planetRebderer;
    [SerializeField]
    private GameObject _selectionIndicator;
    [SerializeField]
    private GameObject _planetIndicator;
    [SerializeField]
    private GameObject _shipPrefab;
    [SerializeField]
    private PlanetColor _planetColor;
    [SerializeField]
    private TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    private int _numOfShips;
    [SerializeField]
    private int _shipSpwanRate;

    //private List<GameObject> _selectedPlanets = new List<GameObject>();
    private float _spwanNewShipTimer;
    public bool _isSelected;
    private int _size;

    private enum PlanetColor
    {
        Red,
        Blue,
        Nuetral // gray
    }
    private void OnMouseEnter()
    {
        _planetIndicator.SetActive(true);

    }

    private void OnMouseExit()
    {

        if (_isSelected == true)
        {
            _selectionIndicator.SetActive(true);
        }
        else
        {
            _planetIndicator.SetActive(false);
        }
    }

    private void Update()
    {
        SpwanNewShipTimer();
        PlanetSelect();
        DeployShips();
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
        SetPlanetColor();
        _numOfShips = 100;
        _numOfshipText.text = _numOfShips.ToString();
        // _selectionIndicator.SetActive(false);
    }
    private void SetPlanetColor()
    {
        switch (_planetColor)
        {
            case PlanetColor.Red:
                _planetRebderer.material.color = Color.red;
                break;
            case PlanetColor.Blue:
                _planetRebderer.material.color = Color.blue;
                break;
            case PlanetColor.Nuetral:
                _planetRebderer.material.color = Color.gray;
                break;
        }
    }

    private void PlanetSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isSelected = true;
            _selectionIndicator.SetActive(true);
            // _selectedPlanets.Add(gameObject);
        }
    }

    void DeployShips()
    {
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < _numOfShips; i++)
            {
                Instantiate(_shipPrefab, transform.position, Quaternion.identity);
            }
            _numOfShips = _numOfShips / 2;
            _numOfshipText.text = _numOfShips.ToString();
        }

    }

}
