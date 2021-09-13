using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UndegroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Debug.Log("Object");
                Level.Instance.ObjectsInScene--;
                UIManager.Instance.UpdateLevelProgress();

                Magnet.Instance.AddToMagnetField(other.attachedRigidbody);

                Destroy(other.gameObject);

                if(Level.Instance.ObjectsInScene == 0)
                {                    
                    UIManager.Instance.ShowLevelComletedUI();
                    Level.Instance.PlayWinFX();
                    Invoke("NextLevel", 2f);
                }
            }
            if (tag.Equals("Obstacle"))
            {
                Game.isGameOver = true;
                Camera.main.transform.DOShakePosition(1f, 2f, 20, 90f).OnComplete(() =>
                 {
                     Level.Instance.RestartLevel();
                 });
                
            }
        }
    }

    private void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
