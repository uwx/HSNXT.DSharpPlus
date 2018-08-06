for %%x in (%*) do (
   echo deleting nupkg %%~x
   del %%~x\Packaged\%%~x.*.nupkg
)
