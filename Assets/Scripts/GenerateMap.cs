using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour
{

    //지형의 종류 1. Rock 2.Sea  //보류. 3. mine
    //private float tileNum = 3f;

    // 타일 중심 간의 간격
    public float distance = 1f;

    // 맵의 시작, 끝 포인트 변수
    public double startPoint_x;
    public double startPoint_y;
    public double endPoint_x;
    public double endPoint_y;

    // 각 줄에 몇 개의 Rock, Sea 타일을 만들건지.
    public int Balance_TileNum;


    Vector3 pos_map; //= new Vector3(1, 1, 0);

    private double i;


    void Start()
    {
        ManagerGame.CreateMapObject();
    }

    private float[] tilePoint = new float[2];

    float x_pos;
    void Update()
    {
        if (ManagerGame.gamePhase == 1)
        {
            //FrameRate 늦춤
            Application.targetFrameRate = 10;

            // 맵 시작,끝 포인트를 초기화함.(x좌표 y좌표)
            startPoint_x = -24.5;
            startPoint_y = 24.5;
            endPoint_x = 24.5;
            endPoint_y = -24.5;

            // 각 줄당 타일의 갯수를 초기화
            Balance_TileNum = 2;

            //맵 생성
            CreateMap();
            ManagerGame.gamePhase = 2;
        }

    }

    void CreateRock(Vector3 position)
    {
        //rock 생성
        GameObject map_rock = Instantiate(Resources.Load("Map/Tile_Rock"),
            position, Quaternion.identity) as GameObject;

        // rock tile 생성한 것을 Map오브젝트의 Child로 넣자
        map_rock.transform.parent = GameObject.Find("Map").transform;
    }

    void CreateSea(Vector3 position)
    {
        //sea 생성
        GameObject map_sea = Instantiate(Resources.Load("Map/Tile_Sea"),
            position, Quaternion.identity) as GameObject;

        // sea tile 생성한 것을 Map오브젝트의 Child로 넣자
        map_sea.transform.parent = GameObject.Find("Map").transform;
    }

    int test = 0;

    // Start()에서 맵 뿌리는 함수.
    void CreateMap()
    {
        // 랜덤을 뽑아가지고 한줄에 tileNum개씩 타일을 뿌린다.
        // 
        for (int i = -25; i < 25; i++)
        {

            x_pos = i + 0.5f;
            for (int j = 0; j < Balance_TileNum; j++)
            {
                tilePoint[j] = (int)Random.Range(-24F, 25F); //y좌표 뽑음
                if (j == 0)
                {
                    CreateRock(new Vector3(x_pos + 0.5f, tilePoint[j], 0));

                    Debug.Log(new Vector3(x_pos + 0.5f, tilePoint[j], 0) + "에 돌 만듦");
                }
                else
                {
                    CreateSea(new Vector3(x_pos + 0.5f, tilePoint[j], 0));

                    Debug.Log(new Vector3(x_pos + 0.5f, tilePoint[j], 0) + "에 바다 만듦");
                }

                //Debug.Log(generatePoint);

                test++;



                //중복일 경우
                if (j == 1 && (tilePoint[0] == tilePoint[1]))
                {
                    break;

                }

            }

        }


        //Debug.Log("루프빠져나옴");
        //Debug.Log("돌 갯수 : " + test);

    }
}