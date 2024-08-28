using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenue : MonoBehaviour
{
   public GameObject pausePanel;

   public void PlayGame()
   {
      SceneManager.LoadScene(1);
   }

   public void MainMenu()
   {
      Time.timeScale = 1f;  
      SceneManager.LoadScene(0);
   }
   public void ScorePage()
   {
      SceneManager.LoadScene(2);
   }
   public void QuitAppe()
   {
      Application.Quit();
   }

   public void Pause()
   {
      Time.timeScale = 0;
      pausePanel.SetActive(true);
   }

   public void Resume()
   {
      pausePanel.SetActive(false);
      Time.timeScale = 1f;
   }
}
