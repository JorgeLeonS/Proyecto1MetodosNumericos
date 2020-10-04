using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongruencialLinealMixto : MonoBehaviour
{
    public InputField x0Input;
    public InputField aInput;
    public InputField cInput;
    public InputField mInput;
    public InputField nInput;

    public GameObject Row;
    private GameObject Semilla;
    private GameObject Generador;
    private GameObject Numero_aleatorio;
    private GameObject Ri;
    public GameObject Content;
    public GameObject HullDobelltext;
    public GameObject TablePrefab;
    public GameObject CanvasReference;

    public void generarNumeros(){
        resetTable();
        int x0 = int.Parse(x0Input.text);
        int a = int.Parse(aInput.text);
        int c = int.Parse(cInput.text);
        int m = int.Parse(mInput.text);
        int n = m;

        if(!HullDobell(a,c,m)){
            HullDobelltext.GetComponent<Text>().text = "No jalo";
        }else{
            HullDobelltext.GetComponent<Text>().text = "Ta bien";
            string x0String = x0Input.text;

            // print(x0 + a + c + m);

            //Se repite las veces que quiera el usuario
            for(int i = 0;i<n;i++){
                int generador = ((x0 * a) + c);

                // print("Generador "+generador);

                int aleatorio = generador % m;

                float aleatorio_float  = (float)aleatorio;
                float  m_float = (float)m;
                float ri = aleatorio_float / m_float;

                createNewRow(x0.ToString(), generador.ToString(), aleatorio.ToString(), ri);

                // print("aleatorio "+aleatorio);
                // print("ri"+ri.ToString("0.000"));

                x0 = aleatorio;
            }
        }

    }

    public bool HullDobell(int a, int c, int m){
        // Sea c y m primos relativo
        if(Mcd(c,m) != 1) return false;

        List<int> primeNumbers = new List<int>();

        primeNumbers = PrimeFactors(m);

        for (int i = 0; i < primeNumbers.Count; i++) {
            if ((a - 1) % primeNumbers[i] != 0) return false;
        }

        if (m % 4 == 0) {
            if ((a - 1) % 4 != 0) return false;
        }

        return true;

    }

    public int Mcd(int a, int b){
        
    if (a == 0 || b == 0) return 0;

    while (a != b) {
        if (a > b) {
            a = a - b;
        } else {
            b = b - a;
        }
    }
        return a;
    }

    public List<int> PrimeFactors(int n) {
        List<int> primeNumbers = new List<int>();

        while (n % 2 == 0) {
            primeNumbers.Add(2);
            n = n / 2;
        }

        for (int i = 3; i <= Mathf.Sqrt(n); i = i + 2) {
            while (n % i == 0) {
                primeNumbers.Add(i);
                n = n / i;
            }
        }

        if (n > 2) primeNumbers.Add(n);

        return primeNumbers;
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
        HullDobelltext.GetComponent<Text>().text = "";
        
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
