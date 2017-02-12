<?php
//connect to the database
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Can't connect to DB");

//get the information from the input fields
$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];
$RePassword = $_REQUEST["RePassword"]; 

//check if they are not empty
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

	//get the query from the accounts table with the given username (if it exists)
	$SQL = "SELECT * FROM Accounts WHERE Username ='$Username'";
	$result = mysqli_query($con, $SQL);
	//check to see if the query in the table exists if not insert the information else username is taken
	if(mysqli_num_rows($result) == 0){
		//make a variable that will insert the query into the accounts table
		$insert = "INSERT INTO `Accounts` (`Username`, `Password`) VALUES ('" . $Username . "', MD5 ('". $Password ."'))";
		//make a variable that will insert the username in the stats table
		$insertstat = "INSERT INTO `Stats` (`Username`) VALUES ('" . $Username . "')";

		//insert into the dabase
		$SQL1 = mysqli_query($con ,$insert);
		$SQL2 = mysqli_query($con ,$insertstat);
		echo"Success";
	} else {
		echo"Username is taken";
	}
}

mysqli_close($con);
?>