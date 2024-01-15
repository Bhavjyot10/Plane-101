using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxManager : MonoBehaviour
{
    public GameObject[] vfxPrefabs;
    
    public void SpawnVFX(int index, Transform t)
    {
        Instantiate(vfxPrefabs[index], t.position, Quaternion.identity);
    }
}
