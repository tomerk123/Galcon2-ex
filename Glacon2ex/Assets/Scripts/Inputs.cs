using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private Planet _planet;
    private List<Planet> _selectedPlanets = new List<Planet>();


    private void Start()
    {

    }


    private void PlanetSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Planet ClickedObject = (hit.collider != null) ? hit.collider.gameObject.GetComponent<Planet>() : null;
            if (ClickedObject != null)
            {
                _selectedPlanets.Add(ClickedObject);
                ClickedObject._selectionIndicator.SetActive(true);
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_selectedPlanets.Count > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.GetComponent<Planet>() != null)
                    {
                        Planet targetPlanet = hit.collider.gameObject.GetComponent<Planet>();
                        foreach (Planet planet in _selectedPlanets)
                        {
                            _planet.DeployShips(targetPlanet);
                        }
                    }
                }
            }
        }
    }
    private void Update()
    {
        PlanetSelect();
        Attack();
    }


}

