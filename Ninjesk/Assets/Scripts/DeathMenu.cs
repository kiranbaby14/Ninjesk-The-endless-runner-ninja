using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public GameObject deathmenu;
    public GameObject Revive_Effect1;
    public GameObject Revive_Effect2;

    public Animator deathAnimator;
    public Animator gameCanvas;
    public Animator player;

    public Text Highscore;
    public Text Highscore_name;
    public Text TotalCoins;
    public Text coinsText;
    public Text reviveText;

    public static bool isRevive;
    private bool coinsInsufficient;

    private static int reviveCoins;

    void Start()
    {
        isRevive = false;
        coinsInsufficient = false;
        reviveCoins = 100;
        reviveText.text = "Revive " + reviveCoins;
    }

  


    public void ToggleMenu()
    {
        if(isRevive.Equals(false))
        {              
            deathmenu.SetActive(true);
            deathAnimator.Play("DeathMenu");
            gameCanvas.SetTrigger("Hide");
        }
        if(coinsInsufficient)
            reviveText.text = "Insufficient Coins";
        else
            reviveText.text = "Revive " + reviveCoins;

        if (Player.isHighScore && isRevive.Equals(false))
        {
            Highscore_name.text = "New HighScore";
            deathAnimator.Play("HS_Death_show");
        }
           

        Highscore.text = "" + (int)PlayerPrefs.GetFloat("Highscore");
        TotalCoins.text = SaveManager.Instance.state.coin.ToString();
        coinsText.text = "" + Player.numberOfCoins;

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      
    }

    public void Revive()
    {

        if (SaveManager.Instance.state.coin - reviveCoins >= 0)
        {
            if ((SaveManager.Instance.state.coin - reviveCoins) < Player.numberOfCoins)
                Player.numberOfCoins = SaveManager.Instance.state.coin - reviveCoins;

            FindObjectOfType<AudioManager>().Play("Revive");
            SaveManager.Instance.state.coin -= reviveCoins;
            SaveManager.Instance.Save();
            reviveCoins *= 2;
            isRevive = true;
            deathmenu.SetActive(false);
            gameCanvas.SetTrigger("Show");
            player.Play("Revive");
            Revive_Effect1.SetActive(true);
            Revive_Effect2.SetActive(true);
            StartCoroutine(ExampleCoroutine());
        }
        else
            coinsInsufficient = true;

    }


    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.8f);
        //player.Play("run");
        Explosion.caughtPlayer = false;
        Collision.isObstacles = false;
        Revive_Effect1.SetActive(false);
        Revive_Effect2.SetActive(false);
        isRevive = false;
        Player.isDead = false;
    }
}
