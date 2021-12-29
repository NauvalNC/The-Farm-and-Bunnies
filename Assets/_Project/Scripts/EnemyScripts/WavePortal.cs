using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavePortal : MonoBehaviour
{
    [Header("Wave Settings")]
    public float spawnCooldown = 0.5f;
    public List<EnemyList> enemyWaves;

    public Text enemyCounterTxt;

    EnemyList m_waveToSpawn;
    Transform m_spawnerPos;
    
    float m_timer, m_spawnCD;


    int m_spawnIndex = 0;
    bool m_waveStarted = false;
    [SerializeField] bool m_waveEnded = true;

    List<Enemy> m_spawnedEnemies = new List<Enemy>();
    int m_numberOfOutDuties = 0;

    private void Awake()
    {
        m_spawnerPos = transform;    
    }

    private void Update()
    {
        if (m_waveStarted && m_waveEnded == false)
        {
            ManageTime();
            Spawn();
            TrackSpawnedEnemies();
        }
        if(m_waveToSpawn != null)
        UpdateGraphics();
    }

    void UpdateGraphics()
    {
        enemyCounterTxt.text = m_numberOfOutDuties + "/" + m_waveToSpawn.enemiesToSpawn.Count;
    }

    public void StartPortal(int waveIndex)
    {
        // Because of the wave array is 0 index based, then we need to reduce it by one
        int m_actualWave = waveIndex - 1;
        if (m_actualWave > enemyWaves.Count - 1)
        {
            Debug.Log("Warning: No such enemy wave in this portal.");
            return;
        }
        // Can't start new wave if the last one is not finished yet
        else if (m_waveEnded == false) return;

        m_spawnedEnemies.Clear();
        
        m_waveToSpawn = enemyWaves[m_actualWave];
        m_numberOfOutDuties = 0;

        m_waveStarted = true;
        m_waveEnded = false;
        m_spawnIndex = 0;
        m_timer = 0;
    }

    void ManageTime()
    {
        m_timer += Time.deltaTime;

        if (m_spawnCD > 0f) m_spawnCD -= Time.deltaTime;
        else m_spawnCD = 0f;
    }

    void Spawn()
    {
        if (m_spawnIndex > m_waveToSpawn.enemiesToSpawn.Count - 1) return;

        EnemySpawnData m_temp = m_waveToSpawn.enemiesToSpawn[m_spawnIndex];

        if (m_timer >= m_temp.timeToSpawn && m_spawnCD <= 0f)
        {
            Enemy m_obj = Instantiate(m_temp.prefabToSpawn, m_spawnerPos.position, Quaternion.identity).GetComponent<Enemy>();
            m_obj.SetDestination(GameManager.Instance.crates[m_temp.crateTargetIndex].transform);
            m_obj.portal = m_spawnerPos;

            m_spawnedEnemies.Add(m_obj);
            m_spawnCD = spawnCooldown + m_temp.timeforNextSpawn;
            m_spawnIndex++;
        }
    }

    void TrackSpawnedEnemies()
    {
        if (m_numberOfOutDuties >= m_waveToSpawn.enemiesToSpawn.Count)
        {
            m_waveEnded = true;
        }

        for (int i = 0; i < m_spawnedEnemies.Count; i++)
        {
            // If enemy is destroyed or not active anymore
            if (m_spawnedEnemies[i] == null)
            {
                m_spawnedEnemies.Remove(m_spawnedEnemies[i]);
                m_numberOfOutDuties++;
            }
        }
    }

    public bool IsWaveEnded() { return m_waveEnded; }

    /// <summary>
    /// Return number of enemies who is ecaped or dead in the active wave.
    /// </summary>
    /// <returns></returns>
    public int GetNumberOutOfDutyEnemies() { return m_numberOfOutDuties; }
}
