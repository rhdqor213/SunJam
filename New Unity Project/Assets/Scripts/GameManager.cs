using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public CameraMove cm;
    public ItemManager im;
    
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
                string index = "";
                if (hitInfo.collider.gameObject.tag == "Trick")
                {
                    if (hitInfo.collider.gameObject.GetComponent<TrickManager>().Check(im.selectItem) == 1)
                        im.Out(im.selectItem);
                }
                if (hitInfo.collider.gameObject.tag == "Item")
                {
                    for (int i = 5; i < targetName.Length; i++)
                    {
                        index += targetName[i];
                    }
                    Destroy(hitInfo.collider.gameObject);
                    im.In(int.Parse(index));
                }
                else if (hitInfo.collider.gameObject.tag == "door")
                {
                    for(int i = 5; i < targetName.Length; i++)
                    {
                        index += targetName[i];
                    }
                    cm.FadeCam(int.Parse(index));
                }
                else
                {
                    for (int i = 0; i < targetName.Length; i++)
                    {
                        if (targetName[i] == '-')
                        {
                            for (int j = i + 1; j < targetName.Length; j++)
                            {
                                index += targetName[j];
                            }
                            cm.MoveCam(-int.Parse(index));
                            break;
                        }
                    }
                }
            }
        }
    }
}
