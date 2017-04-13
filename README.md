# Video Highlight

<iframe width="640" height="360" src="https://www.youtube.com/embed/woAqWj_xFZM" frameborder="0" allowfullscreen></iframe>
<br />
<hr>

# Prominent Features
## Spawning Blocks
At the start of each round, the game handles spawning a new row of blocks at the top of the screen. In each new row, there must be a spawn of the extra ball pickup. After the spot is determined for the pickup by a random integer representing the index of the 8 available spots, the contents of the other spaces are then determined individually by getting a random float and checking it against the predetermined chances of each kind of block, a bumper, or an empty space. There is 50% chance that a space will contain a block with the square block having the highest chance to spawn, and only a 2% chance for the bumper to spawn in a space.

<br />
<hr>

## Event Driven System
Nearly all code and scripts for the game, besides those of the underlying engine framework, take place in an event driven state instead of inside the Update (or Tick) function. This leads to lower overhead processing through each individual object’s code to be updated each frame. In _Blocksplosion_, the only custom code in a script that runs in the Update function is the code that checks for user input and the associated tasks to handle input. Nearly all other code is handled by Unity's event system such as Blocks only running their code when the ball collides with them or the game updating logic as the balls enter the trigger at the bottom of the screen.

<br />
<hr>

## Checking for Stalled Game
A case I noticed during playtesting for the game was that balls would occasionally get stuck bouncing on a horizontal line from a weird collision or stopping against the top of the screen. This was a major issue as the round would never end. To fix this particular problem, I wrote a publicly available function in the GameManager that stopped and started a coroutine for spawning bumpers.

When the ball is first launched, the code starts the coroutine which will wait for 10 seconds and then spawn a bumper at the ball's current y value in the middle of the x plane. Every time the ball hits a block or enters the trigger at the bottom of the screen, the coroutine is stopped and then a new one started. Once the round is over, the coroutine is stopped again. This allows for balls to bounce all around the screen, but any time they appear to stop going up the screen or only moving very slowly upward, the game will spawn a bumper in their path to randomly shoot them back up into the level. Once the coroutine spawns a bumper, it then starts a new instance of the coroutine.

<br />
<hr>

# Code Repository
## [Blocksplosion Repo](https://github.com/scuhooper/BlockBreaker)
