using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

//This class is Inspired by lisperator.net/pltut/parser
public class Lexer
{
    InputStream input;

    public Lexer(InputStream input){
        this.input = input;
    }
    public bool Eof() {
        return input.Eof();
    }

    public void Croak(string msg) {
        input.Croak(msg);
    }

    //Returns the next token in the program
    public Token Read(){
        //operaters aren't implemented yet

        ReadWhile(IsWhitespace);
        char c = input.Peek();

        if (c == '#') {
            ReadComment();
            return Read();
        }

        if (c == '"') return ReadString();
        if (IsDigit(c)) return ReadDigit();
        if (IsWord(c)) return ReadWord();
        if (IsPunctuation(c)) return ReadPunctuation();
        // if (IsOperator(c)) return PeekOperator();

        input.Croak($"Character '{c}' matched no legal token");
        throw new Exception("The Lexer should never throw this exception, as it is litteraly proceeded by another exception, but it won't work without this line???");
    }

    public Token Peek(int offset = 0) {
        //operaters aren't implemented yet

        PeekWhile(IsWhitespace, ref offset);
        char c = input.Peek(offset);

        if (c == '#') {
            PeekComment(ref offset);
            return Peek(offset);
        }

        if (c == '"') return PeekString(offset);
        if (IsDigit(c)) return PeekDigit(offset);
        if (IsWord(c)) return PeekWord(offset);
        if (IsPunctuation(c)) return PeekPunctuation(offset);
        // if (IsOperator(c)) return PeekOperator();

        input.Croak($"Character '{c}' matched no legal token");
        throw new Exception("The Lexer should never throw this exception, as it is litteraly proceeded by another exception, but it won't work without this line???");
    }

    private Token ReadString() {

        //add first " to the string
        string accum = input.Read().ToString();

        //read everything as part of the string until " is seen
        while(input.Peek() != '"') {
            accum += input.Read();

            if (input.Eof()) {
                Croak("String never ended");
            }
        }

        // add the last " to the string
        accum += input.Read();

        return new Token("String", accum);
    }

    private Token PeekString(int offset) {

        //add first " to the string
        string accum = "\"";

        //read everything as part of the string until " is seen again
        int i = offset;
        while (input.Peek(i) != '\"') {

            accum += input.Peek(i);

            if (input.Eof(i++)) {
                Croak("String never ended");
            }
        }

        // add the last " to the string
        accum += "\"";

        return new Token("String", accum);
    }

    private Token ReadPunctuation() {
        return new Token("Punctuation", input.Read().ToString());
    }

    private Token PeekPunctuation(int offset) {
        Token test = new Token("Punctuation", input.Peek(offset).ToString());
        return test;
    }

    //Checks a series of letters against all keywords
    private Token ReadWord(){
        string accum = ReadWhile(IsWord);
        if(KeywordList.Contains(accum))
        {
            return new Token("Keyword", accum);
        }
        else if (StaticMethodDictionary.Contains(accum))
        {
            return new Token("Method", accum);
        }
        else if (ObjectMethodDictionary.Contains(accum)) {
            return new Token("Object method", accum);
        }
        else if (TargetTypeList.Contains(accum)) {
            return new Token("Target type", accum);
        }
        else if (ElementList.Contains(accum)) {
            return new Token("Element", accum);
        }
        else {
            return new Token("Object", accum);
        }
    }

    //Checks a series of letters against all keywords
    private Token PeekWord(int offset) {
        string accum = PeekWhile(IsWord, ref offset);
        if (KeywordList.Contains(accum)) {
            return new Token("Keyword", accum);
        }
        else if (StaticMethodDictionary.Contains(accum)) {
            return new Token("Method", accum);
        }
        else if (ObjectMethodDictionary.Contains(accum)) {
            return new Token("Object method", accum);
        }
        else if (TargetTypeList.Contains(accum)) {
            return new Token("Target type", accum);
        }
        else if (ElementList.Contains(accum)) {
            return new Token("Element", accum);
        }
        else {
            return new Token("Object", accum);
        }
    }

    //reads a digit and moves reader head
    private Token ReadDigit(){
        string accum = ReadWhile(IsDigit);
        return new Token("Integer", accum);
    }

    //peeks a digit but does not move reader head
    private Token PeekDigit(int offset) {
        string accum = PeekWhile(IsDigit, ref offset);
        return new Token("Integer", accum);
    }

    private bool IsPunctuation(char c){
        bool test =  ":;.,(){}".IndexOf(c) >= 0;
        return test;
    }

    //Accepts letters
    private bool IsWord(char c){
        return Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$");
    }

    //accepts 0-9
    private bool IsDigit(char c){
        return Regex.IsMatch(c.ToString(), @"^[0-9]+$");
    }

    //accept newline characters or space
    private bool IsWhitespace(char c){
        return " \n\r\t".IndexOf(c) >= 0;
    }

    //read until \n is encountered
    private void ReadComment(){
        ReadWhile(delegate (char c) { return c != '\n'; });
        //skip the \n character the ReadWhile loop stops at
        input.Read();
    }

    private void PeekComment(ref int offset) {
        PeekWhile(delegate (char c) { return c != '\n'; }, ref offset);
        //skip the \n character the PeekWhile loop stops at
        offset++;
    }


    //returns a sting of the next uninturrupted batch of chararacters that are accepted by the condition function
    //A condition function should take a char as input and return true if it is accepted.
    //Moves read-head position
    private string ReadWhile(Func<char, bool> condition) {
        string accum = "";

        while(!input.Eof() && condition(input.Peek())){
             accum += input.Read();
        }

        return accum;
    }

    //returns a sting of the next uninturrupted batch of chararacters that are accepted by the condition function
    //A condition function should take a char as input and return true if it is accepted.
    //Does not move read-head position
    private string PeekWhile(Func<char, bool> condition, ref int offset) {
        string accum = "";

        while (!input.Eof(offset) && condition(input.Peek(offset))) {
            accum += input.Peek(offset);
            offset++;
        }

        return accum;
    }
}
