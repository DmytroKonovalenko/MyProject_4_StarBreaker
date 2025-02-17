using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class ZolotoManager : MonoBehaviour
{
    public static ZolotoManager Instance { get; private set; }

    public int zoloto;
   

    private const string ZolotoKey = "Zoloto";

   
    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        LoadZoloto();
    }

    public bool HasEnoughCrystals(int amount)
    {
        return zoloto >= amount;
    }

    public int GetCurrentGold()
    {
        return zoloto;
    }

    public void AddZoloto(int amount)
    {
        zoloto += amount;
        SaveZoloto();
        OnGoldChanged?.Invoke(zoloto);
    }

    public void SubtractZoloto(int amount)
    {
        zoloto = Mathf.Max(0, zoloto - amount);
        SaveZoloto();
        OnGoldChanged?.Invoke(zoloto);
    }


    private void SaveZoloto()
    {
        PlayerPrefs.SetInt(ZolotoKey, zoloto);
        PlayerPrefs.Save();
    }

    public void LoadZoloto()
    {
        zoloto = PlayerPrefs.GetInt(ZolotoKey, 0);
    }
}
