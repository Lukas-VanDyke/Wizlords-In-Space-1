using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTorch : MonoBehaviour {
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        anim.Play("BurningTorch");
    }
}
