using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanaScript : MonoBehaviour
{
    public MainController mainController;
    private Vector3 posicionInicial;
    private int posicion;
    private int contadorSaltos;
    private int contadorPruebas;
    private int acumuladorSaltos;


    // Start is called before the first frame update
    void Start()
    {
        posicion = 0;
        contadorSaltos = 0;
        contadorPruebas = 0;
        acumuladorSaltos = 0;

        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(posicion < 10) {
                int desplazamiento = Random.Range(1, mainController.numeroPiedras-posicion+1);
                posicion = posicion + desplazamiento;

                Debug.Log("Nueva posicion " + posicion);

                contadorSaltos++;
                Mover(desplazamiento);
            } else {
                contadorPruebas++;
                acumuladorSaltos += contadorSaltos;

                contadorSaltos = 0;
                posicion = 0;

                MoverPosicionInicial();
            }

            
            
        }
        
    }

    private void Mover(int desplazamiento) {
        Vector3 coordenadasActuales = transform.position;
        transform.position = new Vector3(coordenadasActuales.x + desplazamiento * (mainController.separacion + mainController.anchoPiedra) , coordenadasActuales.y, coordenadasActuales.y);
    }

    private void MoverPosicionInicial(){
        transform.position = posicionInicial;
    }

    void OnGUI () {
        GUI.Label (new Rect (500,10,200,50), "Prueba número: " + contadorPruebas);
        GUI.Label (new Rect (500,30,200,50), "Saltos realizados: " + contadorSaltos);

        if(contadorPruebas > 0) {
            GUI.Label (new Rect (500,70,200,50), "Media pruebas: " + (float)acumuladorSaltos / contadorPruebas);
        }
    }
}
