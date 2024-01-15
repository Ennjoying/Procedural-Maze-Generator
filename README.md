# Procedural Maze Generator
 
Assessment for my Application at DTT Amsterdam

Before i started the Assessment, i watched a video on maze generation to get a feeling for the task. https://www.youtube.com/watch?v=_aeYq5BmDMg 

As i mentioned in the Hour log, i prefer Github to write my Hours and notes. 
I had an Error during the project that i couldnt fix, so i sadly had to start over.
any Hours before that technically shouldnt be counted but i kept all notes and my original repository, in case you wish to review it.

Hours:

02.01. 11:00 - 13:00
- creation of first assets: CellCube, SpawnController, Materials
- SpawnController script: 
 	- Method to create Maze cells. 
 	- Color, width&Height customization through SerializeFields.
    
04.01. 13:00 - 15:30
- Cell and CellCube script: i added the cellCube scripts to deactive the walls of the maze and the Cell script that is 
	the parent of the CellCube, because i wanted to add triangle or hexagon mazes later on
	generation algorithm: i added methods to start the generation, for the recursive visiting of cells,
	choosing of a random neighbour.
 
12.01. 15:30 - 16:00
- small adjustments and bugfixes.
 
13.01. 11:00 - 12:45
- Housekeeping: i realized that many unnecessary files that shouldve been covered by the .gitignore, havent been covered.
	So i did some housekeeping and removed them all from my branch mazeGen.
	Sadly i made an Error which broke my entire Project, so i had to start over. This taught to always check the Files on the repo as early as possible.
 ---
14.01 13:00 - 16:00
- I created a new project and started from scratch.
- The Maze Generation is finished, Width & Height is selectable from the editor.
 
14.01 16:15 - 18:30
- creation of the first UI elements & the UI manager script
 
15.01 10:00 - 12:30
- refined UI elements, added functionality between scripts
- discovered a critical error. increasing the size to 250x250 causes a stack overflow.
 
15.01 12:30 - 13:30
- rewrote the algorithm to an interative version on the MazeGeneration branch.
- added camera distance slider
- added lock size toggle functionality
15.01 14:00 - 15:30
  - added camera movement
  - improved responsiveness of UI
  - reordered all files
15.01 15:30 - 16:00
    - small adjustments, project build

Hours of first try: 6.25

Hours of second try: 9.75

Hours in total: 16
