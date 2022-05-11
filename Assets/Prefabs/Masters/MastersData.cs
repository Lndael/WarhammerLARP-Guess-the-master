using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MastersData : MonoBehaviour
{
    public static List<MasterPrefab> MastersList;
    
    // Start is called before the first frame update
    void Start()
    {
        MastersList = new List<MasterPrefab>();
        foreach (MasterPrefab masterPrefab in Resources.LoadAll("Masters"))
        {
            MastersList.Add(masterPrefab);
            Debug.Log(masterPrefab.name);
            Debug.Log(masterPrefab.GetType());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
