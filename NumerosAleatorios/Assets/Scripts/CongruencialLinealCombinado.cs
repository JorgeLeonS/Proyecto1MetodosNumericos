using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CongruencialLinealCombinado : MonoBehaviour
{

    public InputField xnInput;
    public GameObject Header;
    public GameObject HeaderItem;
    public Button siguienteButton;
    public Button enterButton;
    public Button generarButton;
    
    public InputField xInput;
    public InputField a1Input;
    public InputField m1Input;
    public InputField m3Input;

    public List<int> xInputs = new List<int>();
    public List<int> aInputs = new List<int>();
    public List<int> mInputs = new List<int>();

    public int numInputs = 0;

    public GameObject Row;
    public GameObject Semilla;
    public GameObject Generador;
    public GameObject Numero_aleatorio;
    public GameObject Ri;
    public GameObject Content;
    public GameObject TablePrefab;
    public GameObject CanvasReference;

    public float calcularW(List<int> xInputs, int m3){
        float w = 0;
        float sumW = 0;
        // Para conocer w y si se suma o restan las xi
        for(int i = 0; i<xInputs.Count;i++){
            sumW += Mathf.Pow(-1, (float)i)*xInputs[i];
        }
        if(sumW<0){
            w = m3 - Mathf.Abs(sumW);
        }else{
            w = sumW % m3;
        }
        return w;
    }

    public void generarNumeros(){
        List<int> xActuales = new List<int>();
        // resetTable();
        int m3 = int.Parse(m3Input.text);


        int contador = 0;
        float w = 0;
        float wOriginal = 0;
        float sumW = 0;

        bool variableEqual = false;

        w = calcularW(xInputs, m3);
        wOriginal = w;

        createNewRow(xInputs, w, contador);

        List<int> xTemporales = new List<int>(xInputs);

        do{
            contador++;

            // Calculo para las xi
            for(int i = 0; i<xInputs.Count;i++){
                xActuales.Add((xTemporales[i] * aInputs[i]) % mInputs[i]);
                print(xActuales[i]);
            }
            
            w = calcularW(xActuales, m3);

            variableEqual = true;
            // // Se repiten los aleatorios
            for(int i = 0; i<xInputs.Count; i++){
                variableEqual= (variableEqual&&(xActuales[i]==xInputs[i]));
            }

            print(variableEqual);

            createNewRow(xActuales, w, contador);
            xTemporales = new List<int>(xActuales);
            xActuales.Clear();
        }while(!variableEqual);
    }

    public void createColumns(){

        siguienteButton.interactable = true;
        xInput.interactable = true;
        a1Input.interactable = true;
        m1Input.interactable = true;

        enterButton.interactable = false;
        xnInput.interactable = false;

        numInputs = int.Parse(xnInput.text);

        for(int i=0; i<numInputs;i++){
            //Instanciar nueva fila
            GameObject new_col = Instantiate(HeaderItem, new Vector3(0,0,0) , Quaternion.identity) as GameObject;
            //Unirla a la tabla
            new_col.transform.SetParent (Header.transform, false);
            new_col.GetComponent<Text>().text ="X"+i;
        }
    }

    public void siguienteAction(){
        numInputs--;
        int x = int.Parse(xInput.text);
        int a = int.Parse(a1Input.text);
        int m = int.Parse(m1Input.text);
        print("x: "+x+" a: "+a+" m: "+m);

        xInputs.Add(x);
        aInputs.Add(a);
        mInputs.Add(m);

        xInput.text = "";
        a1Input.text = "";
        m1Input.text = "";
        
        print(numInputs);

        if(numInputs==0){
            siguienteButton.interactable = false;
            xInput.interactable = false;
            a1Input.interactable =false;
            m1Input.interactable =false;
            m3Input.interactable =true;
            generarButton.interactable = true;
        }
    }

    public void createNewRow(List<int> xn, float w, int contador){
        //Instanciar nueva fila
        GameObject new_row = Instantiate(Row,new Vector3(0,0,0) , Quaternion.identity) as GameObject;
        //Unirla a la tabla
        new_row.transform.SetParent (Content.transform, false);

        // Instanciar fila para n
        //Instanciar nueva fila
        GameObject new_RowItemn = Instantiate(HeaderItem, new Vector3(0,0,0) , Quaternion.identity) as GameObject;
        //Unirla a la tabla
        new_RowItemn.transform.SetParent (new_row.transform, false);
        new_RowItemn.GetComponent<Text>().text = contador.ToString();

        // Instanciar fila para w
        //Instanciar nueva fila
        GameObject new_RowItemw = Instantiate(HeaderItem, new Vector3(0,0,0) , Quaternion.identity) as GameObject;
        //Unirla a la tabla
        new_RowItemw.transform.SetParent (new_row.transform, false);
        new_RowItemw.GetComponent<Text>().text = w.ToString();
        
        for(int i=0;i<xn.Count;i++){
            //Instanciar nueva fila
            GameObject new_RowItemx = Instantiate(HeaderItem, new Vector3(0,0,0) , Quaternion.identity) as GameObject;
            //Unirla a la tabla
            new_RowItemx.transform.SetParent (new_row.transform, false);
            new_RowItemx.GetComponent<Text>().text =xn[i].ToString();
        }
    }

    public void resetValues(){
        SceneManager.LoadScene("M5_1");
    }

}
