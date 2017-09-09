dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.CommandsNext/DSharpPlus.CommandsNext.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.Interactivity/DSharpPlus.Interactivity.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.Rest/DSharpPlus.Rest.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.Test/DSharpPlus.Test.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.VoiceNext/DSharpPlus.VoiceNext.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.WebSocket.WebSocket4Net/DSharpPlus.WebSocket.WebSocket4Net.csproj
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus/DSharpPlus.csproj

if [ "$COREAPPTYP" == "net45" ] || [ "$COREAPPTYP" == "net46" ] || [ "$COREAPPTYP" == "net47" ]; then
  dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.WebSocket.WebSocketSharp/DSharpPlus.WebSocket.WebSocketSharp.csproj
fi
