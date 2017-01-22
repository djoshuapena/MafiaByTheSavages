<?php
//connections
$Hostname = "localhost";
$DBName = "savage";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant connect into database");
mysql_select_db($DBName) or die("Cant connect into database");

$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];

if(!$Username || !$Password) {
	echo "Login or password cant be empty.";
}
else{
	$SQL = "SELECT * FROM Accounts WHERE Username = '" . $Username . "'";
	$result_id = @mysql_query($SQL) or die("DATABASE ERROR 1");
	$total = mysql_num_rows($result_id);
	if($total){
		$datas = @mysql_fetch_array($result_id);
		if(!strcmp(MD5($Password), $datas["Password"])){
			$sql2 = "SELECT Characters FROM Accounts where Username = '" . $Username . "'";
			$result_id2 = @mysql_query($sql2) or die("DATABASE ERROR 2");
			while($row = mysql_fetch_array($result_id2))
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
mysql_close();
?>