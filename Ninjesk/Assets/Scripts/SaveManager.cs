using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

    }
    
    public void Save()
    {
        PlayerPrefs.SetString("save",Helper.Serialize<SaveState>(state));
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            
            state = Helper.Desrialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
        }
    }



    public bool IsItemOwned(int index)
    {
        return (state.itemOwned & (1 << index)) != 0;
    }

    public bool BuyItem(int index, int cost)
    {
        if(state.coin >= cost)
        {
            state.coin -= cost;
            UnlockItem(index);

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }
    public void UnlockItem(int index)
    {
        state.itemOwned |= 1 << index;
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}


