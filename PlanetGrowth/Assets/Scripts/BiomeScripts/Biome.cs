using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Biome : MonoBehaviour
{
    public string m_modelName { get; set; }


    [SerializeField] protected int m_liveAbilityScore;
    [SerializeField] protected int[] m_temperatureRange = new int[2];
    [SerializeField] protected int[] m_humidRange = new int[2];
    //[SerializeField] protected BiomeType m_BiomeType; 


    public int LiveAbilityScore => m_liveAbilityScore;

    [SerializeField]
    protected GameObject m_BiomeModel;
    public GameObject BiomeModel
    {
        get { return m_BiomeModel; }
        set { m_BiomeModel = value; }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void deleteBiome()
    {
        Destroy(gameObject);
    }
    void Update()
    {

    }

    public bool MatchBiome(int _temperature, int _humidity)
    {
        bool matchFound = false;
        if (_temperature >= m_temperatureRange[0] && _temperature <= m_temperatureRange[1])
        {
            if (_humidity >= m_humidRange[0] && _humidity <= m_humidRange[1])
            {
                matchFound = true;
            }
        }
        return matchFound;
    }

}
