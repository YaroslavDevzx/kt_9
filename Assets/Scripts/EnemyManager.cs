using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private string[] enemyNames;

    [Header("UI")]
    [SerializeField] private Button bossButton;
    [SerializeField] private Button healthButton;
    [SerializeField] private Button levelButton;
    [SerializeField] private Button resetButton;
    [Space(5f)]
    [SerializeField] private TMP_InputField inputField;

    private List<Enemy> spawnedEnemies = new List<Enemy>();


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        Init();
    }


    void Init()
    {
        bossButton.onClick.AddListener(MakeBossEnemies);
        healthButton.onClick.AddListener(ShowEnemiesWithHealth);
        levelButton.onClick.AddListener(ShowEnemiesWithLevel);
        resetButton.onClick.AddListener(ResetEnemies);

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            spawnedEnemies.Add(enemy);
        }
    }


    void ShowEnemiesWithHealth()
    {
        float healhValue = inputField.text != "" ? float.Parse(inputField.text) : 0f;

        foreach (var enemy in spawnedEnemies)
        {
            enemy.gameObject.SetActive(enemy.GetHealth() == healhValue);
        }
    }

    void ShowEnemiesWithLevel()
    {
        float levelValue = inputField.text != "" ? float.Parse(inputField.text) : 0f;

        foreach (var enemy in spawnedEnemies)
        {
            enemy.gameObject.SetActive(enemy.GetLevel() == levelValue);
        }
    }

    void MakeBossEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy.GetName().ToLower() == inputField.text.ToLower())
            {
                enemy.ModifyHealth(ModType.Multiply, 3);
                enemy.ModifyLevel(ModType.Add, 1);
                Debug.Log($"Враг {enemy.GetName()} теперь босс, его хп: {enemy.GetHealth()}, лвл: {enemy.GetLevel()}");
                enemy.SetName("Boss");
            }
        }
    }

    void ResetEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            enemy.Reset();
            enemy.gameObject.SetActive(true);
        }
    }

    
    public string[] Names => enemyNames;



}
