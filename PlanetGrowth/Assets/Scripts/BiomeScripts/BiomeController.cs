using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class BiomeController : MonoBehaviour
{
    [SerializeField] private Biome[] m_biomes;

    private void Start()
    {

    }
    private void Update()
    {

    }

    public GameObject assignBiome(int _temperature, int _humidity, Transform _transform)
    {
        int index = matchIndex(_temperature, _humidity);
        if (index != -1)
        {
            return createBiome(_transform, index);
        }
        return null;
    }

    public GameObject assignBiome(int _temperature, int _humidity, GameObject _oldBiome)
    {
        int index = matchIndex(_temperature, _humidity);
        if (index != -1)
        {
            GameObject newBiome = createBiome(_oldBiome.transform.parent, index);
            _oldBiome.transform.GetComponent<Biome>().deleteBiome();
            return newBiome;
        }
        return null;
    }

    private int matchIndex(int _temperature, int _humidity)
    {
        List<int> matchingBiomes = new List<int>();
        for (int i = 0; i < m_biomes.Length; i++)
        {
            if (m_biomes[i].MatchBiome(_temperature, _humidity))
            {
                matchingBiomes.Add(i);
            }
        }
        if (matchingBiomes.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, matchingBiomes.Count);
            return index;
        }
        return -1;
    }

    private GameObject createBiome(Transform _parent, int _newBiome)
    {
        GameObject newBiomeModel = Instantiate(
            m_biomes[_newBiome].BiomeModel,
            _parent,
            true);

        return newBiomeModel;
    }

}

