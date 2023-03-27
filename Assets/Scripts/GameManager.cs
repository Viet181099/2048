using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
   public TileBoard board;
   public CanvasGroup gameOver;
   public CanvasGroup gameWin;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI hiscoreText;
   
   
   public AudioSource aus;
   public AudioClip loseSound;
   public AudioClip winSound;
  //public static SoundController instance;

   private int score;

   private void Start()
   {
      NewGame();
   }

   public void NewGame()
   {
      SetScore(0);
      hiscoreText.text = LoadHiscore().ToString();

      gameOver.alpha = 0f;
      gameWin.alpha = 0f;
      gameOver.interactable = false;
      gameWin.interactable = false;

      board.ClearBoard();
      board.CreateTile();
      board.CreateTile();
      board.enabled = true;
   }
   public void GameWin()
   {
       if (loseSound)
      {
         aus.PlayOneShot(winSound);
      } 
      board.enabled = false;
      gameWin.interactable = true;
      StartCoroutine(Fade(gameWin, 1f, 1f));
            
   }
   
   public void CountinueGame()
   {
      board.enabled = true;
      gameWin.interactable = true;
      StartCoroutine(Fade(gameWin, 0f, 0f));
   }

   public void GameOver()
   {
      if (loseSound)
      {
         aus.PlayOneShot(loseSound);
      } 
      board.enabled = false;
      gameOver.interactable = true;
      

      StartCoroutine(Fade(gameOver, 1f, 1f));
   }

   

   private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
   {
      yield return new WaitForSeconds(delay);

      float elapsed = 0f;
      float duration = 0.5f;
      float from = canvasGroup.alpha;

      while (elapsed < duration)
      {
        canvasGroup.alpha = Mathf.Lerp(from, to , elapsed / duration);
        elapsed += Time.deltaTime;
        yield return null;
      }

      canvasGroup.alpha = to;
   }

   public void IncreasScore(int points)
   {
      SetScore(score + points);
   }

   private void SetScore(int score)
   {
      this.score = score;
      scoreText.text = score.ToString();

      SaveHiscore();
   }

   private void SaveHiscore()
   {
      int hiscore = LoadHiscore();

      if (score > hiscore)
      {
         PlayerPrefs.SetInt("hiscore", score);
      }
   }

   private int LoadHiscore()
   {
      return PlayerPrefs.GetInt("hiscore", 0);
   }

   
}
