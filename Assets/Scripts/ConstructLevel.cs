using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tiles;
    public float width;
    public float height;

    int randTile;
    
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap ()
    {
        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                randTile = Random.Range(0, tiles.Length);  
                Vector2 spawnPosition = new Vector2(x, y);
                Instantiate(tiles[randTile],spawnPosition, gameObject.transform.rotation);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
