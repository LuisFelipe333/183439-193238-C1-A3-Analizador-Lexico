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

    bool correctSintax;
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
        if(mainInputField.GetComponent<Changes>().correctLex)
        {
            AnalizadorCool parser = new AnalizadorCool(separateChains());
            correctSintax=parser.NonRecursivePredictive();
            if(correctSintax)
                outputText.text="Sintaxis correcta";
            else
                outputText.text="Sintaxis incorrecta";

        }
        else
        {
            outputText.text="Léxico incorrecta";
        }
        

    
    }
}
