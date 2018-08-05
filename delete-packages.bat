for %%x in (%*) do (
   del %%~x\Packaged\%%~x.*.nupkg
)
