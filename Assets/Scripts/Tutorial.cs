using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public void TutorialPrimeiraParte()
    {
        SceneManager.LoadScene("TutorialParte1");
    }
    public void TutorialSegundaParte()
    {
        SceneManager.LoadScene("TutorialParte2");
    }
    public void TutorialTerceiraParte()
    {
        SceneManager.LoadScene("TutorialParte3");
    }
    public void TutorialQuartaParte()
    {
        SceneManager.LoadScene("TutorialParte4");
    }
    public void TutorialQuintaParte()
    {
        SceneManager.LoadScene("TutorialParte5");
    }
}
