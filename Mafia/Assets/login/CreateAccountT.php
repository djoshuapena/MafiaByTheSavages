<?php

$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];

$Hostname = "localhost";
$DBName = "savage";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Can't connect to DB");
mysql_select_db($DBName) or die("Can't Connect to DB");

if(!$Username || !$Password){
	echo"Empty";
}
else{
	$SQL = "SELECT * FROM Accounts WHERE Username = ' " . $Username . " '";
	$Result = @mysql_query($SQL) or die ("DB Error");
	$Total = mysql_num_rows($Result);
	if($Total == 0){
		$insert = "INSERT INTO `Accounts` (`Username`, `Password`) VALUES ('" . $Username . "', MD5 ('". $Password ."'))";
		$SQL1 = mysql_query($insert);
		echo"Success";
	} else {
		echo"AlreadyUsed";
	}
}

mysql_close();
?>