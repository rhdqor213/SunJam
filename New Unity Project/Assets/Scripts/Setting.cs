using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    int show = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShow()
    {
        if (show == 0)
        {
            show = 1;
            gameObject.SetActive(true);
        }
        else
        {
            show = 0;
            gameObject.SetActive(false);
        }
    }
}
