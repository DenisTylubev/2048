using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
     public int X { get; private set; } //Расположения плиток 
     public int Y { get; private set; } //Расположения плиток  
    public int Value { get; private set; } //номинал плитки 
    public bool isEmpty => Value == 0; //Пустая ли плитка? 
    public int Points => isEmpty ? 0 : (int)Mathf.Pow(2, Value); //хранения номинала плитки 

    public const int MaxValue = 11; //Макс значени плитки 11^2=2048
     
     [SerializeField]private Image img; //Цвет изменния 
    [SerializeField] private TextMeshProUGUI Nominal; // отображения номинала 
    public bool HasMerged { get; private set; }   // объединения плиток 
    public void SetVAlue(int x, int y,int value) // устанавливаем значения 
    {   
        X = x; 
        Y = Y;
        Value = value;
        UpdatteCell();

    }
    public void IncreaseVAlue() // объединения плиток  в которые вливаемся 
    {

        Value++;
        HasMerged = true;
        Controller.instance.AddPoints(Points);
        UpdatteCell();
    }
    public void ResetFlags() //проверка на плитки 
    {
        HasMerged = false;
    }
    public void MergeWithCell(Cell otherCell) // объединения   двух плиток 
    {
        otherCell.IncreaseVAlue();
        SetVAlue(X, Y, 0);
        UpdatteCell();
    }
    public void MoveToCell(Cell target) //перемещения плитки в свободную плитку 
    {
        target.SetVAlue(target.X,target.Y,Value); // значения плитки 
        SetVAlue(X,Y,0);// обнуляем плитку
        UpdatteCell();


    }


    public void UpdatteCell() // отображаем номинал и изменяем цвет плитки
    {
        Nominal.text = isEmpty ? string.Empty : Points.ToString();
        Nominal.color = Value <=2 ?CollorManeg.instance.pointDarkCollor :CollorManeg .instance.pointLightCollor;
         img.color = CollorManeg.instance.ceelColors[Value];


    }
     
}
