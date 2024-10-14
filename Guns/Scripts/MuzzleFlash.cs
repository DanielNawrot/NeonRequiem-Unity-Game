using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public float duration = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

}
