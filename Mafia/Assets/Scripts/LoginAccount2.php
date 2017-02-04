<?php
//connections
$Hostname = "localhost";
$DBName = "id619425_savage";
$User = "id619425_mafia";
$PasswordP = "ghjaadd";

$con = mysqli_connect($Hostname, $User, $PasswordP, $DBName) or die("Cant connect into database");

//get the information from the input fields
$username = $_GET["Username"];
$password = $_GET["Password"];

//check if they are not empty
if(!$username || !$password) {
	echo "Login or password cant be empty.";
}
else
{
    //get the query from the accounts table with the given username
    $queryx = "SELECT * FROM Accounts WHERE Username = '" . $username . "'";
    $Resultx = mysqli_query($con, $queryx);

    //get the password associated with the username
    while($row = mysqli_fetch_array($Resultx))
    {
        $passnya = $row["Password"];
    }

    //compare the password
    if(!strcmp(MD5($password), $passnya))
    {
        $login_info_success = array(
            "ResultCode" => 1, 
            "Message" => "Success!",
            );

        //connect to photon
        $json_success = json_encode($login_info_success);
        echo $json_success;
    }

    else{
        //wrong username and password
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