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

The simplest gameplay element is the moths attraction to light. Each plant light has an attached collider that, when clicked, will toggle the light on or off. This allows the player to move the moths around the game by enabling and disabling lights.

![image](https://user-images.githubusercontent.com/32989729/187002236-4ca1b73d-bff2-411f-8c8d-01158e26eb6e.png)

Each update cycle, a script calculates the force that each light apples on each flock of moths. A linecast is performes between every active light and each moth flock to determine if they have line-of-sight to that light. If so, the light applies a force to the moth based on two factors:
1) The distance to the flock: Lights use the inverse square law to apply less force to moths that are far away.
2) The light intesity: brighter lights attract moths propertionally more than dim lights.

The attraction force from each light is summed to produce a final resultant vector. This vector is scaled by the moth flock speed and delta time to calculate the change in velocity.

### Directional Lights
Directional lights are a varient of the normal light prefab that have a configurable angle of light projection. The moths are only attracted to the light if they inside its field of view. Collider are attached to the end of the leaves to allow the user to adjust the angle during the game. This increased the flexibility and interaction of the puzzles.

![image](https://user-images.githubusercontent.com/32989729/187002862-9e1e8de5-fabb-46ca-9e2c-8df1f9459b39.png)
![image](https://user-images.githubusercontent.com/32989729/187003147-2acd14ae-1e44-4b68-baf5-ef34ea34c4b8.png)

### Dimmer sliders
An additional script was created that allows the users to dim the intensity of the lights during game. When a player clicks on the stalk of the plant, the verticle position of the click is detected. A click on the top of the stalk will increase the light to full intensity while a click on the bottom will reduce the intensity to the minimum value. The possible values created from the dimmer were quantized to only allow integer values to be produced. 


