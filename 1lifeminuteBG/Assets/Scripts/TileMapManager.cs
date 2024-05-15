using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileMapManager : MonoBehaviour
{
    public static TileMapManager Instance;

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

    [SerializeField] private Vector3 boxOffset;

    [SerializeField] FoodTypeListSO foodTypes;

    [SerializeField] private GameObject door;

    private int _points;

    // Start is called before the first frame update
    void Start()
    {


        if (Instance == null)
        {
            Instance = this;
            _points = 0;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }


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
        List<List<bool>> boxes = new List<List<bool>>();

        //List<Position> buildedPlatforms = new List<Position>();

        for (int i = 0; i < sceneSize; i++)
        {
            platforms.Add(Enumerable.Repeat<bool>(false, sceneSize).ToList());
            boxes.Add(Enumerable.Repeat<bool>(false, sceneSize).ToList());
        }




        platforms[0][0] = true;
        PutPlatform(0, 0);
        Instantiate(door, GetWorldPosition(0, 0), Quaternion.identity);

        // console debug map
        // for(int i=0; i<sceneSize; i++)
        // for(int j=0; j<sceneSize; j++)
        //     Debug.Log($"{i} {j} {platforms[i][j]}");
        
        
        // random platform generation
        System.Random random = new System.Random();
        int e = 0;
        while (e < platformNumber)
        {
            int x, y;
            do
            {
                x = random.Next(sceneSize);
                y = random.Next(sceneSize);
            } while (platforms[x][y]);

            if (y != sceneSize - 1)

            {
                if (y == 0)
                {
                    if (platforms[x][y + 1] == true)
                    {
                        continue;
                    }
                    else
                    {
                        PutPlatform(x, y);
                        platforms[x][y] = true;
                        e++;
                        //Position p = new Position(x, y);
                        //buildedPlatforms.Add(p);
                    }
                }
                else if (platforms[x][y + 1] == true || platforms[x][y - 1] == true)
                {
                    continue;
                }
                else
                {
                    PutPlatform(x, y);

                    platforms[x][y] = true;
                    e++;

                    //Position p = new Position(x, y);
                    //buildedPlatforms.Add(p);
                }
            }
            else
            {
                if (platforms[x][y - 1] == true)
                {
                    continue;
                }
                else
                {
                    PutPlatform(x, y);

                    platforms[x][y] = true;
                    e++;

                    //Position p = new Position(x, y);
                    //buildedPlatforms.Add(p);

                }
            }

        }

        //Debug.Log(buildedPlatforms.Count);

        // random box generation
        int b = 0;
        while (b < boxNumber)
        {
            int x, y;

            int index;

            do
            {
                x = random.Next(sceneSize);
                y = random.Next(sceneSize);
                index = random.Next(foodTypes.foods.Count);
            } while (!platforms[x][y]);



            if (x==0 && y ==0)
            { 
                continue; 
            }

            if (platforms[x][y] == true && boxes[x][y] == false)
            {

                GameObject newBox = Instantiate(foodTypes.foods[index].foodPrefab, GetWorldPosition(x, y) + boxOffset, Quaternion.identity);
                Debug.Log(foodTypes.foods[index].food);
                //newBox.GetComponent<SpriteRenderer>().color = foodTypes.foods[index].color;


                boxes[x][y] = true;
                b++;
            }



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


    // HEALTHY POINTS

    public void AddPoints(int quantity)
    {
        _points += quantity;
    }

    public int GetPoints() { return _points; }

    public void ResetPoints() { _points = 0; }


    // Update is called once per frame
    void Update()
    {

    }

    public class Index
    {
        public int Ind;

        public Index(int ind)
        {
            Ind = ind;
        }
    }

    public class FoodToInstance
    {
        public Index ind;
        public FoodTypeSO food;

        public FoodToInstance(Index ind, FoodTypeSO food)
        {
            this.ind = ind;
            this.food = food;
        }
    }


}
