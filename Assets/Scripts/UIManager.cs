using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Level progress UI")]
    [SerializeField] private int _sceneOffset;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private Image _progressFillImage;

    [Space]
    [SerializeField] private TMP_Text _levelCompletedText;

    [Space]
    [SerializeField] private Image _fadePanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }    
    private void Start()
    {
        FadeAtStart();

        _progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
    }

    private void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + _sceneOffset;
        _currentLevelText.text = level.ToString();
        _nextLevelText.text = (level + 1).ToString();
    }    
    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.ObjectsInScene / Level.Instance.TotalObjects);       
        _progressFillImage.DOFillAmount(val, 0.4f);
    }

    public void ShowLevelComletedUI()
    {
        _levelCompletedText.DOFade(1f, 0.6f).From(0f);
    }

    public void FadeAtStart()
    {
        _fadePanel.DOFade(0f, 2f).From(1f);
    }
}
