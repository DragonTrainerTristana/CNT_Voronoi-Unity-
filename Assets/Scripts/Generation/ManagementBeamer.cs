using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementBeamer : MonoBehaviour
{
    //Gameobject
    public int beamNum;
    public GameObject Beam;
    private GameObject[] Beams; //Array Num�� �ٷ� ID�� �ǽðڽ��ϴ�.

    //Random Vector Domain <- �� �����ؾ� �ϴ°�, Fiber�� Square �ۿ��� ���ô�.
    public float xmin, xmax;
    public float ymin, ymax;
    public float zmin, zmax;

    //Random Val of Vector
    public float px,py,pz;
    private Vector3 pVector;

   
    void Start()
    {
        pVector = new Vector3(px, py, pz);
        Beams = new GameObject[beamNum]; // Array Allocation

        for (int i = 0; i < beamNum; i++) {

            Beams[i] = Instantiate(Beam, pVector, Quaternion.identity); 
            

        }


    }


    void Update()
    {
        
    }
}
