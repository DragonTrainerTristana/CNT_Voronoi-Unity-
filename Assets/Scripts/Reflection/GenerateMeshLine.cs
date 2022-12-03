using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeshLine : MonoBehaviour
{

    //LineRenderer
    public LineRenderer lineRenderer;
    private int numLineRenderer = 0;
   

    // public GameObject Ray
    RayReflection rayReflection;


    private int numPosition;
    private int reflections;

    // Time Variable
    private float originalTime = 0;
    private float boundaryTime = 3.0f;
    private bool isTime = true;

    // Generate Variable
    //private bool linegenerate = true;

    // Start is called before the first frame update
    void Start()
    {

        rayReflection = GameObject.Find("Beam").GetComponent<RayReflection>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

    }

 
    void Update()
    {
        originalTime += Time.deltaTime;


        if (isTime == true && originalTime >= boundaryTime) // 시간 지나서 축척된 데이터 가져오기
        {
            isTime = false;
            reflections = rayReflection.reflections;
            numPosition = rayReflection.numPosition;

            lineRenderer.SetPosition(0, rayReflection.arrayPosition[1]);
            lineRenderer.SetPosition(1, rayReflection.arrayPosition[2]);

            //lineRenderer.SetPosition(1, rayReflection.arrayPosition[1]);
            //lineRenderer.SetPosition(2, rayReflection.arrayPosition[2]);

            /*
            for (int i = 0; i < numPosition; i++) {

                lineRenderer.SetPosition(numLineRenderer, rayReflection.arrayPosition[i]);
                numLineRenderer++;

            }*/
        }
        
        
    }
}
