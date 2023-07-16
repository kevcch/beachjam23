using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsolateRotation : MonoBehaviour
{
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, -transform.parent.rotation.eulerAngles.z);
    }
}
