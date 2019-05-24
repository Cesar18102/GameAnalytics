{	
	print "INSERT INTO monetize VALUES(0, \"" $1 "\", " $2 ", " $3 ", " $4 ", " $5 ", " $6 ", " $7 ", " $8 ", " $9 ", " $10 ", " substr($11, 1, length($11) - 1) ");";
}