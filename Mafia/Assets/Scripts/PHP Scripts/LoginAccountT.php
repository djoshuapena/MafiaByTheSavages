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
/*
echo"it came here";
$username = $_REQUEST["username"];
$password = $_REQUEST["password"];

$queryx = "SELECT * FROM Accounts WHERE Username = '" . $username . "'";
$Resultx = mysqli_query($con, $queryx);

while($row = mysqli_fetch_array($Resultx))
{
	$passnya = $row['Characters'];
}

if(strcmp(MD5($password), $passnya))
{
	$login_info_success = array(
		"ResultCode" => 1, 
		"Message" => "Success!",
		);

	$json_success = json_encode($login_info_success);
	echo $json_success;
}

else{
	$login_info_error = array(
		"ResultCode" => 2, 
		"Message" => "Wrong Username or Password",
	);

	$json_error = json_encode($login_info_error);
	echo $json_error;
}
*/
mysqli_close($con);
?>