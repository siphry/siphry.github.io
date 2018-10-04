var firstNames = ["a", "Craven", "b", "Ahru", "c", "Sybil", "d", "Dracen", "e", "Elfin", "f", "Jeff", "g", "Zion", "h", "Fane", "i", "Lunas", 
"j", "Myst", "k", "Jahan", "l", "Shade", "m", "Hellis", "n", "Judis", "o", "Micah", "p", "Siffry", "q", "Solaire", "r", "Zibits", "s", "Beel", 
"t", "Lozaim", "u", "Poe", "v", "Drael", "w", "Cole", "x", "Varrik", "y", "Zaylor", "z", "Kym"]
var lastNames = ["a", "Rubyellus", "b", "Javas", "c", "Pythonos", "d", "Fortranus", "e", "Deth", "f", "Haskellum", "g", "Seaquillis", "h", 
"Nekro", "i", "Moon", "j", "Drizzt", "k", "Ben-Mezd", "l", "Gruffen", "m", "Auros", "p", "Artorius", "q", "Aldrich", "r", "Ornstein", "s",
"Bezel", "t", "Abyssl", "u", "Gael", "v", "Adella", "w", "Yaharl", "x", "Woolf", "y", "Quaim", "z", "Tryst"]
var titles = ["the Crypt Destroyer", "the Soul Devourer", "the Shrieking Tomb", "the Blind Prescence", "the Silent Hunter", "the Blood Syphon", "the Black Rain", 
"the Ghost Flayer", "the Weeping Spirit", "the Lost Child", "the Fallen", "the Normal", "the Bloodlust", "the Smile Stealer",
"the Wailing Siren", "the Stinking Wretch", "the Unseen Butcher", "the Witch of the North"]

var imageList = ["../images/bg1.jpg", "../images/bg2.jpg", "../images/bg3.jpg"]

function getName() {
    var first = document.getElementById("firstN").value;
    var firstLower = first.toLowerCase();
    var fInit = firstLower.charAt(0).toString();
    var last = document.getElementById("lastN").value;
    var lastLower = last.toLowerCase();
    var lInit = lastLower.charAt(0).toString();
    var newFirst = firstNames[(firstNames.indexOf(fInit)) + 1];
    var newLast = lastNames[(lastNames.indexOf(lInit)) + 1];
    var rando = Math.floor(Math.random() * (titles.length));
    var title = titles[rando];
    var fullName = newFirst + " " + newLast + " " + title;
    $(".result_list").append("<dd>" + fullName + "</dd>")
    var listRando = Math.floor(Math.random() * (imageList.length));
    var newBG = imageList[listRando];
    $(".header-image").css("background-image", "url(" + newBG + ")");
    $(".footer-image").css("background-image", "url(" + newBG + ")");
}

function lettersOnly(input){
    var regex = /[^a-z]/gi;
    input.value = input.value.replace(regex, "");
}
