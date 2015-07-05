Lost Keys United
================
Lost Keys United is a AI that can play Cloud Ball. It's a fresh start after
creating "De Wolkenhondjes" for the original challenge of Giraff.

Version 6.0
-----------
Introduced defense field play, improved passing.

Version 5.5 (1675)
------------------
Fixed an important pass bug, and added decision making to the shot on goal. Not only try
three options on the goal, but also for different speeds.

Version 5.3 (1655)
------------------
Made the internal model total independent of the original Cloud Ball model. Remove
the pre-computed passing stuff.

Version 4.0 (1650)
-----------------------
Elementary (pre-computed) passing added. The field is split into zones, how to
get there, and which zones, should not be occupied by any opponent.

Version 3.1 (Elo 1650)
----------------------
Changed to two scenarios: Possession and default. Implemented a very basic
way of passing, and one player that tries to get close to the goal.

Version 2.0 (Elo 1575)
----------------------
Introduced roles (like keeper) on top of scenario's (on the ball, pick up the
ball, etcetera). Implemented only the keeper role for now.

Version 1.0 (Elo 1400)
----------------------
Calculate the path of the ball, and sends only the player that can catch up
with it first. If on the ball, shoot on goal.

Elos
----
Based on a test set where "De Wolkenhondjes 3" was set as the reference engine with an
Elo of 1600.
