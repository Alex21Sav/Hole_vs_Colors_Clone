using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;

    [SerializeField] private ParticleSystem _winFX;
    [Space]
    [SerializeField] private Transform _objectsParent;
    [Space]
    [Header("Level Objects & Obstaccles")]
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _objectsMaterial;
    [SerializeField] private Material _obstacclesMaterial;

    [SerializeField] private SpriteRenderer _grounBorderSprite;
    [SerializeField] private SpriteRenderer _grounSideSprite;

    [SerializeField] private Image _imageProgressFill;

    [SerializeField] private SpriteRenderer _bgFadeSprite;

    [Space]
    [Header("Level Color")]
    [Header("Ground")]
    [SerializeField] private Color _graundColor;
    [SerializeField] private Color _borderColor;
    [SerializeField] private Color _sideColor;

    [Header("bjects & Obstaccles")]
    [SerializeField] private Color _objectsColor;
    [SerializeField] private Color _obstacclesColor;

    [Header("UI progress")]
    [SerializeField] private Color _progressFillColor;

    [Header("Background")]
    [SerializeField] private Color _cameraColor;
    [SerializeField] private Color _fadeColor;


    [HideInInspector] public int ObjectsInScene;
    [HideInInspector] public int TotalObjects;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateLevelColor();
        CountObjects();
    }

    private void CountObjects()
    {
        TotalObjects = _objectsParent.childCount;
        ObjectsInScene = TotalObjects;
    }

    public void PlayWinFX()
    {
        _winFX.Play();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLevelColor()
    {
        _groundMaterial.color = _graundColor;
        _grounSideSprite.color = _sideColor;
        _grounBorderSprite.color = _borderColor;

        _obstacclesMaterial.color = _obstacclesColor;
        _objectsMaterial.color = _objectsColor;

        _imageProgressFill.color = _progressFillColor;
        Camera.main.backgroundColor = _cameraColor;
        _bgFadeSprite.color = _fadeColor;
    }

    private void OnValidate()
    {
        UpdateLevelColor();
    }

}
