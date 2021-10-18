(Tech demo)

https://user-images.githubusercontent.com/37876827/129513256-66ddf5fc-47df-490d-8823-12fc72fdd370.mp4

# Algorithmage - English
"Algortihmage is an action RPG in which you finally get to live out the fantasy of being a mighy mage inventing new spells. Rather than learning or selecting premade spells, you code your own spells in this game though a custom programming language. If you can dream it, you can cast it! You'll start off with a small example of a simple spell, and will then have to experiment with what you know or expolore the world to find new knowledge you can incorporate into your spells."
Currently I'm working on reworking the spell creation laguage to be programmed through player made drawings and diagrams rather than words.

# Algorithmage - Danish
"Algorithmage er et Action RPG hvor du selv skaber dine evner fra bunden vha. et custrom programmeringssprog. Til at starte med har du kun en enkelt lille besværgelse som eksempel, men efterhånden som du skaber andre algoritmagere's troldbøger får du eksempler på nye keywords og spells der kan skrives. Kun din fantasi sætter grænser."
Jeg startede med at udvikle en hurtigt prototype til algorithmage på en uge for præcis et år siden, for at lære at lave et programmeringssprog. Ud over et par kuriositeter er sproget er meget C-like. Der er ikke specielt meget gameplay i spillet endnu, men en fuld lexer, parser, tokenizer og delvis interpreter er implementeret.

https://user-images.githubusercontent.com/37876827/129513256-66ddf5fc-47df-490d-8823-12fc72fdd370.mp4

Der ligger både et build og alle source filerne i dette repo.
## Spil instruktioner
Gå - wasd

Åben troldbog - esc

For at læse en gemt besværgelse skal du blot klikke på dens navn i venstre kolonne. Dette loader besværgelsen ind i tekstredigeringsfeltet.

I tekstredigeringsfeltet kan du enten skrive en ny besværgelse eller rette i en gammel besværgelse. Klik "Quick save" for at gemme besværgelsen under navnet af den sidste besværgelse der blev loadet. For i stedet at gemme som en ny besværgelse gives først et nyt navn over tekstredigeringsfeltet hvorefter du skal klikke "Save as".

For at kaste en besværgelse i spillet skal du blot klikke 1, 2 eller 3 osv. tilsvarende til besværgelsens index i troldbogen. Den øverste besværgelse har index 1.
(Lige nu er det kun besværgelse nr. 1 der kan kastes.)

## Besværgelsessyntax
Her er et eksempel på en Fireball besværgelse:


_Target : Circle_

_Scale: 10_

_Range: 20_   

_CastTime: 3   # Everything after a '#' is ignored_

_Cooldown: 4_

_Body: {_

   _TARGETS.Damage(5, Fire);_

_}_

Alle numeriske værdier kan ændres, og nogen element typer kan være hvad som helst. "Fire" og "Ice" har forskellige effekter på fjenderne i spillet.

# Algorithmage revamp
Algorithmage revamp er det hobby projekt jeg primært arbejder på for tiden. Konceptet er det samme, men istedet for et C-like sprog, er jeg ved at lave et ikke-imperativt graph-baseret sprog, hvor spillerne selv skal tegne og sammensætte runer og skrifttegn. Det er lidt som at simulerer et elektrisk diagram. Der er ikke så meget konkret gameplay at vise frem endnu, for jeg er først nu blevet færdig med at reasearche gesture recognition, hvilket skal bruges til at bygge en parser til de håndtegnede skrifttegn.
