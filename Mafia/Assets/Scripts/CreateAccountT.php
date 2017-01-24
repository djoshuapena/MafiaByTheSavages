<?php

$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Can't connect to DB");

$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];
$RePassword = $_REQUEST["RePassword"]; 

if(!$Username){
	echo"Must enter username";
}
else if(!$Password){
	echo"Must enter password";
}
else if(!$RePassword){
	echo"Must re-enter password";
}

else{
	$SQL = "SELECT * FROM Accounts WHERE Username = ' " . $Username . " '";
	$Result = @mysqli_query($con ,$SQL) or die ("DB Error");
	Echo"$SQL";
	$Total = mysqli_num_rows($Result);
	if($Total == 0){
		$insert = "INSERT INTO `Accounts` (`Username`, `Password`) VALUES ('" . $Username . "', MD5 ('". $Password ."'))";
		$SQL1 = mysqli_query($con ,$insert);
		echo"Success";
	} else {
		echo"AlreadyUsed";
	}
}

mysqli_close($con);
?>