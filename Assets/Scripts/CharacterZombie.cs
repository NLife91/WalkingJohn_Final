using UnityEngine;
using System.Collections;

public class CharacterZombie : MonoBehaviour
{
    public enum State
    {
        Idle, Right, Left, Up, Down, Dead
    }

    public float movePixel = 1f;
    public float moveSpeed = 2f;
    public State state = State.Idle;

    [HideInInspector]
    public new Transform transform;
    private Animator _animator;

    public GameObject johnGhost; // 나중에 private로 만들어야됨. 리스폰매니저가 좀비 생성할때 johnGhostGhost가지고 있다가 대입해주어야함
    private Vector2 prevPosition;
    private Vector2 nextPosition;
    public float range = 5f;

    private new Rigidbody2D rigidbody2D;
    private bool isCalcued;
    public bool isMoving;
    public float r = 6f;

    public int zombieValue = 10;

    void Awake()
    {
        transform = GetComponent<Transform>();
        prevPosition = transform.position;
        nextPosition = transform.position;
        //_animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isCalcued = false;
        isMoving = false;
    }

    void Update()
    {
        if (state == State.Dead)
            return;
        else if (state == State.Idle)
        {
            if (isMoving != true && ManagerGame.ghostMoved != true)
                isCalcued = false;
        }

        if (ManagerGame.ghostMoved == true && isCalcued == false)
        {
            ProcessNextPosition();
        }
        else if (isMoving == true && isCalcued == true)
        {
            Invoke(state.ToString(), 0.0f);
        }
    }

    void ProcessNextPosition()
    {
        prevPosition = transform.position;
        float tx = transform.position.x;
        float ty = transform.position.y;
        float jx = johnGhost.transform.position.x;
        float jy = johnGhost.transform.position.y;
        float distanceX = Mathf.Abs(tx - jx);
        float distanceY = Mathf.Abs(ty - jy);
        Vector2 next = transform.position;

        r = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

        if (r <= range)
        {
            if (distanceX < distanceY)
            {
                if (ty < jy)
                {
                    // ^
                    next.y += movePixel;
                    nextPosition = next;
                    state = State.Up;
                }
                else
                {
                    // v
                    next.y -= movePixel;
                    nextPosition = next;
                    state = State.Down;
                }
            }
            else if (distanceX > distanceY)
            {
                if (tx < jx)
                {
                    // >
                    next.x += movePixel;
                    nextPosition = next;
                    state = State.Right;
                }
                else
                {
                    // <
                    next.x -= movePixel;
                    nextPosition = next;
                    state = State.Left;
                }
            }
            else // distanceX == distanceY 정사각형
            {
                int selectNumber = Random.Range(0, 2);

                if (ty > jy)
                {
                    if (tx > jx)
                    {
                        if (selectNumber == 0)
                        {
                            // <
                            next.x -= movePixel;
                            nextPosition = next;
                            state = State.Left;
                        }
                        else if (selectNumber == 1)
                        {
                            // v
                            next.y -= movePixel;
                            nextPosition = next;
                            state = State.Down;
                        }
                    }
                    else
                    {
                        if (selectNumber == 0)
                        {
                            // > 
                            next.x += movePixel;
                            nextPosition = next;
                            state = State.Right;
                        }
                        else if (selectNumber == 1)
                        {
                            // v
                            next.y -= movePixel;
                            nextPosition = next;
                            state = State.Down;
                        }
                    }
                }
                else
                {
                    if (tx > jx)
                    {
                        if (selectNumber == 0)
                        {
                            // <
                            next.x -= movePixel;
                            nextPosition = next;
                            state = State.Left;
                        }
                        else if (selectNumber == 1)
                        {
                            // ^
                            next.y += movePixel;
                            nextPosition = next;
                            state = State.Up;
                        }
                    }
                    else
                    {
                        if (selectNumber == 0)
                        {
                            // > 
                            next.x += movePixel;
                            nextPosition = next;
                            state = State.Right;
                        }
                        else if (selectNumber == 1)
                        {
                            // ^
                            next.y += movePixel;
                            nextPosition = next;
                            state = State.Up;
                        }
                    }
                }
            }
        }


        isMoving = true;
        //ManagerGame.zombieMove = true;
        isCalcued = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "JohnGhost")
        {
            Debug.Log("PlayerCollision");
            UI_HPGauge.HPDamage(zombieValue); // ManagerGame.john_hp -= zombieValue;
                                              // HP 게이지 조작
                                              //메세지Test
            Debug.Log("Zombie 부딪혀서 HP깎임 - ManagerGame.john_hp = "
                        + ManagerGame.john_hp);

            state = State.Idle;
            isMoving = false;
            GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        }
        else if (coll.gameObject.tag == "Zombie")
        {
            if (r > coll.gameObject.GetComponent<CharacterZombie>().r)
            {
                nextPosition = prevPosition;
                transform.position = prevPosition;
                //state = State.Idle;
                //isMoving = false;
            }
        }
        else if (coll.gameObject.tag == "Tile_Rock" || coll.gameObject.tag == "Tile_Sea")
        {
            nextPosition = prevPosition;
            transform.position = prevPosition;
        }

    }

    /** For Animation State **/
    void Idle()
    {
        //ManagerGame.zombieMove = false;
        isMoving = false;
        isCalcued = false;
    }

    void Right()
    {
        if (MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed) == 0.0f)
        {
            transform.position = nextPosition;
            state = State.Idle;
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void Left()
    {
        if (MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed) == 0.0f)
        {
            transform.position = nextPosition;
            state = State.Idle;
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void Up()
    {
        if (MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed) == 0.0f)
        {
            transform.position = nextPosition;
            state = State.Idle;
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void Down()
    {
        if (MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed) == 0.0f)
        {
            transform.position = nextPosition;
            state = State.Idle;
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }
}