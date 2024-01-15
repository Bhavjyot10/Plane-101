using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform planeTransform;
    public GameObject[] spawnPrefab;
    private Transform nextStartPoint;
    private Transform nextEndPoint;
    public float checkDistance;
    public Transform sectionsParent;
    private GameObject activeSectionObject;
    public GameObject PlanePrefab;
    private GameObject activePlaneObject;
    private Transform planeEndPoint;
    public float planeSpawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        activeSectionObject = sectionsParent.GetChild(0).gameObject;
        nextStartPoint = activeSectionObject.transform.Find("start pt");
        nextEndPoint = activeSectionObject.transform.Find("end pt");
        activePlaneObject = sectionsParent.GetChild (1).Find("Plane").gameObject;
        planeEndPoint = activePlaneObject.transform.Find("end point");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(planeTransform.position, nextStartPoint.position) < checkDistance)
        {
            activeSectionObject = Instantiate(spawnPrefab[RandomNum()], nextEndPoint.position, Quaternion.identity, sectionsParent);
            nextStartPoint = activeSectionObject.transform.Find("start pt");
            nextEndPoint = activeSectionObject.transform.Find("end pt"); ;
        }

        if(Vector3.Distance(planeTransform.position, planeEndPoint.position) < planeSpawnDistance)
        {
            activePlaneObject = Instantiate(PlanePrefab, planeEndPoint.position, Quaternion.identity, sectionsParent);
            planeEndPoint = activePlaneObject.transform.Find("Plane").transform.Find("end point");
        }
    }

    int RandomNum()
    {
        return Random.Range(0, spawnPrefab.Length);
    }
}
