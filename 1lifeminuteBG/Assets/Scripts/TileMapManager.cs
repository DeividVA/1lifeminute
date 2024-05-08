using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private Tilemap background;
    [SerializeField] private Tilemap grid;
    [SerializeField] private Tile tileBackground;

    [SerializeField] private int sceneSize;

    [SerializeField] private List<Tile> tileAtom;
    [SerializeField] private int atomSize;

    [SerializeField] private int platformNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int position;
        // fill background
        for (int i = 0; i < sceneSize*atomSize; i++)
            for (int j = 0; j < sceneSize*atomSize; j++)
            {
                position = new Vector3Int(i, j, 0);
                background.SetTile(position, tileBackground);

            }
        
        // fill every cell with platforms
        // for (int i = 0; i < sceneSize; i++)
        //     for (int j = 0; j < sceneSize; j++)
        //     {
        //         PutPlatform(i, j);
        //     }
        
        // platform matrix
        List<List<bool>> platforms = new List<List<bool>>();
        for (int i = 0; i < sceneSize; i++)
        {
            platforms.Add(Enumerable.Repeat<bool>(false, sceneSize).ToList());
        }
        
        platforms[0][0] = true;
        
        // console debug map
        // for(int i=0; i<sceneSize; i++)
        // for(int j=0; j<sceneSize; j++)
        //     Debug.Log($"{i} {j} {platforms[i][j]}");
        
        System.Random random = new System.Random();
        // random platforms generation
        for (int i = 0; i < platformNumber; i++)
        {
            int x, y;
            do
            {
                x = random.Next(sceneSize);
                y = random.Next(sceneSize);
            } while (platforms[x][y]);

            PutPlatform(x, y);
            platforms[x][y] = true;
        }
        

    }

    void PutPlatform(int i, int j)
    {
        Vector3Int position;
        position = new Vector3Int(i*atomSize, j*atomSize + (atomSize -1), 0);
        for (int k = 0; k < atomSize; k++)
        for (int l = 0; l < atomSize; l++)
        {
            Vector3Int atomPos = position + new Vector3Int(l, -k, 0);
            Tile atomTile = tileAtom[k * atomSize + l];
            grid.SetTile(atomPos, atomTile);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
