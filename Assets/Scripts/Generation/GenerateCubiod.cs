using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubiod : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 startPos;
    Vector3 endPos;

    Vector3 CentralPos;

    public GameObject prefabObj;
    public GameObject[] prefabObjNum;

    public GameObject redObj;

    //Magnitude Data
    private float dirObj;
    private float dirAng;
    private Transform target;

    //bool Val
    private bool active = true;

    //Time Val
    private float origianlTime = 0f;
    private float activeTime = 2.0f;



    void Start()
    {
        startPos.x = -3.4f;
        startPos.y = 5.0f;
        startPos.z = 11.4f;

        endPos.x = 0.9f;
        endPos.y = 5.0f;
        endPos.z = 0.5f;

        dirObj = 0;
        prefabObjNum = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {

        dirObj = Vector3.Distance(startPos, endPos);

        dirAng = Vector3.Angle(startPos, endPos);

        origianlTime += Time.deltaTime;
        if (origianlTime >= activeTime && active == true) {
            active = false;


            Instantiate(redObj, startPos, Quaternion.identity);
            Instantiate(redObj, endPos, Quaternion.identity);

            prefabObj.transform.localScale = new Vector3(1, 1, dirObj);
            prefabObj.transform.Rotate(endPos);
           

            CentralPos.x = (startPos.x + endPos.x) / 2;
            CentralPos.y = (startPos.y + endPos.y) / 2;
            CentralPos.z = (startPos.z + endPos.z) / 2;


            //Debug.Log(dirAng);

            prefabObjNum[0] = Instantiate(prefabObj, CentralPos, Quaternion.identity);
            prefabObjNum[0].transform.LookAt(endPos);
            //prefabObjNum[0].transform.rotation = Quaternion.LookRotation(startPos);
            //prefabObjNum[0].transform.rotation = Quaternion.LookRotation(endPos);
            //prefabObjNum[0] = Instantiate(prefabObj, CentralPos, Quaternion.FromToRotation(startPos, endPos));
            //prefabObjNum[0] = Instantiate(prefabObj, CentralPos, Quaternion.identity);

        }
        
    }
}
