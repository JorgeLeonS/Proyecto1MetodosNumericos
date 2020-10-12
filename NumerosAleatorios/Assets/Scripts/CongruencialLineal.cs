using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CongruencialLineal : MonoBehaviour
{
    public InputField x0Input;
    public InputField aInput;
    public InputField cInput;
    public InputField mInput;
    public InputField nInput;
    public InputField significanciaInput;

    public GameObject Row;
    public GameObject Semilla;
    public GameObject Generador;
    public GameObject Numero_aleatorio;
    public GameObject Ri;
    public GameObject Content;
    public GameObject TablePrefab;
    public GameObject CanvasReference;
    public GameObject textChi;
    public GameObject textKol;

    public GameObject Errortext;




    public void generarNumeros(){
        resetTable();
        long x0 = int.Parse(x0Input.text);
        long a = int.Parse(aInput.text);
        long c = int.Parse(cInput.text);
        long m = int.Parse(mInput.text);
        long n = int.Parse(nInput.text);
        double s = double.Parse(significanciaInput.text);

        if(x0<0 || a<0 || c<0 || m<0 || n<0 || s<0){
            Errortext.GetComponent<Text>().text = "El programa no acepta números negativos, se pasarán a positivos.";
            x0 = Math.Abs(x0);
            a = Math.Abs(a);
            c = Math.Abs(c);
            m = Math.Abs(m);
            n = Math.Abs(n);
            if(s<0){
                s = s*-1;
            }
        }

        string x0String = x0Input.text;

        List<float> listResut = new List<float>();

        // print(x0 + a + c + m);

        //Se repite las veces que quiera el usuario
        for (int i = 0;i<n;i++){
            long generador = ((x0 * a) + c);

            print("Generador "+generador);

            long aleatorio = generador % m;

            float aleatorio_float  = (float)aleatorio;
            float  m_float = (float)m;
            float ri = aleatorio_float / m_float;

            createNewRow(x0.ToString(), generador.ToString(), aleatorio.ToString(), ri);
            listResut.Add(ri);

            print("aleatorio "+aleatorio);
            print("ri"+ri.ToString("0.000"));

            x0 = aleatorio;
        }

        listResut.Sort();


        calcularChi(listResut, s);
        calcularKolmogorov(listResut, s);


    }

    public void calcularChi(List<float> listResut, double s)
    {
        float k = 1f + 3.322f * Mathf.Log10(listResut.Count);
        k = Mathf.Floor(k);
        float clase = 1 / k;


        print("K " + k);
        print("clase " + clase);
        print("taman " + listResut.Count);

        float lower = 0f;
        float upper = clase;

        int grados = (int)k - 2;

        double[,] tablaChi = new double[,]
        {
            { 10.82757,
              13.81551,
              16.26624,
              18.46683,
              20.51501,
              22.45774,
              24.32189,
              26.12448,
              27.87716,
              29.5883,
              31.26413,
              32.90949,
              34.52818,
              36.12327,
              37.6973,
              39.25235,
              40.79022,
              42.3124,
              43.8202,
              45.31475
            },
            {
              6.6349,
              9.21034,
              11.34487,
              13.2767,
              15.08627,
              16.81189,
              18.47531,
              20.09024,
              21.66599,
              23.20925,
              24.72497,
              26.21697,
              27.68825,
              29.14124,
              30.57791,
              31.99993,
              33.40866,
              34.80531,
              36.19087,
              37.56623
            },
            {
              5.41189,
              7.82405,
              9.83741,
              11.66784,
              13.38822,
              15.03321,
              16.62242,
              18.16823,
              19.67902,
              21.16077,
              22.61794,
              24.05396,
              25.47151,
              26.87276,
              28.2595,
              29.63318,
              30.99505,
              32.34616,
              33.68743,
              35.01963
            },
            {
              3.84146,
              5.99146,
              7.81473,
              9.48773,
              11.0705,
              12.59159,
              14.06714,
              15.50731,
              16.91898,
              18.30704,
              19.67514,
              21.02607,
              22.36203,
              23.68479,
              24.99579,
              26.29623,
              27.58711,
              28.8693,
              30.14353,
              31.41043
            },
            {
              2.70554,
              4.60517,
              6.25139,
              7.77944,
              9.23636,
              10.64464,
              12.01704,
              13.36157,
              14.68366,
              15.98718,
              17.27501,
              18.54935,
              19.81193,
              21.06414,
              22.30713,
              23.54183,
              24.76904,
              25.98942,
              27.20357,
              28.41198
            },
            {
              2.07225,
              3.79424,
              5.31705,
              6.74488,
              8.1152,
              9.4461,
              10.7479,
              12.02707,
              13.28804,
              14.53394,
              15.7671,
              16.98931,
              18.20198,
              19.40624,
              20.60301,
              21.79306,
              22.97703,
              24.15547,
              25.32885,
              26.49758
            },
            {
              1.64237,
              3.21888,
              4.64163,
              5.98862,
              7.28928,
              8.55806,
              9.80325,
              11.03009,
              12.24215,
              13.44196,
              14.63142,
              15.81199,
              16.9848,
              18.15077,
              19.31066,
              20.46508,
              21.61456,
              22.75955,
              23.90042,
              25.03751
            }
        };



        float esperado = (float)listResut.Count / k;


        float chi = 0;

        while (upper <= 1.0f)
        {
            float observado = 0;



            for (int i = 0; i < listResut.Count; i++)
            {
                if (listResut[i] > lower && listResut[i] <= upper)
                {
                    observado++;

                }

            }



            print("observado " + observado);
            print("esperado " + esperado);


            lower = lower + clase;
            upper = upper + clase;


            chi += Mathf.Pow(observado - esperado, 2) / esperado;
            print("chi " + chi);

        }

        int significanciaTablaChi = 0;

        switch (s)
        {
            case 0.001:
                significanciaTablaChi = 0;
                break;
            case 0.01:
                significanciaTablaChi = 1;
                break;
            case 0.02:
                significanciaTablaChi = 2;
                break;
            case 0.05:
                significanciaTablaChi = 3;
                print("Significancia " + s);
                print("grados " + grados);
                break;
            case 0.1:
                significanciaTablaChi = 4;
                break;
            case 0.15:
                significanciaTablaChi = 5;
                break;
            case 0.2:
                significanciaTablaChi = 5;
                break;
            default:
                significanciaTablaChi = 3;
                break;

        }

        if (chi <= (float)tablaChi[significanciaTablaChi, grados])
        {
            print("Si es valido con Chi Cuadrada porque " + chi + " es menor que  " + tablaChi[significanciaTablaChi, grados]);
            textChi.GetComponent<Text>().text = "Si es aceptado con Chi Cuadrada porque el valor:  " + chi + " es menor que el valor de la tabla " + tablaChi[significanciaTablaChi, grados];
        }
        else
        {
            print("No es valido porque " + chi + " es mayor que  " + tablaChi[significanciaTablaChi, grados]);
            textChi.GetComponent<Text>().text = "No es aceptado con Chi Cuadrada porque el valor: " + chi + " es mayor que el valor de la tabla " + tablaChi[significanciaTablaChi, grados];

        }
    }

    public void calcularKolmogorov(List<float> listResut, double s)
    {
        int n = listResut.Count;
        List<float> listIN = new List<float>();
        List<float> DPlus = new List<float>();
        List<float> DMinus = new List<float>();

        listIN.Add((float)(1 / n));
        DPlus.Add(listIN[0] - listResut[0]);
        DMinus.Add(listResut[0]);

        for (int i = 1; i < n; i++)
        {
            listIN.Add((float)(i / n));
            DPlus.Add(Mathf.Abs(listIN[i] - listResut[i]));
            DMinus.Add(Mathf.Abs(listResut[i] - listIN[i - 1]));

        }

        DPlus.Sort();
        DMinus.Sort();

        float valorKolmogorov = Mathf.Max(DPlus[n - 1], DMinus[n - 1]);

        double[,] tablaKol = new double[,] {
            {
                 0.99950,
                  0.97764,
                  0.92063,
                  0.85046,
                  0.78137,
                  0.72479,
                  0.6793,
                  0.64098,
                  0.06846,
                  0.58042,
                  0.55588,
                  0.53422,
                  0.5149,
                  0.49753,
                  0.48182,
                  0.4675,
                  0.4544,
                  0.44234,
                  0.43119,
                  0.42085
            },
            {
                0.995,
              0.9293,
              0.829,
              0.73421,
              0.66855,
              0.6166,
              0.5758,
              0.5418,
              0.5133,
              0.48895,
              0.4677,
              0.44905,
              0.43246,
              0.4176,
              0.4042,
              0.392,
              0.38085,
              0.37063,
              0.36116,
              0.3524
            },
            {
                0.99,
              0.9,
              0.78456,
              0.68887,
              0.62718,
              0.57741,
              0.53844,
              0.50654,
              0.4796,
              0.45662,
              0.4367,
              0.41918,
              0.40362,
              0.3897,
              0.37713,
              0.36701,
              0.35528,
              0.34569,
              0.33685,
              0.32866
            },
            {
                0.975,
              0.84189,
              0.7076,
              0.62394,
              0.56327,
              0.51926,
              0.48343,
              0.45427,
              0.43001,
              0.40925,
              0.39122,
              0.37543,
              0.36143,
              0.3489,
              0.3376,
              0.32733,
              0.31796,
              0.30936,
              0.30142,
              0.29407
            },
            {
                0.95,
              0.77369,
              0.63604,
              0.56522,
              0.50945,
              0.46799,
              0.43607,
              0.40962,
              0.38746,
              0.36866,
              0.35242,
              0.33815,
              0.32548,
              0.31417,
              0.30397,
              0.29471,
              0.28627,
              0.27851,
              0.27135,
              0.26473
            },
            {
                0.925,
              0.72614,
              0.59582,
              0.52476,
              0.47439,
              0.43526,
              0.40497,
              0.38062,
              0.36006,
              0.3425,
              0.32734,
              0.31408,
              0.30233,
              0.29181,
              0.28233,
              0.27372,
              0.26587,
              0.25867,
              0.25202,
              0.24587
            },
            {
                0.9,
              0.68377,
              0.56481,
              0.49265,
              0.44697,
              0.41035,
              0.38145,
              0.35828,
              0.33907,
              0.32257,
              0.30826,
              0.29573,
              0.28466,
              0.27477,
              0.26585,
              0.25774,
              0.25035,
              0.24356,
              0.23731,
              0.23152
            }
        };

        int significanciaTablaKol = 0;
        double valorTabla = 0;

        switch (s)
        {
            case 0.001:
                significanciaTablaKol = 0;
                if (n > 19)
                {
                    valorTabla = (double)(1.94947f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.01:
                significanciaTablaKol = 1;
                if (n > 19)
                {
                    valorTabla = (double)(1.62762f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.02:
                significanciaTablaKol = 2;
                if (n > 19)
                {
                    valorTabla = (double)(1.51743f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.05:
                significanciaTablaKol = 3;
                print("Significancia k " + s);
                print("grados " + n);
                if (n > 19)
                {
                    valorTabla = (double)(1.35810f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.1:
                significanciaTablaKol = 4;
                if (n > 19)
                {
                    valorTabla = (double)(1.22385f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.15:
                significanciaTablaKol = 5;
                if (n > 19)
                {
                    valorTabla = (double)(1.13795f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
            case 0.2:
                significanciaTablaKol = 5;
                if (n > 19)
                {
                    valorTabla = (double)(1.07275f / Mathf.Sqrt(n));
                }
                else
                {
                    valorTabla = tablaKol[significanciaTablaKol, n];
                }
                break;
        }



        if ((float)valorTabla >= valorKolmogorov)
        {
            print("No es valido porque " + valorTabla + " es mas grande que " + valorKolmogorov);
            textKol.GetComponent<Text>().text = "No es valido con Kolmogorov porque el valor :" + valorTabla + " es mas grande que el de la tabla :" + valorKolmogorov;
        }
        else
        {
            print("Si es valido porque " + valorTabla + " es menor que " + valorKolmogorov);
            textKol.GetComponent<Text>().text = "Si es valido con Kolmogorov porque el valor :" + valorTabla + " es menor que el de la tabla:" + valorKolmogorov;
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
