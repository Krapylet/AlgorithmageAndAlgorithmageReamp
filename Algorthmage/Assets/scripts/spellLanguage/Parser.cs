using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

//This class is Inspired by lisperator.net/pltut/parser
public class Parser
{
    private readonly Lexer tokenStream;

    public Parser(Lexer tokenStream) {
        this.tokenStream = tokenStream;
    }

    //finds the correct way to parse all possible valid method arguments
    public AST ParseArgument() {
        string tokenType = tokenStream.Peek().GetTokenType();
        switch (tokenType){
            case "String": return ParseString();
            case "Integer": return ParseInteger();
            case "Method": return ParseMethod();
            case "Object method": return ParseMethod();
            case "Target type": return ParseTargetType();
            case "Element": return ParseElement();
            default:
                    tokenStream.Croak($"Recieved unexpected argument token {tokenType}");
                    return new ErrorNode($"Recieved unexpected argument token {tokenType}");
        }
    }

    //Parses damage types / elements
    private AST ParseElement() {
        //we don't have to test the elements validity, as the lexer already has done so. 
        string tokenValue = tokenStream.Read().GetValue();

        return new ElementNode(tokenValue);
    }

    public SpellNode ParseSpell() {

        StringNode name = (StringNode) ParseSpellParameter("Name", ParseString);
        TargetNode target = (TargetNode)ParseSpellParameter("Target", ParseTargetType);
        NumberNode scale = (NumberNode)ParseSpellParameter("Scale", ParseInteger) ;
        NumberNode range = (NumberNode)ParseSpellParameter("Range", ParseInteger);
        NumberNode castTime = (NumberNode)ParseSpellParameter("CastTime", ParseInteger);
        NumberNode cooldown = (NumberNode)ParseSpellParameter("Cooldown", ParseInteger);
        BodyNode body = (BodyNode)ParseSpellParameter("Body", ParseBody);

        return new SpellNode(name, target, scale, range, castTime, cooldown, body);
    }

    private AST ParseSpellParameter(string parameter, Func<AST> returnFunction) {
        //Check for parameter identifier
        SkipPunctuation(parameter);
        //Check for :
        SkipPunctuation(":");
        return returnFunction();
    }

    private BodyNode ParseBody() {
        SkipPunctuation("{");

        List<MethodNode> effects = new List<MethodNode>();
        //Parse each effect in the body
        while (!tokenStream.Eof()) {

            effects.Add(ParseEffect());

            //this line ensures that each expression in the body is seperated by a ';'
            if (!tokenStream.Eof()) SkipPunctuation(";");

            //break the loop once the body has been closed
            if (isPunctuation("}")) {
                SkipPunctuation("}");
                break;
            }
        }

        return new BodyNode(effects);
    }

    private MethodNode ParseEffect() {
        Token token = tokenStream.Peek();
        string tokenType = token.GetTokenType();
        string tokenValue = token.GetValue();

        switch (tokenType) {
            case "Method": return ParseMethod();
            case "Object": return ParseObjectMethod();
            case "Keyword": return ParseKeyword();
            default:
                //unknown method - return error
                tokenStream.Croak($"Expected Object, Method or Keyword but recieved {tokenType} '{tokenValue}'");
                return new MethodNode("Error", new List<AST>());
        }
    }

    private MethodNode ParseKeyword() {
        //every keyword needs to be handled individually here?
        string tokenValue = tokenStream.Peek().GetValue();

        switch (tokenValue) {
            case "TARGETS":
                return ParseObjectMethod(); //Treat as an object called Targets. Actually finding and handling targets is handled by interpreter.
            //case "For":
            //case "Foreach"
            default:
                //unknown method - return error
                tokenStream.Croak($"Unknown keyword {tokenValue}");
                return new MethodNode("Error", new List<AST>());
        }
    }

    private MethodNode ParseObjectMethod() {

        ObjectNode obj = ParseObject();
        List<AST> argNodes = new List<AST>();
        argNodes.Add(obj);

        SkipPunctuation(".");

        //check and collect the external args
        MethodNode method = ParseMethod();

        //prepend the object to the list of arguments
        argNodes.AddRange(method.GetContents());

        return new MethodNode(method.GetMethodName(), argNodes);
    }

    private ObjectNode ParseObject() {
        return new ObjectNode(tokenStream.Read().GetValue());
    }


    private MethodNode ParseMethod() {
        string methodName = tokenStream.Read().GetValue();
        //Check for valid method name
        bool MethodDoesNotExist = !AnyMethodDictionary.Contains(methodName);
        if (MethodDoesNotExist) { tokenStream.Croak($"Unknown method name {methodName}"); }

        //Check for argument start
        if (tokenStream.Read().GetValue() != "(") { tokenStream.Croak("Expected '(' after method name"); }

        //Checks that the argument are of a valid type and return their nodes in a list
        List<AST> argNodes = CheckArgumentSyntax(methodName);

        //check for argument end
        if (tokenStream.Read().GetValue() != ")") { tokenStream.Croak($"Expected ')'. {methodName} only takes {argNodes.Count} arguments"); }

        return new MethodNode(methodName, argNodes);
    }

    private StringNode ParseString() {

        string value = tokenStream.Read().GetValue();
        return new StringNode(value);
        
    }
 
    private TargetNode ParseTargetType() {
        //No need to test the validity of the target type, as the Lexer has already done so and context doesn't matter.
        string tokenValue = tokenStream.Read().GetValue();
        return new TargetNode(tokenValue);
    }

    private NumberNode ParseInteger() {
        return new NumberNode(int.Parse(tokenStream.Read().GetValue()));
    }


    private List<AST> CheckArgumentSyntax(string methodName) {
        List<AST> argNodes = new List<AST>();

        //Check arguments
        string[] argTypes = AnyMethodDictionary.GetArgs(methodName);
        for (int i = 0; i < argTypes.Length; i++) {
            //check argument types
            string currentArgType = tokenStream.Peek().GetTokenType();
            if (currentArgType != argTypes[i]) { tokenStream.Croak($"Arugment type mismatch. Expected {argTypes[i]} but got {currentArgType}"); }

            argNodes.Add(ParseArgument());

            //check if comma is needed
            if (i < argTypes.Length - 1) {
                //check if arguments are seperated by commas
                currentArgType = tokenStream.Read().GetValue();
                if (currentArgType != ",") { tokenStream.Croak($"Expected ',' but got {currentArgType}"); }
            }
        }

        return argNodes;
    }


    private void SkipPunctuation(string punctuation) {
        string tokenValue = tokenStream.Read().GetValue();
        if (tokenValue != punctuation) {
            tokenStream.Croak($"Expected '{punctuation}' but got '{tokenValue}'");
        }
    }

    private bool isPunctuation(string punctuation) {
        Token test2 = tokenStream.Peek();
        bool test = tokenStream.Peek().GetValue() == punctuation;
        return test;
    }
}