$url = "https://github.com/geaz/sharpDox/releases/download/v1.2.3/sharpDox.1.2.3.zip"
$output = "C:\sharpDox.1.2.3.zip"
$start_time = Get-Date

Invoke-WebRequest -Uri $url -OutFile $output
Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)"

Add-Type -assembly "System.IO.Compression.Filesystem";
[String]$Source = $output;
[String]$Destination = "C:\";
[IO.Compression.Zipfile]::ExtractToDirectory($Source, $Destination);

Rename-Item -Path "C:\sharpDox 1.2.3" -NewName "sharpDox" -ErrorAction Stop