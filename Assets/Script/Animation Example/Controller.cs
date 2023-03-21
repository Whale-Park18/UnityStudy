using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator _anim;

    [Range(0f, 1f)]
    public float changeTime = 0.7f;
    private float weight = 1f;
    private float currentWeight;

    private void Start()
    {
        currentWeight = weight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("Throw");
        }

        // 1번 레이어의 애니메이션이 70퍼(0.7f) 초과해서 진행됐다면
        print(_anim.GetCurrentAnimatorStateInfo(1).normalizedTime);
        if(_anim.GetCurrentAnimatorStateInfo(1).normalizedTime > changeTime)
        {
            if(currentWeight > 0f)
            {
                currentWeight -= Time.deltaTime;
            }

            _anim.SetLayerWeight(1, currentWeight);
        }
    }
}
