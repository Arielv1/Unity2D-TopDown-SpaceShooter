using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidGenerator : MonoBehaviour
{

    [SerializeField] private float xRange, yRange;
    public GameObject[] myPrefabs;
    //private AstroidBehaviour astroidBehaviour;
    public int amount;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        xRange = Mathf.Abs(xRange);
        yRange = Mathf.Abs(yRange);
        for (int i = 0; i < amount; i++)
        {
            GameObject clone = Instantiate(myPrefabs[Random.Range(0, myPrefabs.Length)], 
                new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0), 
                Quaternion.identity);
            clone.AddComponent(typeof(AstroidBehaviour));
            clone.tag = "Astroid";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
