using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
     public int X { get; private set; } //������������ ������ 
     public int Y { get; private set; } //������������ ������  
    public int Value { get; private set; } //������� ������ 
    public bool isEmpty => Value == 0; //������ �� ������? 
    public int Points => isEmpty ? 0 : (int)Mathf.Pow(2, Value); //�������� �������� ������ 

    public const int MaxValue = 11; //���� ������� ������ 11^2=2048
     
     [SerializeField]private Image img; //���� �������� 
    [SerializeField] private TextMeshProUGUI Nominal; // ����������� �������� 
    public bool HasMerged { get; private set; }   // ����������� ������ 
    public void SetVAlue(int x, int y,int value) // ������������� �������� 
    {   
        X = x; 
        Y = Y;
        Value = value;
        UpdatteCell();

    }
    public void IncreaseVAlue() // ����������� ������  � ������� ��������� 
    {

        Value++;
        HasMerged = true;
        Controller.instance.AddPoints(Points);
        UpdatteCell();
    }
    public void ResetFlags() //�������� �� ������ 
    {
        HasMerged = false;
    }
    public void MergeWithCell(Cell otherCell) // �����������   ���� ������ 
    {
        otherCell.IncreaseVAlue();
        SetVAlue(X, Y, 0);
        UpdatteCell();
    }
    public void MoveToCell(Cell target) //����������� ������ � ��������� ������ 
    {
        target.SetVAlue(target.X,target.Y,Value); // �������� ������ 
        SetVAlue(X,Y,0);// �������� ������
        UpdatteCell();


    }


    public void UpdatteCell() // ���������� ������� � �������� ���� ������
    {
        Nominal.text = isEmpty ? string.Empty : Points.ToString();
        Nominal.color = Value <=2 ?CollorManeg.instance.pointDarkCollor :CollorManeg .instance.pointLightCollor;
         img.color = CollorManeg.instance.ceelColors[Value];


    }
     
}
