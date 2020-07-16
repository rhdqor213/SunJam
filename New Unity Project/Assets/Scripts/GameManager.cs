using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public CameraMove cm;
    public ItemManager im;
    public GameObject shortHand;
    public GameObject longHand;
    public GameObject Set;
    public float timeSpeed = 1.0f;
    RectTransform sh;
    RectTransform lh;
    float timer;
    bool isRun = false;
    Setting setting;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isRun = true;
        sh = shortHand.GetComponent<RectTransform>();
        lh = longHand.GetComponent<RectTransform>();
        setting = Set.GetComponent<Setting>();
    }

    // Update is called once per frame
    void Update()
    {
        Clock();
        if (Input.GetMouseButtonUp(0) && !(setting.isPause))
        {
            ShootRay();
        }
    }

    void ShootRay()
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
            else if (hitInfo.collider.gameObject.tag == "Door")
            {
                for (int i = 5; i < targetName.Length; i++)
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
    void Clock()
    {
        if (isRun)
        {
            lh.Rotate(new Vector3(0, 0, -Time.deltaTime * 0.6f * timeSpeed));
            sh.Rotate(new Vector3(0, 0, -Time.deltaTime * 0.05f * timeSpeed));
            timer += Time.deltaTime * timeSpeed;
            if (timer >= 600)
            {
                TimeOut();
                isRun = false;
            }
        }
    }
    void TimeOut()
    {
        if (isRun)
        {
            Debug.Log("Timer = " + timer);
            Debug.Log("Time Out");
        }
    }
}
