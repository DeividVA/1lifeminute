using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Tilemap background;
    [SerializeField] private Tilemap grid;
    [SerializeField] private Tile tileBackground;

    //How many atoms in the stage? If sceneSize = 5, the scene will be 5x5 (25 atoms)
    [SerializeField] private int sceneSize;

    //List of tiles to assign the platform graphics
    [SerializeField] private List<Tile> tileAtom;
    
    //How many tiles in one atom? If atomSize = 3, the atom will be 3x3 tiles (9)
    [SerializeField] private int atomSize;

    //Quantity of platforms that will be placed in the Generation
    [SerializeField] private int platformNumber;

    [SerializeField] private GameObject box;
    [SerializeField] private int boxNumber;

    private int randomTest = 10;
    //private string[] foodType = new List<> {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
    [SerializeField] List<string> foodType = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Vector3Int position;
        // fill background
        for (int i = 0; i < sceneSize * atomSize; i++)
            for (int j = 0; j < sceneSize * atomSize; j++)
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
        PutPlatform(0, 0);
        // console debug map
        // for(int i=0; i<sceneSize; i++)
        // for(int j=0; j<sceneSize; j++)
        //     Debug.Log($"{i} {j} {platforms[i][j]}");

        System.Random random = new System.Random();
        // random platform generation
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



        // random box generation
        for (int i = 0; i < boxNumber; i++)
        {
            int x, y;

            int index;

            do
            {
                x = random.Next(sceneSize);
                y = random.Next(sceneSize);
                index = random.Next(foodType.Count);
                if (platforms[x][y] == true)
                    Instantiate(box, GetWorldPosition(x, y), Quaternion.identity);
                    Debug.Log($"{foodType[index]}");
                    
            } while (platforms[x][y]);
        }

        // detects first platform from top-right
        bool hasInitialPosition = false;
        for (int i = sceneSize - 1; !hasInitialPosition && i >= 0; i--)
            for (int j = sceneSize - 1; !hasInitialPosition && j >= 0; j--)
            {
                if (platforms[j][i])
                {
                    hasInitialPosition = true;
                    Instantiate(player, GetWorldPosition(j, i), Quaternion.identity);
                }
            }


    }

    void PutPlatform(int i, int j)
    {
        Vector3Int position;
        position = new Vector3Int(i * atomSize, j * atomSize + (atomSize - 1), 0);
        for (int k = 0; k < atomSize; k++)
            for (int l = 0; l < atomSize; l++)
            {
                Vector3Int atomPos = position + new Vector3Int(l, -k, 0);
                Tile atomTile = tileAtom[k * atomSize + l];
                grid.SetTile(atomPos, atomTile);
            }
    }

    Vector3 GetWorldPosition(int i, int j)
    {
        Vector3Int position = new Vector3Int(i * atomSize + 1, j * atomSize + (atomSize - 1), 0);
        return grid.CellToWorld(position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
