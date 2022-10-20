use gamestore
select * from product
select * from Developer
select * from Category
select * from ProductCategory

select DevId,count(DevId) from Product,Category where Product.DevId=Category.Id
group by DevId

insert into Developer
values('Santa Monica Studio'),
('Valve'),('Ubisoft'),
('Infinity Ward'),('DICE'),('Capcom'),('EA'),('2K Game'),('Bethesda'),('CD PROJEKT RED'),('Rockstar Games')
set dateformat dmy
insert into Product
values('Conquerors Blade',
'Conquerors Blade gives you a chance to be a true warlord. Commanding medieval troops in epic 15 vs. 15 siege battles, conquering cities with allies in the open world or fighting against players from all over the world – its you who make the choice.'
,0,'Apex Legends.jpg',7,
'Gameplay consists of graphic and realistic-looking depictions of physical conflict, weapons and depictions of human injury and death.'
,'05/11/2020'),
('Apex Legends',
'Apex Legends is the award-winning, free-to-play Hero Shooter from Respawn Entertainment. 
Master an ever-growing roster of legendary characters with powerful abilities, 
and experience strategic squad play and innovative gameplay in the next evolution of Hero Shooter and Battle Royale.'
,0,'Apex Legends.jpg',7,
'Conquer with character in Apex Legends,
a free-to-play* Hero shooter where legendary characters with powerful abilities team up to battle for fame & fortune on the fringes of the Frontier.'
,'05/11/2020'),
('Assassin Creed® Odyssey',
'Choose your fate in Assassins Creed® Odyssey. From outcast to living legend, embark on an odyssey to uncover the secrets of your past and change the fate of Ancient Greece.'
,44,'Assassins Creed Odyssey.jpg',3,
'Choose your fate in Assassins Creed® Odyssey.From outcast to living legend, embark on an odyssey to uncover the secrets of your past and change the fate of Ancient Greece.'
,'06/10/2018'),
('Battlefield 2042',
'Play your cards right with Season 2: Master of Arms. Battlefield™ 2042 is a first-person shooter that marks the return to the iconic all-out warfare of the franchise.'
,44,'Battlefield 2042.jpg',5,
'Battlefield™ 2042 is a first-person shooter that marks the return to the iconic all-out warfare of the franchise. Adapt and overcome in a near-future world transformed by disorder. Squad up and bring a cutting-edge arsenal into dynamically-changing battlegrounds supporting 128 players, unprecedented scale, and epic destruction.'
,'19/10/2021'),
('Borderlands 2',
'The Ultimate Vault Hunter’s Upgrade lets you get the most out of the Borderlands 2 experience.'
,30,'Borderlands 2.jpg',8,
'A new era of shoot and loot is about to begin. Play as one of four new vault hunters facing off against a massive new world of creatures, psychos and the evil mastermind, Handsome Jack. Make new friends, arm them with a bazillion weapons and fight alongside them in 4 player co-op on a relentless quest for revenge and redemption across the undiscovered and unpredictable living planet.'
,'20/12/2012'),
('Borderlands 3',
'The original shooter-looter returns, packing bazillions of guns and a mayhem-fueled adventure! Blast through new worlds and enemies as one of four new Vault Hunters.'
,44,'Borderlands 3.jpg',8,
'The original shooter-looter returns, packing bazillions of guns and an all-new mayhem-fueled adventure! Blast through new worlds and enemies as one of four brand new Vault Hunters – the ultimate treasure-seeking badasses of the Borderlands, each with deep skill trees, abilities, and customization. Play solo or join with friends to take on insane enemies, score loads of loot and save your home from the most ruthless cult leaders in the galaxy.'
,'13/03/2012'),
('Call of Duty®: WWII',
'Call of Duty® returns to its roots with Call of Duty®: WWII - a breathtaking experience that redefines World War II for a new gaming generation.'
,60,'Call of Duty® WWII.jpg',4,
'Call of Duty®: WWII creates the definitive World War II next generation experience across three different game modes: Campaign, Multiplayer, and Co-Operative. 
Featuring stunning visuals, the Campaign transports players to the European theater as they engage in an all-new Call of Duty® story set in iconic World War II battles. 
Multiplayer marks a return to original, boots-on-the ground Call of Duty gameplay. 
Authentic weapons and traditional run-and-gun action immerse you in a vast array of World War II-themed locations. 
The Co-Operative mode unleashes a new and original story in a standalone game experience full of unexpected, adrenaline-pumping moments.'
,'02/12/2017'),
('Call of Duty®: Modern Warfare® 3',
'The best-selling first-person action series of all-time returns with an epic sequel to the multiple GOTY award winner Call of Duty®: Modern Warfare® 2'
,40,'Call of Duty Modern Warfare® 3.jpg',4,
'The best-selling first person action series of all-time returns with the epic sequel to multiple “Game of the Year” award winner, 
Call of Duty®: Modern Warfare® 2. In the world’s darkest hour, are you willing to do what is necessary? 
Prepare yourself for a cinematic thrill-ride only Call of Duty can deliver. 
The definitive Multiplayer experience returns bigger and better than ever, loaded with new maps, modes and features. 
Co-Op play has evolved with all-new Spec-Ops missions and leaderboards, as well as Survival Mode, an action-packed combat progression unlike any other.'
,'08/11/2011'),
('Counter-Strike: Global Offensive',
'Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. 
CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content (de_dust2, etc.).'
,15,'csgo.jpg',2,
'Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. 
CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content (de_dust2, etc.).'
,'22/08/2012'),
('Cyberpunk 2077',
'Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — 
a dangerous megalopolis obsessed with power, glamor, and ceaseless body modification.'
,44,'Cyberpunk 2077.jpg',10,
'Cyberpunk 2077 is an open-world, action-adventure RPG set in the megalopolis of Night City, 
where you play as a cyberpunk mercenary wrapped up in a do-or-die fight for survival. Improved and featuring all-new free additional content, customize your character and playstyle as you take on jobs, build a reputation, and unlock upgrades. 
The relationships you forge and the choices you make will shape the story and the world around you. 
Legends are made here. What will yours be?'
,'10/12/2020'),
('Days Gone',
'Ride and fight into a deadly, post pandemic America. 
Play as Deacon St. John, a drifter and bounty hunter who rides the broken road, 
fighting to survive while searching for a reason to live in this open-world action-adventure game.'
,50,'daygone.jpg',1,
'Step into the dirt flecked shoes of former outlaw biker Deacon St. John, a bounty hunter trying to find a reason to live in a land surrounded by death. 
Scavenge through abandoned settlements for equipment to craft valuable items and weapons, 
or take your chances with other survivors trying to eke out a living through fair trade… or more violent means.'
,'18/05/2021'),
('Devil May Cry 5',
'The ultimate Devil Hunter is back in style, in the game action fans have been waiting for.'
,30,'Devil May Cry 5.jpg',6,
'A brand new entry in the legendary action series, 
Devil May Cry 5 brings together its signature blend of high-octane action 
and otherworldly original characters with the latest Capcom gaming technology to deliver a graphically groundbreaking action-adventure masterpiece.'
,'08/03/2019'),
('DOOM Eternal',
'Hell’s armies have invaded Earth. 
Become the Slayer in an epic single-player campaign to conquer demons across dimensions and stop the final destruction of humanity. 
The only thing they fear... is you.'
,11,'DOOM Eternal.jpg',9,
'Similar to DOOM from 2016, DOOM Eternal will contain Blood and Gore, Intense Violence and Strong Language'
,'20/03/2020'),
('Dota 2',
'Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if its their 10th hour of play or 1,000th, theres always something new to discover. 
With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.'
,0,'dota2.jpg',2,
'When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. 
Any hero can fill multiple roles, and theres an abundance of items to help meet the needs of each game. 
Dota doesnt provide limitations on how to play, it empowers you to express your own style.'
,'10/06/2020'),
('EA SPORTS™ FIFA 23',
'FIFA 23 brings The World’s Game to the pitch, with HyperMotion2 Technology that delivers even more gameplay realism, men’s and women’s FIFA World Cup™ coming during the season, women’s club teams, cross-play features*, and more.'
,47,'EA SPORTS FIFA 23.jpg',7,
'EA SPORTS™ FIFA 23 brings The World’s Game to the pitch, with HyperMotion2 Technology that delivers even more gameplay realism, 
both the men’s and women’s FIFA World Cup™ coming to the game as post-launch updates, the addition of women’s club teams, cross-play features*, and more. 
Experience unrivaled authenticity with over 19,000 players, 700+ teams, 100 stadiums, and over 30 leagues in FIFA 23.'
,'29/09/2022'),
('Fallout 4',
'Bethesda Game Studios, the award-winning creators of Fallout 3 and The Elder Scrolls V: Skyrim, 
welcome you to the world of Fallout 4 – their most ambitious game ever, and the next generation of open-world gaming.'
,20,'Fallout 4.jpg',9,
'As the sole survivor of Vault 111, you enter a world destroyed by nuclear war. Every second is a fight for survival, and every choice is yours. 
Only you can rebuild and determine the fate of the Wasteland. Welcome home.'
,'10/11/2015'),
('Far Cry® 5',
'Welcome to Hope County, Montana, home to a fanatical doomsday cult known as Eden’s Gate. 
Stand up to cult leader Joseph Seed & his siblings, the Heralds, to spark the fires of resistance & liberate the besieged community.'
,44,'farcry5.jpg',3,
'Far Cry comes to America in the latest installment of the award-winning franchise.'
,'27/03/2018'),
('God of War',
'His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. 
It is in this harsh, unforgiving world that he must fight to survive… and teach his son to do the same.'
,44,'god of war.jpg',1,
'Gameplay consists of frequent combat scenarios with characters punching and kicking or using their axe to slash/stab/impale enemies. 
Some larger enemies are opened up to intense finishing moves showing enemies being ripped apart, dismembered, or decapitated.'
,'14/01/2022'),
('Grand Theft Auto V',
'Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, 
as well as the chance to experience the game running at 60 frames per second.'
,10,'Grand Theft Auto V.jpg',11,
'When a young street hustler, a retired bank robber and a terrifying psychopath find themselves entangled with some of the most frightening and deranged elements of the criminal underworld, 
the U.S. government and the entertainment industry, they must pull off a series of dangerous heists to survive in a ruthless city in which they can trust nobody, least of all each other.'
,'14/04/2015'),
('Half-Life 2',
'1998. HALF-LIFE sends a shock through the game industry with its combination of pounding action and continuous, immersive storytelling. 
Valves debut title wins more than 50 game-of-the-year awards on its way to being named "Best PC Game Ever" by PC Gamer, 
and launches a franchise with more than eight million retail units sold worldwide.'
,10,'halflife2.jpg',2,
'The player again picks up the crowbar of research scientist Gordon Freeman,
who finds himself on an alien-infested Earth being picked to the bone, its resources depleted, its populace dwindling. 
Freeman is thrust into the unenviable role of rescuing the world from the wrong he unleashed back at Black Mesa. 
And a lot of people he cares about are counting on him.'
,'16/10/2014'),
('Horizon Zero Dawn™ Complete Edition',
'Experience Aloy’s legendary quest to unravel the mysteries of a future Earth ruled by Machines. 
Use devastating tactical attacks against your prey and explore a majestic open world in this award-winning action RPG!'
,44,'Horizon zero dawn.jpg',1,
'Experience Aloy’s entire legendary quest to unravel the mysteries of a world ruled by deadly Machines.
An outcast from her tribe, the young hunter fights to uncover her past, discover her destiny… and stop a catastrophic threat to the future.
Unleash devastating, tactical attacks against unique Machines and rival tribes as you explore an open world teeming with wildlife and danger.'
,'07/08/2020'),
('It Takes Two',
'Embark on the craziest journey of your life in It Takes Two. 
Invite a friend to join for free with Friend’s Pass and work together across a huge variety of gleefully disruptive gameplay challenges. 
Winner of GAME OF THE YEAR at the Game Awards 2021.'
,17,'it takes two.jpg',7,
'It Takes Two is developed by the award-winning studio Hazelight, the industry leader of cooperative play. 
They’re about to take you on a wild and wondrous ride where only one thing is for certain: we’re better together.'
,'26/03/2021'),
('L.A. Noire',
'L.A. Noire is a violent crime thriller that blends breathtaking action with true detective work to deliver an unprecedented interactive experience. 
This complete edition of L.A. Noire includes the complete original game and all previously released downloadable content.'
,15,'L.A. Noire.jpg',11,
'Using groundbreaking new animation technology, MotionScan, that captures every nuance of an actors facial performance in astonishing detail, 
L.A. Noire is a violent crime thriller that blends breathtaking action with true detective work to deliver an unprecedented interactive experience. 
Search for clues, chase down suspects and interrogate witnesses as you struggle to find the truth in a city where everyone has something to hide.'
,'09/11/2011'),
('Left 4 Dead 2',
'Set in the zombie apocalypse, Left 4 Dead 2 (L4D2) is the highly anticipated sequel to the award-winning Left 4 Dead, the #1 co-op game of 2008.'
,5,'left4dead2.jpg',2,
'Youll play as one of four new survivors armed with a wide and devastating array of classic and upgraded weapons. 
In addition to firearms, youll also get a chance to take out some aggression on infected with a variety of carnage-creating melee weapons, 
from chainsaws to axes and even the deadly frying pan.
Youll be putting these weapons to the test against (or playing as in Versus) three horrific and formidable new Special Infected. 
Youll also encounter five new uncommon common infected, including the terrifying Mudmen.'
,'17/11/2009'),
('Mafia: Definitive Edition',
'An inadvertent brush with the mob thrusts cabdriver Tommy Angelo into the world of organized crime. 
Initially uneasy about falling in with the Salieri family, the rewards become too big to ignore.'
,10,'Mafia Definitive Edition.jpg',8,
'Re-made from the ground up, rise through the ranks of the Mafia during the Prohibition era of organized crime. 
After a run-in with the mob, cab driver Tommy Angelo is thrust into a deadly underworld. Initially uneasy about falling in with the Salieri crime family, 
Tommy soon finds that the rewards are too big to ignore.'
,'25/11/2020'),
('MONSTER HUNTER RISE',
'Rise to the challenge and join the hunt! In Monster Hunter Rise, the latest installment in the award-winning and top-selling Monster Hunter series, 
you’ll become a hunter, explore brand new maps and use a variety of weapons to take down fearsome monsters as part of an all-new storyline.'
,38,'MONSTER HUNTER RISE.jpg',6,
'Hunt down a plethora of monsters with distinct behaviors and deadly ferocity. 
From classic returning monsters to all-new creatures inspired by Japanese folklore, including the flagship wyvern Magnamalo, 
you’ll need to think on your feet and master their unique tendencies if you hope to reap any of the rewards!'
,'13/01/2022'),
('Portal 2',
'The "Perpetual Testing Initiative" has been expanded to allow you to design co-op puzzles for you and your friends!'
,5,'portal2.jpg',2,
'The game’s two-player cooperative mode features its own entirely separate campaign with a unique story, 
test chambers, and two new player characters. This new mode forces players to reconsider everything they thought they knew about portals. 
Success will require them to not just act cooperatively, but to think cooperatively.'
,'19/04/2011'),
('Red Dead Redemption 2',
'Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, 
RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, 
on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.'
,21,'Red Dead Redemption 2.jpg',11,
'Arthur Morgan and the Van der Linde gang are outlaws on the run. 
With federal agents and the best bounty hunters in the nation massing on their heels, 
the gang must rob, steal and fight their way across the rugged heartland of America in order to survive. 
As deepening internal divisions threaten to tear the gang apart, Arthur must make a choice between his own ideals and loyalty to the gang who raised him.'
,'06/12/2019'),
('Resident Evil 3',
'Jill Valentine is one of the last remaining people in Raccoon City to witness the atrocities Umbrella performed. 
To stop her, Umbrella unleashes their ultimate secret weapon: Nemesis!'
,35,'Resident Evil 3.jpg',6,
'This game contains strong language and extreme violence.'
,'03/04/2020'),
('Resident Evil Village',
'Experience survival horror like never before in the 8th major installment in the Resident Evil franchise - Resident Evil Village. 
With detailed graphics, intense first-person action and masterful storytelling, the terror has never felt more realistic.'
,38,'Resident Evil Village.jpg',6,
'Set a few years after the horrifying events in the critically acclaimed Resident Evil 7 biohazard, 
the all-new storyline begins with Ethan Winters and his wife Mia living peacefully in a new location, free from their past nightmares. 
Just as they are building their new life together, tragedy befalls them once again.'
,'07/05/2021'),
('Marvels Spider-Man',
'The worlds of Peter Parker and Spider-Man collide in an original action-packed story. 
Play as an experienced Peter Parker, fighting big crime and iconic villains in Marvel’s New York. 
Web-swing through vibrant neighborhoods and defeat villains with epic takedowns.'
,44,'spiderman.jpg',1,
'Developed by Insomniac Games in collaboration with Marvel, and optimized for PC by Nixxes Software, 
Marvels Spider-Man Remastered on PC introduces an experienced Peter Parker who’s fighting big crime and iconic villains in Marvel’s New York. 
At the same time, he’s struggling to balance his chaotic personal life and career while the fate of Marvel’s New York rests upon his shoulders.'
,'12/08/2022'),
('Team Fortress 2',
'Nine distinct classes provide a broad range of tactical abilities and personalities. 
Constantly updated with new game modes, maps, equipment and, most importantly, hats!'
,0,'teamfortress2.jpg',2,
'One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. 
Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills.'
,'10/10/2007'),
('The Evil Within 2',
'Detective Sebastian Castellanos has lost everything, including his daughter, Lily. 
To save her, he must descend into the nightmarish world of STEM. 
Horrifying threats emerge from every corner, and he must rely on his wits to survive. 
For his one chance at redemption, the only way out is in.'
,40,'The Evil Within 2.jpg',9,
'From mastermind Shinji Mikami, The Evil Within 2 is the latest evolution of survival horror. 
Detective Sebastian Castellanos has lost it all. 
But when given a chance to save his daughter, he must descend once more into the nightmarish world of STEM. 
Horrifying threats emerge from every corner as the world twists and warps around him. 
Will Sebastian face adversity head on with weapons and traps, or sneak through the shadows to survive.'
,'12/10/2017'),
('The Witcher® 3: Wild Hunt',
'As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, 
a living weapon that can alter the shape of the world.'
,17,'The Witcher® 3 Wild Hunt.jpg',10,
'The Witcher: Wild Hunt is a story-driven open world RPG set in a visually stunning fantasy universe full of meaningful choices and impactful consequences. 
In The Witcher, you play as professional monster hunter Geralt of Rivia tasked with finding a child of prophecy in a vast open world rich with merchant cities, 
pirate islands, dangerous mountain passes, and forgotten caverns to explore.'
,'18/05/2015'),
('Tom Clancys Rainbow Six® Siege',
'Tom Clancys Rainbow Six Siege is the latest installment of the acclaimed first-person shooter franchise developed by the renowned Ubisoft Montreal studio.'
,14,'Tom Clancys Rainbow Six Siege.jpg',3,
'Master the art of destruction and gadgetry in Tom Clancy’s Rainbow Six Siege.
Face intense close quarters combat, high lethality, tactical decision making, team play and explosive action within every moment. 
Experience a new era of fierce firefights and expert strategy born from the rich legacy of past Tom Clancys Rainbow Six games.'
,'02/12/2015'),
('Watch_Dogs® 2',
'Welcome to San Francisco. 
Play as Marcus, a brilliant young hacker, and join the most notorious hacker group, DedSec. Your objective: execute the biggest hack of history.'
,44,'watchdog2.jpg',3,
'Play as Marcus Holloway, a brilliant young hacker living in the birthplace of the tech revolution, the San Francisco Bay Area.Team up with Dedsec, 
a notorious group of hackers, to execute the biggest hack in history; take down ctOS 2.0, 
an invasive operating system being used by criminal masterminds to monitor and manipulate citizens on a massive scale.'
,'28/11/2016'),
('Wolfenstein II The New Colossus',
'Overview:America, 1961. The assassination of Nazi General Deathshead was a short-lived victory. 
The Nazis maintain their stranglehold on the world. You are BJ Blazkowicz, aka “Terror-Billy,” 
member of the Resistance, scourge of the Nazi empire, and humanity’s last hope for liberty.'
,40,'Wolfenstein II The New Colossus.jpg',9,
'Description:An exhilarating adventure brought to life by the industry-leading id Tech® 6, 
Wolfenstein® II sends players to Nazi-controlled America on a mission to recruit the boldest resistance leaders left. 
Fight the Nazis in iconic American locations, equip an arsenal of badass guns, 
and unleash new abilities to blast your way through legions of Nazi soldiers in this definitive first-person shooter.'
,'27/10/2017')
insert into category
values('Free To Play'),('Multiplayer'),('FPS'),('Action'),('Open World'),('Singleplayer'),('Adventure'),
('Historical'),('Shooter'),('PvP'),('Co-op'),('Loot'),('Horror'),('Hack and Slash'),('Great Soundtrack'),
('Sports'),('Football'),('Survival')
select * from Category
insert into ProductCategory
values(1,1),(1,2),(1,9),(1,3),(1,4),(1,18),(1,11),(1,10),
(2,7),(2,6),(2,4),(2,2),
(3,9),(3,2),(3,4),(3,3),(3,10),(3,6),
(4,9),(4,4),(4,11),(4,3),(4,12),(4,2),
(5,11),(5,4),(5,12),(5,9),(5,3),
(6,11),(6,2),(6,4),(6,3),(6,9),
(7,4),(7,3),(7,2),(7,9),(7,11),
(8,4),(8,3),(8,9),(8,2),(8,10),
(9,5),(9,6),(9,3),(9,4),(9,15),(9,7),
(10,7),(10,5),(10,6),(10,13),(10,18),(10,4),
(11,4),(11,14),(11,15),(11,6),(11,7),
(12,4),(12,3),(12,15),(12,9),(12,6),(12,7),
(13,1),(13,2),(13,4),(13,10),(13,11),
(14,16),(14,17),(14,10),(14,11),(14,2),(14,6),
(15,5),(15,6),(15,7),(15,9),(15,4),(15,3),(15,18),
(16,5),(16,11),(16,4),(16,2),(16,3),(16,9),(16,6),
(17,6),(17,4),(17,7),(17,14),(17,5),
(18,5),(18,4),(18,2),(18,9),(18,6),(18,11),
(19,3),(19,4),(19,6),(19,7),(19,9),(19,13),
(20,5),(20,7),(20,6),(20,4),
(21,11),(21,2),(21,7),(21,4),
(22,5),(22,7),(22,6),(22,4),
(23,11),(23,3),(23,2),(23,9),(23,4),(23,18),(23,13),(23,6),(23,7),
(24,4),(24,5),(24,7),(24,9),
(25,4),(25,11),(25,2),(25,6),
(26,4),(26,3),(26,7),
(27,5),(27,7),(27,4),(27,6),(27,9),
(28,4),(28,13),(28,6),(28,7),
(29,13),(29,6),(29,4),(29,3),(29,18),(29,7),
(30,4),(30,5),(30,6),(30,7),
(31,1),(31,6),(31,3),(31,9),(31,4),(31,11),
(32,13),(32,4),(32,7),(32,6),(32,18),(32,9),
(33,5),(33,7),(33,6),(33,4),
(34,3),(34,6),(34,9),(34,4),(34,11),
(35,5),(35,4),(35,9),(35,6),(35,7),
(36,3),(36,4),(36,6),(36,9),(36,5)

select Product.Id,Product.Name,Overview,price,image,Description,ReleaseDate,Category.Name,Developer.Name
from Product,ProductCategory,Category,Developer
where Product.DevId=Developer.Id and Product.Id=ProductCategory.ProductId
and ProductCategory.CategoryId=Category.Id and Product.Name='Cyberpunk 2077'