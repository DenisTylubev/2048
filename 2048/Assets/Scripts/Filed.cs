using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filed : MonoBehaviour
{
    public static Filed Instance;
    public int initCellCount;
    public float cellSize;
    public float spacing;
    public int fieldSize;
    [Space(10)]
    [SerializeField] private Cell cellPref;
    [SerializeField] private RectTransform rect;
    private Cell[,] field;
    private bool anyCellMoved; // был ход?
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Swipe.SwipeEvent += OnInput;
    }


    private void OnInput(Vector2 direction) //ход игрока
    {
        if (!Controller.GameStarting)
            return;
        anyCellMoved = false;
        ResetCellsFlags();
        Move(direction);
        if(anyCellMoved)
        {
            GeneratRandomCell();
            CheackResult();
        }
    }
    private void Move(Vector2 direction) //передвижения 
    {
        int StareXY = direction.x > 0 || direction.y < 0 ? fieldSize - 1 : 0;
        int dir= direction.x != 0 ?(int )direction.x : -(int )direction.y;
        for (int i = 0; i < fieldSize; i++)
        {
            for(int j  =StareXY; j >=0 && j < fieldSize; j -= dir)
            {
                var cell  =direction.x !=0 ? field[j,i] : field[i,j];
                if (cell.isEmpty)
                    continue;
                var cellToMerge = FindcellToMarger(cell, direction);
                if (cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge);    
                    anyCellMoved=true;
                    continue;
                }
                var emptyCell = FindEmptyCell(cell, direction);
                if(emptyCell != null)
                {
                    cell.MoveToCell(emptyCell);
                    anyCellMoved =true;
                }
            }
        }
    }
    private Cell FindcellToMarger(Cell cell ,Vector2 direction) // поиск плиток 
    {
        int startX = cell.X + (int )direction.x;   
        int startY = cell.Y - (int )direction.y;
        for(int x = startX,y=startY; x >=  0 && x < fieldSize && y>=0 && y < fieldSize; x+=(int)direction.x,y-=(int)direction.y)
        {
            if (field[x, y].isEmpty)
                continue;
            if (field[x, y].Value == cell.Value && !field[x, y].HasMerged)
                return field[x, y];

            break;
        }
        return null;

    }
    private Cell FindEmptyCell(Cell cell,Vector2 direction)
    {
        Cell emptyCell = null;
        int startX = cell.X + (int )direction.x;
        int startY = cell.Y - (int)direction.y;
        for (int x = startX, y = startY; x >= 0 && x < fieldSize && y >= 0 && y < fieldSize; x+= (int)direction.x,y -=(int )direction.y )
        {
            if (field[x, y].isEmpty)
                emptyCell = field[x, y];
            else
                break;
                
        }
        return emptyCell;


    }
    private void CheackResult() // побуда или проиграш,проверка
    {
        bool lose = true;
        for(int x = 0; x < fieldSize; x++)
        {   
            for (int i = 0; i < fieldSize; i++)
            {
                if(field[x,i].Value == Cell.MaxValue)
                {
                   
                    return;
                }
                if(  lose && field[x,i].isEmpty ||  FindcellToMarger(field[x,i],Vector2.left) || FindcellToMarger(field[x,i],Vector2.right) || FindcellToMarger(field[x,i],Vector2.up) || FindcellToMarger(field[x, i], Vector2.down))
                {
                    lose = false; 
                }

            }
        }

        
    }

    
    private void CreatField() //создания поля 
    {
        field = new Cell[fieldSize,fieldSize]; //счтаем ширину и высоту
        float fieldWight = fieldSize *(cellSize + spacing) + spacing;
        rect.sizeDelta = new Vector2 (fieldWight,fieldWight);
        float startX = -(fieldWight / 2) + (cellSize/2) + spacing; // высчитуем начальную позицыю плитки 
        float startY = (fieldWight / 2) - (cellSize / 2) - spacing;
        for(int i = 0; i < fieldSize; i++) // цыкл для поля 
        {
            for(int j = 0; j < fieldSize; j++)
            {
                var cell  =Instantiate(cellPref,transform,false);
                var posithion = new Vector2(startX +(i * (cellSize +spacing)),startY - (j * (cellSize +spacing)));
                cell.transform.localPosition = posithion;
                field[i,j] = cell;
                cell.SetVAlue(i, j, 0);
            }
        }
    }
    public void  GenerateFeled() //подготовка поля для новой игры 
    {
        if (field == null)
            CreatField();
        for (int i = 0; i < fieldSize; i++)
            for (int j = 0; j < fieldSize; j++)
                field[i, j].SetVAlue(i, j, 0);
        for (int k = 0; k < initCellCount; k++)
            GeneratRandomCell();


    }
    private void GeneratRandomCell() //генерации начальных плиток 
    {
        var emptyCecll = new List<Cell>();
        for(int i = 0; i < fieldSize; i++)
            for (int j = 0; j < fieldSize; j++)
                if(field[i,j].isEmpty)
                    emptyCecll.Add(field[i,j]);
        if (emptyCecll.Count == 0)
            throw new System.Exception("There is no  empty call");
        int value = Random.Range(0,10)== 0 ? 2 : 1;
        var cell = emptyCecll[Random.Range(0,emptyCecll.Count)];
        cell.SetVAlue(cell.X,cell.Y,value);
    }
    private void ResetCellsFlags() //обнуляем   ResetFlags
    {
        for (int i = 0; i < fieldSize; i++)
            for (int j = 0; j < fieldSize; j++)
                field[i,j].ResetFlags();    


    }
}
