var firstNames = ["a", "Craven", "b", "Ahru", "c", "Sybil", "d", "Dracen", "e", "Elfin", "f", "Jeff", "g", "Zion", "h", "Fane", "i", "Lunas", 
"j", "Myst", "k", "Jahan", "l", "Shade", "m", "Hellis", "n", "Judis", "o", "Micah", "p", "Siffry", "q", "Solaire", "r", "Zibits", "s", "Beel", 
"t", "Lozaim", "u", "Poe", "v", "Drael", "w", "Cole", "x", "Varrik", "y", "Zaylor", "z", "Kym"]
var lastNames = ["a", "Rubyellus", "b", "Javas", "c", "Pythonos", "d", "Fortranus", "e", "Deth", "f", "Haskellum", "g", "Seaquillis", "h", 
"Nekro", "i", "Moon", "j", "Drizzt", "k", "Ben-Mezd", "l", "Gruffen", "m", "Auros", "n", "Skrull", "o", "Stain", "p", "Artorius", "q", "Aldrich", "r", "Ornstein", "s",
"Bezel", "t", "Abyssl", "u", "Gael", "v", "Adella", "w", "Yaharl", "x", "Woolf", "y", "Quaim", "z", "Tryst"]
var titles = ["the Crypt Destroyer", "the Soul Devourer", "the Shrieking Tomb", "the Blind Prescence", "the Silent Hunter", "the Blood Syphon", "the Black Rain", 
"the Ghost Flayer", "the Weeping Spirit", "the Lost Child", "the Fallen", "the Normal", "the Bloodlust", "the Smile Stealer",
"the Wailing Siren", "the Stinking Wretch", "the Unseen Butcher", "the Witch of the North"]

var imageList = ["../images/bg1.jpg", "../images/bg2.jpg", "../images/bg3.jpg"]

//this function occurs when the user clicks the button in order to generate the SPOOPY name
function getName() {
    var fInit = document.getElementById("firstN").value.charAt(0).toLowerCase(); //this pulls the first character from the user input and converts to lower case
    var lInit = document.getElementById("lastN").value.charAt(0).toLowerCase(); //this pulls the first character from the user input and converts to lower case
    var newFirst = firstNames[(firstNames.indexOf(fInit)) + 1]; //using the initial, find the corresponding name from the list firstNames
    var newLast = lastNames[(lastNames.indexOf(lInit)) + 1]; //using the initial, find the corresponding name from the list lastNames
    var rando = Math.floor(Math.random() * (titles.length)); //pick a random number from 0 to length of list titles
    var title = titles[rando]; //assigns random title from list to var title
    var fullName = newFirst + " " + newLast + " " + title; //generate a string of the spoopy name
    $(".result_list").append("<dd>" + fullName + "</dd>") //append detailed list with newly generated name with JQuery
    var listRando = Math.floor(Math.random() * (imageList.length)); //generate a random number to pick new header/footer bg
    var newBG = imageList[listRando]; //assigns that bg to a string
    $(".header-image").css("background-image", "url(" + newBG + ")"); //replace old bg with new bg url via JQuery and css
    $(".footer-image").css("background-image", "url(" + newBG + ")"); //replace old bg with new bg url via JQuery and css
}

//This functions makes sure that the user only inputs alpha characters
function lettersOnly(input) {
    var regex = /[^a-z]/gi;
    input.value = input.value.replace(regex, "");
}

//Clears the list
function clearList() {
    $(".result_list").empty();    
}
