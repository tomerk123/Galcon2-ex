using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Win/Lose Screens")]
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;


    private void Start()
    {
    
    }
     public void Win()
    {
        _winScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void Lose()
    {
        _loseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}


