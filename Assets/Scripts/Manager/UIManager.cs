using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviorInstance<UIManager>
{
    [SerializeField] GameObject ButtonZone, TownZone, IntroPanel;
    [SerializeField] Button leftBtn, rightBtn, startBtn;
    [SerializeField] Text txtTown1, txtTown2, txtRouteText, txtIntroText, txtEndText;
    bool isFirstClick = true;
    bool isDisplayEndGame = false;

    public string town1, town2;

    private void Start()
    {
        IntroPanel.SetActive(true);
        ButtonZone.SetActive(false);
        TownZone.SetActive(false);
        txtIntroText.gameObject.SetActive(true);
        txtRouteText.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        txtEndText.gameObject.SetActive(false);

        leftBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetLeft(true);
            PlayerScripts.Instance.isStart = false;
            PlayerScripts.Instance.isFinish = false;
            leftBtn.gameObject.transform.DOMoveY(-200f, 0.5f).SetEase(Ease.InBounce);
            rightBtn.gameObject.transform.DOMoveY(-200f, 0.5f).SetEase(Ease.InBounce);
            txtTown1.gameObject.transform.parent.DOMoveY(1200f, 0.5f).SetEase(Ease.InBounce);
            txtTown2.gameObject.transform.parent.DOMoveY(1200f, 0.5f).SetEase(Ease.InBounce);
            GameManager.Instance.CheckChooseCorrect();
            Invoke("SetActiveFalse", 0.5f);
        });

        rightBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetLeft(false);
            PlayerScripts.Instance.isStart = false;
            PlayerScripts.Instance.isFinish = false;
            leftBtn.gameObject.transform.DOMoveY(-200f, 0.5f).SetEase(Ease.InBounce);
            rightBtn.gameObject.transform.DOMoveY(-200f, 0.5f).SetEase(Ease.InBounce);
            txtTown1.gameObject.transform.parent.DOMoveY(1200f, 0.5f).SetEase(Ease.InBounce);
            txtTown2.gameObject.transform.parent.DOMoveY(1200f, 0.5f).SetEase(Ease.InBounce);
            GameManager.Instance.CheckChooseCorrect();
            Invoke("SetActiveFalse", 0.5f);
        });

        startBtn.onClick.AddListener(() =>
        {
            if (isFirstClick)
            {
                txtIntroText.gameObject.SetActive(false);
                txtRouteText.gameObject.SetActive(true);
                isFirstClick = false;
            }
            else
            {
                txtRouteText.gameObject.transform.parent.gameObject.SetActive(false);
                ButtonZone.SetActive(true);
                TownZone.SetActive(true);
            }
        });
    }

    void SetActiveFalse()
    {
        leftBtn.gameObject.SetActive(false);
        rightBtn.gameObject.SetActive(false);
        txtTown1.transform.parent.gameObject.SetActive(false);
        txtTown2.transform.parent.gameObject.SetActive(false);
    }

    public void SetUIActiveTrue()
    {
        leftBtn.gameObject.SetActive(true);
        rightBtn.gameObject.SetActive(true);
        txtTown1.transform.parent.gameObject.SetActive(true);
        txtTown2.transform.parent.gameObject.SetActive(true);
    }

    public void DisplayTown(string town1, string town2)
    {
        int r = Random.Range(0, 2);
        if(r < 1)
        {
            txtTown1.text = town1;
            txtTown2.text = town2;
            this.town1 = town1;
            this.town2 = town2;
        }
        else
        {
            txtTown1.text = town2;
            txtTown2.text = town1;
            this.town1 = town2;
            this.town2 = town1;
        }
    }

    public void SetRouteText(List<string> routes)
    {
        foreach(var route in routes)
        {
            if(routes.IndexOf(route) == routes.Count - 1)
            {
                txtRouteText.text += route + ".";
            }
            else
            {
                txtRouteText.text += route + " -> ";
            }
        }
    }

    public void DisplayEndText(bool isWin)
    {
        if (isDisplayEndGame) return;

        txtEndText.gameObject.SetActive(true);
        if (isWin) txtEndText.text = "You Won";
        else txtEndText.text = "You was wrong";
        txtEndText.gameObject.transform.DOScale(0, 0f);
        txtEndText.gameObject.transform.DOScale(1, 1f).SetEase(Ease.OutBack);
        isDisplayEndGame = true;
    }
}
