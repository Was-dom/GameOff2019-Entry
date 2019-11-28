using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
  public void NextScene()
  {
      SceneManager.LoadScene("Game");
  }

  public void doExitGame()
  {
      Application.Quit();
  }

  public void HowScene()
  {
      SceneManager.LoadScene("Tut");
  }

  public void BackScene()
  {
      SceneManager.LoadScene("Menu");
  }
}
