var firstNames = ['a', 'Craven', 'b', 'Ahru', 'c', 'Sybil', 'd', 'Dracen', 'e', 'Elfin', 'f', 'Jeff', 'g', 'Zion', 'h', 'Fane', 'i', 'Lunas', 
'j', 'Myst', 'k', 'Jahan', 'l', 'Shade', 'm', 'Hellis', 'n', 'Judis', 'o', 'Micah', 'p', 'Siffry', 'q', 'Solaire', 'r', 'Zibits', 's', 'Beel', 
't', 'Lozaim', 'u', 'Poe', 'v', 'Drael', 'w', 'Cole', 'x', 'Varrik', 'y', 'Zaylor', 'z', 'Kym']
var lastNames = ['a', 'Rubyellus', 'b', 'Javas', 'c', 'Pythonos', 'd', 'Fortranus', 'e', 'Deth', 'f', 'Haskellum', 'g', 'Seaquillis', 'h', 
'Nekro', 'i', 'Moon', 'j', 'Drizzt', 'k', 'Ben-Mezd', 'l', 'Gruffen', 'm', 'Auros', 'p', 'Artorius', 'q', 'Aldrich', 'r', 'Ornstein', 's',
'Bezel', 't', 'Abyssl', 'u', 'Gael', 'v', 'Adella', 'w', 'Yaharl', 'x', 'Woolf', 'y', 'Quaim', 'z', 'Tryst']
var titles = ['the Crypt Destroyer', 'the Soul Devourer', 'the Shrieking Tomb', 'the Blind Prescence', 'the Silent Hunter', 'the Blood Syphon', 'the Black Rain', 
'the Ghost Flayer', 'the Weeping Spirit', 'the Lost Child', 'the Fallen', 'the Normal', 'the Bloodlust']

function getName() {
    var first = document.getElementById("firstN").value;
    first.toLowerCase();
    var fInit = first.charAt(0);
    var last = document.getElementById("lastN").value;
    last.toLowerCase();
    var lInit = last.charAt(0);
    var newFirst = firstNames[firstNames.findIndex(fInit) + 1];
    var newLast = lastNames[lastNames.findIndex(lInit) + 1];
    var rando = Math.floor(Math.random() * (titles.length));
    var title = titles[rando];
    document.getElementById("result").innerHTML = newFirst + " " + newLast + " " + title;
}