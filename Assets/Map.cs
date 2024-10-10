
using UnityEngine;


public class Map : MonoBehaviour
{
    public GameObject solidSquare;
    public GameObject solidFood;

    public static int HEIGHT = 14;
    public static int WIDTH = 23;



    // Start is called before the first frame update
    void Start()
    {
        objects = new MapObject[WIDTH, HEIGHT];
        renderMap();
    }



    // Update is called once per frame
    void Update()
    {
        // If space is pressed, clear the map and build a new one
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     renderMap();
        // }

    }

    private void renderMap()
    {
        clear();
        generateMapBorder();
        buildRandomMap();
        addFood();
        render();
    }




    void buildRandomMap()
    {

        int HEIGHT_STEP = 2;
        int level = 1;

        for (int y = HEIGHT_STEP + 1; y < HEIGHT; y += HEIGHT_STEP + 1, level++)
        {

            if (level % 2 == 0)
            {
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    if (objects[x, y - HEIGHT_STEP - 1] != null)
                        continue;

                    MapObject square = new MapObject(x, y, solidSquare);
                    square.walkable = true;
                    add(square);
                }
                continue;
            }

            for (int x = 1; x < WIDTH - 1; x++)
            {
                float probability = 50;

                int length = platformLength(x - 1, y);

                probability = probability + length * 3;


                if (Random.Range(0, 100) <= probability)
                {
                    MapObject square = new MapObject(x, y, solidSquare);
                    square.walkable = true;
                    add(square);
                }

            }
        }

    }

    private int platformLength(int x, int y)
    {
        if (x < 0 || y < 0 || x >= WIDTH || y >= HEIGHT)
            return 0;


        int length = 0;
        for (int i = x; i >= 0; i--)
        {
            if (objects[i, y] != null)
                length++;
            else
                break;
        }
        return length;
    }

    MapObject[,] objects;

    void add(MapObject obj)
    {
        if (obj.x < 0 || obj.y < 0 || obj.x >= WIDTH || obj.y >= HEIGHT)
            return;

        objects[obj.x, obj.y] = obj;
    }

    private void generateMapBorder()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                if (x == 0 || y == 0 || x == WIDTH - 1)
                {
                    MapObject obj = new MapObject(x, y, solidSquare);

                    if (y == 0 && x > 0 && x < WIDTH - 1)
                        obj.walkable = true;

                    add(obj);
                }
            }
        }
    }

    private void addFood()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                if (objects[x, y] == null)

                    continue;

                if (objects[x, y].walkable)
                {
                    MapObject obj = new MapObject(x, y + 1, solidFood);
                    add(obj);
                }

            }
        }
    }



    public void render()
    {
        for (int x = 0; x < WIDTH; x++)
            for (int y = 0; y < HEIGHT; y++)
            {
                if (objects[x, y] != null)
                {
                    objects[x, y].render();
                }
            }
    }

    public void clear()
    {
        for (int x = 0; x < WIDTH; x++)
            for (int y = 0; y < HEIGHT; y++)
            {
                if (objects[x, y] != null)
                {
                    objects[x, y].destroy();
                }
                objects[x, y] = null;

            }
    }


    class MapObject
    {
        public int x;
        public int y;
        public GameObject obj;
        private GameObject objInstance;

        public bool walkable = false;

        public MapObject(int x, int y, GameObject obj)
        {
            this.x = x;
            this.y = y;
            this.obj = obj;
        }

        public void render()
        {
            objInstance = GameObject.Instantiate(obj);
            objInstance.transform.position = new Vector3(x - (Map.WIDTH / 2.0f), y - (Map.HEIGHT / 2.0f), 0.0f);
            objInstance.name = "x_" + x + " _y_" + y + " _" + walkable;
        }


        public void destroy()
        {
            GameObject.Destroy(objInstance);
        }
    }



}





