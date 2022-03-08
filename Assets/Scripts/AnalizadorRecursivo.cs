using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;


public class AnalizadorRecursivo : MonoBehaviour
{

    public InputField fieldToInsert;

    InputField mainInputField;
    public Text outputText;

    public Text sintaxText;

    bool correctSintax;

    bool correctLex;

    List<string>globalChain=new List<string>();


    // Start is called before the first frame update
    void Start()
    {
         mainInputField=fieldToInsert.gameObject.GetComponent<InputField>();
         mainInputField.onValueChanged.AddListener(delegate{generateLexer();});
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

    void generateLexer()
    {
        correctLex=true;
        outputText.text="";
        string tokensAux=mainInputField.text.ToString();
        string[] tokens=tokensAux.Split(' ');
        foreach(string i in tokens)
        {
            if(i.Length>0)
            {
                if(checkRegex("^(^git|^pull|^origin)$",i))
                {
                    outputText.text+="Token: Palabra Reservada      Valor:"+ i;
                }else
                {
                    if(checkRegex(@"^[0-9a-zA-Z_@$]|[0-9a-zA-Z_@$][0-9a-zA-Z_@/$]+",i))
                    {
                        outputText.text+="Token: Nombre Rama             Valor:"+ i;
                    }
                    else
                    {
                        outputText.text+="Token Desconocido         Valor:"+ i;
                        correctLex=false;
                    }
                }
                outputText.text+="\n";
            }
        }

        globalChain.Clear();
        limitChar=0;
        foreach(string i in tokens)
        {
            if(i.Length>0)
            {
                //print("posdurante: "+posChain);
                if(i=="git"||i=="pull"||i=="origin")
                {
                    globalChain.Add(i);
                    limitChar++;
                }
                else
                {
                    char[] characters = i.ToCharArray();
                    foreach (char character in characters) { 
                        globalChain.Add(character.ToString());
                        limitChar++;
                    }
                }
            }
            
        }

        //print(charChain.Length);
    }

    int limitChar;
    int actualChar;

    int process;

    public void recursividad(){
        // foreach(string i in globalChain)
        //     print(i);
        print(correctLex);
        print("AAAAAa");
        if(correctLex)
        {
            process=0;
            actualChar=0;
            git();
            pull();
            origin();
            nombreRama();
            if(limitChar==actualChar)
                sintaxText.text="Sintaxis correcta";
            else
                sintaxText.text="Sintaxis incorrecta";
        }
        else
            sintaxText.text="Léxico incorrecta";
        // print("limitChar: "+limitChar);
        // print("actualChar: "+actualChar);

    }

    void git()
    {   
        if(process==limitChar)
            return;
        //print("git");
        process++;
        if(globalChain[actualChar]=="git")
            actualChar++;
        
    }

    void pull()
    {
        if(process==limitChar)
            return;
        //print("pull");
        process++;
        if(globalChain[actualChar]=="pull")
            actualChar++;
    }

    void origin()
    {
        if(process==limitChar)
            return;
        //print("origin");
        process++;
        if(globalChain[actualChar]=="origin")
            actualChar++;
    }

    void nombreRama()
    {
        if(process==limitChar)
            return;
        //print("nombreRama");
        process++;
        if(checkRegex(@"^[0-9a-zA-Z_@$]",globalChain[actualChar]))
        {
            actualChar++;
            resto();
        }
        
    }

    void resto()
    {
        if(process==limitChar)
            return;
        //print("resto");
        process++;
        if(checkRegex(@"^[0-9a-zA-Z_@$]",globalChain[actualChar]))
        {
            //print("regex1 resto  " + globalChain[actualChar]);
            actualChar++;
            resto();
        }
        else
        {
            if(globalChain[actualChar]=="/")
            {
                //print("regex2 resto");
                actualChar++;
                nombreRama();
            }
        }
    }






}
