using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame() {
        GameManager.Instance.SetIsGamePaused(false);
    }

    public void GoToMainMenu() {
        GameManager.Instance.SetIsGamePaused(false);
        SceneManager.LoadScene("Main Menu");
    }
}
