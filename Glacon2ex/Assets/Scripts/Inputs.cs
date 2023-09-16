using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Inputs : MonoBehaviour
{

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            Planet ClickedObject = (hit.collider != null) ? hit.collider.gameObject.GetComponent<Planet>() : null;
            
            if (ClickedObject != null && ClickedObject.isFrendly)
            {
                GameManager.Instance.Select(ClickedObject);
            }
            else if (ClickedObject == null)
            {
                GameManager.Instance.ClearSelection();
            }
        }

        if (hit.collider == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (hit.collider.tag == "EnemyPlanet" || hit.collider.tag == "NeutralPlanet" || hit.collider.tag == "FriendlyPlanet")
            {
                Planet enemyPlanet = hit.collider.gameObject.GetComponent<Planet>();
                foreach (Planet planet in GameManager.Instance.selectedPlanets)
                {
                    planet.DeployShips(enemyPlanet);
                }
                GameManager.Instance.ClearSelection();
            }
        }
    }



}

