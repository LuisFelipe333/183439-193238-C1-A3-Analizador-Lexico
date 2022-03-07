using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class AnalizadorSintactico : MonoBehaviour
{

    public InputField fieldToInsert;

    InputField mainInputField;
    public Text outputText;

    // Start is called before the first frame update
    void Start()
    {
        mainInputField=fieldToInsert.gameObject.GetComponent<InputField>();
        //mainInputField.onValueChanged.AddListener(delegate{checkSintax();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] separateChains()
    {
        string tokensAux=mainInputField.text.ToString().Replace("("," ( ").Replace(")"," ) ").Replace("{"," { ").Replace("}"," } ").Replace(","," , "); //remplazando delimitadores
        tokensAux=tokensAux.Replace("+"," + ").Replace("-"," - ").Replace("*"," * ").Replace("/"," / ").Replace("="," = "); //Remplazando operadores aritmeticos
        tokensAux=tokensAux.Replace("<"," < ").Replace(">"," > "); //operadores relacionales 
        tokensAux=tokensAux.Replace("\n"," ").Replace("\t"," ");     
        //tokensAux=tokensAux.Replace("presiona (","presiona(");

        string[] tokens=tokensAux.Split(' ');

        // foreach(string i in tokens)
        // {
        //      print("Lexer="+i);
        // }

        return tokens;
    }

    public void checkSintax()
    {
        //separateChains();
        Analizador parser = new Analizador(separateChains());
        print(parser.NonRecursivePredictive());
    }
}
