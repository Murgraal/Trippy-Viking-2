# Trippy-Viking-Remake
Hacking together a sequel to 2016 debut game of my studio. Testing out some arcitechtural ideas i've gotten on the way, for fast iteration and modularity for a game codebase.

First tought it will be a Remake, but i'm quite sure it will be a sequel as i'll be transfering from 2D to 3D and also iterating some of the design of the game. 
The original was relatively difficult to get into and it took a lot of practice to understand the logic behind how the character moves.
In this version i want to make the game more user friendly on the maneuverability side of things and instead focus on creating more challenging content, and hopefully make a more interesting game without losing the charm of the original.

I have been pondering on some arcitechtural problems in game codebases that have occured to me while working on multiple projects and trying to tackle them here.
Decided this would be a good project to test out how those ideas would work in a project that becomes a full game and gets released. 

### Based on my current understanding the two key problems you need to tackle in game code base arcitechture is: 

1. The game codebase need to be able to adjust and change as fast as the design changes. 
2. Everything needs to be able to know about any state of the game world anytime, without impairing performance too much. 

#### Problem 1

My current approach is basically to try to separate the implementation logic as much as possible from the GameObjects that uses that logic.
Hence if i want to create a new GameObject i will write most of it's logic into a static function and then just call those functions through the unity lifecycle methods.

Another thing i have often seen when working on games is that there's and object, that has tightly coupled logic and data bound to it.
Sometimes you would potentially want to reuse some of that logic, but you really can't without making an instance of that object.
There's a high likelihood probably encapsulates a lot of it's logic and has multiple side-effects so accessing that part of the logic in isolation, will probably be difficult. 

With my approach i have basically just a lot of static utility functions that i can call from wherever in the codebase, so if i ever need to reuse some part of the logic, that is easily doable.
Also not having to think between the relationship of and object and the logic / data it needs to encapsulate / implement, makes it much easier to just write the functionality that you need. 

#### Problem 2

This one i'm not currently 100% sure about, but my current approach has been to have all the data in a static blob that is easily accesible.
People who vouch for OOP practices might think that this seems dangerous, but i believe that you can expose data as long as everyone who works in the codebase are disciplined.
With discipline i mean that they know which data they should read only and which data they should modify.

I made one base class that is Entity that updates it's data to the global data blob every frame, which can then be accessed by anyone as long as they know the instance id of that entity.
I tested this out in another project and as long as the data that is shared in this way between entities and read on a every frame basis is structs, it won't create any memory overhead.
This way i don't need to build complicated event structures where entities notifies other entities about state changes, i can just read that data every frame if necessary and react upon it instantly when it changes.
I don't know if this will be my final approach, also this is not really bringing much value to this game as there's really little information that any entity needs to know about in the game, but i'd tought i'd test it anyways to see if i can hit some potential drawbacks.

I'm not going to start to define any sort of style or paradigm that these ideas are inspired from, or derived from. 
Instead i see it as i've learned of multiple language features throughout my years of game development in Unity and c# and i'm just trying to make the best out of it to solve these two problems.

The code will be relatively messy as i'm working on multiple computers and don't want to waste time on keeping it too tidy for now.
Once the game is ready and probably somewhere during the process i will do some rounds of cleanup most likely. 

#### Other thoughts

When i have been working with bigger codebases in a more OOP manner, where data is strictly bound to the object that will use that data, it will get relatively difficult to locate that data as the project grows and amount of assets in the project goes up. Having all data in one place makes it very simple to locate that information. Easily finding all settings that drives the games logic, is relatively crucial for not wasting a lot of time on trying to remember where something was located. 
