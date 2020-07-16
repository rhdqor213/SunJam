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
    GameObject[] curOb = new GameObject[5];
    int curi;
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
        if (Input.GetMouseButtonUp(0) && !cm.isMove && !cm.isFade)
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
            GameObject target = hitInfo.collider.gameObject;
            Debug.Log(target);
            string index = "";
            if (target.tag == "NumKey")
            {
                target.transform.Rotate(new Vector3(0, 1, 0) * 36);
            }
            else if (target.tag == "Trick")
            {
                if (target.GetComponent<TrickManager>().Check(im.selectItem) == 1)
                    im.Out(im.selectItem);
            }
            else if (target.tag == "Item")
            {
                for (int i = 5; i < target.name.Length; i++)
                {
                    index += target.name[i];
                }
                Destroy(hitInfo.collider.gameObject);
                im.In(int.Parse(index));
            }
            else if (target.tag == "Door")
            {
                for (int i = 5; i < target.name.Length; i++)
                {
                    index += target.name[i];
                }
                cm.FadeCam(int.Parse(index));
            }
            else
            {
                for (int i = 0; i < target.name.Length; i++)
                {
                    if (target.name[i] == '-')
                    {
                        for (int j = i + 1; j < target.name.Length; j++)
                        {
                            index += target.name[j];
                        }
                        curOb[curi] = hitInfo.collider.gameObject;
                        curOb[curi++].GetComponent<BoxCollider>().enabled = false;
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
    public void ClickMovebtn(int n)
    {
        if (n == 1 && curi > 0)
        {
            curOb[--curi].GetComponent<BoxCollider>().enabled = true;
            curOb[curi] = null;
        }
        cm.MoveCam(n);
    }
}
