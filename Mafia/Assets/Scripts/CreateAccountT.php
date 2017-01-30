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
	$SQL = "SELECT * FROM Accounts WHERE Username ='$Username'";
	$result = mysqli_query($con, $SQL);
	if(mysqli_num_rows($result) == 0){
		$insert = "INSERT INTO `Accounts` (`Username`, `Password`) VALUES ('" . $Username . "', MD5 ('". $Password ."'))";
		$insertstat = "INSERT INTO `Stats` (`Username`) VALUES ('" . $Username . "')";
		$SQL1 = mysqli_query($con ,$insert);
		$SQL2 = mysqli_query($con ,$insertstat);
		echo"Success";
	} else {
		echo"Username is taken";
	}
}

mysqli_close($con);
?>