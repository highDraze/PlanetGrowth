using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestScript : MonoBehaviour {

    [SerializeField] private GameObject biomeModel;
    private string modelName = "Rota Parent_waste";
    // Start is called before the first frame update
    void Start() {
        updateBiomePrefab();
        Debug.Log("LogMessage");

    }

    // Update is called once per frame
    void Update() {

    }

    void updateBiomePrefab() {
        Debug.Log("LogMessage");
        GameObject modellCopy = Instantiate(biomeModel);

        foreach (var item in transform) {
            Debug.Log(item);
        }

        Destroy(transform.Find(modelName).gameObject);
        modellCopy.transform.parent = transform;

        /*Destroy()

        GameObject rotaParent =

            Planet planet = GameObject.Find("Planet").GetComponent<Planet>();
        Biome newBiome = planet.DetermineMatchingBiome(temperature, humidity);

        if (newBiome != null) {

            score = newBiome.LiveAbilityScore;
            Transform obj = transform.Find("Rota Parent");
            UnityEngine.Quaternion cRotation = obj.rotation;

            Destroy(obj.gameObject);

            GameObject newBiom = Instantiate(newBiome.getModell(), transform.position, cRotation, transform);
            newBiom.name = "Rota Parent";        
        }*/
    }


}
