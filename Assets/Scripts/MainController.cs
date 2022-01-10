using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject ranaPrefab;
    public GameObject steppingStonePrefab;

    public float separacion;
    private float anchoPiedra;

    private GameObject[] steps;

    private GameObject rana;

    private int numeroPiedras = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(ranaPrefab == null) {
            Debug.Log("No está establecido el prefab para la rana");
        }

        if(steppingStonePrefab == null) {
            Debug.Log("No está establecido el prefab para las piedras");
        } else {
            anchoPiedra = steppingStonePrefab.transform.localScale.x;
        }

        if(separacion == 0) {
            separacion = 1.0f;
        }

        steps = new GameObject[numeroPiedras];

        ColocarPiedras();
        ColocarRana();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ColocarRana() {
        rana = Instantiate(ranaPrefab, new Vector3((- numeroPiedras/2) * (separacion + anchoPiedra), 0.2f, 0.0f), Quaternion.identity);
    }

    private void ColocarPiedras() {
        for(int i =0; i<numeroPiedras; i++) {
            float x = (numeroPiedras/2 - i) * (separacion + anchoPiedra);
            //steps[numeroPiedras - i] = 
            Instantiate(steppingStonePrefab, new Vector3(x, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    public GameObject GetStep(int i) {
        return steps[i];
    }
}
