
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security;


public static class StaticMethodDictionary
{
    //Contains all built-in methods in a gobally accessable dictionary. 
    //Stores as key-value paris on the form 'methodName-args'
    private static readonly Dictionary<string, string[]> methodTable = new Dictionary<string, string[]>() {
        {"Cast", new[]{"string"}},
        {"Learn", new[]{"string"}},
        {"Error", new[]{"string"}}
    };

    public static bool Contains(string method) {
        
        return methodTable.ContainsKey(method);
    }

    public static string[] GetArgs(string method) {
        return methodTable[method];
    }
}

public static class ObjectMethodDictionary
{
    //Contains all built-in object methods in a gobally accessable dictionary. 
    //Stores as key-value paris on the form 'objectMethodName-args'
    //Internaly, object methods are always passed the object as the first argument in addition to the rest.
    private static readonly Dictionary<string, string[]> objectMethodTable = new Dictionary<string, string[]>() {
        {"Damage", new[]{"Integer", "Element"}},
        {"Heal", new[]{"Integer"}}
    };

    public static bool Contains(string method) {
        return objectMethodTable.ContainsKey(method);
    }

    public static string[] GetArgs(string method) {
        return objectMethodTable[method];
    }
}


public static class SpellParameterDictionary
{
    //Contains spell parameters and their  methods in a gobally accessable dictionary. 
    //Stores as key-value paris on the form 'objectMethodName-args'
    //Internaly, object methods are always passed the object as the first argument in addition to the rest.
    private static readonly Dictionary<string, string[]> objectMethodTable = new Dictionary<string, string[]>() {
        {"Damage", new[]{"Integer", "Element"}},
        {"Heal", new[]{"Integer"}}
    };

    public static bool Contains(string method) {
        return objectMethodTable.ContainsKey(method);
    }

    public static string[] GetArgs(string method) {
        return objectMethodTable[method];
    }
}

public static class AnyMethodDictionary
{
    //Contains all methods in a gobally accessable dictionary. 
    public static bool Contains(string method) {
        return ObjectMethodDictionary.Contains(method) || StaticMethodDictionary.Contains(method);
    }

    public static string[] GetArgs(string method) {
        if (ObjectMethodDictionary.Contains(method)) return ObjectMethodDictionary.GetArgs(method);
        return StaticMethodDictionary.GetArgs(method);
    }
}


public static class KeywordList
{
    //Contains all keywords in a gobally accessable list
    private static readonly List<string> keywords = new List<string>() {
        "TARGETS"
    };

    public static bool Contains(string s) {
        return keywords.Contains(s);
    }
}

public static class TargetTypeList
{
    //Contains all keywords in a gobally accessable list
    private static readonly List<string> TargetTypes = new List<string>() {
        "Targets", "Cone", "Square", "Circle", "Line", "Torus", "Polygon" 
    };

    public static bool Contains(string s) {
        return TargetTypes.Contains(s);
    }
}

public static class ElementList
{
    //Contains all keywords in a gobally accessable list
    private static readonly List<string> elements = new List<string>() {
        "Fire", "Ice", "Lightning", "Psychic", "Blunt", "Slash", "Crush", "Pierce", "Acid", "Water", "Blood", "Sickness", "Rot", "Holy"
    };

    public static bool Contains(string s) {
        return elements.Contains(s);
    }
}