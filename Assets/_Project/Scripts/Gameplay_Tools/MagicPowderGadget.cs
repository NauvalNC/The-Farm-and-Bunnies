
using UnityEngine;
using UnityEngine.UI;
public class MagicPowderGadget : MonoBehaviour
{

    [Header("General")]
    public float powderAOERadius = 5f;
    public float maxEnergy;
    public float magicCost;
    public float energyRecoveryPerSecond;
    public int magicDMG;
    float m_energy;

    [Header("Renderers")]
    [SerializeField] protected ParticleSystem powderVFX;
    [SerializeField] protected Image energyBar;

    private void Awake()
    {
        SetupAttributtes();
    }

    private void Update()
    {
        UsePowder();
        GenerateEnergy();
        UpdateGraphics();
    }

    void SetupAttributtes()
    {
        m_energy = maxEnergy;
    }

    void GenerateEnergy()
    {
        if (m_energy >= maxEnergy) m_energy = maxEnergy;
        m_energy += energyRecoveryPerSecond * Time.deltaTime;
    }

    void UsePowder()
    {
        if (!GameManager.Instance.IsPriorityAvailable(gameObject) || GameManager.Instance.IsPointerOverUI()) return;

        if (Input.GetMouseButtonDown(0) && m_energy >= magicCost)
        {
            m_energy -= magicCost;
            if (m_energy <= 0) m_energy = 0;

            RaycastHit m_hit;
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, 1 << 6))
            {
                Vector3 m_splashPos = m_hit.point;
                powderVFX.gameObject.transform.position = m_splashPos;
                powderVFX.Play();

                // AOE effects to enemies
                Collider[] m_colls = Physics.OverlapSphere(m_hit.point, powderAOERadius, 1 << 7);
                Debug.Log("Hit count: " + m_colls.Length.ToString());
                foreach(Collider c in m_colls)
                {
                    //apply damage
                    int randDMG = Random.Range(magicDMG - 3, magicDMG + 4);
                    c.GetComponent<Enemy>().OnDamage(randDMG);
                }
            }
        }
    }

    void UpdateGraphics()
    {
        float m_targetScale = m_energy / maxEnergy;
        float m_formerScale = energyBar.transform.localScale.y;
        float m_lerpScale = Mathf.Lerp(m_formerScale, m_targetScale, 5f * Time.deltaTime);
        energyBar.transform.localScale = new Vector3(1f, m_lerpScale, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(powderVFX.gameObject.transform.position, powderAOERadius);
    }
}
