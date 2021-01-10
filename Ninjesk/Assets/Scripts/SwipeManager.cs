using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeManager : MonoBehaviour
{
    public static SwipeManager Instance { set; get; }

   
    public bool Tap { get { return tap; } }
    public Animator anim;
    public Animator gameCanvas;

    private bool isGameStarted = false;
    public static bool isTransactionOver;

    public Player playermotor;
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;

   
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;



    public static float transition = 0.0f;
    private readonly float animationDuration = 2.0f;

    private void Update()
    {
      
        if (transition >= 0.5f)
        {
            isTransactionOver = true;
            if (Tap && !isGameStarted)
            {

                anim.Play("run");
                isGameStarted = true;
                Player.isRunning = true;
                gameCanvas.SetTrigger("Show");
                FindObjectOfType<CameraFollow>().IsMoving = true;
            }
        }

        else
        {
            transition += Time.deltaTime *1 / animationDuration;
        }


        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            //tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
           
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                //tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public void OnTap()
    {
        tap = true;
    }
  
}