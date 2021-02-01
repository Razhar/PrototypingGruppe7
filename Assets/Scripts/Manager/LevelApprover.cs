using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LevelApprover : MonoBehaviour
{
    public Tilemap spawnMap;

    public Tile boxTile;
    public GameObject prefabBox;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (spawnMap.ContainsTile(boxTile))
        {
            Debug.Log("Es gibt eine oder mehrere Boxen!");

            BoundsInt bounds = spawnMap.cellBounds;
            TileBase[] allTiles = spawnMap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        if (tile == boxTile)
                        {
                            Debug.Log("x:" + x + " y:" + y + " Box!");
                            Instantiate(prefabBox, new Vector3(spawnMap.origin.x + x + 0.5f, spawnMap.origin.y + y + 0.5f, 0f), Quaternion.identity);
                        }
                        else
                        {
                            Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        }
                    }
                }
            }
            spawnMap.ClearAllTiles();

        }
        else
        {
            Debug.Log("Keine Boxen gefunden!");
        }
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
}
