
// put in the tokens counts
// get the info on screen when you pick up stuff.
// draw and fix the bank and the map used.
// think about the entrance

function SpawnTriggers() {
	%triggered = 0;
	if ( $SpawningTriggerCount < 9) {
		if ( $Tokens_Collected == 0 && $SpawningTriggerCount == 0 && $Playing == 1 ) {
			if ( $Human.getPosition() < 200 ) {
				// this is the first spawning at the start of the game
				$SpawningTriggerCount = 1;
				$HowManyEnemies = 4;
				$Many_Beast1 = 4; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0;
				$XSpawn = 130; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
				echo("I SPAWNED HERE 1");
			}
		}
		else if ( $Tokens_Collected == 0 && $SpawningTriggerCount == 1 && $Playing == 1) {
			if ( $Human.getPosition() < 50 ) {
				// this is the second spawning before the ac201 room
				$SpawningTriggerCount = 2;
				$HowManyEnemies = 4;
				$Many_Beast1 = 4; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0;
				$XSpawn = -20; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
				echo("I SPAWNED HERE 2");
			}
		}
		else if ( $SpawningTriggerCount == 2 && $Tokens_Collected == 1 ) {
			if ( $Human.getPosition() < -100 ) {
				// this is the third spawning outside the bank
				$SpawningTriggerCount = 3;
				$HowManyEnemies = 4;
				$Many_Beast1 = 2; $Many_Beast2 = 2; $Many_Beast3 = 0; $Many_Beast4 = 0;
				$XSpawn = -175; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 3 && $Tokens_Collected == 1 ) {
			if ( $Human.getPosition() < -204 ) {
				// this is the forth spawning in geography section
				$SpawningTriggerCount = 4;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 0; $Many_Beast4 = 0;
				$XSpawn = -300; $YSpawn = 115; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 4 && $Tokens_Collected == 2 ) {
			if ( $Human.getPosition() > -260 ) {
				// this is the fifth spawning in geography section
				$SpawningTriggerCount = 5;
				$HowManyEnemies = 2;
				$Many_Beast1 = 1; $Many_Beast2 = 1; $Many_Beast3 = 0; $Many_Beast4 = 0;
				$XSpawn = -208; $YSpawn = 112; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 5 && $Tokens_Collected == 2 ) {
			if ( $Human.getPosition() > -167 ) {
				// this is the fifth spawning along the concourse
				$SpawningTriggerCount = 6;
				$HowManyEnemies = 5;
				$Many_Beast1 = 2; $Many_Beast2 = 2; $Many_Beast3 = 1; $Many_Beast4 = 0;
				$XSpawn = -100; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 6 && $Tokens_Collected == 2 ) {
			if ( $Human.getPosition() > 20 ) {
				// this is the sixth spawning along the concourse
				$SpawningTriggerCount = 7;
				$HowManyEnemies = 5;
				$Many_Beast1 = 0; $Many_Beast2 = 3; $Many_Beast3 = 2; $Many_Beast4 = 0;
				$XSpawn = 90; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 7 && $Tokens_Collected == 2 ) {
			if ( $Human.getPosition() > 130 ) {
				// this is the sixth spawning along the concourse
				$SpawningTriggerCount = 8;
				$HowManyEnemies = 3;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 7;
				$XSpawn = 200; $YSpawn = 24; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 8 && $Tokens_Collected == 3 ) {
			if ( $Human.getPosition() < 100 ) {
				// this is the 7th spawning along the concourse
				$SpawningTriggerCount = 9;
				$HowManyEnemies = 6;
				$Many_Beast1 = 0; $Many_Beast2 = 3; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = 30; $YSpawn = 18; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 16) {
		if ( $SpawningTriggerCount == 9 && $Tokens_Collected == 3 ) {
			if ( $Human.getPosition() < -108 ) {
				// this is the 8th spawning along the concourse
				$SpawningTriggerCount = 10;
				$HowManyEnemies = 8;
				$Many_Beast1 = 10; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 3;
				$XSpawn = -178; $YSpawn = 12; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 10 && $Tokens_Collected == 3 ) {
			if ( $Human.getPosition() < -204 ) {
				// this is the 9th spawning along the concourse
				$SpawningTriggerCount = 11;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 1; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -244; $YSpawn = 115; $SpawnInterval = 1000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 11 && $Tokens_Collected == 4 ) {
			if ( $Human.getPosition() > -138 ) {
				// spawn 11. ac's spawning
				$SpawningTriggerCount = 12;
				$HowManyEnemies = 4;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -78; $YSpawn = 12; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 12 && $Tokens_Collected == 5 ) {
			if ( $Human.getPosition() > 45 ) {
				// spawn 12. ac's spawning
				$SpawningTriggerCount = 13;
				$HowManyEnemies = 4;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = 110; $YSpawn = 12; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 13 && $Tokens_Collected == 6 ) {
			if ( $Human.getPosition() < -108 ) {
				// spawn 13. geography lecture spawning
				$SpawningTriggerCount = 14;
				$HowManyEnemies = 5;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 4; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -178; $YSpawn = 18; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 14 && $Tokens_Collected == 6 ) {
			if ( $Human.getPosition() < -210 ) {
				// spawn 14. geography section spawning
				$SpawningTriggerCount = 15;
				$HowManyEnemies = 2;
				$Many_Beast1 = 0; $Many_Beast2 = 2; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -300; $YSpawn = 113; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 15 && $Tokens_Collected == 7 ) {
			if ( $Human.getPosition() > -300 ) {
				// spawn 15. geography section again spawning
				$SpawningTriggerCount = 16;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -204; $YSpawn = 116; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 23) {
		if ( $SpawningTriggerCount == 16 && $Tokens_Collected == 7 ) {
			if ( $Human.getPosition() > -160 ) {
				// spawn 16. geography lecture hall spawing
				$SpawningTriggerCount = 17;
				$HowManyEnemies = 5;
				$Many_Beast1 = 5; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -90; $YSpawn = 18; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 17 && $Tokens_Collected == 7 ) {
			if ( $Human.getPosition() > -130 ) {
				// spawn 17. geography lecture hall spawing near above spawning
				$SpawningTriggerCount = 18;
				$HowManyEnemies = 5;
				$Many_Beast1 = 5; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -60; $YSpawn = 18; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 18 && $Tokens_Collected == 8 ) {
			if ( $Human.getPosition() < -139 ) {
				// spawn 18. on the way to the bank. spawn enemies outside the bank
				$SpawningTriggerCount = 19;
				$HowManyEnemies = 4;
				$Many_Beast1 = 0; $Many_Beast2 = 4; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -190; $YSpawn = 13; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 19 && $Tokens_Collected == 9 ) {
			if ( $Human.getPosition() > 15 ) {
				// spawn 19. spawn along concourse going to the first entrance room BIG
				$SpawningTriggerCount = 20;
				$HowManyEnemies = 4;
				$Many_Beast1 = 4; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = 60; $YSpawn = 13; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 20 && $Tokens_Collected == 9 ) {
			if ( $Human.getPosition() > 130 && $SpawningTriggerCount == 20 && $Tokens_Collected == 9) {
				// spawn 20. concourse going to big room on the right
				$SpawningTriggerCount = 21;
				$HowManyEnemies = 4;
				$Many_Beast1 = 0; $Many_Beast2 = 4; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = 190; $YSpawn = 13; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 21 && $Tokens_Collected == 9 ) {
			if ( $Human.getPosition() > 300 && $SpawningTriggerCount == 21 && $Tokens_Collected == 9) {
				// spawn 21. INSIDE the far big room on the left
				$SpawningTriggerCount = 22;
				$HowManyEnemies = 6;
				$Many_Beast1 = 6; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 3;
				$XSpawn = 370; $YSpawn = 19; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 22 && $Tokens_Collected == 9 ) {
			if ( $Human.getPosition() > 350 && $SpawningTriggerCount == 22 && $Tokens_Collected == 9) {
				// spawn 22. INSIDE the far big room on the left
				$SpawningTriggerCount = 23;
				$HowManyEnemies = 7;
				$Many_Beast1 = 7; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 3;
				$XSpawn = 400; $YSpawn = 19; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 29) {
		if ( $SpawningTriggerCount == 23 && $Tokens_Collected == 10 ) {
			if ( $Human.getPosition() < 200 && $SpawningTriggerCount == 23 && $Tokens_Collected == 10) {
				// spawn 23. along the concourse  going to gepgraphy room 3
				$SpawningTriggerCount = 24;
				$HowManyEnemies = 3;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = 130; $YSpawn = 19; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 24 && $Tokens_Collected == 10 ) {
			if ( $Human.getPosition() < -108 && $SpawningTriggerCount == 24 && $Tokens_Collected == 10) {
				// spawn 24. along concourse on the way to the geography 3
				$SpawningTriggerCount = 25;
				$HowManyEnemies = 3;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -178; $YSpawn = 24; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 25 && $Tokens_Collected == 10 ) {
			if ( $Human.getPosition() < -204 && $SpawningTriggerCount == 25 && $Tokens_Collected == 10) {
				// spawn 25. along geography on the way to the geography 3
				$SpawningTriggerCount = 26;
				$HowManyEnemies = 2;
				$Many_Beast1 = 0; $Many_Beast2 = 1; $Many_Beast3 = 1; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -270; $YSpawn = 112; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 26 && $Tokens_Collected == 11 ) {
			if ( $Human.getPosition() > -210 && $SpawningTriggerCount == 26 && $Tokens_Collected == 11) {
				// spawn 26. out of the geography section going to ac103
				$SpawningTriggerCount = 27;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 1; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -200; $YSpawn = 100; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 27 && $Tokens_Collected == 11 ) {
			if ( $Human.getPosition() > -160 && $SpawningTriggerCount == 27 && $Tokens_Collected == 11) {
				// spawn 27. out of the geography section going to ac103 spawned along concourse
				$SpawningTriggerCount = 28;
				$HowManyEnemies = 3;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -90; $YSpawn = 22; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		} 
		else if ( $SpawningTriggerCount == 28 && $Tokens_Collected == 12 ) {
			if ( $Human.getPosition() > -60 ) {
				// spawn 28. along concourse going to porter's desk
				$SpawningTriggerCount = 29;
				$HowManyEnemies = 4;
				$Many_Beast1 = 1; $Many_Beast2 = 1; $Many_Beast3 = 1; $Many_Beast4 = 1; $YSpawnSpread = 9;
				$XSpawn = 10; $YSpawn = 25; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 36) {
		if ( $SpawningTriggerCount == 29 && $Tokens_Collected == 13 ) {
			if ( $Human.getPosition() < -90 ) {
				// spawn 29. along geography lecture going to geography 2
				$SpawningTriggerCount = 30;
				$HowManyEnemies = 5;
				$Many_Beast1 = 2; $Many_Beast2 = 2; $Many_Beast3 = 1; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -160; $YSpawn = 25; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 30 && $Tokens_Collected == 13 ) {
			if ( $Human.getPosition() < -190 ) {
				// spawn 30. along geography section going to geography 2
				$SpawningTriggerCount = 31;
				$HowManyEnemies = 2;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 1; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -250; $YSpawn = 112; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 31 && $Tokens_Collected == 14 ) {
			if ( $Human.getPosition() > -210 ) {
				// spawn 31. along geography section going to the bank
				$SpawningTriggerCount = 32;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 2; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -200; $YSpawn = 90; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 32 && $Tokens_Collected == 14 ) {
			if ( $Human.getPosition() > -203 ) {
				// spawn 32. along geography section going to bank
				//echo("THE TRICKING SPAWNING IS GOING TO HAPPEN VERY SOON - PLEASE DONT CRASH");
				$SpawningTriggerCount = 33;
				$HowManyEnemies = 1;
				$Many_Beast1 = 4; $Many_Beast2 = 2; $Many_Beast3 = 2; $Many_Beast4 = 0; $YSpawnSpread = 4;
				$XSpawn = -206; $YSpawn = 24; $SpawnInterval = 2000; $XSpawnSpread = 4;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 33 && $Tokens_Collected == 15 ) {
			if ( $Human.getPosition() > -160 ) {
				// spawn 33. ALONG GEOGRAPHY going to geography lecture
				$XSpawnSpread = 0;
				$SpawningTriggerCount = 34;
				$HowManyEnemies = 6;
				$Many_Beast1 = 2; $Many_Beast2 = 2; $Many_Beast3 = 1; $Many_Beast4 = 1; $YSpawnSpread = 3;
				$XSpawn = -90; $YSpawn = 15; $SpawnInterval = 1200;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 34 && $Tokens_Collected == 16 ) {
			if ( $Human.getPosition() > -30 ) {
				// spawn 34. ALONG concourse going to big lecture on the right
				$XSpawnSpread = 0;
				$SpawningTriggerCount = 35;
				$HowManyEnemies = 6;
				$Many_Beast1 = 0; $Many_Beast2 = 3; $Many_Beast3 = 3; $Many_Beast4 = 0; $YSpawnSpread = 3;
				$XSpawn = 30; $YSpawn = 26; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 35 && $Tokens_Collected == 17 ) {
			if ( $Human.getPosition() < -110 ) {
				// spawn 35. along bank going to the bank
				$XSpawnSpread = 0;
				$SpawningTriggerCount = 36;
				$HowManyEnemies = 6;
				$Many_Beast1 = 6; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 0; $YSpawnSpread = 3;
				$XSpawn = -180; $YSpawn = 14; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 43) {
		if ( $SpawningTriggerCount == 36 && $Tokens_Collected == 17 ) {
			if ( $Human.getPosition() < -140 ) {
				// spawn 36. along bank going to the bank number 2
				$SpawningTriggerCount = 37;
				$HowManyEnemies = 4;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 3; $Many_Beast4 = 1; $YSpawnSpread = 3;
				$XSpawn = -200; $YSpawn = 14; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 37 && $Tokens_Collected == 18 ) {
			if ( $Human.getPosition() > -140 ) {
				// spawn 37. along concourse going to the ac202 room
				$SpawningTriggerCount = 38;
				$HowManyEnemies = 6;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 5; $Many_Beast4 = 1; $YSpawnSpread = 3;
				$XSpawn = -75; $YSpawn = 14; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 38 && $Tokens_Collected == 19 ) {
			if ( $Human.getPosition() < -103 ) {
				// spawn 38. along concourse going to lecture hall beside ac's
				$SpawningTriggerCount = 39;
				$HowManyEnemies = 7;
				$Many_Beast1 = 2; $Many_Beast2 = 1; $Many_Beast3 = 3; $Many_Beast4 = 2; $YSpawnSpread = 4;
				$XSpawn = -140; $YSpawn = 19; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 39 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 39. along concourse going to Blue room
				$SpawningTriggerCount = 40;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -165; $YSpawn = 64; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 40 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 40. along concourse going to Blue room
				$SpawningTriggerCount = 41;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -198; $YSpawn = 64; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 41 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 41. along concourse going to Blue room
				$SpawningTriggerCount = 42;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -198; $YSpawn = 82; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 42 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 42. along concourse going to Blue room
				$SpawningTriggerCount = 43;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -165; $YSpawn = 82; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	if ( $SpawningTriggerCount < 45 ) {
		if ( $SpawningTriggerCount == 43 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 43. along concourse going to Blue room
				$SpawningTriggerCount = 44;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -194; $YSpawn = 112; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
		else if ( $SpawningTriggerCount == 44 && $Tokens_Collected == 20 ) {
			if ( $Human.getPosition() < -160 ) {
				// spawn 44. along concourse going to Blue room
				$SpawningTriggerCount = 45;
				$HowManyEnemies = 1;
				$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 1; $YSpawnSpread = 4;
				$XSpawn = -165; $YSpawn = 112; $SpawnInterval = 2000;
				CreateEnemies();
				%triggered = 1;
			}
		}
	}
	else if ( $Tokens_Collected == 21 ) {
		// spawn 45. along ENTRANCES going HOME
		$SpawningTriggerCount = 46;
		$HowManyEnemies = 10;
		$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 10; $YSpawnSpread = 2;
		$XSpawn = -190; $YSpawn = 15; $SpawnInterval = 1000; $XSpawnSpread = 4;
		CreateEnemies();
		%triggered = 1;
		schedule(15000, 0, "the_end", 1);
		schedule(30000, 0, "the_end", 2);
	}
	if ( %triggered == 0 ) {
		echo("I haven't set off any triggers yet and enemy count is "@$Enemy);
	}
	if ( $Tokens_Collected < 21 && $Playing == 1) {
		schedule(1000, 0, "SpawnTriggers");
	}
	if ( %triggered == 1 ) {
		if ( $Duke_Spawn % 7 == 0 ) {
			PlayMusic(Duke_Spawned_Enemies1);
		}
		else if ( $Duke_Spawn % 7 == 1 ) {
			PlayMusic(Duke_Spawned_Enemies2);
		}
		else if ( $Duke_Spawn % 7 == 2 ) {
			PlayMusic(Duke_Spawned_Enemies3);
		}
		else if ( $Duke_Spawn % 7 == 3 ) {
			PlayMusic(Duke_Spawned_Enemies4);
		}
		else if ( $Duke_Spawn % 7 == 4 ) {
			PlayMusic(Duke_Spawned_Enemies5);
		}
		else if ( $Duke_Spawn % 7 == 5 ) {
			PlayMusic(Duke_Spawned_Enemies6);
		}
		else if ( $Duke_Spawn % 7 == 6 ) {
			PlayMusic(Duke_Spawned_Enemies7);
		}
		$Duke_Spawn++;	
	}
	
}

function here() { // testing purposes
	echo(""@$Human.getPosition());
}

function the_end(%i) {
	 if ( %i == 1) {
			$HowManyEnemies = 10;
			$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 10; $YSpawnSpread = 2;
			$XSpawn = -8; $YSpawn = 15; $SpawnInterval = 1000; $XSpawnSpread = 4;
			CreateEnemies();
		}
		if ( %i == 2) {
			$HowManyEnemies = 10;
			$Many_Beast1 = 0; $Many_Beast2 = 0; $Many_Beast3 = 0; $Many_Beast4 = 10; $YSpawnSpread = -2;
			$XSpawn = 285; $YSpawn = 33; $SpawnInterval = 1000; $XSpawnSpread = -4;
			CreateEnemies();
		}
}

function End_Game() {
	cancel($Game::Schedule);
  $Game::Running = false;
  delete_enemies();
  alxStopAll();
  Canvas.setContent(SplashScreenGameOver);
	PlayMusic(Game_Over);
	schedule(2700, 0,"PlayMusic",Duke_Pickup5);
	schedule(7700, 0,"onMissionEnded");
	
}


function SpawnTriggers_Survive() {
	// first few spawns should be near you
		$HowManyEnemies = 5;
		$Many_Beast1 = 1; $Many_Beast2 = 1; $Many_Beast3 = 1; $Many_Beast4 = 1;
		$YSpawnSpread = 3;
		if ( $How_Many_Spawns < 5 ) {
			$XSpawn = 70; 
		}
		else if ( $How_Many_Spawns < 10 ) {
			$XSpawn = -20; 
		}
		else if ( $How_Many_Spawns < 15 ) {
			$XSpawn = -158; 
		}
		else {
			$XSpawn = -190; 
		}
		$YSpawn = 13; $SpawnInterval = 1000; $XSpawnSpread = 1;
		%lefty = $Beasts_Left;
		$Beasts_Left = $HowManyEnemies + %lefty;	
		CreateEnemies();
}

function leftyhere() {
	
	echo("$Beasts_Left is "@$Beasts_Left);
}