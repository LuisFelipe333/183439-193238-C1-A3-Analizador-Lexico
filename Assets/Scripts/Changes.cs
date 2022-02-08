using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Changes : MonoBehaviour
{
    // Start is called before the first frame update
    InputField mainInputField;
    public Text outputText;

    void Start()
    {
        mainInputField=gameObject.GetComponent<InputField>();
        mainInputField.onValueChanged.AddListener(delegate{ValueChanged();});
    }

    bool checkRegex(string regex,string token)
    {
        Regex rx = new Regex(@regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        bool match = rx.IsMatch(token);
        return match;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChanged()
    {
        string tokensAux=mainInputField.text.ToString().Replace("("," ( ").Replace(")"," ) ").Replace("{"," { ").Replace("}"," } ").Replace(","," , "); //remplazando delimitadores
        tokensAux=tokensAux.Replace("+"," + ").Replace("-"," - ").Replace("*"," * ").Replace("/"," / ").Replace("="," = "); //Remplazando operadores aritmeticos
        tokensAux=tokensAux.Replace("<"," < ").Replace(">"," > "); //operadores relacionales 
        tokensAux=tokensAux.Replace("\n"," ").Replace("\t"," ");     



        string[] tokens=tokensAux.Split(' ');
        outputText.text="";
        foreach(string i in tokens)
        {
            if(i.Length>0)
            {
                if(checkRegex("^(^repetir|^veces|^presiona|^mover|^fuerza|^posicionx|^posiciony|^flechaarriba|^flechaabajo|^flechaderecha|^flechaizquierda|^espacio|^Si)$",i))//regex Palabra reservada
                    outputText.text+="Token: Palabra Reservada      Valor:"+ i;
                else
                {
                    if(checkRegex("^(^cadena|^entero|^decimal)$",i))                                            //regex Tipo de dato                           
                        outputText.text+="Token: Tipo de dato           Valor:"+ i;
                    else
                    {
                        if(checkRegex(@"^\(|^\)|^\{|^\}|^,",i))                                                    //regex delimitadores
                            outputText.text+="Token: Delimitador            Valor:"+ i;
                        else
                        {
                            if(checkRegex(@"^\+|^\-|^\*|^\/|^o|^y|^=|^iguala|^diferente|^<|^>|^verdadero|^falso",i))    //regex operadores
                                outputText.text+="Token: Operador           Valor:"+ i;
                            else
                            {
                                if(checkRegex(@"^[a-zA-Z$][0-9a-zA-Z$_]+|^[a-zA-Z$]",i))                   //regex Nombre
                                    outputText.text+="Token: Nombre             Valor:"+ i;
                                else
                                {
                                    if(checkRegex(@"^([-+]?[0-9]+\.[0-9]+$)$",i))                           //regex numero decimal
                                        outputText.text+="Token: Numero Decimal     Valor:"+ i;    
                                    else
                                    {
                                        
                                        if(checkRegex(@"^[-+]?[0-9]+$",i))                                  //regex numero entero
                                            outputText.text+="Token: Numero Entero      Valor:"+ i; 
                                        else
                                        {
                                            outputText.text+="Token Desconocido         Valor:"+ i;   
                                        }
                                    }
                                }

                            }
                        
                        }
                    }
                }
                outputText.text+="\n";
            }

                
            Debug.Log(i.Length);
        }
    }
}
