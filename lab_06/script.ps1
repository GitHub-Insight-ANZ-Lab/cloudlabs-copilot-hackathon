# Sysadmin Disk Space Monitoring Script

# Set the threshold for disk space usage (in percentage)
$threshold = 90

# Get the current disk space usage for the C: drive
$diskUsage = Get-WmiObject Win32_LogicalDisk -Filter "DeviceID='C:'" | Select-Object @{Name = 'FreeSpace'; Expression = { $_.FreeSpace } }, @{Name = 'Size'; Expression = { $_.Size } }

# Calculate the percentage of free space
$freeSpacePercentage = ($diskUsage.FreeSpace / $diskUsage.Size) * 100

# Check if disk space usage exceeds the threshold
if ($freeSpacePercentage -gt $threshold) {
    # Disk space usage exceeds the threshold, send an alert
    Write-Host "Disk space usage is above the threshold: $($freeSpacePercentage)%"
    # Add your alerting mechanism here (e.g., send an email, log to a file, etc.)
}
else {
    # Disk space usage is within the acceptable range
    Write-Host "Disk space usage is within the acceptable range: $($freeSpacePercentage)%"
}