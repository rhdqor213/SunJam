using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMove cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                string targetName = hitInfo.collider.gameObject.name;
                Debug.Log(targetName);
                string index = "";
                for (int i = 0; i < targetName.Length; i++)
                {
                    if (targetName[i] == '-')
                    {
                        for (int j = i + 1; j < targetName.Length; j++)
                        {
                            index += targetName[j];
                        }
                        Debug.Log(int.Parse(index));
                       cm.MoveCam(-int.Parse(index));
                        break;
                    }
                }
            }
        }
    }
}
