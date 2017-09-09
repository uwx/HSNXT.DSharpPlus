dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.CommandsNext/DSharpPlus.CommandsNext.csproj || exit 1
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.Interactivity/DSharpPlus.Interactivity.csproj || exit 1
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.Rest/DSharpPlus.Rest.csproj || exit 1
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.VoiceNext/DSharpPlus.VoiceNext.csproj || exit 1
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.WebSocket.WebSocket4Net/DSharpPlus.WebSocket.WebSocket4Net.csproj || exit 1
dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus/DSharpPlus.csproj || exit 1

if [ "$COREAPPTYP" == "net45" ] || [ "$COREAPPTYP" == "net46" ] || [ "$COREAPPTYP" == "net47" ]; then
  dotnet publish $CORESC -f $COREAPPTYP -c $COREBTYP DSharpPlus.WebSocket.WebSocketSharp/DSharpPlus.WebSocket.WebSocketSharp.csproj || exit 1
fi

dotnet publish $CORESC -f netcoreapp1.1 -c $COREBTYP DSharpPlus.Test/DSharpPlus.Test.csproj
