using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject cubePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private Vector3 spawnBounds = new Vector3(10f, 0f, 10f);

    [Header("Size Settings")]
    [Range(0.5f, 1.5f)][SerializeField] private float minScale = 0.5f;
    [Range(0.5f, 2.5f)][SerializeField] private float maxScale = 2.5f;

    [Header("Sort Settings")]
    [SerializeField] private float spacing = 3f;

    private GameObject[] _spawnedCubes;

    private void Awake()
    {
        CreateCubes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ArrangeCubesBySize();
        }
    }

    private void CreateCubes()
    {
        _spawnedCubes = new GameObject[spawnCount];

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-spawnBounds.x, spawnBounds.x), spawnBounds.y, Random.Range(-spawnBounds.z, spawnBounds.z));
            float scale = Random.Range(minScale, maxScale);
            GameObject newCube = Instantiate(cubePrefab, pos, Quaternion.identity);
            newCube.transform.localScale = Vector3.one * scale;
            _spawnedCubes[i] = newCube;
            Debug.Log($"Кубик №{i} заспавнился. Размер: {scale:F2}");
        }
    }

    private void ArrangeCubesBySize()
    {
        for (int i = 0; i < _spawnedCubes.Length - 1; i++)
        {
            for (int j = 0; j < _spawnedCubes.Length - i - 1; j++)
            {
                float currentSize = _spawnedCubes[j].transform.localScale.x;
                float nextSize = _spawnedCubes[j + 1].transform.localScale.x;

                if (currentSize < nextSize)
                {
                    (_spawnedCubes[j], _spawnedCubes[j + 1]) = (_spawnedCubes[j + 1], _spawnedCubes[j]);
                }
            }
        }

        Vector3 basePosition = new Vector3(-10f, spawnBounds.y, 0f);
        for (int i = 0; i < _spawnedCubes.Length; i++)
        {
            Vector3 targetPos = basePosition + Vector3.right * (i * spacing);
            _spawnedCubes[i].transform.position = targetPos;
            Debug.Log($"Кубик #{i} перемещён на {targetPos}");
        }
    }
}
