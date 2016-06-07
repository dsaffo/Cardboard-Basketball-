using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Score : MonoBehaviour
{
    private string filePath = Application.persistentDataPath + "/results.txt";
    private string fileName;
    public Text score;
    private int totalscore;


    void OnTriggerEnter()
    {
        int currentscore = int.Parse(score.text) + 1;
        score.text = currentscore.ToString();
        totalscore = currentscore;

    }

    /*  void OnApplicationQuit()
      {

          if (!File.Exists(filePath))
          {

              // Create a file to write to. 
              using (StreamWriter sw = File.CreateText(filePath))
              {
                  sw.WriteLine(totalscore.ToString());
              }
          }
          // Open the file to read from. 


              // This text is always added, making the file longer over time 
              // if it is not deleted. 
              using (StreamWriter sw = File.AppendText(filePath))
              {
                  sw.WriteLine(totalscore);
              }
          }
         // highScoreText.text = highscore.ToString();

      }
      */
}


    