using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class EditorLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LineRenderer>().enabled = false;
    }
}
