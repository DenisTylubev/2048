using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public static int Points { get; private set; } //�������� ����� 
    public static bool GameStarting { get; private set; } //�������� �� ������ ����  


    [SerializeField] private TextMeshProUGUI pointsText; //���������� ����� 


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
    public void AddPoints(int points) // ���������� ����� 
    {
        SetPoints(Points + points);

    }
    private void SetPoints(int points) // ������������� ��������� ����� � ������� �� ����� 
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
    
}

   
