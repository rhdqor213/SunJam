using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControl : MonoBehaviour {

	void Update () {
        Vector2 mPos = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        transform.Rotate(mPos.x,mPos.y,0f);
        Vector3 curPos = transform.rotation.eulerAngles;
        curPos.z = 0f;
        transform.rotation = Quaternion.Euler(curPos);
    }
}
