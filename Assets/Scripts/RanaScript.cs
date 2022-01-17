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
    private float alturaSalto = 1.0f;

    private bool saltando = false;
    private bool bajandoSalto = false;

    private Vector3 coordenadasIniciales;
    private Vector3 coordenadasPicoSalto;
    private Vector3 coordenadasFinalSalto;

    private float t;
    private float vSalto = 3.0f;
    private float tSalto;

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
        if(saltando) {
            t += Time.deltaTime/tSalto;
            transform.position = Vector3.Lerp(coordenadasIniciales, coordenadasPicoSalto, t);
            if(t >= 1.0f) {
                t = 0;
                saltando = false;
                bajandoSalto = true;
                coordenadasIniciales = transform.position;

            }
            return;
        } else if(bajandoSalto) {
            t += Time.deltaTime/tSalto;
            transform.position = Vector3.Lerp(coordenadasIniciales, coordenadasFinalSalto, t);
            if(t >= 1.0f) {
                bajandoSalto = false;
            }
            return;
        }

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
        coordenadasIniciales = transform.position;

        coordenadasPicoSalto = new Vector3(coordenadasIniciales.x + desplazamiento * (mainController.separacion + mainController.anchoPiedra) / 2,
                                           coordenadasIniciales.y + alturaSalto, coordenadasIniciales.z);

        coordenadasFinalSalto = new Vector3(coordenadasIniciales.x + desplazamiento * (mainController.separacion + mainController.anchoPiedra),
                                           coordenadasIniciales.y, coordenadasIniciales.z);

        saltando = true;
        t=0;
        tSalto = (coordenadasPicoSalto-coordenadasIniciales).magnitude / vSalto;

        //transform.position = new Vector3(coordenadasActuales.x + desplazamiento * (mainController.separacion + mainController.anchoPiedra) , coordenadasActuales.y, coordenadasActuales.y);
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

