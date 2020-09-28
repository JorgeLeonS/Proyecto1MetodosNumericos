using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Math;
public class CuadradosMedios : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField semillaInput;
    public InputField nInput;
    public GameObject Row;
    public GameObject Semilla;
    public GameObject Generador;
    public GameObject Numero_aleatorio;
    public GameObject Ri;
    public GameObject Content;
    public void generarNumeros(){
        //Recibir los input
        int semilla = int.Parse(semillaInput.text);
        int n = int.Parse(nInput.text);

        //Se eleva al cuadrado
        float n_generado = Mathf.Pow(semilla,2);
        //Se da formato de 8 numeros
        string n_generado_string = n_generado.ToString("00000000");
        string semilla_string =semilla.ToString();
        //Se repite las veces que quiera el usuario
        for(int i = 0;i<n;i++){
            //Se guarda el valor del generador para crear la fila
            string generador = n_generado_string;
            //Se parte a la mitad
            n_generado_string = n_generado_string.Substring(2,4); 
            //Se convierte a numero
            n_generado = float.Parse(n_generado_string);

            //Se crea una nueva fila en la interfaz
            createNewRow(semilla_string, generador,n_generado_string,n_generado/10000);
            
            //Se guarda el valor de la nueva semilla
            semilla_string=n_generado_string;

            //Se eleva para el nuevo numero generador
            n_generado = Mathf.Pow(n_generado,2);
            //Se regresa a string y sobreescribe
            n_generado_string = n_generado.ToString("00000000");
            
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
        Ri.GetComponent<Text>().text= ri.ToString("0.0000");
    }

}
