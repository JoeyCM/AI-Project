# AI Project
 
##Description of the behaviors

Wandering: The soccer player in red is wandering around the scene, as is the robber when the cops are far away from the treasure.

Patrolling: The pilgrim is following the different waypoints that are all through the scene. The ghost following technique is also used for this. 

Flocking: The bees are the ones that are flocking in the scene around the bee nest on the tree. 

Perception: There is a group of zombies that use the wander and ai vision implemented. They wander until they see a villager and they start chasing him.

Behavior trees: The cops and the robber that are trying to protect and steal the treasure respectively, use behavior trees for their respective behaviors.
The cops will wander around as a group until the robber gets too close to the treasure. When he does, some cops will go protect the treasure and the rest will go hunt the robber. How this is decided is random.
The robber will wander around the scene until the cops are far away from the treasure. Then, he will go to the treasure and rob it, and after that it will wander around again. 

Formation motion: A group of knights are patrolling the scene keeping a formation.