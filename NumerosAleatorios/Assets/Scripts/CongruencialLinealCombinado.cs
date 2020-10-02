using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongruencialLinealCombinado : MonoBehaviour
{

    public InputField xInput;
    public InputField yInput;

    public InputField a1Input;
    public InputField a2Input;
    // public InputField cInput;
    public InputField m1Input;
    public InputField m2Input;
    public InputField m3Input;

    public GameObject Row;
    public GameObject Semilla;
    public GameObject Generador;
    public GameObject Numero_aleatorio;
    public GameObject Ri;
    public GameObject Content;

    public void generarNumeros(){
        int x0 = int.Parse(xInput.text);
        int y0 = int.Parse(yInput.text);
        int a1 = int.Parse(a1Input.text);
        int a2 = int.Parse(a2Input.text);
        int m1 = int.Parse(m1Input.text);
        int m2 = int.Parse(m2Input.text);
        int m3 = int.Parse(m3Input.text);

        int xn = x0;
        int yn = y0;

        int contador = 0;
        int w = 0;

        do{
            if(xn-yn < 0){
                w = m3 - Mathf.Abs(xn-yn);
            }else{
                w = (xn - yn) % m3;
            }

            createNewRow(contador, xn, yn, w);
            contador++;

            xn = (a1*xn) % m1;
            print("xn"+xn);
            yn = (a2*yn) % m2;
            print("yn"+yn);
            

        }while(xn != x0 || yn != y0);

    }

    public void createNewRow(int c, int x, int y, int w){
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
        Semilla.GetComponent<Text>().text =c.ToString();
        Generador.GetComponent<Text>().text = x.ToString();
        Numero_aleatorio.GetComponent<Text>().text = y.ToString();
        Ri.GetComponent<Text>().text= w.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
