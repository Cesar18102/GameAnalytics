{
	arr[0] = "CPA";
	arr[1] = "CPC";
	arr[2] = "CPM";
	
	typeid = 2;
	
	for( ; typeid >= 0 && arr[typeid] != $2; typeid--);
	
	Count = NF == 4 ? substr($4, 1, length($4) - 1) : $4;
	Conversion = NF >= 5 ? (NF == 5? substr($5, 1, length($5) - 1) : $5) / 100 : "NULL";
	CRT = NF >= 6 ? (NF == 6? substr($6, 1, length($6) - 1) : $6) / 100 : "NULL";
	
	print "INSERT INTO company VALUES(0, \"" $1 "\", " (typeid + 1) ", " $3 ", " Count ", " Conversion ", " CRT ");";
}