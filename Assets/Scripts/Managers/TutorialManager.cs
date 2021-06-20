using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool shootZone=false, slideZone = false;
    [SerializeField]
    private Image handImage;
    [SerializeField]
    private PlayerCombat playerCombat;

    bool slideTutorial = false, shootTutorial = false, stopTutorial = false;

    void Update()
    {
        if (slideTutorial)
        {
            SlideTutorial();
        }
        if (shootTutorial)
        {
            ShootTutorial();
        }
        
        if (playerCombat.characterDied)
        {
            CharacterDead(); // Stops tutorials when character is dead.
        }
        if (stopTutorial)
        {
            StopTutorial();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stopTutorial = false;
            if (shootZone)
            {
                shootTutorial = true;
            }
            if (slideZone)
            {
                slideTutorial = true;
            }
            Time.timeScale = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stopTutorial = true;
        }
    }
    void ShootTutorial()
    {
        handImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 50, 0);

        if (handImage.GetComponent<CanvasGroup>().alpha == 0)
        {
            handImage.GetComponent<CanvasGroup>().DOFade(1, 0.25f);
        }
        if (handImage.GetComponent<CanvasGroup>().alpha == 1)
        {
            handImage.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
        }
    }
    void SlideTutorial()
    {
        if (handImage.GetComponent<CanvasGroup>().alpha <= 0)
        {
            handImage.GetComponent<CanvasGroup>().DOFade(1, 0.75f);
        }
        if (handImage.GetComponent<CanvasGroup>().alpha >= 1)
        {
            handImage.GetComponent<CanvasGroup>().DOFade(0, 0.75f);
        }


        if (handImage.GetComponent<RectTransform>().anchoredPosition.x == 220)
        {
            handImage.GetComponent<RectTransform>().DOAnchorPosX(-220, 0.75f);
        }
        if (handImage.GetComponent<RectTransform>().anchoredPosition.x==-220)
        {
            handImage.GetComponent<RectTransform>().DOAnchorPosX(220, 0.75f);
        }
        
    }
    void StopTutorial()
    {
        shootTutorial = false;
        slideTutorial = false;

        if (!playerCombat.characterDied)
        {
            handImage.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
        }

        Time.timeScale = 1;
        stopTutorial = false;
    }
    void CharacterDead()
    {
        GetComponent<Collider>().enabled = false;
        stopTutorial = true;
        Destroy(this.handImage);
    }
}
