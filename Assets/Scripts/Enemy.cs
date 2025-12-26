using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private string _currentName = "Enemy";
    [SerializeField] private int _currentHealth = 10;
    [SerializeField] private int _currentLevel = 1;

    [Header("Settings Random")]
    [SerializeField] private int minHealth = 0;
    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int minLevel = 1;
    [SerializeField] private int maxLevel = 1;

    private string _defaultName;
    private int _defaultHealth;
    private int _defaultLevel;

    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        RandomizeName();

        _currentHealth = Random.Range(minHealth, maxHealth);
        _currentLevel = Random.Range(minLevel, maxLevel);

        _defaultName = _currentName;
        _defaultHealth = _currentHealth;
        _defaultLevel = _currentLevel;
    }

    private void RandomizeName()
    {
        _currentName = EnemyManager.Instance.Names[Random.Range(0, EnemyManager.Instance.Names.Length)];
    }

    public int GetHealth() => _currentHealth;
    public int GetLevel() => _currentLevel;
    public string GetName() => _currentName;

    public void ModifyHealth(ModType operation, int value)
    {
        _currentHealth = ApplyOperation(_currentHealth, operation, value);
    }

    public void ModifyLevel(ModType operation, int value)
    {
        _currentLevel = ApplyOperation(_currentLevel, operation, value);
    }

    private int ApplyOperation(int value, ModType type, int newValue)
    {
        return type switch
        {
            ModType.Add => value + newValue,
            ModType.Subtract => value - newValue,
            ModType.Multiply => value * newValue,
            ModType.Divide => newValue != 0 ? value / newValue : value,
            _ => value
        };
    }

    public void Reset()
    {
        _currentName = _defaultName;
        _currentHealth = _defaultHealth;
        _currentLevel = _defaultLevel;
    }

    public void SetName(string newName)
    {
        _currentName = newName ?? string.Empty;
    }
}

public enum ModType
{
    Add,
    Subtract,
    Multiply,
    Divide
}
