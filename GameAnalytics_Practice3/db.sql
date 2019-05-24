CREATE TABLE level(
  	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	name CHAR(255) NOT NULL,
    starts INTEGER NOT NULL DEFAULT 0,
    wins INTEGER NOT NULL DEFAULT 0,
    almost_wins INTEGER NOT NULL DEFAULT 0
);