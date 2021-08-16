using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    readonly string type;
    readonly string value;

    //A token is a single "part" of the language, which has a type and a value
    public Token(string type, string value){
        this.type = type;
        this.value = value;
    }

    public string GetTokenType(){
        return type;
    }

    public string GetValue(){
        return value;
    }
}
