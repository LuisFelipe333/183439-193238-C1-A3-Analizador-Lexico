using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class AnalizadorCool
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<String> _terminals = new List<string>()
		{
            "si",
            "a",
			"b",
			"c",
			"d",
			"e",
			"f",
			"g",
			"h",
			"i",
			"j",
			"k",
			"l",
			"m",
			"n",
			"ñ",
			"o",
			"p",
			"q",
			"r",
			"s",
			"t",
			"u",
			"v",
			"w",
			"x",
			"y",
			"z",
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H",
			"I",
			"J",
			"K",
			"L",
			"M",
			"N",
			"Ñ",
			"O",
			"P",
			"Q",
			"R",
			"S",
			"T",
			"U",
			"V",
			"W",
			"X",
			"Y",
			"Z",
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">=",
			"posicionX",
			"posicionY",
			"presiona",
			"flechaarriba",
			"flechaabajo",
			"flechaderecha",
			"flechaizquierda",
			"espacio",
			"mover",
            "fuerza",
			"{",
            "}",
			",",
			"(",
			")",
			".",
			"_",
		};

		public Stack<TableItem> _stack = new Stack<TableItem>();




		List<TableItem> tokenAsItems;



		public AnalizadorCool(string[] tokens)
		{
			tokenAsItems = new List<TableItem>();
			foreach (string i in tokens)
			{
                string aux=i.Trim();
                if(aux.Length>0&&aux!=" ")
                {
                    // Debug.Log("Token entrante:"+i);
                    // Debug.Log(checkRegex(@"^[a-zA-Z$][0-9a-zA-Z$_]+|^[a-zA-Z$]",i));
                    if (_terminals.Contains(i) )
                    {
                        tokenAsItems.Add(new TableItem(true,i));
                        //Debug.Log("Contains: " + i);
                    }
                    else
                    {
                        if (checkRegex(@"^[a-zA-Z$][0-9a-zA-Z$_]+|^[a-zA-Z$]",i)||checkRegex(@"^([-+]?[0-9]+\.[0-9]+$)$",i)||checkRegex(@"^[-+]?[0-9]+$",i)) { 

                            char[] characters = i.ToCharArray();
                            foreach (char character in characters) { 
                            tokenAsItems.Add( new TableItem(true, character.ToString()) ); 
                            // Debug.Log("Caracter separado:"+character.ToString());
                            }  
                            //Debug.Log("Regex: " + i);
                        }
                        else
                        {
                            tokenAsItems.Add(new TableItem(false, i));
                            //Debug.Log("item: " + i);
                        }
                    }
                }
			}

            tokenAsItems.Add(new TableItem(false, "$"));     
            // foreach(TableItem i in tokenAsItems)
            // {
            //      Debug.Log("A:"+i._value);
            // }


		}

		public Boolean NonRecursivePredictive ()
		{
            string[] letrasMin= new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","ñ","o","p","q","r","s","t","u","v","w","x","y","z"};
            string[] letrasMa= new string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","Ñ","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
            string[] digits= new string[]{"0","1","2","3","4","5","6","7","8","9",};
            Dictionary<Tuple<TableItem, TableItem>, Stack<TableItem>> _table = new Dictionary<Tuple<TableItem, TableItem>, Stack<TableItem>>();

           // _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, ""), new TableItem(true, "")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, ""), new TableItem(true, "") }));

           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "S"), new TableItem(true, "si")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "IF"), new TableItem(false, "RESTOACCION"), new TableItem(true, "$") }));
           Debug.Log("aaaa");
           foreach(string i in letrasMin)
           {
           Debug.Log("eeeee "+i);    
           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "S"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "DECLARACION"), new TableItem(false, "RESTOACCION"), new TableItem(true, "$") }));
           }
           foreach(string i in letrasMa)
           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "S"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "DECLARACION"), new TableItem(false, "RESTOACCION"), new TableItem(true, "$") }));

           ///////

           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESTOACCION"), new TableItem(true, "si")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "IF"), new TableItem(false, "RESTOACCION") }));
           foreach(string i in letrasMin)
           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESTOACCION"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "DECLARACION"), new TableItem(false, "RESTOACCION") }));
           foreach(string i in letrasMa)
           _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESTOACCION"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "DECLARACION"), new TableItem(false, "RESTOACCION") }));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESTOACCION"), new TableItem(true, "$")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));
           
            ////////

            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "DECLARACION"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, "="), new TableItem(false, "NE") }));
            
            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "DECLARACION"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, "="), new TableItem(false, "NE") }));

            ///////
            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "V"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "L"), new TableItem(false, "R") }));
            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "V"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "L"), new TableItem(false, "R") }));

            //////
            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "L"), new TableItem(false, "R") }));

            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "L"), new TableItem(false, "R") }));

            foreach(string i in digits)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "NR")}));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, ")")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, "_")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "_"), new TableItem(false, "V") }));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, "=")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "R"), new TableItem(true, ",")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));

            /////

            foreach(string i in letrasMin)
                    _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "L"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, i) }));

            foreach(string i in letrasMa)
                    _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "L"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, i) }));

            /////

            foreach(string i in digits)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NE"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "D"), new TableItem(false, "NR")}));

            /////

            foreach(string i in digits)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "D"), new TableItem(false, "NR")}));

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, "si")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));
            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));
            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, ")")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "NR"), new TableItem(true, ",")), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "") }));

            //////

            foreach(string i in digits)
                _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "D"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, i) }));

            /////

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "IF"), new TableItem(true, "si")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "si"),new TableItem(false, "C"),new TableItem(true, "{"),new TableItem(false, "INSTRUCCION"),new TableItem(true, "}") }));

            ///////

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "C"), new TableItem(true, "presiona")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "presiona"),new TableItem(true, "("),new TableItem(false, "K"),new TableItem(true, ")") }));

            /////

            foreach(string i in letrasMa)
            {
                    _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, i) }));
            }

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, "flechaarriba")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "flechaarriba") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, "flechaabajo")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "flechaabajo") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, "flechaderecha")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "flechaderecha") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, "flechaizquierda")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "flechaizquierda") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "K"), new TableItem(true, "espacio")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "espacio") }));

            /////

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "INSTRUCCION"), new TableItem(true, "mover")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "mover"), new TableItem(false, "VECTOR") }));
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "INSTRUCCION"), new TableItem(true, "fuerza")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "fuerza"), new TableItem(false, "VECTOR") }));

            /////

            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "VECTOR"), new TableItem(true, "(")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "("), new TableItem(false, "VALVECTOR") }));
            
            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "VALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, ","), new TableItem(false, "RESVALVECTOR") }));
            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "VALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, ","), new TableItem(false, "RESVALVECTOR") }));
            foreach(string i in digits)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "VALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "NE"), new TableItem(true, ","), new TableItem(false, "RESVALVECTOR") }));

            /////
            foreach(string i in letrasMin)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESVALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, ")") }));
            foreach(string i in letrasMa)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESVALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "V"), new TableItem(true, ")") }));
            foreach(string i in digits)
            _table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "RESVALVECTOR"), new TableItem(true, i)), new Stack<TableItem>(new List<TableItem>() { new TableItem(false, "NE"), new TableItem(true, ")") }));

            ///////
        
            //_table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "MOVER"), new TableItem(true, "mover")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "mover"),new TableItem(true, "("),new TableItem(false, "NE"),new TableItem(true, ","),new TableItem(false, "NE"),new TableItem(true, ")")}));
            
            /////

            //_table.Add(new Tuple<TableItem, TableItem>(new TableItem(false, "FUERZA"), new TableItem(true, "fuerza")), new Stack<TableItem>(new List<TableItem>() { new TableItem(true, "fuerza"),new TableItem(true, "("),new TableItem(false, "NE"),new TableItem(true, ","),new TableItem(false, "NE"),new TableItem(true, ")")}));



            

			_stack.Push(new TableItem(false, "S"));
			TableItem a = tokenAsItems[0];
			TableItem x = _stack.Peek();

            // foreach(TableItem i in _stack)
            // {
            //     Debug.Log ("aaaaaaaaaaa");
            //     Debug.Log(i);
            // }

            // foreach(TableItem i in tokenAsItems)
            // {
            //      Debug.Log("B:"+i._value);
            // }

			do
			{
				if (x._isTerminal)
				{
                    // Debug.Log("Comparacion X: "+x._value+","+x._isTerminal+" y A:"+ a._value);
					if (x._value == a._value || x._value == "$")
					{
						_stack.Pop();
						tokenAsItems.Remove(a);
						a = tokenAsItems[0];
                        // foreach(TableItem i in _stack)
                        //     Debug.Log("valores pila: "+i._value);
                        // Debug.Log("X igual A");
					}
					else
					{
                        foreach(TableItem i in _stack)
                            Debug.Log("valores error pila: "+i._value);
						return false;
					}
				}
				else
				{
                    // Debug.Log("Valor x: "+x._value+" y valor a: "+a._value);
					Tuple<TableItem, TableItem> key = new Tuple<TableItem, TableItem>(x, a);
					if (x._value != "")
					{
						if (_table.ContainsKey(key))
						{
							_stack.Pop();
							foreach (var item in _table[key])
								_stack.Push(item);
						}
						else
						{
                            Debug.Log("valores error pila: ");
                            foreach(TableItem i in _stack)
                                Debug.Log(i._value);
							return false;
						}
					}
					else { _stack.Pop(); }

				}
				x = _stack.Peek();

			} while (x._value != "$");
			return true;
		}

    bool checkRegex(string regex,string token)
    {
        Regex rx = new Regex(@regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        bool match = rx.IsMatch(token);
        return match;
    }

}
