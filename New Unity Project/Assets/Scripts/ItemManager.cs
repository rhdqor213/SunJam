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
    public int selectItem;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            slotItem[i] = GameObject.Find("Slot_" + i);
            icode[i] = -1;
        }
        selectItem = -1;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            if (icode[i] > -1)
                slotItem[i].GetComponent<RawImage>().texture = item[icode[i]];
            else
                slotItem[i].GetComponent<RawImage>().texture = null;
            if (icode[i] == selectItem && selectItem >= 0)
                slotItem[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1f);
            else
                slotItem[i].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
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
        if (selectItem == icode[n])
            selectItem = -1;
        else
            selectItem = icode[n];
    }
}
