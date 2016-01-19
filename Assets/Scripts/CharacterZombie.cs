using UnityEngine;
using System.Collections;

public class CharacterZombie : MonoBehaviour
{
    public enum Zombie_Movement
    {
        zombie_right_idle = 1, zombie_left_idle, zombie_back_idle, zombie_forward_idle, zombie_right = 11, zombie_left = 22, zombie_back = 33, zombie_forward = 44, zombie_dead
    }

    public float movePixel = 1f;
    public float moveSpeed = 2f;
    public Zombie_Movement zombie_movement = Zombie_Movement.zombie_forward_idle;

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
        _animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isCalcued = false;
        isMoving = false;
    }

    void Update()
    {
        if (zombie_movement == Zombie_Movement.zombie_dead)
            return;
        else if (zombie_movement == Zombie_Movement.zombie_back_idle || zombie_movement == Zombie_Movement.zombie_forward_idle || zombie_movement == Zombie_Movement.zombie_right_idle || zombie_movement == Zombie_Movement.zombie_left_idle)
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
            Invoke(zombie_movement.ToString(), 0.0f);
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
                    zombie_movement = Zombie_Movement.zombie_back;
                }
                else
                {
                    // v
                    next.y -= movePixel;
                    nextPosition = next;
                    zombie_movement = Zombie_Movement.zombie_forward;
                }
            }
            else if (distanceX > distanceY)
            {
                if (tx < jx)
                {
                    // >
                    next.x += movePixel;
                    nextPosition = next;
                    zombie_movement = Zombie_Movement.zombie_right;
                }
                else
                {
                    // <
                    next.x -= movePixel;
                    nextPosition = next;
                    zombie_movement = Zombie_Movement.zombie_left;
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
                            zombie_movement = Zombie_Movement.zombie_left;
                        }
                        else if (selectNumber == 1)
                        {
                            // v
                            next.y -= movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_back;
                        }
                    }
                    else
                    {
                        if (selectNumber == 0)
                        {
                            // > 
                            next.x += movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_right;
                        }
                        else if (selectNumber == 1)
                        {
                            // v
                            next.y -= movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_forward;
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
                            zombie_movement = Zombie_Movement.zombie_left;
                        }
                        else if (selectNumber == 1)
                        {
                            // ^
                            next.y += movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_back;
                        }
                    }
                    else
                    {
                        if (selectNumber == 0)
                        {
                            // > 
                            next.x += movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_right;
                        }
                        else if (selectNumber == 1)
                        {
                            // ^
                            next.y += movePixel;
                            nextPosition = next;
                            zombie_movement = Zombie_Movement.zombie_back;
                        }
                    }
                }
            }
        }

        _animator.SetInteger("zombie_movement", (int)zombie_movement);
        isMoving = true;
        //ManagerGame.zombieMove = true;
        isCalcued = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "JohnGhost")
        {
            Debug.Log("PlayerCollision");
            CameraRigScript.ShakeCamera(0.2f, 0.2f);

            UI_HPGauge.HPDamage(zombieValue); // ManagerGame.john_hp -= zombieValue;
                                              // HP 게이지 조작
                                              //메세지Test
            Debug.Log("Zombie 부딪혀서 HP깎임 - ManagerGame.john_hp = "
                        + ManagerGame.john_hp);
            ShowMessage2.ShowMessage_Zombie();

            zombie_movement = Zombie_Movement.zombie_forward_idle;
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

    void zombie_forward_idle()
    {
        isMoving = false;
        isCalcued = false;
    }

    void zombie_back_idle()
    {
        isMoving = false;
        isCalcued = false;
    }

    void zombie_right_idle()
    {
        isMoving = false;
        isCalcued = false;
    }

    void zombie_left_idle()
    {
        isMoving = false;
        isCalcued = false;
    }

    void zombie_right()
    {
        if ((r = MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed)) == 0.0f)
        {
            transform.position = nextPosition;
            zombie_movement = Zombie_Movement.zombie_right_idle;
            _animator.SetInteger("zombie_movement", (int)zombie_movement);
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void zombie_left()
    {
        if ((r = MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed)) == 0.0f)
        {
            transform.position = nextPosition;
            zombie_movement = Zombie_Movement.zombie_left_idle;
            _animator.SetInteger("zombie_movement", (int)zombie_movement);
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void zombie_back()
    {
        if ((r = MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed)) == 0.0f)
        {
            transform.position = nextPosition;
            zombie_movement = Zombie_Movement.zombie_back_idle;
            _animator.SetInteger("zombie_movement", (int)zombie_movement);
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }

    void zombie_forward()
    {
        if ((r = MoveUtil.MoveByFrame(transform, nextPosition, moveSpeed)) == 0.0f)
        {
            transform.position = nextPosition;
            zombie_movement = Zombie_Movement.zombie_forward_idle;
            _animator.SetInteger("zombie_movement", (int)zombie_movement);
            isMoving = false;
            //ManagerGame.zombieMove = false;
            isCalcued = false;
            return;
        }
    }
}