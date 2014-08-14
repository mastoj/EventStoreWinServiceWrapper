$exePath = '.\EventStoreWinServiceWrapper.exe'

$service = get-service | Where {$_.Name -eq "EventStoreServiceWrapper"}
if($service -ne $null) {
    Write-Host "Service already installed, will do a reinstall"
    if($service.Status -eq "Running") {
        Write-Host "Stopping service"
        & $exePath stop
    }
    Write-Host "Uninstalling service"
    & $exePath uninstall
}
Write-Host "Installing service"
& $exePath install
Write-Host "Starting service"
& $exePath start