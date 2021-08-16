using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Compiler
{
    public Compiler() {

    }

    public SpellNode CompileSpell(string rawText) {
        InputStream inputStream = new InputStream(rawText);
        Lexer tokenInput = new Lexer(inputStream);
        Parser parser = new Parser(tokenInput);
        SpellNode spell = parser.ParseSpell();


        string currentFilePath = "C:/Users/askej/Documents/Algorithmage/parseTreeOutput.txt";
        File.WriteAllText(currentFilePath, spell.ReturnString());

        return spell;
    }
}
