using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayReflection : MonoBehaviour
{

    //Beamer ID
    public int beamerID = 1; // Parent Script에서 관리함 (임시로 1로만 설정함)
    private int fiberNum; // Child Script인 FiberCollsion을 여기서 관리함

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



    //LineRenderer Separation (안쓰는데 왜 만들었지?)
    public GameObject[,,] separationObj;

    // Time Variable
    private float activationMeshRenderer = 2.0f;
    private float originalTime = 0.0f;

    // Generate Variable
    private bool generatePosition = true;

    //Fiber Management Variable (다른 Field Script를 위한)
    public float objID = 0; //(csv File 관리용)


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

            //임시로 만든 Mesh Collider for LineRenderer, 근데 어디서 핀트가 안맞는건지 잘 안먹혀서 방법 바꿈

            MeshCollider collider = GetComponent<MeshCollider>();
            collider = gameObject.AddComponent<MeshCollider>();

            Mesh lineMesh = new Mesh();
            collider.sharedMesh = lineMesh;
            lineRenderer.BakeMesh(lineMesh, true);

            //GetComponent<MeshCollider>().enabled = true;



            // arrayPosition 이용해서 (0,1) 좌표, (1,2) 좌표 이런식으로 선 그으면 되고 null뜨면 그만 두면 됨.
            // 여기서도 bool status variable 추가 해야 할듯 (Update 특성 때문에)

            // 스크립트 하나 더 만들어서 분할 하면 정말 좋을 듯 (여기서 코드 더 짜면 스파게티 코드 될 수도)

            // arrayPosition;
            // numPosition;

            //for (int i = 0; i < numPosition; i++) {


            //arrayPosition을 사용해보자.
            /*
             *  1)Lookat함수로 Vector3 pivot에서 다른 pivot 데이터로 방향 잡고  
                2)유클리드로 거리 재서 x scale arbitary data로 넣어준다음에
                3) Instantiate 함수 써보기.
             * 
             */

            //Debug.Log("배열 크기 확인");
            //Debug.Log(numPosition);
            for (int i = 0; i < numPosition - 1; i++) {

                CentralPos.x = (arrayPosition[i].x + arrayPosition[i+1].x) / 2;
                CentralPos.y = (arrayPosition[i].y + arrayPosition[i+1].y) / 2;
                CentralPos.z = (arrayPosition[i].z + arrayPosition[i+1].z) / 2;

                dirObj = Vector3.Distance(arrayPosition[i], arrayPosition[i+1]);

                //Debug.Log("direction parameter check");
                //Debug.Log(dirObj);
                prefabCapsuleObj.transform.localScale = new Vector3(0.05f, 0.05f, dirObj);

                //여기서 생성해야하고 FiberCollision부분에 ID 할당해야함
                prefabCapsuleObjNum[i] = Instantiate(prefabCapsuleObj, CentralPos, Quaternion.identity);

                //Child Script인 FiberCollision에 Number Allocation
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
