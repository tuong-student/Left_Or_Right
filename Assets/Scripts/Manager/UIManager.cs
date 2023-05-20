using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using NOOD;

public class UIManager : MonoBehaviorInstance<UIManager>
{
    [SerializeField] GameObject TownZone, ButtonZone, IntroZone, EndZone;
    [SerializeField] CustomButton btnLeft, btnRight;
    [SerializeField] CustomPanel txtTown1, txtTown2;
    [SerializeField] Text txtRouteText, txtEndText, txtIntroText;
    [SerializeField] Button startBtn, restartBtn;
    string town1, town2;

    bool isDisplayEnd = false;
    bool isFirstTime = true;

    private void Start()
    {
        IntroZone.SetActive(true);
        TownZone.SetActive(false);
        ButtonZone.SetActive(false);
        EndZone.SetActive(false);
        txtEndText.gameObject.SetActive(false);

        startBtn.onClick.AddListener(() =>
        {
            if (isFirstTime)
            {
                txtIntroText.gameObject.SetActive(false);
                DisplayRoutes();
                isFirstTime = false;
            }
            else
            {
                ActiveUI();
                IntroZone.SetActive(false);
                GameManager.Instance.OnGameStart?.Invoke();
            }
            SoundManager.Instance.PlayAudio(SoundType.Click);
        });

        restartBtn.onClick.AddListener(() =>
        {
            PlayerScripts.Instance.Return();
            MapManager.Instance.CreateNPC(NPCType.Sender);
        });

        btnLeft.SetAction(() =>
        {
            PlayerScripts.Instance.Go(true);
            btnLeft.HideAnimation();
            btnRight.HideAnimation();
            txtTown1.HideAnimation();
            txtTown2.HideAnimation();
            GameManager.Instance.SetChoseTown(town1);
            SoundManager.Instance.PlayAudio(SoundType.Click);

            NoodyCustomCode.StartDelayFunction(DeActiveUI, 0.5f);
        });

        btnRight.SetAction(() =>
        {
            PlayerScripts.Instance.Go(false);
            btnLeft.HideAnimation();
            btnRight.HideAnimation();
            txtTown1.HideAnimation();
            txtTown2.HideAnimation();
            GameManager.Instance.SetChoseTown(town2);
            SoundManager.Instance.PlayAudio(SoundType.Click);

            NoodyCustomCode.StartDelayFunction(DeActiveUI, 0.5f);
        });
    }

    public void SetTown(string town1, string town2)
    {
        this.town1 = town1;
        this.town2 = town2;
        txtTown1.SetText(town1);
        txtTown2.SetText(town2);
    }

    public void DisplayRoutes()
    {
        List<string> routes = GameManager.Instance.routes;
        string displayRoutes = "";
        for (int i = 0; i < routes.Count; i++)
        {
            if (i != routes.Count - 1)
                displayRoutes += routes[i] + " -> ";
            else
                displayRoutes += routes[i] + ".";
        }
        txtRouteText.text = displayRoutes;
    }

    public void ActiveUI()
    {
        TownZone.SetActive(true);
        ButtonZone.SetActive(true);
    }

    public void DeActiveUI()
    {
        TownZone.SetActive(false);
        ButtonZone.SetActive(false);
    }

    public void SetEndText(bool isWin)
    {
        if (isDisplayEnd) return;
        EndZone.SetActive(true);
        if (isWin)
        {
            txtEndText.transform.DOScale(0, 0f);
            txtEndText.gameObject.SetActive(true);
            txtEndText.text = "Welcome home";
            txtEndText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutExpo);
            SoundManager.Instance.PlayAudio(SoundType.Success);
        }
        else
        {
            txtEndText.transform.DOScale(0, 0f);
            txtEndText.gameObject.SetActive(true);
            txtEndText.text = "You choose wrong";
            txtEndText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutExpo);
            SoundManager.Instance.PlayAudio(SoundType.Fail);
        }
        isDisplayEnd = true;
    }
}
