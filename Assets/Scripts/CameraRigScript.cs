﻿using UnityEngine;
using System.Collections;

public class CameraRigScript : MonoBehaviour
{

    public Transform target;

    public float followSpeed = 0.2f;

    private Vector3 _velocity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = target.position;
        //거리 계산은 에디터에서 한다.
        // CameraRig의 position 은Player와 똑같이 일단 맞추고 
        //Main Camera를 CameraRig 오브젝트의 자식으로 넣는 방법으로..

        //방법2
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, followSpeed);
        // ref : 참조전달 포인터의 역할.. 1에서 2로 바꾸는데 velocity값을 가지고 followSpeed의 시간만큼 딜레이
        // 1.선언값은 1 -> 2.에디터값은 2 이면 Play 하면 2값으로 적용된다. 

    }
}