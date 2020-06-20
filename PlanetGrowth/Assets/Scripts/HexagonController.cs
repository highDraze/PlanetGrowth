using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonController : MonoBehaviour
{

    [SerializeField] private List<GameObject> m_hexagon = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHexagon(GameObject _hex)
    {
        m_hexagon.Add(_hex);
    }


}
