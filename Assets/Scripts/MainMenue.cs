using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenue : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene(1);
   }

   public void MainMenu()
   {
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
}
