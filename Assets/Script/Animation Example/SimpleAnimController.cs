using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimController : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("Flair");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("Silly");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("Break");
        }
    }
}
