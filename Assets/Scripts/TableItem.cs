using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TableItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean _isTerminal { get; set; }
		public String _value { get; set; }

		public TableItem(Boolean terminal, String input) { _isTerminal = terminal; _value = input; }

		public override int GetHashCode()
		{
			if (_value == null) return 0;
			return _value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			TableItem other = obj as TableItem;
			return other != null && other._value == this._value;
		}
}
