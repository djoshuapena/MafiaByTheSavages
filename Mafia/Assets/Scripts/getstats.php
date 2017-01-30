<?php
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Can't connect to DB");

$username = $_REQUEST["Username"];

$sql = "SELECT * FROM Stats WHERE Username = '" . $username . "'";
$result = mysqli_query($con, $sql);

if(mysqli_num_rows($result) > 0){
	while ($row = mysqli_fetch_assoc($result)){
		echo "TotalGameWin".$row['TotalGameWin'] . "|TotalGameLoss".$row['TotalGameLoss'] . "|CivilianWin".$row['CivilianWin'] . "|CivilianLoss".$row['CivilianLoss'] . "|MafiaWin".$row['MafiaWin'] . "|MafiaLoss".$row['MafiaLoss'] . "|MafiaKill".$row['MafiaKill'] . "|DoctorWin".$row['DoctorWin'] . "|DoctorLoss".$row['DoctorLoss'] . "|DoctorSave".$row['DoctorSave'] . "|SherrifWin".$row['SherrifWin'] . "|SherrifLoss".$row['SherrifLoss'] . "|SherrifCaught".$row['SherrifCaught'] ."";
	}
}

mysqli_close($con);
?>