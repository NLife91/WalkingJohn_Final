using UnityEngine;
using System.Collections;

public class CreateMapTest : MonoBehaviour {

    // Tile_Plane의 스프라이트를 불러오기 위한 변수.
    public Sprite Sprite_TilePlane;

 

    //지형의 종류 1. plane 2.rock 3. mine
    private float tileNum = 3f;

    public int startPoint_x;
    public int startPoint_y;
    public int endPoint_x;
    public int endPoint_y;

    //Plane타일의 프리팹을 할당하는 변수.
    public GameObject tile_prefab =
               Resources.Load("Map/testObj", typeof(GameObject)) as GameObject;//("Prefab_Tile_Plane");
    GameObject map;
    
    //test
       public GameObject testMap = Resources.Load("Test/Prefabs/testMap", typeof(GameObject)) as GameObject;//("Prefab_Tile_Plane");

    // Use this for initialization
    void Start ()
    {
        // 맵 시작 포인트를 초기화함.(x좌표 y좌표)
        startPoint_x = -25;
        startPoint_y = 25;
        endPoint_x = 25;
        endPoint_y = -25;

        // Tile_Plane에 리소스를 할당함.
        Sprite_TilePlane = Resources.Load("Map/Plane", typeof(Sprite)) as Sprite;

        //John의 초기화 위치.
        Vector3 pos = new Vector3(0, 0, 0);


        //pos의 위치에 Plane을 생성하는 함수를 호출함.
        CreatePlane(pos);


        //map generating test
        Vector3 pos_map = new Vector3(1, 1, 0);
        CreateMapPrefab(pos_map);

    }
	
	// Update is called once per frame
	void Update ()
    {
        int randNumber = (int) Random.Range(0f, tileNum);
        Debug.Log("randNumber : " + randNumber);

       
    }

    public void CreatePlane(Vector2 pos)
    {
        GameObject tile = new GameObject();
        tile.transform.position = pos;
        tile.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
        tile.GetComponent<SpriteRenderer>().sprite = Sprite_TilePlane;
    }

    public void CreateMapPrefab(Vector3 pos_map)
    {
       // map = Instantiate(tile_prefab, pos, Quaternion.identity);
        Instantiate(Resources.Load("Map/testMap"), pos_map, Quaternion.identity);
         
    }
}
