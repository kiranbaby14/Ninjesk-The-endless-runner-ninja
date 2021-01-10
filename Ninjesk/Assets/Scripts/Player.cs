using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public enum SIDE { Left, Mid, Right }
public class Player : MonoBehaviour
{

    
    public GameObject swordTrail;
    public GameObject speedUp_Image;


    public DeathMenu deathmenu;
    public AudioManager audiomanager;

    public Text coinsText;
   
    public Text Score;
    public Text Score_Death;
    

    public static bool isRunning = false;
    public static bool isHighScore = false;

    private int difficultLevel = 3;
    private readonly int maxDifficultyLevel = 18;
    private int scoreToNextLevel = 250;
    private int scoreTimer = 2;

    public static int numberOfCoins;
    public float score;
    public static int TotalCoins;

    public static bool isDead;
    public static bool isAttack;

    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue;

    public CharacterController m_char;

    public Animator m_Animator;
    public Animator canvas_Anim;

    private float x;
    public float SpeedDodge;
    public float JumpPower;
    private float y;
    public bool InJump;
    public bool InRoll;
    public  float FwdSpeed;
    private float ColHeight;
    private float ColCenterY;

    public static Player Instance { set; get; }
    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        Explosion.caughtPlayer = false;
        Collision.isObstacles = false;
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        transform.position = Vector3.zero;
        numberOfCoins = 0;
        score = 0;
        TotalCoins = PlayerPrefs.GetInt("Coins");
        isDead = false;
        isRunning = false;
        isHighScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isRunning)
            return;
        

        coinsText.text = "" + numberOfCoins;//Coins Text
        
        

        if (score >= scoreToNextLevel)
            LevelUp();

           

        SwipeLeft = SwipeManager.swipeLeft;
        SwipeRight = SwipeManager.swipeRight;
        SwipeUp = SwipeManager.swipeUp;
        SwipeDown = SwipeManager.swipeDown;

        if (SwipeLeft && isDead.Equals(false))
        {
            
            if (m_Side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
                //m_Animator.Play("dodgeLeft");    
            }
            else if (m_Side == SIDE.Right)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                //m_Animator.Play("dodgeLeft");    
            }
            
        }

        else if (SwipeRight && isDead.Equals(false))
        {

            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
                //m_Animator.Play("dodgeRight");    
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                //m_Animator.Play("dodgeRight");    
            }
     
        }

        if(Explosion.caughtPlayer.Equals(false) && Collision.isObstacles.Equals(false))
        {
            
            score += Time.deltaTime * 3 * scoreTimer;
            Score.text = ((int)score).ToString();// Score Text
            Score_Death.text = "" + ((int)score).ToString();// Death Menu Score Text

            Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
            x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
            m_char.Move(moveVector);


            if(PlayerPrefs.GetFloat("Highscore") < score)
            {
                
                canvas_Anim.Play("HS_show");
                PlayerPrefs.SetFloat("Highscore", score);
                isHighScore = true;
            }
                


            Roll();
            Jump();
       
        }
        else
        {
         
            if(DeathMenu.isRevive.Equals(false))
            {
                isDead = true;
                if (InJump.Equals(true))
                {
                    y -= JumpPower * 3.5f * Time.deltaTime;
                    Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, 0);
                    x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
                    m_char.Move(moveVector);
                    m_Animator.Play("Flying Back Death");
                }
                else
                {
                    y -= JumpPower * 3.5f * Time.deltaTime;
                    Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, 0);
                    x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
                    m_char.Move(moveVector);
                    m_Animator.Play("Death");
                }

            }

            deathmenu.ToggleMenu();
        }


    }

    public void Jump()
    {
        if (m_char.isGrounded)
        {
           
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") && isAttack.Equals(false))
            {
                m_Animator.Play("Landing");
                InJump = false;
            }
            if (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
            }
        }
        else
        {
          
            y -= JumpPower * 3.5f * Time.deltaTime;
            
            if (m_char.velocity.y < -0.1f && isAttack.Equals(false))
            {
      
                m_Animator.Play("Falling");               
            }
                
       }

    }

    internal float RollCounter;



    public void Roll()
    {
        RollCounter -= Time.deltaTime;
        if(RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0, ColCenterY, 0.14f);
            m_char.height = ColHeight;
            InRoll = false;
        }
        if (SwipeDown)
        {
           
            RollCounter = 0.5f;
            y -= 10f;
            if(InJump.Equals(false))
            {
           
                m_char.center = new Vector3(0, 0.38f, 0.14f);
                m_char.height = ColHeight / 2.5f;
            }

            m_Animator.CrossFadeInFixedTime("roll", 0.1f);
            InRoll = true;
            InJump = false;
            
        }
    }


    public void Attack()
    {
        if (isDead.Equals(false) && isAttack.Equals(false))
        {
            InJump = false;
            m_Animator.Play("Attack");
            audiomanager.Play("Sword_Slash");
            swordTrail.SetActive(true);
            isAttack = true;
            StartCoroutine(ExampleCoroutine());
        }

    }

    IEnumerator ExampleCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.4f);
        swordTrail.SetActive(false);
        isAttack = false;
       

    }

    void LevelUp()
    {
        if (difficultLevel <= maxDifficultyLevel)
        {
            speedUp_Image.SetActive(true);
            scoreToNextLevel *= 2;
            scoreTimer *= 2;          
            FwdSpeed = 15 + difficultLevel;
            difficultLevel += 3;            
            StartCoroutine(SpeedUp_Animation());
            
        }
     
    }

    IEnumerator SpeedUp_Animation()
    {
       
        yield return new WaitForSeconds(1.8f);
        speedUp_Image.SetActive(false);
    }


}
