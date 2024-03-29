# Original Intention
It all starts with a seed. This is a pun. A seed as in a little bit of random data, where following the rules with the same random data would always yield the same result. A seed can also become a tree. And that is what the randomly generated seed will become. A tree. A tree that can produce leaves and branches, and seeds of it's own, and those seeds will become more trees. Moment by moment the trees will grow, till from a single tiny seed, a great forest is born. For the sake of visual flavor, elements like a day and night cycle, wind, rain, snow and the occasional forest fire shall occur.

# How it works
A seed is generated from System.Random.NextDouble and then cast to an unsigned long. This is the only time random data is used in the entire project, besides unity's darling attempts at physics. Once a seed has been generated, the spawner spawns a tree from a prefab. The tree will spawn a single cell. The cell is another prefab, with many complex subroutines. If the cell's ancestor count has not exceeded the tree's limit, the cell will spawn a second cell within itself. It will then start the grow subroutine on the second cell, which will make the second cell slowly rise from the first. Based on the seed, the number of ancestors and the side the cell was spawned from, the cell might also decide to spawn a second child cell, tilted to a 75 degree angle. It then increments the branches value of the child cell (branches representing the number of times the tree has branched by the generation of the present cell) and adds a number of extra ancestors based upon how high up the tree the branch is. The value tree.branchLimit limits the number of times the tree is allowed to branch. If the value of branches for the cell is greater than zero the cell will grow a pair of leaves from each side. When the value ancestors reaches that of tree.limit, the cell will spawn an apple. The apple will grow based on the number of leaves in ancestor cells. The apple will stop growing when it's width becomes greater than 1 unit. As the amount of growth per frame is based on the number of leaves, the more leaves on the cell's ancestors, the larger the apple. Once the apple has spawned it start's two co-routines. The first will have it deparent itself from the tree after a pseudo-random amount of time based on the seed, and the number of leaves. This often yields explosive velocity, allowing the apple to fall far from the parent tree. The other will make the apple rot after 15 seconds. This takes care of apples that hit the ground and don't germinate, helping to keep the scene tidy. The apple will germinate if it hits the ground with enough clearance from the existing trees. The minimum distance between trees is set by the value of minimumDistance. A lower value gives the apple a greater chance of germinating but increases the likelyhood of clipping issues between trees. If germination takes place then the apple will be destroyed and a tree with the apple's seed, which is salted by the number of leaves the cell it spawned from had. The new tree will have a completely different seed from it's parent. The new tree will spawn a cell, and the cycle will begin anew.

# What came from coursework
* The controller script from the camera was largely covered in class.
* Spawning from a prefab was too, as well as making them.
* Texturing was covered in class too if I remember.
* The growth animation is inspired by the bullets flying from the turret of the tanks.
* The use of coroutines to have the object act based on time not based on frames was covered in class.

# What is original
* The algorithm for spawning cells is original.
* The use of recursion through ancestors is a trick I have used before.
* The algorithm to check distance from every other tree before germinating is based on the Vector2.distance trick shown in class, but ultimately my own work.
* All models are my own work.

# Where is my pride
I am most proud of the use of ancestoral recursion. Recursion is a programming technique where a function calls itself. In this case functions call themselves but on the parent cell. It is used in the functions getPedigree and countLeaves. getPedigree is used to salt the seed for generating new seeds to put in the apples. This means it's not randomness but the butterfly effect. The fact that I only googled things about the unity api itself, and nothing about algorithms besides the algorithm to generate the inital value is also a source of pride for me. I'm also kinda proud it doesn't look like garbage, like everything else I've ever made visually. The christmas theme was a total accident.

# Build and run
You may wish to modify the controls. They are for an XBox360 controller on linux. Other than that, standard unity project.

# Demo
[![YouTube](http://img.youtube.com/vi/ltsh8RBHQcY/0.jpg)](https://www.youtube.com/watch?v=ltsh8RBHQcY)
