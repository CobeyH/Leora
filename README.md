# MothLight

MothLight is a 2D puzzle game in which the player controls lights in the environment to guide flocks of moths to a goal. This project was started in June 2022 to build Unity skills for future employment.
Some of the key concepts used are listed below:

- Scene Management
- GameObjects
- Colliders
- Object Hierarchy (child/parent)
- Scriptable Objects
- Coroutines
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

The game is centered around flocks of moths that are created in the scene. This is achieved using a particle system with a texture sheet animation. The particles are contained using two Particle System Force Fields of different sizes. The inner field provides a small gravity to the moths to keep them in a flock, while the outer field prevents the moths from escaping from their local cluster.

![image](https://user-images.githubusercontent.com/32989729/187002120-40adbdd5-f5ba-49bf-80ed-b5adf4f8205b.png)

The simplest gameplay element is the moth's attraction light. Each plant light has an attached collider that, when clicked, will toggle the light on or off. This allows the player to move the moths around the game by enabling and disabling lights.

![image](https://user-images.githubusercontent.com/32989729/187002236-4ca1b73d-bff2-411f-8c8d-01158e26eb6e.png)

Each update cycle, a script calculates the equilibrium point for each flock. A linecast is performed between every active light and each moth flock to determine if they have line-of-sight to that light. If there are no obstacles blocking sight, the light will be used in a center-of-mass calculation. The intensity of the light is used as the "mass" in the COM calculation. This means that the equilibrium point will be closer to more powerful lights.

### Directional Lights

Directional lights are a variant of the normal light prefab that have a configurable angle of light projection. The moths are only attracted to the light if they are contained within its field of view. Colliders are attached to the end of the each leaf to allow the user to adjust the angle during the game. This increases the flexibility and interaction of the puzzles.

![image](https://user-images.githubusercontent.com/32989729/187002862-9e1e8de5-fabb-46ca-9e2c-8df1f9459b39.png)
![image](https://user-images.githubusercontent.com/32989729/187009624-1cbcf6d3-8db7-41b0-bbca-c2444bf7171d.png)

### Dimmer sliders

An additional script allows the users to dim the intensity of the lights during gameplay. When a player clicks on the stalk of the plant, the vertical position of the click is detected. A click on the top of the stalk will increase the light to full intensity while a click on the bottom will reduce the intensity to the minimum value. The possible values created from the dimmer were quantized to only allow integer values to be produced. Local coordinates were used to account for the scale and rotation of the plant.

### Lux Limits

A resource called "Lux" was created to limit the number of lights that can be enabled in the scene at one time. The current available lux is represented as a glowing orb with a number of rings to represent the current available lux. The lights each have a glowing ring around them to represent the amount of lux they require to activate. When a light is activated, a co-routine starts that animates a projectile from the lux source to the light location. 
![image](https://user-images.githubusercontent.com/32989729/187006899-405ca7d6-7294-4b60-b4be-2a029a606c4c.png)

### Glass Material

A glass material was created that allows light to pass through but blocks moths. The glass materials are dynamically added to the moth particle's collisions module and a custom ray-casting layer was created to allow light to pass through. In the future, coloured glass will be implemented that changes the color of the light that passes through it. Coloured light will effect the moths in various ways such as speeding them up, destroying them, or other effects.

![image](https://user-images.githubusercontent.com/32989729/187007195-794ed13a-c94a-4697-b812-831ea8ff04b9.png)

### Frog Enemies

The frog enemy is a trap that can eat moths in range. On each update frame it will look for moths that fall within it's field of view and maximum range. If a target is available, a script will move the end point of its tongue (Line Renderer) towards the group of moths. If the moths move out of range, or the end of the tongue hits the target, the frog tongue will be retracted. A waiting period has been implemented based on the frogs "hunger". A script adds hunger each frame that it doesn't acquire food, and reduces hunger when a moth is eaten. The frog will only target moths if it is hungry enough.

The frog script samples an animation curve to create a realistic feel for the tongue. The curve creates a fast acceleration at the start with a dramatic drop-off over the animation.

![image](https://user-images.githubusercontent.com/32989729/187007307-45f18eb3-51a5-4beb-91b0-f3caf2529f01.png)

### Decoy Flocks

Decoy flocks are a variant of normal flocks that do not count towards level progress. This unique property allows them to be used in puzzles to distract moths, or trigger zones.

### Event System

This project leverages scriptable objects to build an event system that doesn't require direct object assignments. The scriptable object script contains a function that will get called when the OnEventRaised function is invoked. This event will then get propagated to all the subscribers on that channel without references being assigned directly.

For example, the game controller raises an event on the "GamePause" channel when the user presses the escape button. The pause menu can listen for this event and enable itself without the game controller directly knowing about the pause menu. This system is much more scalable and allows logic for each component to be encapsulated inside individual scripts instead of calling functions from other scripts. It's also very easy to add other components that care about game pausing without modifying the game controller.

### Zones
Zones act as our version of "buttons" in a traditional platformer puzzle game. Each zone has a activation requirement that will cause it to trigger some action in the world. This system uses the scriptable object event system to create a many-to-many relationship between zones and trigger objects. For example, a zone could cause two movable platforms to update their position or cause two lights to turn on.

At this point we have two different types of zones. One zone requires any flock of moths to overlap with the zone for a set amount of time to activate, the other zone requires a certain number of particles to overlap the zone.

![image](https://user-images.githubusercontent.com/32989729/197032764-d8bd8437-19c6-4fd2-9fba-03da0db10966.png)


### Progress Management

Each level has a customizable number of "checkpoints" that must be met to succeed at the puzzle. At the start of the level, the player is shown the empty outlines of the checkpoints in the top corner of the screen. These checkpoints represent the number of moths that need to make it into the goal to complete that checkpoint. The default checkpoints are completed when 25%, 50% and 100% of the total moths make it into the goal.

![image](https://user-images.githubusercontent.com/32989729/187007752-6c835331-e8fb-408a-9161-98176f7b4192.png)

As moths reach the goal, the checkpoints fill in to indicate progress.

![image](https://user-images.githubusercontent.com/32989729/187007862-2ff9ba71-cc22-44d0-9327-49a3dda6a114.png)

When the player either completes 100% of the checkpoints, or enough moths die that reaching the next checkpoint is impossible, a "Level Complete" menu is shown to allow the player to proceed to the next level. If none of the checkpoints have been attained, a "Level Failed" menu is shown instead.

![image](https://user-images.githubusercontent.com/32989729/187007932-3d02be2d-f720-40d7-94ee-3bd462734eae.png)

### Level Selection

A level selection scene allows the player to choose which puzzle they want to play. Gating prevents a player from proceeding to the next level until they have completed at least one checkpoint on the previous level.

![image](https://user-images.githubusercontent.com/32989729/187008022-480d5266-5c19-4874-ba57-9281ad475122.png)

The level selection screen has many moths flying around the environment. When a player hovers over a level to play, it turns on a light over the level which attracts moths towards the level.

![image](https://user-images.githubusercontent.com/32989729/187008068-c3df6165-99f1-43aa-850a-f864558e3d11.png)

The levels previously completed by the player are tracked in the Player Prefs so that they persist between game sessions. The number of checkpoints completed for each level are also recorded and displayed under each level.

## Conclusion

That concludes a summary of the main features implemented so far. The project is still in active development so the list will continue to grow over time. Please feel free to report any bugs you find and we will fix them.
