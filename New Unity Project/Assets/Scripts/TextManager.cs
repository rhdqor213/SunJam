using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text namet;
    public Text textt;
    string[,,] ss = new string[,,] {
        {
            {"주인공", "..."}
        },
    };
    int n = 0, m = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            namet.text = ss[n, m, 0];
            textt.text = ss[n, m, 1];
        }
       catch (System.IndexOutOfRangeException e)
        {
            new SceneChange().ToGame();
        }
    }

    public void NextText()
    {
        m++;
    }
}
/*
대사

*/