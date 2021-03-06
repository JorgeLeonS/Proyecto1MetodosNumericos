﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongruencialLineal : MonoBehaviour
{
    public InputField x0Input;
    public InputField aInput;
    public InputField cInput;
    public InputField mInput;
    public InputField nInput;

    public GameObject Row;
    public GameObject Semilla;
    public GameObject Generador;
    public GameObject Numero_aleatorio;
    public GameObject Ri;
    public GameObject Content;
    public GameObject TablePrefab;
    public GameObject CanvasReference;
    public void generarNumeros(){
        resetTable();
        long x0 = int.Parse(x0Input.text);
        long a = int.Parse(aInput.text);
        long c = int.Parse(cInput.text);
        long m = int.Parse(mInput.text);
        long n = int.Parse(nInput.text);

        string x0String = x0Input.text;

        // print(x0 + a + c + m);

        //Se repite las veces que quiera el usuario
        for(int i = 0;i<n;i++){
            long generador = ((x0 * a) + c);

            print("Generador "+generador);

            long aleatorio = generador % m;

            float aleatorio_float  = (float)aleatorio;
            float  m_float = (float)m;
            float ri = aleatorio_float / m_float;

            createNewRow(x0.ToString(), generador.ToString(), aleatorio.ToString(), ri);

            print("aleatorio "+aleatorio);
            print("ri"+ri.ToString("0.000"));

            x0 = aleatorio;
        }



    }

    public void createNewRow(string semilla, string generador, string nAleatorio, float ri){
        //Instanciar nueva fila
        GameObject new_row = Instantiate(Row,new Vector3(0,0,0) , Quaternion.identity) as GameObject;
        //Unirla a la tabla
        new_row.transform.SetParent (Content.transform, false);
        
        //Unir los objetos de texto con el codigo
        Semilla = new_row.transform.Find("Semilla").gameObject;
        Generador = new_row.transform.Find("Generador").gameObject;
        Numero_aleatorio = new_row.transform.Find("Numero aleatorio").gameObject;
        Ri = new_row.transform.Find("Ri").gameObject;

        //Poner los valores correspondientes
        Semilla.GetComponent<Text>().text =semilla;
        Generador.GetComponent<Text>().text = generador;
        Numero_aleatorio.GetComponent<Text>().text = nAleatorio;
        Ri.GetComponent<Text>().text= ri.ToString("0.000");
    }

    public void resetValues(){

        x0Input.text = "";
        aInput.text = "";
        cInput.text = "";
        mInput.text = "";
        nInput.text = "";
        
        resetTable();
    }

    public void resetTable(){
        if (GameObject.Find("Table")){
            Destroy(GameObject.Find("Table"));
        }else{
            Destroy(GameObject.Find("Table(Clone)"));
        }   
        GameObject new_Table = Instantiate(TablePrefab,new Vector3(-504.9432f,150.2171f,-266.1887f) , Quaternion.identity) as GameObject;
        new_Table.transform.SetParent (CanvasReference.transform, false);
        Content = new_Table.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

}
