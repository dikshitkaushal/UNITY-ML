using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PopulationManager : MonoBehaviour
{
    public GameObject personprefab;
    public int populationsize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialtime = 10;
    int generation = 1;

    GUIStyle guistyle = new GUIStyle();
    private void OnGUI()
    {
        guistyle.fontSize = 50;
        guistyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation : " + generation, guistyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time : " + (int)elapsed, guistyle);
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<populationsize;i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-3f, 5f), 0);
            GameObject go = Instantiate(personprefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().r = UnityEngine.Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = UnityEngine.Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = UnityEngine.Random.Range(0.0f, 1.0f);
            population.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed>trialtime)
        {
            breednewpopulation();
            elapsed = 0;
        }
    }

    private void breednewpopulation()
    {
        List<GameObject> sortedlist = population.OrderBy(p => p.GetComponent<DNA>().timetodie).ToList();
        population.Clear();

        //breed half sorted list
        for(int i= (int)(sortedlist.Count/2.0f)-1;i<sortedlist.Count-1;i++)
        {
            population.Add(Breed(sortedlist[i], sortedlist[i + 1]));
            population.Add(Breed(sortedlist[i + 1], sortedlist[i]));
        }

        //Destroy all parents and previous population
        for(int i=0;i<sortedlist.Count;i++)
        {
            Destroy(sortedlist[i]);
        }
        generation++;
    }
    GameObject Breed(GameObject parent1,GameObject parent2)
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-3f, 5f), 0);
        GameObject offspring = Instantiate(personprefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        //swap parent dna 
        //MAIN GENETICS ALGORITHM

        offspring.GetComponent<DNA>().r = UnityEngine.Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = UnityEngine.Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = UnityEngine.Random.Range(0, 10) < 5 ? dna1.b : dna2.b;


        return offspring;
    }
}
