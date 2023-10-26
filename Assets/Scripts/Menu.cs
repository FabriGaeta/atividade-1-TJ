using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void AbrirCredito() {
        SceneManager.LoadScene("Credits");
    }
    public void AbrirLeaderboard() {
        SceneManager.LoadScene("Leaderboard");
    }
    public void AbrirGame() {
        SceneManager.LoadScene("Game");
    }
    public void AbrirTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
    public void Sair() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
