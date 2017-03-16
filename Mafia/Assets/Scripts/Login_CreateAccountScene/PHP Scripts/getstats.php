<?php
//connect to the database
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "savagemafia007";
$PasswordP = "Realmafia@Ecu";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Can't connect to DB");

//get the username from the game
$username = $_REQUEST["Username"];

//check to see if the user name has a query in the stats table
$sql = "SELECT * FROM Stats WHERE Username = '" . $username . "'";
$result = mysqli_query($con, $sql);

if(mysqli_num_rows($result) > 0){
	//get information from the table
	while ($row = mysqli_fetch_assoc($result)){
		echo "TotalGameWin".$row['TotalGameWin'] . "|TotalGameLoss".$row['TotalGameLoss'] . "|CivilianWin".$row['CivilianWin'] . "|CivilianLoss".$row['CivilianLoss'] . "|MafiaWin".$row['MafiaWin'] . "|MafiaLoss".$row['MafiaLoss'] . "|MafiaKill".$row['MafiaKill'] . "|DoctorWin".$row['DoctorWin'] . "|DoctorLoss".$row['DoctorLoss'] . "|DoctorSave".$row['DoctorSave'] . "|SherrifWin".$row['SherrifWin'] . "|SherrifLoss".$row['SherrifLoss'] . "|SherrifCaught".$row['SherrifCaught'] ."";
	}
}

mysqli_close($con);
?>