# PassLock
.NET Core console application that will manage, encrypt, serialize, and decrypt passwords to a JSON file.

## TODO
- Do not store keys and initialization vectors along with cipher text
- Do not output plaintext password to the console window

## Publish Executable
dotnet publish \
    --configuration Release \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:CopyOutputSymbolsToPublishDirectory=false \
    --self-contained \