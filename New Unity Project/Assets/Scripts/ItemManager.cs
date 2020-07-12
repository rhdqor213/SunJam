using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Texture[] item; // public 바꿀것
    GameObject[] slotItem = new GameObject[8];
    int[] icode = new int[8];
    public int selectItem = -1;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            slotItem[i] = GameObject.Find("Slot_" + i);
            slotItem[i].SetActive(false);
            icode[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            if (icode[i] > -1)
            {
                slotItem[i].SetActive(true);
                slotItem[i].GetComponent<RawImage>().texture = item[icode[i]];
            }
            else
            {
                slotItem[i].SetActive(false);
                slotItem[i].GetComponent<RawImage>().texture = null;
            }
                
        }
    }

    public void In(int n)
    {
        icode[i++] = n;
        Array.Sort(icode);
        for(int i = 0; i < 8; i++)
        {
            for(int j = i + 1; j < 8; j++)
            {
                if (icode[i] == -1)
                {
                    icode[i] = icode[j];
                    icode[j] = -1;
                }
            }
        }
    }

    public void Out(int n)
    {
        i--;
        for (int i = 0; i < 8; i++)
            if (icode[i] == n)
                icode[i] = -1;
        Array.Sort(icode);
        for (int i = 0; i < 8; i++)
        {
            for (int j = i + 1; j < 8; j++)
            {
                if (icode[i] == -1)
                {
                    icode[i] = icode[j];
                    icode[j] = -1;
                }
            }
        }
    }

    public void Select(int n)
    {
        slotItem[i].GetComponent<RawImage>().uvRect = new Rect(0, 0, 1.2f, 1.2f);
        selectItem = icode[n];
    }
}
