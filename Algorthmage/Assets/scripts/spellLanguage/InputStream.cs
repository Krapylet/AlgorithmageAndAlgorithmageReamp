using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

//This class is Inspired by lisperator.net/pltut/parser
public class InputStream
{
    int line;
    int col;
    int pos;
    readonly char[] rawProgram;

    //InputStream allows the lexer to read the program one character at a time.
    public InputStream(string rawProgram){
        line = 1;
        col = 0;
        pos = 0;
        this.rawProgram = rawProgram.ToCharArray();
    }

    // Returns the next character in the string
    public char Read(){
        char ch = rawProgram[pos++];
        if (ch == '\n'){
            line++;
            col = 0;
        }
        else col++;

        return ch;
    }

    //returns the next character without moving from the current position
    public char Peek(){
        return rawProgram[pos];
    }

    //Returns the caracter offset n spaces in front of the current position without moving
    public char Peek(int offset) {
        if (Eof(offset)) {
            Croak("Cannot peak after EOF");
        }
        return rawProgram[pos + offset];
    }

    //returns true if the entire program has been read
    public bool Eof(){
        return pos == rawProgram.Length;
    }

    //returns true if the program stops in offset number of charactrs
    public bool Eof(int offset) {
        return pos + offset == rawProgram.Length;
    }

    //Thows a custom error
    public void Croak(string msg){
        throw new Exception($"{msg} ({line}:{col})");
    }
}
