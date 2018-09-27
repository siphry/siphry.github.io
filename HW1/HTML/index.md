## Homework 1  
The goal of this assignment was to get familiar with git, HTML, Bootstrap, and CSS. To do, we
installed and got familiar with various programs and basic web design building by making
a simple webpage to showcase these skills. Through this assignment, I have learned the basics 
of HTML and CSS. 

### Links
[Home](https://siphry.github.io)
[HW1 Demo](https://siphry.github.io/HW1/HTML/home.html)

### Step 1 [Setup]  
I already had a GitHub account set up from my summer internship at University of Connecticut, but I had not worked with it via a bash or command line. I downloaded and installed Git, Bootstrap, and Visual Studio Code, and enabled the necessary plugins for Visual Studio for both this assignment and future assignments. I set up folders on both my Surface and my home PC so I can switch between working at home and working on campus. I will keep each assignment in it’s own folder separately within a main CS460 folder. 

First I added an empty README through GitHub's website, then created and organized all my folders via Visual Studio Code. While all my folder and file organization is done via
Visual, all updates to my repository are done from Git Bash. I set up the bash by following the tutorial from [Don't be Afraid to Commit](https://dont-be-afraid-to-commit.readthedocs.io/en/latest/git/commandlinegit.html):

```
git config --global user.name “siphry”
git config --global user.email “stacia.i.fry@gmail.com”
```

### Step 2 [Setup]
To add my files or changes made through Visual, I follow this base cycle of commands in the bash:

```
git add . 
git status
git commit -m "commit message here"
git push origin [branch]
```
I use `add .` because I am usually altering multiple files at a time and don't want to miss anything. Then I check the status just to make sure things are looking good.
I make a multiple commits because I like to check to make sure the alterations I am making actually work properly before I write a ton of code, in case I am incorrect.

Unfortunately, I switch between my surface and my pc a lot. Originally I was using two branches separately depending on where I was working, but then it got messy moving things
to the master branch...but I often forget to pull first after switching machines so I run into the mess of my commits not lining up.

### Step 3 [Content/Coding]
Once the basics were complete, I jumped right into making the website since I wanted to get that part done completely before moving on to writing my blog.
I used [W3Schools](https://www.w3schools.com/bootstrap4/default.asp) for help on bootstrap and css styling. Here is a sample code of my navbar using bootstrap.

#### Bootstrap Navbar
```
<nav class="navbar navbar-expand-sm navbar-custom">
    <ul class="navbar-nav">
        <li class="nav-item">
            <a class="nav-link active" href="../HTML/home.html">Home</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="../HTML/link1.html">Art</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="../HTML/link2.html">Commission</a>
        </li>
    </ul>
</nav>
```

I customized my navbar by creating a new class `navbar-custom` so I could have a navigation bar with colors that matched the rest of the webpage.

#### Columns and Table

#### Lists (UL and DL)

#### CSS Style Sheet