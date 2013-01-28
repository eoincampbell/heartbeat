param(
    # String Param 1
    [string]$param1 = "Hello", 
    
    # String Param 2
    [string]$param2 = "World", 
    
    # Switch to put a separator between each file which includes the file path
    [switch]$SwitchA 
)

if($param1 -eq "Hello" -and $param2 -eq "World"){
    return $false;
}
else {
    return $true;
}


