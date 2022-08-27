# MothLight
MothLight is a 2D puzzle game in which the player controls lights in the environment to guide flocks of moths to a goal. This project was undertaken over the past 4 months to build Unity skills for future employment.
Some of the key concepts used are listed below:
- Scene Management
- GameObjects
- Tags
- Colliders
- Object Hierarchy (child/parent)
- Particle Systems
- Prefabs
- Sound Effects
- Lots of C# Scripting
- Texture Sheet Animation
- Vectors
- Volumes + Post Processing
- User Prefs and Persistent Settings

## Key Game Features
### Moths
The main focus of the game is centered on flocks of moths that are created in the scene when the level starts. This is achieved using a particle system with a texture sheet animation. The particles are contained using two Particle System Force Fields of different sizes. The inner field provides a small gravity to the moths to keep them in a flock, while the outer field prevents the moths from escaping from their local cluster. 
![image](https://user-images.githubusercontent.com/32989729/187002120-40adbdd5-f5ba-49bf-80ed-b5adf4f8205b.png)

The simplest gameplay element is the moth's attraction to light. Each plant light has an attached collider that, when clicked, will toggle the light on or off. This allows the player to move the moths around the game by enabling and disabling lights.

![image](https://user-images.githubusercontent.com/32989729/187002236-4ca1b73d-bff2-411f-8c8d-01158e26eb6e.png)

Each update cycle, a script calculates the force that each light apples on each flock of moths. A linecast is performed between every active light and each moth flock to determine if they have line-of-sight to that light. If so, the light applies a force to the moth based on two factors:
1) The distance to the flock: Lights use the inverse square law to apply less force to moths that are far away.
2) The light intesity: brighter lights attract moths propertionally more than dim lights.

The attraction force from each light is summed to produce a final resultant vector. This vector is scaled by the moth flock speed and delta time to calculate the change in velocity.

### Directional Lights
Directional lights are a varient of the normal light prefab that have a configurable angle of light projection. The moths are only attracted to the light if they inside its field of view. Collider are attached to the end of the leaves to allow the user to adjust the angle during the game. This increased the flexibility and interaction of the puzzles.

![image](https://user-images.githubusercontent.com/32989729/187002862-9e1e8de5-fabb-46ca-9e2c-8df1f9459b39.png)
![image](https://user-images.githubusercontent.com/32989729/187003147-2acd14ae-1e44-4b68-baf5-ef34ea34c4b8.png)

### Dimmer sliders
An additional script was created that allows the users to dim the intensity of the lights during game. When a player clicks on the stalk of the plant, the verticle position of the click is detected. A click on the top of the stalk will increase the light to full intensity while a click on the bottom will reduce the intensity to the minimum value. The possible values created from the dimmer were quantized to only allow integer values to be produced. Local coordinates were used to account for the scale and rotation of the plant.

### Voltage Limits
Another mechanic was introduced to increase puzzle complexity. A light usage limit was created that limits the total intesity of all the lights in the scene. If a light's intesity is increased using a dimmer or many lights are turned on, the voltage limit may be exceeded and a brown-out will occur. This will prevent the moths from being attracted to any of the lights and cause the lights to produce sparks.
![image](https://user-images.githubusercontent.com/32989729/187006899-405ca7d6-7294-4b60-b4be-2a029a606c4c.png)

### Glass Material
A glass material was created that allows light to pass through but blocks moths. The glass materials are dynamically added to the moth particle's collisions module and a custom raycasting layer was created to allow light to pass through. In the future, coloured glass will be implemented that changes the color of the light that passes through it. Coloured light will effect the moths in various ways such as speeding them up, destroying them, or other effects. 

![image](https://user-images.githubusercontent.com/32989729/187007195-794ed13a-c94a-4697-b812-831ea8ff04b9.png)

### Frog Enemies (Textures pending)
The frog enemy is a trap that can eat moths in range. On each update frame it will look for moths that fall within its field of view and maximum range. If a target is available, a script will move the end point of its tongue (Line Renderer) towards the group of moths. If the moths move out of range, or the end of the tongue hits the target, the frog tongue will be retracted. A waiting period has been implemented based on the frogs "hunger". A script adds hunger eat frame that it doesn't aquire food, and reduces hunger when a moth is eaten. The frog will only target moths if it is hungry enough. 

The frog script samples an animation curve to create a realistic feel for the tongue. The curve creates a fast acceleration at the start with a dramatic dropoff over the animation. 

![image](https://user-images.githubusercontent.com/32989729/187007307-45f18eb3-51a5-4beb-91b0-f3caf2529f01.png)

### Progress Management
Each level has a customizable number of "checkpoints" that must be met to succeed at the puzzle. At the start of the level, the player is shown the empty outlines of the checkpoints in the top corner of the screen. These checkpoints represent the number of moths that need to make it into the goal to complete that checkpoint. The default checkpoints are completed when 25%, 50% and 100% of the total moths make it into the goal. 

![image](https://user-images.githubusercontent.com/32989729/187007752-6c835331-e8fb-408a-9161-98176f7b4192.png)

As moths reach the goal, the checkpoints fill in to indicate progress.

![image](https://user-images.githubusercontent.com/32989729/187007862-2ff9ba71-cc22-44d0-9327-49a3dda6a114.png)

When the player either completes 100% of the checkpoints, or enough moths die that reaching the next checkpoint is impossible, a "Level Complete" menu is shown to allow the player to proceed to the next level.

![image](https://user-images.githubusercontent.com/32989729/187007932-3d02be2d-f720-40d7-94ee-3bd462734eae.png)

### Level Selection
A level selection scene allows the player to choose which puzzle they want to play. Gating prevents a player from proceeding to the next level until they have completed at least one checkpoint on the previous level.

![image](https://user-images.githubusercontent.com/32989729/187008022-480d5266-5c19-4874-ba57-9281ad475122.png)

The level selection screen has many moths flying around the environment. When a player hovers over a level to play, it turns on a light over the level which attracts moths towards the level.

![image](https://user-images.githubusercontent.com/32989729/187008068-c3df6165-99f1-43aa-850a-f864558e3d11.png)

The levels previously completed by the player are tracked in the Player Prefs so that they presist between game sessions. The number of checkpoints completed for each level are also recorded and displayed under each level. 


## Conclusion
That concludes a summary of the main features implemented so far. The project is still in active development so the list will continue to grow over time. Please feel free to report any bugs you find and we will fix them.


