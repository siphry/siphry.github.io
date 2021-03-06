## Homework 2
The goal of homework 2 was to get familiar with both Javascript and JQuery for building webpages with user input, as well as modifying existing elements of the webpage.
I am slowly getting more comfortable exploring different elements of webdesign as well as understanding and incorporating different capabilities such user input and action events via Javascript, JQuery, etc.

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW2.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW2)  
[HW2 Demo](https://siphry.github.io/HW2/HTML/index.html)

### Step 1 [Setup]
I created a new folder in the directory, and created the files for this assignment via git bash and the `touch` command. I forgot about working from a new branch until I was almost done with the assignment unfortunately, but created the branch "surface" (later renamed to "hw2) about halfway through the styling process of my webpage after I had worked out most of the actual Javascript elements.    

### Step 2 [Planning & Design]
![names sketch](https://siphry.github.io/HW2/images/namelist.jpg)
At first I had no idea what to do at all, but when talking with other students on Monday someone mentioned a name generator and I decided to make a Halloween themed name generator, since whatever I did I wanted to make Halloween themed since this assignment was started/completed in October. A name generator is a relatively simple but fun project to make. I started by brain storming all of the first and last name options as well as the titles. Originally I did not want to come up with 52 different options but once I started thinking about how to translate the user inputted name into my Halloween-themed names, I decided it would just be simpler to come up with 52 options and assign each option to a letter in the alphabet. I was going to make titles tied to birth month but thought it'd be more fun to choose those randomly instead. 

### Step 3 [Planning & Design]
![sketch](https://siphry.github.io/HW2/images/sketch.jpg)
My original design was similar to the webpage I created for assignment 1, but while working on the design in visual studio code and seeing how the name generator/list looked, I decided to go with the end result seen in the demo. The only elements that I really kept from my original design were the colors, center alignment, and including halloween-themed images. 

### Step 4 [Content/Coding]
*index.html and hw2.js are found in the HW2/HTML while styles.css is found in HW2/css* 
```html
<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="../css/bootstrap.min.css">
        <link rel="stylesheet" href="../css/styles.css">
        <title>the SPOOPY GENERATOR</title>
    </head>
<body>
    <div class="container">
         <!--This image is replaced randomly via JQuery upon button click above-->  
        <div class="header-image"></div>
          
            <h1>the SPOOPY NAME GENERATOR</h1>
 
            <p>Please input your first and last name to find your SPOOPY name!</p>
            <form>
                <!--These two input types create single line text boxes for user input-->
                <input type="text" id="firstN" value="First name" onkeyup="lettersOnly(this)">
                <input type="text" id="lastN" value="Last name" onkeyup="lettersOnly(this)">
                <!--This button calls the javascript function getName() upon click-->
                <button type="button" id="name">Find your spoopy name!</button>
            </form>
            <!--An empty list for JQuery in the .js file to append with the generated names-->
            <dl id="result">
            <dt>Results display here:</dt>
               <div class="result_list"></div>
            </dl>
            <button onclick="clearList()">Clear list</button>
        <!--This image is replaced randomly via JQuery upon button click above-->    
        <div class="footer-image"></div>
        <div class="space"></div>
        <div class="gif"></div>
    </div>
</div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="../HTML/hw2.js"></script>
</body>
</html>
```   

I kept the HTML and design for this assignment relatively simple, and I preferred to keep the Javascript separate from the HTML. Once again, I used [W3Schools](https://www.w3schools.com/jsref/jsref_obj_array.asp), as well as [this](https://medium.freecodecamp.org/creating-a-bare-bones-quote-generator-with-javascript-and-html-for-absolute-beginners-5264e1725f08) blog for inspiration and reference to create my name generator. I also checked stack overflow whenever I ran into issues, such as when I couldn't figure out how to replace the background image in the .css file via the .js file with JQuery.

Since the name generator is a relatively simple-looking list, I wanted to make the website colors and graphics interesting and in line with the theme, so orange and black was almost a requirement. I found the banner images online and set them to repeat and created the shapes via padding in the .css file. After I finished everything and merged my branhes, I found the dancing skeleton gifs and had to add them. I like the simple design, it's eye-catching without being too busy or jarring. I tried to use columns and put the skeletons on the bottom corners, but it just didn't look right and did not work on mobile so I switched to placing them in the container undernear the generator. 

```javascript
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
function lettersOnly(input){
    var regex = /[^a-z]/gi;
    input.value = input.value.replace(regex, "");
}

//Clears the list
function clearList() {
    $(".result_list").empty();    
}

//Clicking button calls getName()
$("#name").click(function(){
    getName();
});
```   

I also tried to keep my Javascript relatively short and simple as well, however I do think that my `getName()` function could be cleaner and simpler, but I could not think of a better way to link the user inputted initials with my Halloween-themed names. The function reads in the value inputted by the user from the text boxes via the `.getElementById()` call to the input text boxes by their designated names, pulls the first char from the string, and converts it to lower case, which I use to search the array for it's corresponding "spoopy" name. A random number generator then selects the title from the titles list, and I combine all into a new string, which is added to the list via JQuery `append()`. I also use JQuery to replace the banner images with the `.css` call to replace the values of the designated element -- in this case, the background-image in the "header-image" and "footer-image" classes.

I found the `lettersOnly()` function on [YouTube](https://www.youtube.com/watch?v=OpajusnOfYo), since I could not get any regex pattern matching to work with my text inputs. The functions `getName()` and `clearList()` both use JQuery to alter the HTML on the webpage -- `getName()` appends the generated names to an emtpy detailed list, while `clearList()` then clears all of those appened items from the list section divider by using the `.empty()` method from JQuery that deletes all the HTML within the designated section/division.  

### Step 5 [Test] & Step 6 [Turn it In]
![git log](https://siphry.github.io/HW2/images/gitlog.PNG)  
Unfortunately I forgot to create a new branch to do everything on, probably due to the issues I ran into with I was working on two different branches for assignment 1. I did eventually create a branch named *surface* (later renamed to *hw2*), after finishing the basic javascript but before finishing my JQuery/css/bootstrap parts. Then, once I got the page working the way I liked, I merged *hw2* back to *master* and wrapped up the assignment/blog on the master branch. After doing some more work on the portfolio and talking with others, I have changed a few things since the merge so switched back to the *surface* branch until I was completely happy with the blog/website. 

### Step 7 [Portfolio Content]
![the SPOOPY NAME GENERATOR](https://siphry.github.io/HW2/images/home_html.PNG)  
This is what the homepage looks like before any names have been generated.

![the SPOOPY NAME GENERATOR](https://siphry.github.io/HW2/images/generated_names.PNG)  
This shows a few names generated as well as the changing banner that occurs on click.

#### Notes and Resources
[W3Schools Javascript](https://www.w3schools.com/js/)  
[W3Schools JQuery](https://www.w3schools.com/jquery/default.asp)  
[JQuery](https://api.jquery.com/)  
