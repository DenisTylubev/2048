using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public static int Points { get; private set; } //хранение очков 
    public static bool GameStarting { get; private set; } //проверка на начало игры  


    [SerializeField] private TextMeshProUGUI pointsText; //количество очков 


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartGame();

    }
    public void StartGame()
    {
        
        SetPoints(0);
        GameStarting = true;
        Filed.Instance.GenerateFeled();

    }
    public void AddPoints(int points) // добавления очков 
    {
        SetPoints(Points + points);

    }
    private void SetPoints(int points) // устанавливает количство очков и выводит на экран 
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
    
}

   
