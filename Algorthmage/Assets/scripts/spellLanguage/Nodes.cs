using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//TODO: Nodes to be added
//str, bool, float/number, function, call, if, assign, binary, prog, repeat.

public abstract class AST : MonoBehaviour
{

    //each node calls this function to build the tree. It builds the hastable and pass it to ReturnStringTree(int depth, string name, Hashtable treeElements)
    
    public string ReturnString() {
        List<string> tree = ReturnStringTree();
        //cull the outmost layer of tabs
        tree = tree.Select(s => s.Substring(1)).ToList();
        return string.Join(Environment.NewLine, tree);
    }

    public abstract List<string> ReturnStringTree();

    //returns a string representation of the node
    //The ListList<<object>> is a tempoary datastructure. Optimally a insertion-ordered dictionary where you can
    //retrieve both keys and values by index be used.
    //the nested list should only contain two elements on the form string,primitive or string,AST or string,List<AST>
    public List<string> ReturnStringTree(List<List<object>> nodeProperties) {
        //string is built in reverse
        List<string> lines = new List<string>();

        foreach (List<object> property in nodeProperties) {
            string key = (string)property[0];
            object value = property[1];

            bool isAST = value.GetType().IsSubclassOf(typeof(AST));
            bool isCollection = null != value as List<string>;
            if (isAST) {
                lines.Add($"{key}:{{");
                lines.AddRange(((AST)value).ReturnStringTree());
                lines.Add("}");
            }
            else if (isCollection) {
                lines.Add($"{key}:{{");
                foreach (string s in (List<string>)value) {
                    lines.Add(s);
                }
                lines.Add("}");
            }
            else {
                lines.Add($"{key}: {value}");
            }
        }
        PrependDepth(ref lines);
        return lines;
    }

    //helper function to build trees for collections
    public List<string> ReturnCollectionStringTree<T>(List<T> elements) where T : AST {

        List<string> lines = new List<string>();

        foreach (AST ast in elements) {
            lines.Add("{");
            lines.AddRange(ast.ReturnStringTree());
            lines.Add("}");
        }

        PrependDepth(ref lines);

        return lines;
    }

    //helper function to insert correct amount of tabs in the tree.
    public void PrependDepth(ref List<string> lines) {
        for (int i = 0; i < lines.Count; i++) {
            lines[i] = "\t" + lines[i];
        }
    }

    //A little sortcut to make code cleaner
    //should be removed once the List<List<object>> has been replaced by some ordered table/map
    public List<object> ListOf(string key, object value) {
        return new List<object>() { key, value };
    }
}



//Represents a single int in the AST
public class NumberNode : AST
{
    private readonly int value;

    public NumberNode(int value) {
        this.value = value;
    }
    public int GetValue() {
        return value;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Number"),
            ListOf("Value", value)
        };
        return ReturnStringTree(info);
    }
}

public class StringNode : AST
{
    private readonly string value;

    public StringNode(string value) {
        this.value = value;
    }
    public string GetValue() {
        return value;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "String"),
            ListOf("Value", value)
        };
        return ReturnStringTree(info);
    }
}

public class ElementNode : AST
{
    private readonly string value;

    public ElementNode(string value) {
        this.value = value;
    }
    public string GetValue() {
        return value;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Element"),
            ListOf("Value", value)
        };
        return ReturnStringTree(info);
    }
}

public class TargetNode : AST
{
    private readonly string value;

    public TargetNode(string value) {
        this.value = value;
    }
    public string GetTargetType() {
        return value;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Target Type"),
            ListOf("Value", value)
        };
        return ReturnStringTree(info);
    }
}


//Represents a built-in method in the AST
public class MethodNode : CollectionNode
{
    private readonly string methodName;
    private readonly List<AST> args;

    public MethodNode(string methodName, List<AST> args) : base(args) {
        this.methodName = methodName;
        this.args = args;
    }

    public string GetMethodName() {
        return methodName;
    }
    override public List<AST> GetContents() {
        return args;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Method"),
            ListOf("Value", methodName),
            ListOf("Args", ReturnCollectionStringTree(args))
        };
        List<string> returnTest = ReturnStringTree(info);
        return ReturnStringTree(info);
    }
}


//Represents a single spell in the AST
public class SpellNode : AST
{
    private readonly StringNode spellName;
    private readonly TargetNode targetType;
    private readonly NumberNode scale;
    private readonly NumberNode range;
    private readonly NumberNode castTime;
    private readonly NumberNode cooldown;
    private readonly BodyNode body;

    public SpellNode(StringNode spellName, TargetNode targetType, NumberNode scale, NumberNode range, NumberNode castTime, NumberNode cooldown, BodyNode body) {
        this.spellName = spellName;
        this.targetType = targetType;
        this.scale = scale;
        this.range = range;
        this.castTime = castTime;
        this.cooldown = cooldown;
        this.body = body;
    }

    public StringNode GetName() {
        return spellName;
    }

    public TargetNode GetTargetNode() {
        return targetType;
    }

    public NumberNode GetScale() {
        return scale;
    }
    public NumberNode GetRange() {
        return range;
    }

    public NumberNode GetCastTime() {
        return castTime;
    }
    public NumberNode GetCooldown() {
        return cooldown;
    }

    public BodyNode GetBody() {
        return body;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>>() {
            ListOf("Type", "Spell"),
            ListOf("Name", spellName),
            ListOf("Target type", targetType),
            ListOf("Scale", scale),
            ListOf("Range", range),
            ListOf("Cast Time", castTime),
            ListOf("Cooldown", cooldown),
            ListOf("Body", body)
        };
        return ReturnStringTree(info);
    }
}

//Represents a single spell effect in the AST
public class BodyNode : CollectionNode
{
    private readonly List<MethodNode> methods;

    //List<MethodNode> should be changed to List<AST> in order to make everything into expressions.
    public BodyNode(List<MethodNode> methods) : base(methods.Cast<AST>().ToList()) {
        this.methods = methods;
    }

    override public List<AST> GetContents() {
        return methods.Cast<AST>().ToList();
    }

    override public List<string> ReturnStringTree() {

        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Body"),
            ListOf("Contents", ReturnCollectionStringTree(methods))
        };
        return ReturnStringTree(info);
    }
}

//represents a collection of objects
public abstract class CollectionNode : AST
{
    private readonly List<AST> contents;

    public CollectionNode(List<AST> contents) {
        this.contents = contents;
    }

    public abstract List<AST> GetContents();
}


//represents an object with an ID
public class ObjectNode : AST
{
    private readonly string objectID;

    public ObjectNode(string objectID) {
        this.objectID = objectID;
    }
    public string GetID() {
        return objectID;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Object"),
            ListOf("Value", objectID)
        };
        return ReturnStringTree(info);
    }
}


//Represents an unparsable token
public class ErrorNode : AST
{
    private readonly string error;

    public ErrorNode(string error) {
        this.error = error;
    }
    public string GetError() {
        return error;
    }

    override public List<string> ReturnStringTree() {
        List<List<object>> info = new List<List<object>> {
            ListOf("Type", "Error"),
            ListOf("Value", error)
        };
        return ReturnStringTree(info);
    }
}

