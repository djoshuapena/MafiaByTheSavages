<?php
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Can't connect to DB");

$username = $username = $_REQUEST["Username"];
//pulls the stats string from the wwwform in menus.cs
//old grab for httputility code
//$data = $_POST['stats'];
//new grab base on createAccount and wwwform
$data = $_REQUEST["stats"];

//convert string into array
$stats = str_split($data);
//or $stats = array($_POST['stats']);

$sql1 = "SELECT * FROM Stats WHERE Username = '" . $username . "'";
$result = mysqli_query($con, $sql) or die("Account Error: Incorrect Username");
//$update = "UPDATE Stats SET TotalGameWin='$stats[0]', TotalGameLoss='$stats[1]', CivilianWin='$stats[2]', CivilianLoss='$stats[3]', MafiaWin='$stats[4]', MafiaLoss='$stats[5]', MafiaKill='$stats[6]', DoctorWin='$stats[7]', DoctorLoss='$stats[8]', DoctorSave='$stats[9]', SherrifWin='$stats[10]', SherrifLoss='$stats[11]', SherrifCaught='$stats[12]' WHERE Username = '" . $username . "'";
$update = "UPDATE Stats SET TotalGameWin='$stats[0]', TotalGameLoss='$stats[2]', CivilianWin='$stats[4]', CivilianLoss='$stats[6]', MafiaWin='$stats[8]', MafiaLoss='$stats[10]', MafiaKill='$stats[12]', DoctorWin='$stats[14]', DoctorLoss='$stats[16]', DoctorSave='$stats[18]', SherrifWin='$stats[20]', SherrifLoss='$stats[22]', SherrifCaught='$stats[24]' WHERE Username = '" . $username . "'";
//$sql = "UPDATE student SET Reason = '$_POST[reason]' WHERE RegNo ='$_POST[regno]'";
$result2 = mysqli_query($con, $update) or die("Update Database Error");
if(! $result2 ){
	die('Could not update stats: ' . mysql_error());
}
else{
	echo "Updated stats successfully\n";
}

mysqli_close($con);
?>