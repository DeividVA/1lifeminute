//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class TileMapManagerN : MonoBehaviour
//{

//    //Variables
//    [SerializeField] private GameObject player;
//    [SerializeField] private Tilemap background;
//    [SerializeField] private Tilemap grid;
//    [SerializeField] private Tile tileBackground;
//    //How many atoms in the stage? If sceneSize = 5, the scene will be 5x5 (25 atoms)
//    [SerializeField] private int sceneSize;
//    //List of tiles to assign the platform graphics
//    [SerializeField] private List<Tile> tileAtom;
//    //How many tiles in one atom? If atomSize = 3, the atom will be 3x3 tiles (9)
//    [SerializeField] private int atomSize;
//    //Quantity of platforms that will be placed in the Generation
//    [SerializeField] private int platformNumber;
//    [SerializeField] private GameObject box;
//    [SerializeField] private int boxNumber;
//    [SerializeField] private Vector3 boxOffset;
//    [SerializeField] List<string> foodType = new List<string>();
//    [SerializeField] private GameObject door;


//    // platform matrix
//    List<List<bool>> platforms = new List<List<bool>>();
//    List<List<bool>> boxes = new List<List<bool>>();

//    List<Position> buildedPlatforms = new List<Position>();


//    // Start is called before the first frame update
//    void Start()
//    {
//        do
//        {
//            Generate();
//        } while (!Validate());

//        Create();
//    }

//    void Generate()
//    {
//        Vector3Int position;
//        // fill background
//        for (int i = 0; i < sceneSize * atomSize; i++)
//            for (int j = 0; j < sceneSize * atomSize; j++)
//            {
//                position = new Vector3Int(i, j, 0);
//                background.SetTile(position, tileBackground);

//            }

//        // generate empty (false) matrix
//        for (int i = 0; i < sceneSize; i++)
//        {
//            platforms.Add(Enumerable.Repeat<bool>(false, sceneSize).ToList());
//            boxes.Add(Enumerable.Repeat<bool>(false, sceneSize).ToList());
//        }

//        //Platform and door at 0,0
//        platforms[0][0] = true;
//        PutPlatform(0, 0);
//        Instantiate(door, GetWorldPosition(0, 0), Quaternion.identity);

//        // random platform generation
//        System.Random random = new System.Random();
//        for (int i = 0; i < platformNumber; i++)
//        {
//            int x, y;
//            do
//            {
//                x = random.Next(sceneSize);
//                y = random.Next(sceneSize);
//            } while (platforms[x][y]);


//            PutPlatform(x, y);

//            platforms[x][y] = true;

//        }

//    }

//    bool Validate()
//    {
//        return true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }


//    void PutPlatform(int i, int j)
//    {
//        Vector3Int position;
//        position = new Vector3Int(i * atomSize, j * atomSize + (atomSize - 1), 0);
//        for (int k = 0; k < atomSize; k++)
//            for (int l = 0; l < atomSize; l++)
//            {
//                Vector3Int atomPos = position + new Vector3Int(l, -k, 0);
//                Tile atomTile = tileAtom[k * atomSize + l];
//                grid.SetTile(atomPos, atomTile);
//            }
//    }

//    Vector3 GetWorldPosition(int i, int j)
//    {
//        Vector3Int position = new Vector3Int(i * atomSize + 1, j * atomSize + (atomSize - 1), 0);
//        return grid.CellToWorld(position);
//    }


//    public class Position
//    {
//        public int Row;
//        public int Col;

//        public Position(int row, int col)
//        {
//            Row = row;
//            Col = col;
//        }
//    }

//}
