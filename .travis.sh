if [ "$COREBTYP" != "BuildAlt" ]; then
  dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus.CommandsNext/DSharpPlus.CommandsNext.csproj || exit 1
  dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus.Interactivity/DSharpPlus.Interactivity.csproj || exit 1
  dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus.Rest/DSharpPlus.Rest.csproj || exit 1
  dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus.VoiceNext/DSharpPlus.VoiceNext.csproj || exit 1
  dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus/DSharpPlus.csproj || exit 1

  if [ "$COREAPPTYP" != "netstandard1.1" ]; then
    dotnet publish $CORESC --output ./travis-bin/ -f $COREAPPTYP -c $COREBTYP DSharpPlus.WebSocket.WebSocket4Net/DSharpPlus.WebSocket.WebSocket4Net.csproj || exit 1
  fi
  dotnet publish $CORESC --output ./travis-bin/ -f netcoreapp1.1 -c $COREBTYP DSharpPlus.Test/DSharpPlus.Test.csproj
else
  dotnet build $CORESC -v Minimal -c Release --version-suffix "$TRAVIS_BUILD_NUMBER"
fi
