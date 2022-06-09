using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);
    private Vector2 SwipeDelta;
    private Vector2 TApPosithion;

    private float DeadZOne = 80;
    private bool IsSwiping;
    private bool IsMobile;
    public Reclam rec;
    int Count;


    private void Start()
    {
        Count = PlayerPrefs.GetInt("");
        IsMobile = Application.isMobilePlatform;
    }
    private void Update()
    {
        if (!IsMobile)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Count+=1;
                PlayerPrefs.SetInt("",Count);
                if(PlayerPrefs.GetInt("") ==2)
                {
                    rec.Show();
                    PlayerPrefs.SetInt("", 3);
                        

                    
                }

                IsSwiping = true;
                TApPosithion =Input.mousePosition;
            }
            else if(Input.GetMouseButtonDown(0))
                ResetSwipe();


        }
        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    IsSwiping =true;
                    TApPosithion = Input.GetTouch(0).position;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Canceled ||  Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                    ResetSwipe();
                }

            }
        }
        CheckSwipe();
        
    }
    private void CheckSwipe()
    {
        SwipeDelta = Vector2.zero;
        if(IsSwiping)
        {
            if (!IsMobile && Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2)Input.mousePosition - TApPosithion;
            }
            else if (Input.touchCount > 0)
                SwipeDelta = Input.GetTouch(0).position - TApPosithion;
            
        }
        if(SwipeDelta.magnitude > DeadZOne)
        {
            if (Mathf.Abs(SwipeDelta.x) > Mathf.Abs(SwipeDelta.y))
                SwipeEvent?.Invoke(SwipeDelta.x >  0 ? Vector2.right : Vector2.left);
            else
                SwipeEvent?.Invoke(SwipeDelta.x >  0 ? Vector2.up : Vector2.down);
            ResetSwipe();
        }

    }

    private void ResetSwipe()
    {

        IsSwiping = false;  
        TApPosithion = Vector2.zero;
        SwipeDelta = Vector2.zero;
        
    }
}
