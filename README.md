# Algorithmage + Algorithmage revamp
"Algorithmage er et Action RPG hvor du selv skaber dine evner fra bunden vha. et custrom programmeringssprog. Til at starte med har du kun en enkelt lille besværgelse som eksempel, men efterhånden som du skaber andre algoritmagere's troldbøger får du eksempler på nye keywords og spells der kan skrives. Kun din fantasi sætter grænser."
Jeg startede med at udvikle en hurtigt prototype til algorithmage på en uge for præcis et år siden, for at lære at lave et programmeringssprog. Ud over et par kuriøsiteter er sproget er meget C-like. Der er ikke specielt meget gameplay i spillet endnu, men en fuld parser, lexer, tokenizer og delvis compuler er implementeret.


Der ligger både et build og alle source filerne i dette repo.
## Spil instruktioner
Gå - wasd

Åben troldbog - esc

For at læse en gemt besværgelse skal du blot klikke på dens navn i venstre kolonne. Dette loader besværgelsen ind i tekstredigeringsfeltet.

I tekstredigeringsfeltet kan du enten skrive en ny besværgelse eller rette i en gammel besværgelse. Klik "Quick save" for at gemme besværgelsen under navnet af den sidste besværgelse der blev loadet. For istedet at gemme som en ny besværgelse gives først et nyt navn over tekstredigeringsfeltet hvorefter du skal klikke "Save as".

For at kaste en besværgelse i spillet skal du blot klikke 1, 2 eller 3 osv. tilsvarende til besværgelsens index i troldbogen. Den øverste besværgelse har index 1.

## Besværgelsessyntax
Siden det er et år siden jeg arbejde på projektet (og fordi jeg ikke skrev det ned) kan jeg ikke huske hvor meget af syntaxen var nået at blive implementeret eller hvordan den så ud. Kig på IceBall besværgelsen for et meget simpelt eksempel. Alle numæriske værdier kan ændres, og nogen element typer er også implementeret, f.eks. "Fire" og "Ice".