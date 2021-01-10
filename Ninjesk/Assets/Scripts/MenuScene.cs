using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuScene : MonoBehaviour
{


    public RectTransform menuContainer;
    public Transform shopItemsPanel;

    private GameObject currentItem;
    public GameObject player;


    private Vector3 desiredMenuPosition;

    public Text itemBuyText;
    public Text highscoreText;
    public Text totalCoinsText;
    public Text coinText;

    private int[] itemCost = new int[] { 0, 1000, 1500, 2000, 2500, 3000};
    private int selectedItemIndex; 
    private int activeItemIndex;

    void Start()
    {
        
        UpdateCoinText();
        UpdateHighscoreText();
        InitShop();

        OnItemSelect(SaveManager.Instance.state.activeItem);
        SetItem(SaveManager.Instance.state.activeItem);

        shopItemsPanel.GetChild(SaveManager.Instance.state.activeItem).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }

    // Update is called once per frame
    void Update()
    {
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
   
    }

    private void InitShop()
    {
        int i = 0;
        foreach(Transform t in shopItemsPanel)
        {
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnItemSelect(currentIndex));
            
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsItemOwned(i) ? Color.white : new Color(0.7f,0.7f,0.7f,0.5f);
            i++;
        }

       
    }


    private void UpdateCoinText()
    {
     
        coinText.text = SaveManager.Instance.state.coin.ToString();
        totalCoinsText.text = SaveManager.Instance.state.coin.ToString();
    }

    private void UpdateHighscoreText()
    {
        highscoreText.text = "" + (int)PlayerPrefs.GetFloat("Highscore");
    }

    private void OnItemSelect(int currentIndex)
    {
      
        if (selectedItemIndex == currentIndex)
            return;


        shopItemsPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        shopItemsPanel.GetChild(selectedItemIndex).GetComponent<RectTransform>().localScale = Vector3.one;


        selectedItemIndex = currentIndex;

        if(SaveManager.Instance.IsItemOwned(currentIndex))
        {
            if(activeItemIndex == currentIndex)
            {
                itemBuyText.text = "Current";
                PreviewItem(selectedItemIndex);
            }
            else
            {
                itemBuyText.text = "Equip";
                PreviewItem(selectedItemIndex);
            }

        }
        else
        {
            itemBuyText.text = "Buy: "+ itemCost[currentIndex].ToString();
            PreviewItem(selectedItemIndex);
        }
    }

    public void OnItemBuySet()
    {
        if(SaveManager.Instance.IsItemOwned(selectedItemIndex))
        {
            SetItem(selectedItemIndex);
        }
        else
        {
            if(SaveManager.Instance.BuyItem(selectedItemIndex , itemCost[selectedItemIndex]))
            {
                SetItem(selectedItemIndex);
                shopItemsPanel.GetChild(selectedItemIndex).GetComponent<Image>().color = Color.white;

                UpdateCoinText();
            }
            else
            {

            }
        }
    }




    private void SetItem(int index)
    {
        activeItemIndex = index;
        SaveManager.Instance.state.activeItem = index;

        if (currentItem != null)
            Destroy(currentItem);

        currentItem = Instantiate(Manager.Instance.items[index]) as GameObject;

        currentItem.transform.SetParent(player.transform);

        currentItem.transform.localPosition = new Vector3(0.089f, -0.022f, 0.012f);
        currentItem.transform.localRotation = Quaternion.Euler(44.441f, 100.599f, 72.955f);
        currentItem.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        itemBuyText.text = "Current";
        SaveManager.Instance.Save();
    }



    private void PreviewItem(int index)
    {  
        if (currentItem != null)
            Destroy(currentItem);

        currentItem = Instantiate(Manager.Instance.items[index]) as GameObject;

        currentItem.transform.SetParent(player.transform);

        currentItem.transform.localPosition = new Vector3(0.089f, -0.022f, 0.012f);
        currentItem.transform.localRotation = Quaternion.Euler(44.441f, 100.599f, 72.955f);
        currentItem.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

    }


    public void OnShopClick()
    {
        if(SwipeManager.isTransactionOver)
            NavigateTo(1);
    }


    public void OnBackClick()
    {
        NavigateTo(0);
        SetItem(activeItemIndex);
        OnItemSelect(activeItemIndex);
    }



    public void Quit()
    {
        if (SwipeManager.isTransactionOver)
            Application.Quit();
    }


    private void NavigateTo(int menuIndex)
    {
        switch(menuIndex)
        {
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;

            case 1:
                desiredMenuPosition = Vector3.up * 1700;
                break;


        }
    }

}
