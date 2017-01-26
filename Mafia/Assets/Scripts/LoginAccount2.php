<?php
//connections
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Cant connect into database");

$username = $_GET["Username"];
$password = $_GET["Password"];

if(!$username || !$password) {
	echo "Login or password cant be empty.";
}
else
{
    $queryx = "SELECT * FROM Accounts WHERE Username = '" . $username . "'";
    $Resultx = mysqli_query($con, $queryx);

    while($row = mysqli_fetch_array($Resultx))
    {
        $passnya = $row["Password"];
    }

    if(!strcmp(MD5($password), $passnya))
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
}
mysqli_close($con);
?>