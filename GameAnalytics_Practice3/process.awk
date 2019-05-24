{	
	print "INSERT INTO level VALUES(0, \"" $1 "\", " $2 ", " $3 ", " substr($4, 1, length($4) - 1) ");";
}