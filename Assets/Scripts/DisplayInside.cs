using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayInside : MonoBehaviour
{
    public void DisplayTruck() {
        SceneManager.LoadScene("Inside", LoadSceneMode.Additive);
    }
}
