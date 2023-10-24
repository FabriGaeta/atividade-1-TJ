using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chamaCredito : MonoBehaviour
{
    public void AbrirCredito() {
        SceneManager.LoadScene("Credits");
    }
}
