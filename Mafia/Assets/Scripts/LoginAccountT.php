<?php
//connections
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Cant connect into database");

$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];

if(!$Username || !$Password) {
	echo "Login or password cant be empty.";
}
else{
	$SQL = "SELECT * FROM Accounts WHERE Username = '" . $Username . "'";
	$result_id = @mysqli_query($con, $SQL) or die("DATABASE ERROR 1");
	$total = mysqli_num_rows($result_id);
	if($total){
		$datas = @mysqli_fetch_array($result_id);
		if(!strcmp(MD5($Password), $datas["Password"])){
			$sql2 = "SELECT Characters FROM Accounts where Username = '" . $Username . "'";
			$result_id2 = @mysqli_query($con,$sql2) or die("DATABASE ERROR 2");
			while($row = mysqli_fetch_array($result_id2))
			{
				echo $row['Characters'];
				echo":";
				echo"Success";
			} 
		} 
		else{
			echo "WrongPassword";
		}
	}
	else{
		echo "UsernameDoesNotExist";
	}		
}

//Close mySQL Connections
mysqli_close($con);
?>