using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayReflection : MonoBehaviour
{

    //Beamer ID
    public int beamerID = 1; // Parent Script���� ������ (�ӽ÷� 1�θ� ������)
    private int fiberNum; // Child Script�� FiberCollsion�� ���⼭ ������

    //Hit Range
    public int reflections;
    public float maxLength;

    // Hit Array Management Variable
    public Vector3 [] arrayPosition;
    private bool storePosition = false;
    public int numPosition = 0; // arraynumber
    private Vector3 arbitaryPosition_yes;


    // LineRenderer & Ray Variable 
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    //Collision Object from RayCast
    private Vector3 arbitaryObj;
    private Vector3 [] alreadyhitObj;
    private int numObj = 0; // arraynumber
    private bool objStatus = false;
    Vector3 pos;

    //Collision Prefab of Cubiod shaped obj
    public GameObject[] prefabCapsuleObjNum;
    public GameObject prefabCapsuleObj;
    Vector3 CentralPos;
    private float dirObj;



    //LineRenderer Separation (�Ⱦ��µ� �� �������?)
    public GameObject[,,] separationObj;

    // Time Variable
    private float activationMeshRenderer = 2.0f;
    private float originalTime = 0.0f;

    // Generate Variable
    private bool generatePosition = true;

    //Fiber Management Variable (�ٸ� Field Script�� ����)
    public float objID = 0; //(csv File ������)


    private void Awake()
    {
        //Declare Array Size of arrayPosition
        arrayPosition = new Vector3[reflections];
        alreadyhitObj = new Vector3[reflections];
        prefabCapsuleObjNum = new GameObject[reflections];

        //LineRenderer Component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material.SetColor("_Color", Color.gray);

        //Initialize
        dirObj = 0f;
        fiberNum = 1;

        //Mesh mesh = new Mesh();
        //lineRenderer.BakeMesh(mesh);

    }

    void Update()
    {

        originalTime += Time.deltaTime;
        if (activationMeshRenderer <= originalTime && generatePosition == true) {

            generatePosition = false;

            //�ӽ÷� ���� Mesh Collider for LineRenderer, �ٵ� ��� ��Ʈ�� �ȸ´°��� �� �ȸ����� ��� �ٲ�

            MeshCollider collider = GetComponent<MeshCollider>();
            collider = gameObject.AddComponent<MeshCollider>();

            Mesh lineMesh = new Mesh();
            collider.sharedMesh = lineMesh;
            lineRenderer.BakeMesh(lineMesh, true);

            //GetComponent<MeshCollider>().enabled = true;



            // arrayPosition �̿��ؼ� (0,1) ��ǥ, (1,2) ��ǥ �̷������� �� ������ �ǰ� null�߸� �׸� �θ� ��.
            // ���⼭�� bool status variable �߰� �ؾ� �ҵ� (Update Ư�� ������)

            // ��ũ��Ʈ �ϳ� �� ���� ���� �ϸ� ���� ���� �� (���⼭ �ڵ� �� ¥�� ���İ�Ƽ �ڵ� �� ����)

            // arrayPosition;
            // numPosition;

            //for (int i = 0; i < numPosition; i++) {


            //arrayPosition�� ����غ���.
            /*
             *  1)Lookat�Լ��� Vector3 pivot���� �ٸ� pivot �����ͷ� ���� ���  
                2)��Ŭ����� �Ÿ� �缭 x scale arbitary data�� �־��ش�����
                3) Instantiate �Լ� �Ẹ��.
             * 
             */

            //Debug.Log("�迭 ũ�� Ȯ��");
            //Debug.Log(numPosition);
            for (int i = 0; i < numPosition - 1; i++) {

                CentralPos.x = (arrayPosition[i].x + arrayPosition[i+1].x) / 2;
                CentralPos.y = (arrayPosition[i].y + arrayPosition[i+1].y) / 2;
                CentralPos.z = (arrayPosition[i].z + arrayPosition[i+1].z) / 2;

                dirObj = Vector3.Distance(arrayPosition[i], arrayPosition[i+1]);

                //Debug.Log("direction parameter check");
                //Debug.Log(dirObj);
                prefabCapsuleObj.transform.localScale = new Vector3(0.05f, 0.05f, dirObj);

                //���⼭ �����ؾ��ϰ� FiberCollision�κп� ID �Ҵ��ؾ���
                prefabCapsuleObjNum[i] = Instantiate(prefabCapsuleObj, CentralPos, Quaternion.identity);

                //Child Script�� FiberCollision�� Number Allocation
                prefabCapsuleObjNum[i].GetComponent<FiberCollision>().fiberiD = fiberNum;
                prefabCapsuleObjNum[i].GetComponent<FiberCollision>().beamerID = beamerID;
                fiberNum++;
               

                prefabCapsuleObjNum[i].transform.LookAt(arrayPosition[i + 1]);



            }

        }

        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));


                if (hit.collider.tag == "Dot") {


                    //Debug.Log("HIT!");

                    arbitaryObj = hit.point;
                    arbitaryPosition_yes = hit.point;

                    if (numObj == 0) {
                        alreadyhitObj[numObj] = arbitaryObj;
                        numObj++;

                        arrayPosition[numPosition] = this.gameObject.transform.position;
                        Debug.Log(arrayPosition[numPosition]);
                        numPosition++;
                        

                        arrayPosition[numPosition] = hit.point;
                        Debug.Log(arrayPosition[numPosition]);
                        numPosition++;

                        //Debug.Log(hit.point);
                    }
                    if (numObj > 0){ // Compare last Collision obj for store of hit.point

                        for (int j = 0; j < numObj; j++) {
                            if(j == 0)objStatus = true;
                            if (alreadyhitObj[j] == arbitaryObj) objStatus = false;
                        }

                        if (objStatus == true) {
                            objStatus = false;
                            alreadyhitObj[numObj] = arbitaryObj;
                            numObj++;

                            arrayPosition[numPosition] = hit.point;
                            Debug.Log(arrayPosition[numPosition]);
                            numPosition++;

                            //Debug.Log(hit.point);
                           // Debug.Log(numPosition);
                        }

                    }         
                    

                    //Debug.Log(arbitaryObj.gameObject.transform.position.x);
                    //Debug.Log(arbitaryObj.gameObject.transform.position.y);
                    //Debug.Log(arbitaryObj.gameObject.transform.position.z);
                }

                if(hit.collider.tag == "Beam")
                {
                    //Debug.Log("I HITS BEAM!!!");
                }

                if (hit.collider.tag != "Dot")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}
